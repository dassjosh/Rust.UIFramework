using System;
using System.Collections;
using System.IO;
using Facepunch.Sqlite;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Data;
using Oxide.Ext.UiFramework.Harmony;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class UiImageDatabase : BaseUiFrameworkLibrary, ISingleton, IImageDatabase
{
    private readonly ImageDb _db;
    private readonly LruDictionary<ImageId, CachedImage> _cache;
    private readonly ImageDbData _data = ImageDbData.Instance;
    private readonly IUiLogger<UiImageDatabase> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<UiImageDatabase>();
    
    private UiImageDatabase()
    {
#if SERVER
        if (UiFrameworkConfig.Instance.ImageDb.Enabled)
        {
            string dbPath = Path.Combine(PathConstants.DataFolder, "images.db");
            _db = new ImageDb();
            _db.Open(dbPath);
            if (!_db.TableExists("data"))
            {
                _logger.Debug("Creating Image DB Table");
                _db.Execute("CREATE TABLE data ( crc INTEGER PRIMARY KEY, image BLOB)");
            }
        
            _cache = new LruDictionary<ImageId, CachedImage>(UiFrameworkConfig.Instance.ImageDb.CacheSize);
        }
#endif
    }
    
    public byte[] Get(ImageId id)
    {
        if (_db == null)
        {
            return null;
        }
        
        if (!_data.IsStored(id))
        {
            return null;
        }
        
        if (_cache.TryGetValue(id, out CachedImage image) && image.IsValid)
        {
            _data.Touch(id);
            return image.Data;
        }
        
        byte[] bytes = _db.Query<byte[], uint>("SELECT image FROM data WHERE crc = ? LIMIT 1", id.Id);
        if (bytes != null)
        {
            _data.Touch(id);
            _cache.Add(id, new CachedImage(bytes));
        }

        return bytes;
    }

    public ImageId Store(byte[] image)
    {
        if (_db == null)
        {
            return default;
        }

        uint crc = Crc.GetCRC(image);
        ImageId id = new(crc);
        if (_data.IsStored(id))
        {
            return id;
        }
        
        _data.Touch(id);
        _db.Execute("INSERT OR REPLACE INTO data (crc, image) VALUES (?, ?)", crc, image);
        return id;
    }

    public SaveVersion GetSaveVersion(CommunityEntity entity) => new(ulong.MaxValue);
    
    public void OnImageRegistered(ImageId id)
    {
        _data.Touch(id);
    }

    private IEnumerator ClearExpiredImages()
    {
        foreach (ImageId id in _data.GetExpiredImages())
        {
            RemoveExpiredImage(id);
            yield return null;
        }
    }

    private void RemoveExpiredImage(ImageId id)
    {
        try
        {
            _db.Execute("DELETE FROM data WHERE crc = ?", id.Id);
            _data.Remove(id);
        }
        catch (Exception ex)
        {
            _logger.Exception("An error occured removing Image {0}", id, ex);
        }
    }

    protected override void OnCommunityEntitySpawned(CommunityEntity entity)
    {
        FileStorage_Get_Prefix_Patch.Patch(entity);
    }

    protected override void OnServerInitialized()
    {
        ServerMgr.Instance.StartCoroutine(ClearExpiredImages());
    }

    protected override void OnServerSave() => _cache.ClampToSize();
    protected override void OnServerShutdown() => _db.Close();
    
    private sealed class ImageDb : Database;
    
    private readonly struct CachedImage(byte[] data) : ICacheSize
    {
        public readonly byte[] Data = data;
        public uint Size => (uint) Data.Length;
        public bool IsValid => Data != null && Data.Length != 0;
    }
}