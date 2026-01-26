using System;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Data;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Libraries.ImagePrecache;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public class UiImageStorage : BaseUiFrameworkLibrary, ISingleton
{
    private readonly ImageStorageData _data = ImageStorageData.Instance;
    private readonly ImageDownloadQueue _downloader;
    private readonly IImageDatabase _db = OxideLibrary.GetLibrary<IImageDatabase>(nameof(IImageDatabase));
    private readonly IUiLogger<UiImageStorage> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<UiImageStorage>();
    private readonly IImageStorageBehavior _storage;
    
    public bool IsReady { get; private set; }
    
    private static readonly byte[] SignaturePNG = [137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82];

    private UiImageStorage()
    {
        _downloader = new ImageDownloadQueue();
#if SERVER
        _storage = SingletonBehavior<ImageStorageBehavior>.Instance;
#else
        _storage = Singleton<ImageStorageBehavior>.Instance;
#endif
    }

    public string Get(IUiFrameworkPlugin plugin, string nameOrUrl, GetImageOptions options = null)
    {
#if SERVER
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        return Get(plugin.Id(), nameOrUrl, options);
#else
        return nameOrUrl;
#endif
    }
    
    internal string Get(PluginId pluginId, string nameOrUrl, GetImageOptions options)
    {
        InvalidPluginIdException.ThrowIfInvalidPluginId(pluginId); 
        if (nameOrUrl == null) throw new ArgumentNullException(nameof(nameOrUrl));
        options ??= GetImageOptions.Default;

        ImageId id = _data.Get(pluginId, nameOrUrl);
        if (id.IsValid)
        {
            return id.ToString();
        }

        if (nameOrUrl.IsValidUrl())
        {
            RegisterImageRequest request = RegisterImage(pluginId, nameOrUrl);
            if (request.Download is { HadDownloadError: false, IsDownloading: true })
            {
                return nameOrUrl;
            }

            if (request.Download.HadDownloadError && !string.IsNullOrEmpty(options.FallbackImageNameOrUrl) && nameOrUrl != options.FallbackImageNameOrUrl)
            {
                return Get(pluginId, options.FallbackImageNameOrUrl, null);
            }
            
            _logger.Debug("Image had an error downloading and no fallback image registered for plugin: {0} name: {1}. Using not found image.", pluginId.FullName(), nameOrUrl);
            
            return Get(UiFrameworkPlugin.Instance, UiImageDefaults.NotFound);
        }

        if (nameOrUrl.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
        {
            _logger.Warning("URL's must start with http:// or https://. Url: {0}", nameOrUrl);
        }
        else
        {
            _logger.Warning("Failed to get image for plugin: {0} name: {1}", pluginId.FullName(), nameOrUrl);
        }
        
        return Get(UiFrameworkPlugin.Instance, UiImageDefaults.NotFound);
    }

    public RegisterImageRequest RegisterImage(IUiFrameworkPlugin plugin, string url, RegisterImageOptions options = null) => RegisterImage(plugin, url, url, options);
    internal RegisterImageRequest RegisterImage(PluginId plugin, string url, RegisterImageOptions options = null) => RegisterImage(plugin, url, url, options);
    public RegisterImageRequest RegisterImage(IUiFrameworkPlugin plugin, string name, string url, RegisterImageOptions options = null) => RegisterImage(plugin.Id(), name, url, options);
    internal RegisterImageRequest RegisterImage(PluginId pluginId, string name, string url, RegisterImageOptions options = null)
    {
        InvalidPluginIdException.ThrowIfInvalidPluginId(pluginId); 
        CommunityEntityNotReadyException.ThrowIfNotReady();
        InvalidUrlException.ThrowIfInvalidUrl(url);
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
        options ??= RegisterImageOptions.Default;
        
        ImageId id = _data.GetByUrl(url);
        if (id.IsValid && _db.Exists(id))
        {
            _data.AddPluginImage(pluginId, name, id);
            _db.OnImageRegistered(id);
            return _downloader.AddStoredImageRequest(pluginId, name, url, id, options);
        }
        
        return _downloader.AddRequest(pluginId, name, url, options);
    }

    public RegisterImageErrorCode RegisterImage(IUiFrameworkPlugin plugin, string name, byte[] image, RegisterImageOptions options = null)
    {
        CommunityEntityNotReadyException.ThrowIfNotReady();
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
        if (image == null) throw new ArgumentNullException(nameof(image));
        options ??= RegisterImageOptions.Default;
        
        ImageId id = _data.Get(plugin.Id(), name);
        if (id.IsValid && id.Id == Crc.GetCRC(image) && _db.Exists(id))
        {
            _db.OnImageRegistered(id);
            if (options.EnableClientPrecache)
            {
                Singleton<UiImagePrecache>.Instance.AddPrecachedImage(plugin.Id(), id, image);
            }
            return RegisterImageErrorCode.AlreadyRegistered;
        }
        
        ImageId imageId = ProcessImage(image, out RegisterImageErrorCode error);
        if (!imageId.IsValid)
        {
            return error;
        }
        
        _data.AddPluginImage(plugin.Id(), name, imageId);
        if (options.EnableClientPrecache)
        {
            Singleton<UiImagePrecache>.Instance.AddPrecachedImage(plugin.Id(), id, image);
        }
        return RegisterImageErrorCode.None;
    }

    public bool IsDownloading(string url) => _downloader.IsDownloading(url);

    internal void OnDownloadCompleted(ImageDownloadRequest download) => _storage.OnDownloadCompleted(download);
    
    internal void StoreDownloadedImage(ImageDownloadRequest download)
    {
        byte[] image = download.Image;
        ImageId imageId = ProcessImage(image, out RegisterImageErrorCode error);
        if (!imageId.IsValid)
        {
            _logger.Error("Failed to download image from url: {0} Error: {1}", download.Url, error);
            download.OnInvalidImage(error);
            return;
        }
        
        _data.AddUrlImage(download.Url, imageId);
        foreach (RegisterImageRequest request in download.URLRequests)
        {
            _data.AddPluginImage(request.PluginId, request.Name, imageId);
            request.ExecuteOnDownloadCompleted();
        }
        
        download.OnImageStored(imageId);
        Singleton<ImageDownloadAnimationHandler>.Instance.OnDownloadCompleted(download.Url, true, imageId);
    }

    private ImageId ProcessImage(byte[] image, out RegisterImageErrorCode error)
    {
        if (image == null || image.Length == 0)
        {
            error = RegisterImageErrorCode.InvalidByteArray;
            return default;
        }

        if (!IsValidRustPng(image) && !IsValidJpegImage(image))
        {
            error = RegisterImageErrorCode.InvalidImageType;
            return default;
        }

        ImageId id = StoreImage(image);
        if (!id.IsValid)
        {
            error = RegisterImageErrorCode.DbStorageFailed;
            return default;
        }
        
        error = RegisterImageErrorCode.None;
        return id;
    }

    private static bool IsValidRustPng(byte[] image) => image.AsSpan().StartsWith(SignaturePNG);
    private static bool IsValidJpegImage(byte[] image) => image is [0xFF, 0xD8, ..];
    private ImageId StoreImage(byte[] image)
    {
#if SERVER
        return _db.Store(image);
#else
        return new ImageId((uint)Core.Random.Range(0, int.MaxValue));
#endif
    }

    protected override void OnCommunityEntitySpawned(CommunityEntity entity)
    {
        IsReady = true;
        ImageStorageData.Instance.OnCommunityEntityLoaded(_db.GetSaveVersion(entity));
        RegisterImage(UiFrameworkPlugin.Instance, UiImages.White1x1Name, Convert.FromBase64String(UiImages.White1x1Base64));
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