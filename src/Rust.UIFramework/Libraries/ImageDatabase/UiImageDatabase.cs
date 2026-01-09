using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    private readonly ImageDatabase _db;
    private readonly LruDictionary<ImageId, CachedImage> _cache;
    private readonly ImageDbData _data = ImageDbData.Instance;
    private readonly IUiLogger<UiImageDatabase> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<UiImageDatabase>();
    
    private UiImageDatabase()
    {
        string dbPath = Path.Combine(PathConstants.DataFolder, "images.db");
        _db = new ImageDatabase();
        _db.Open(dbPath);
        if (!_db.TableExists("data"))
        {
            _db.Execute("CREATE TABLE data ( crc INTEGER PRIMARY KEY, image BLOB)");
        }
        
        _cache = new LruDictionary<ImageId, CachedImage>(UiFrameworkConfig.Instance.ImageDatabase.CacheSize.Bytes);
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
            _logger.Error("Database is not initialized");
            return default;
        }

        uint crc = Crc.GetCRC(image);
        ImageId id = new(crc);
        if (_data.IsStored(id))
        {
            _logger.Debug("Image {0} already stored", id);
            return id;
        }
        
        _data.Touch(id);
        _db.Execute("INSERT OR REPLACE INTO data (crc, image) VALUES (?, ?)", crc, image);
        _logger.Debug("Image {0} stored", id, crc);
        return id;
    }

    public SaveVersion GetSaveVersion(CommunityEntity entity) => new(ulong.MaxValue);
    
    public void OnImageRegistered(ImageId id)
    {
        _logger.Debug("OnImageRegistered : {0}", id);
        _data.Touch(id);
    }

    private IEnumerator ClearExpiredImages()
    {
        foreach (ImageId id in _data.GetExpiredImages())
        {
            RemoveImage(id);
            yield return null;
        }
    }

    private void RemoveImage(ImageId id)
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
        SyncData();
    }

    private void SyncData()
    {
        HashSet<uint> savedCrcs = _db.GetAllCrc();
        foreach (ImageId id in _data.GetAllImages())
        {
            if (!savedCrcs.Contains(id.Id))
            {
                _data.Remove(id);
            }
        }

        foreach (uint crc in savedCrcs)
        {
            if (!_data.IsStored(new ImageId(crc)))
            {
                RemoveImage(new ImageId(crc));
            }
        }
    }

    protected override void OnServerSave() => _cache.ClampToSize();
    protected override void OnServerShutdown() => _db.Close();

    private sealed class ImageDatabase : Database
    {
        public HashSet<uint> GetAllCrc()
        {
            IntPtr stmHandle = Prepare("SELECT crc FROM data");
            List<uint> crcs = [];
            ExecuteAndReadQueryResults(stmHandle, crcs, ptr => GetColumnValue<uint>(ptr, 0));
            return crcs.ToHashSet();
        }
    }
    
    private readonly struct CachedImage(byte[] data) : ICacheSize
    {
        public readonly byte[] Data = data;
        public uint Size => (uint) Data.Length;
        public bool IsValid => Data != null && Data.Length != 0;
    }
}