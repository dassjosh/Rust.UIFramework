using System;
using System.Collections.Generic;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Data;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;


namespace Oxide.Ext.UiFramework.Libraries;

public class UiImageStorage : BaseUiFrameworkLibrary, ISingleton
{
    private readonly ImageStorageData _data = ImageStorageData.Instance;
    private readonly ImageDownloader _downloader = new();
    private readonly IUiLogger<UiImageStorage> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<UiImageStorage>();
    public bool IsReady { get; private set; }
    
    private static readonly byte[] SignaturePNG = [137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82];

    private UiImageStorage() {}

    public string Get(Plugin plugin, string name)
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        return Get(plugin.Id(), name);
    }
    
    internal string Get(PluginId pluginId, string name)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        ImageId id = _data.Get(pluginId, name);
        if (id.IsValid)
        {
            return id.Id;
        }

        if (name.StartsWith("http"))
        {
            RegisterImage(pluginId, name);
            return name;
        }
        
        _logger.Debug("Failed to get image for plugin: {0} name: {1}", pluginId.FullName(), name);
        
        return Get(UiFrameworkPlugin.Instance, UiImageDefaults.NotFound);
    }

    public bool RegisterImage(Plugin plugin, string name, string url) => RegisterImage(plugin.Id(), name, url);
    
    internal bool RegisterImage(PluginId pluginId, string name, string url)
    {
        CommunityEntityNotReadyException.ThrowIfNotReady();
        ImageId id = _data.GetByUrl(url);
        if (id.IsValid)
        {
            _data.AddPluginImage(pluginId, name, id);
            return true;
        }
        
        return _downloader.AddRequest(pluginId, name, url);
    }
    
    public bool RegisterImage(Plugin plugin, string url) => RegisterImage(plugin, url, url);

    internal bool RegisterImage(PluginId plugin, string url) => RegisterImage(plugin, url, url);

    public bool RegisterImage(Plugin plugin, string name, byte[] image, out string error)
    {
        CommunityEntityNotReadyException.ThrowIfNotReady();
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
        if (image == null) throw new ArgumentNullException(nameof(image));
        
        ImageId id = _data.Get(plugin.Id(), name);
        if (id.IsValid)
        {
            error = "Image already registered";
            return false;
        }
        
        ImageId imageId = ProcessImage(image, out error);
        if (!imageId.IsValid)
        {
            return false;
        }
        
        _data.AddPluginImage(plugin.Id(), name, imageId);
        return true;
    }

    public void BulkRegisterImages(Plugin plugin, Dictionary<string, string> images)
    {
        CommunityEntityNotReadyException.ThrowIfNotReady();
        _downloader.BulkAddRequests(plugin.Id(), images);
    }

    internal void OnImageDownloaded(in CompletedDownload download)
    {
        DownloadRequest request = download.Request;
        byte[] image = download.Data;
        ImageId imageId = ProcessImage(image, out string error);
        if (!imageId.IsValid)
        {
            _logger.Warning("Failed to download image from url: {0} Error: {1}", request.Url, error);
            return;
        }
        
        _data.AddPluginImage(request.PluginId, request.Name, imageId);
        _data.AddUrlImage(request.Url, imageId);
    }

    private static ImageId ProcessImage(byte[] image, out string error)
    {
        if (image == null || image.Length == 0)
        {
            error = "Image byte[] is empty";
            return default;
        }

        if (!IsValidRustPng(image) && !IsValidJpegImage(image))
        {
            error = "Image is not a valid PNG or JPEG";
            return default;
        }
        
        error = null;
        return StoreImage(image);
    }

    private static bool IsValidRustPng(byte[] image) => image.AsSpan().StartsWith(SignaturePNG);
    private static bool IsValidJpegImage(byte[] image) => image is [0xFF, 0xD8, ..];
    private static ImageId StoreImage(byte[] image) => new(FileStorage.server.Store(image, FileStorage.Type.png, CommunityEntity.ServerInstance.net.ID).ToString());

    internal void OnCommunityEntitySpawned()
    {
        IsReady = true;
        Interface.Oxide.CallHook(UiFrameworkHooks.OnUiImageStorageReady);
    }

    protected override void OnPluginLoaded(Plugin plugin)
    {
        if (IsReady)
        {
            plugin.CallHook(UiFrameworkHooks.OnUiImageStorageReady);
        }
    }

    protected override void OnServerShutdown()
    {
        _downloader.OnServerShutdown();
    }
}