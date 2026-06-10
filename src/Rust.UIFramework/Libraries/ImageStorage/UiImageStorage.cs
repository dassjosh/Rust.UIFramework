using System;
using Cysharp.Threading.Tasks;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Data;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Guards;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Libraries.ImagePrecache;
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
        CommunityEntityNotReadyException.ThrowIfNotReady();
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
        CommunityEntityNotReadyException.ThrowIfNotReady();
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
        CommunityEntityNotReadyException.ThrowIfNotReady();
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

    public bool IsDownloading(string url) => Singleton<RegisteredImageData>.Instance.IsDownloading(url);

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
        Singleton<ImageDownloadHandler>.Instance.OnServerShutdown();
    }
}