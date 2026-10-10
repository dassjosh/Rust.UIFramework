using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Facepunch.Sqlite;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Guards;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class UiImageDatabase : BaseUiFrameworkLibrary, IImageDatabase
{
    public ImageDatabaseType Type => ImageDatabaseType.ImageDatabase;
    private readonly ImageDatabase _db;
    private readonly LruDictionary<ImageId, CachedImage> _cache;
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
        Guard.IsMainThread();
        if (_db == null)
        {
            _logger.Error($"{nameof(Get)} Database is not initialized");
            return null;
        }
        
        if (_cache.TryGetValue(id, out CachedImage image) && image.IsValid)
        {
            _logger.Verbose($"{nameof(Get)} Image From Cache: {{0}}", id.Id);
            return image.Data;
        }
        
        byte[] bytes = _db.Query<byte[], uint>("SELECT image FROM data WHERE crc = ? LIMIT 1", id.Id);
        if (bytes != null)
        {
            _logger.Verbose($"{nameof(Get)} Image From DB: {{0}}", id.Id);
            _cache.Add(id, new CachedImage(bytes));
        }
        else
        {
            _logger.Verbose($"{nameof(Get)} Image Not Found In DB: {{0}}", id.Id);
        }

        return bytes;
    }

    public List<ImageId> GetExistingImages() => _db.GetAllCrc();

    public bool Exists(ImageId id) => _db.Query<bool, uint>("SELECT EXISTS(SELECT 1 FROM data WHERE crc = ? LIMIT 1);", id.Id);

    public ImageId Store(byte[] image)
    {
        Guard.IsMainThread();
        if (_db == null)
        {
            _logger.Error("Database is not initialized");
            return default;
        }

        uint crc = Crc.GetCRC(image);
        ImageId id = new(crc);

        _db.Execute("INSERT OR REPLACE INTO data (crc, image) VALUES (?, ?)", crc, image);
        _cache.Remove(id);
        _cache.Add(id, new CachedImage(image));
        _logger.Verbose($"{nameof(Store)} Image stored ID: {{0}} IsValid: {{1}}", id.Id, id.IsValid);
        return id;
    }

    public void Remove(ImageId id)
    {
        Guard.IsMainThread();
        try
        {
            _db.Execute("DELETE FROM data WHERE crc = ?", id.Id);
            _cache.Remove(id);
            _logger.Verbose($"{nameof(Remove)} Image Removed ID: {{0}} IsValid: {{1}}", id.Id, id.IsValid);
        }
        catch (Exception ex)
        {
            _logger.Exception("An error occured removing Image {0}", id, ex);
        }
    }

    protected override void OnServerSave() => _cache.ClampToSize();
    protected override void OnServerShutdown() => _db.Close();

    private sealed class ImageDatabase : Database
    {
        public List<ImageId> GetAllCrc()
        {
            IntPtr stmHandle = Prepare("SELECT crc FROM data");
            List<uint> crcs = [];
            ExecuteAndReadQueryResults(stmHandle, crcs, ptr => GetColumnValue<uint>(ptr, 0));
            return [.. crcs.Select(crc => new ImageId(crc))];
        }
    }
    
    private readonly struct CachedImage(byte[] data) : ICacheSize
    {
        public readonly byte[] Data = data;
        public uint Size => (uint) Data.Length;
        public bool IsValid => Data != null && Data.Length != 0;
    }
}