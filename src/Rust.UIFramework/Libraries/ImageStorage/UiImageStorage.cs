using System;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Data;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Guards;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public class UiImageStorage : BaseUiFrameworkLibrary, ISingleton
{
    private readonly ImageStorageData _data = ImageStorageData.Instance;
    private readonly IImageDatabase _db = OxideLibrary.GetLibrary<IImageDatabase>(nameof(IImageDatabase));
    private readonly IUiLogger<UiImageStorage> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<UiImageStorage>();
    
    public bool IsReady { get; private set; }

    private UiImageStorage() { }

    /// <summary>
    /// Returns the Image Id or Url for the given image
    /// If the image is already registered the Image Id will be returned else the Url will be returned and queued to be downloaded
    /// </summary>
    /// <param name="plugin">Plugin requesting the image</param>
    /// <param name="image">The Name, Url, or ID of the image</param>
    /// <param name="options">Options for getting the image</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Thrown if the plugin is null</exception>
    /// <exception cref="ArgumentNullException">Thrown if the image is null</exception>
    public string Get(IUiFrameworkPlugin plugin, string image, GetImageOptions options = null)
    {
#if SERVER
        Guard.IsNotNull(plugin);
        return Get(plugin.Id(), image, options);
#else
        return image;
#endif
    }
    
    internal string Get(PluginId pluginId, string image, GetImageOptions options)
    {
        Guard.IsCommunityEntityReady();
        Guard.IsValid(pluginId);
        Guard.IsNotNullOrEmpty(image);
        options ??= GetImageOptions.Default;

        if(ImageId.TryParse(image, out _))
        {
            return image;
        }

        ImageId id = _data.Get(pluginId, image);
        if (id.IsValid)
        {
            return id.ToString();
        }

        if (image.IsValidUrl())
        {
            DownloadImageRequest request = RegisterImage(pluginId, image);
            if (request.State is { HadDownloadError: false, IsDownloading: true })
            {
                return image;
            }

            if (request.State.HadDownloadError && !string.IsNullOrEmpty(options.FallbackImage) && image != options.FallbackImage)
            {
                return Get(pluginId, options.FallbackImage, null);
            }
            
            _logger.Debug("Image had an error downloading and no fallback image registered for plugin: {0} name: {1}. Using not found image.", pluginId.FullName(), image);
            return Get(UiFrameworkPlugin.Instance, UiImageDefaults.NotFound);
        }

        if (image.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
        {
            _logger.Warning("URL's must start with http:// or https://. Url: {0}", image);
        }
        else
        {
            _logger.Warning("Failed to get image for plugin: {0} name: {1}", pluginId.FullName(), image);
        }
        
        return Get(UiFrameworkPlugin.Instance, UiImageDefaults.NotFound);
    }

    public IDownloadImageRequest RegisterImage(IUiFrameworkPlugin plugin, string url, RegisterImageOptions options = null) => RegisterImage(plugin, url, url, options);
    internal DownloadImageRequest RegisterImage(PluginId plugin, string url, RegisterImageOptions options = null) => RegisterImage(plugin, url, url, options);
    public IDownloadImageRequest RegisterImage(IUiFrameworkPlugin plugin, string name, string url, RegisterImageOptions options = null) => RegisterImage(plugin.Id(), name, url, options);
    internal DownloadImageRequest RegisterImage(PluginId pluginId, string name, string url, RegisterImageOptions options = null)
    {
        Guard.IsCommunityEntityReady();
        Guard.IsValid(pluginId);
        Guard.IsValidUrl(url);
        Guard.IsNotNullOrEmpty(name);
        options ??= RegisterImageOptions.Default;
        
        ImageId id = _data.GetByUrl(url);
        if (id.IsValid && _db.Exists(id))
        {
            _data.AddPluginImage(pluginId, name, id);
            _db.OnImageRegistered(id);
            return Singleton<RegisteredImageData>.Instance.AddExistingImageRequest(pluginId, name, url, id, options);
        }
        
        return Singleton<RegisteredImageData>.Instance.AddRequest(pluginId, name, url, options);
    }

    public IRegisterImageRequest RegisterImage(IUiFrameworkPlugin plugin, string name, byte[] image, RegisterImageOptions options = null)
    {
        Guard.IsCommunityEntityReady();
        Guard.IsNotNull(plugin);
        Guard.IsNotNullOrEmpty(name);
        Guard.IsNotNullOrEmpty(image);
        options ??= RegisterImageOptions.Default;
        
        ImageId id = _data.Get(plugin.Id(), name);
        if (id.IsValid && id.Id == Crc.GetCRC(image) && _db.Exists(id))
        {
            _db.OnImageRegistered(id);
            return Singleton<RegisteredImageData>.Instance.AddExistingImageRequest(plugin.Id(), name, image, id, options);
        }
        
        return Singleton<RegisteredImageData>.Instance.AddRequest(plugin.Id(), name, image, options);
    }

    public IRegisterImageRequest RegisterBorderRadius(IUiFrameworkPlugin plugin, in UiBorderRadius radius, bool antiAlias = true, float edgeWidth = 1f)
    {
        return RegisterBorderRadius(plugin, new UiDimensions2D(200, 200), radius, antiAlias, edgeWidth);
    }

    public IRegisterImageRequest RegisterBorderRadius(IUiFrameworkPlugin plugin, UiDimensions2D size, in UiBorderRadius radius, bool antiAlias = true, float edgeWidth = 1f)
    {
        Guard.IsCommunityEntityReady();
        Guard.IsNotNull(plugin);
        Guard.IsGreaterThanZero(size.Width);
        Guard.IsGreaterThanZero(size.Height);
        Guard.IsGreaterThanOrEqualToZero(edgeWidth);

        using BorderRadiusData data = BorderRadiusData.Get(plugin, size, radius, antiAlias, edgeWidth);
        ImageId id = _data.GetBorderRadius(BorderRadiusKeyCache.GetKey(data));
        if (id.IsValid && _db.Exists(id))
        {
            _db.OnImageRegistered(id);
            byte[] image = _db.Get(id);
            return Singleton<RegisteredImageData>.Instance.AddExistingImageRequest(plugin.Id(), data.ToName(), image, id, RegisterImageOptions.Default);
        }

        (BorderRadiusRequest request, BorderRadiusRequestHandler handler) = Singleton<RegisteredImageData>.Instance.CreateRequest(plugin.Id(), data.New());
        Singleton<BorderRadiusHandler>.Instance.Enqueue(handler);
        return request;
    }

    internal string GetBorderRadius(IUiFrameworkPlugin plugin, UiDimensions2D size, in UiBorderRadius radius, bool antiAlias = true, float edgeWidth = 1f)
    {
        Guard.IsCommunityEntityReady();
        Guard.IsNotNull(plugin);
        Guard.IsGreaterThanZero(size.Width);
        Guard.IsGreaterThanZero(size.Height);
        Guard.IsGreaterThanOrEqualToZero(edgeWidth);

        using BorderRadiusData data = BorderRadiusData.Get(plugin, size, radius, antiAlias, edgeWidth);
        // ImageId id = _data.GetBorderRadius(BorderRadiusKeyCache.GetKey(data));
        // if(id.IsValid)
        // {
        //     _logger.Debug("Border radius image already stored for plugin: {0} name: {1}", plugin.Id(), data.ToName());
        //     return id.ToString();
        // }

        Guard.IsMainThread();

        (BorderRadiusRequest _, BorderRadiusRequestHandler handler) = Singleton<RegisteredImageData>.Instance.CreateRequest(plugin.Id(), data.New());
        return Singleton<SynchronousHandler>.Instance.RunSynchronously(handler) == ProcessResult.Success ? handler.ImageId.ToString() : null;
    }

    internal string GetBorderRadius(IUiFrameworkPlugin plugin, string png, in UiBorderRadius radius, bool antiAlias, float edgeWidth, UiColor replacementColor)
    {
        if (string.IsNullOrEmpty(png) || !uint.TryParse(png, out _))
        {
            return png;
        }

        Guard.IsCommunityEntityReady();
        Guard.IsNotNull(plugin);
        Guard.IsGreaterThanOrEqualToZero(edgeWidth);

        using BorderRadiusImageData key = BorderRadiusImageData.Get(plugin, png, radius, antiAlias, edgeWidth, replacementColor);
        ImageId id = _data.GetBorderRadius(BorderRadiusKeyCache.GetKey(key));
        if(id.IsValid)
        {
            return id.ToString();
        }

        Guard.IsMainThread();

        byte[] image = _db.Get(id);
        if (image == null || image.Length == 0)
        {
            return png;
        }

        (RegisterImageRequest _, RegisterImageRequestHandler handler) = Singleton<RegisteredImageData>.Instance.CreateRequest(plugin.Id(), image, key.New());
        return Singleton<SynchronousHandler>.Instance.RunBorderRadiusImageSynchronously(handler) == ProcessResult.Success ? handler.ImageId.ToString() : png;
    }

    public bool IsDownloading(string url) => Singleton<RegisteredImageData>.Instance.IsDownloading(url);

    protected override void OnCommunityEntitySpawned(ICommunityEntity entity)
    {
        IsReady = true;
        ImageStorageData.Instance.OnCommunityEntityLoaded(_db.GetSaveVersion(entity));
#if SERVER
        RegisterImage(UiFrameworkPlugin.Instance, UiImages.White1x1Name, Convert.FromBase64String(UiImages.White1x1Base64));
        Interface.Oxide.CallHook(UiFrameworkHooks.OnUiImageStorageReady);
#endif
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
        Singleton<ImageDownloadHandler>.Instance.OnServerShutdown();
    }
}