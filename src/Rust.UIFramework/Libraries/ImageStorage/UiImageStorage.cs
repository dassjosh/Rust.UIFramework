using System;
using System.IO;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Guards;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

#if SERVER
using Oxide.Core;
#endif

namespace Oxide.Ext.UiFramework.Libraries;

public class UiImageStorage : BaseUiFrameworkLibrary, ISingleton
{
    private readonly UiImageDatabaseHandler _handler = OxideLibrary.GetLibrary<UiImageDatabaseHandler>(nameof(UiImageDatabaseHandler));
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
        Guard.IsNotNull(plugin);
        return Get(plugin.Id(), image, options);
    }
    
    internal string Get(PluginId pluginId, string image, GetImageOptions options)
    {
        Guard.IsCommunityEntityReady();
        Guard.IsValid(pluginId);
        Guard.IsNotNullOrEmpty(image);
        options ??= GetImageOptions.Default;

        if(_handler.TryGetImageId(pluginId, image, out ImageId id))
        {
            return id.ToString();
        }

        if (image.IsValidUrl())
        {
            if (image == UiImageDefaults.NotFound)
            {
                return image;
            }

            DownloadImageRequest request = RegisterImage(pluginId, image);
            if (request.State is { HadDownloadError: false, IsDownloading: true })
            {
                return image;
            }

            if (request.State.HadDownloadError && !string.IsNullOrEmpty(options.FallbackImage) && image != options.FallbackImage)
            {
                return Get(pluginId, options.FallbackImage, options);
            }
            
            _logger.Debug("Image had an error downloading and no fallback image registered for plugin: {0} name: {1}. Using not found image.", pluginId.FullName(), image);
            return NotFound();
        }

        if (image.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
        {
            _logger.Warning("URL's must start with http:// or https://. Url: {0}", image);
        }
        else
        {
            _logger.Warning("Failed to get image for plugin: {0} name: {1}", pluginId.FullName(), image);
        }
        
        return NotFound();
    }

    public bool TryGet(IUiFrameworkPlugin plugin, string image, out ImageId id)
    {
        Guard.IsNotNull(plugin);
        return _handler.TryGetImageId(plugin.Id(), image, out id);
    }

    public bool TryGetImage(IUiFrameworkPlugin plugin, string image, out byte[] bytes)
    {
        Guard.IsNotNull(plugin);
        return TryGetImage(plugin.Id(), image, out bytes);
    }

    private bool TryGetImage(PluginId pluginId, string image, out byte[] bytes)
    {
        Guard.IsCommunityEntityReady();
        Guard.IsValid(pluginId);
        Guard.IsNotNullOrEmpty(image);

        return _handler.TryGetBytes(pluginId, image, out bytes);
    }

    public bool WriteImage(IUiFrameworkPlugin plugin, string filePath, string image)
    {
        Guard.IsNotNull(plugin);
        return WriteImage(plugin.Id(), filePath, image);
    }

    public bool WriteImage(PluginId pluginId, string filePath, string image)
    {
        Guard.IsCommunityEntityReady();
        Guard.IsValid(pluginId);
        Guard.IsNotNullOrEmpty(filePath);
        Guard.IsNotNullOrEmpty(image);

        if (!TryGetImage(pluginId, image, out byte[] data))
        {
            _logger.Error("Failed to write image for plugin: {0} name: {1}. Image not found.", pluginId.FullName(), image);
            return false;
        }

        UiImageValidation.TryGetImageType(data, out UiImageType type);
        if (type == UiImageType.Unknown)
        {
            _logger.Error("Failed to write image for plugin: {0} name: {1}. Image type not supported.", pluginId.FullName(), image);
            return false;
        }

        string extension = Path.GetExtension(filePath);
        filePath = type switch
        {
            UiImageType.Png when extension.Equals(".jpg") => Path.ChangeExtension(filePath, ".png"),
            UiImageType.Jpg when extension.Equals(".png") => Path.ChangeExtension(filePath, ".jpg"),
            _ => filePath
        };

        File.WriteAllBytes(filePath, data);
        return true;
    }

    public string NotFound() => Get(UiFrameworkPlugin.Instance, UiImageDefaults.NotFound);

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

        if (_handler.TryGetByUrl(url, out ImageId id) && _handler.TryGetBytes(id, out byte[] image))
        {
            _handler.AddPluginImage(pluginId, name, id);
            return Singleton<RegisteredImageData>.Instance.AddExistingImageRequest(pluginId, name, url, image, id, options);
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

        if (_handler.TryGetImageId(plugin.Id(), name, out ImageId id) && id.Id == FileStorage.ComputeCrc(image))
        {
            return Singleton<RegisteredImageData>.Instance.AddExistingImageRequest(plugin.Id(), name, image, id, options);
        }
        
        return Singleton<RegisteredImageData>.Instance.AddRequest(plugin.Id(), name, image, options);
    }

    public IRegisterImageRequest RegisterBorderRadius(IUiFrameworkPlugin plugin, UiSize2D size, in UiBorderRadius radius,
        UiColor? fillColor = null, UiColor? transparentColor = null,
        bool antiAlias = true, float edgeWidth = 1f,
        bool enableBorder = false, float borderWidth = 1f, UiColor? borderColor = null,
        bool enableDashedBorder = false, float dashLength = 1f, float gapLength = 1f,
        RegisterImageOptions options = null)
    {
        Guard.IsCommunityEntityReady();
        Guard.IsNotNull(plugin);
        Guard.IsGreaterThanZero(size.Width);
        Guard.IsGreaterThanZero(size.Height);

        UiColor selectedFillColor = fillColor ?? UiColors.White;
        UiColor selectedTransparentColor = transparentColor ?? UiColors.Transparent;
        UiColor selectedBorderColor = borderColor ?? UiColors.Black;

        using BorderRadiusData data = BorderRadiusData.Get(plugin, size, radius, selectedFillColor, selectedTransparentColor, antiAlias, edgeWidth, enableBorder, borderWidth, selectedBorderColor, enableDashedBorder, dashLength, gapLength);
        if (_handler.TryGetBorderRadiusImageId(data, out ImageId id) && _handler.TryGetBytes(id, out byte[] image))
        {
            return Singleton<RegisteredImageData>.Instance.AddExistingImageRequest(plugin.Id(), data.ToName(), image, id, RegisterImageOptions.Default);
        }

        BorderRadiusRequest request = Singleton<RegisteredImageData>.Instance.AddRequest(plugin.Id(), data.New(), options ?? RegisterImageOptions.Default);
        return request;
    }

    public IRegisterImageRequest RegisterBorderRadius(IUiFrameworkPlugin plugin, string image, in UiBorderRadius radius, UiColor? transparentColor = null,
        bool antiAlias = true, float edgeWidth = 1f,
        bool enableBorder = false, float borderWidth = 1f, UiColor? borderColor = null,
        bool enableDashedBorder = false, float dashLength = 1f, float gapLength = 1f,
        RegisterImageOptions options = null
    )
    {
        Guard.IsCommunityEntityReady();
        Guard.IsNotNull(plugin);
        Guard.IsNotNullOrEmpty(image);
        UiColor selectedTransparentColor = transparentColor ?? UiColors.Transparent;
        UiColor selectedBorderColor = borderColor ?? UiColors.Black;

        using BorderRadiusData data = BorderRadiusData.Get(plugin, image, radius, selectedTransparentColor, antiAlias, edgeWidth, enableBorder, borderWidth, selectedBorderColor, enableDashedBorder, dashLength, gapLength);
        if (_handler.TryGetBorderRadiusImageId(data, out ImageId id) && _handler.TryGetBytes(id, out byte[] imageData))
        {
            return Singleton<RegisteredImageData>.Instance.AddExistingImageRequest(plugin.Id(), data.ToName(), imageData, id, options ?? RegisterImageOptions.Default);
        }

        if (ImageId.TryParse(image, out id) && _handler.TryGetBytes(id, out imageData))
        {
            return Singleton<RegisteredImageData>.Instance.AddRequest(plugin.Id(), imageData, data.New(), options ?? RegisterImageOptions.Default);
        }

        if (image.IsValidUrl())
        {
            if (_handler.TryGetByUrl(image, out id) && _handler.TryGetBytes(id, out imageData))
            {
                return Singleton<RegisteredImageData>.Instance.AddRequest(plugin.Id(), imageData, data.New(), options ?? RegisterImageOptions.Default);
            }

            return Singleton<RegisteredImageData>.Instance.AddRequest(plugin.Id(), image, data.New(), options ?? RegisterImageOptions.Default);
        }

        if (_handler.TryGetImageId(plugin.Id(), image, out id) && _handler.TryGetBytes(id, out imageData))
        {
            return Singleton<RegisteredImageData>.Instance.AddRequest(plugin.Id(), imageData, data.New(), options ?? RegisterImageOptions.Default);
        }

        _logger.Error("Failed to register border radius image for plugin: {0} name: {1}. Image not found.", plugin.Id(), image);
        return Singleton<RegisteredImageData>.Instance.CreateFailed(plugin.Id(), image, options ?? RegisterImageOptions.Default);
    }

    public bool IsDownloading(string url) => Singleton<RegisteredImageData>.Instance.IsDownloading(url);

    protected override void OnCommunityEntitySpawned(ICommunityEntity entity)
    {
        IsReady = true;
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