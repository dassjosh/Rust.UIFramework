using System;
using System.IO;
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
        Guard.IsNotNull(plugin);
        return Get(plugin.Id(), image, options);
    }
    
    internal string Get(PluginId pluginId, string image, GetImageOptions options)
    {
        Guard.IsCommunityEntityReady();
        Guard.IsValid(pluginId);
        Guard.IsNotNullOrEmpty(image);
        options ??= GetImageOptions.Default;

        if(TryGet(pluginId, image, out ImageId id))
        {
            return id.ToString();
        }

        if (image.IsValidUrl())
        {
            DownloadImageRequest request = RegisterImage(pluginId, image);
            if (request.State is { HadDownloadError: false, IsDownloading: true } || image == UiImageDefaults.NotFound)
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

    public bool TryGet(IUiFrameworkPlugin plugin, string image, out ImageId id)
    {
        Guard.IsNotNull(plugin);
        return TryGet(plugin.Id(), image, out id);
    }

    private bool TryGet(PluginId pluginId, string image, out ImageId id)
    {
        Guard.IsValid(pluginId);
        Guard.IsNotNullOrEmpty(image);

        if(ImageId.TryParse(image, out id))
        {
            return true;
        }

        id = _data.Get(pluginId, image);
        if (id.IsValid)
        {
            return true;
        }

        id = default;
        return false;
    }

    public ImageId GetByUrl(string url)
    {
        return _data.GetByUrl(url);
    }

    public byte[] GetBytes(IUiFrameworkPlugin plugin, string image)
    {
        Guard.IsNotNull(plugin);
        return GetBytes(plugin.Id(), image);
    }

    private byte[] GetBytes(PluginId pluginId, string image)
    {
        Guard.IsCommunityEntityReady();
        Guard.IsValid(pluginId);
        Guard.IsNotNullOrEmpty(image);

        if(ImageId.TryParse(image, out ImageId id))
        {
            return _db.Get(id);
        }

        id = _data.Get(pluginId, image);
        return id.IsValid ? _db.Get(id) : null;
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

        byte[] data = GetBytes(pluginId, image);
        if (data == null)
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

    public IRegisterImageRequest RegisterBorderRadius(IUiFrameworkPlugin plugin, UiSize2D size, in UiBorderRadius radius,
        UiColor? fillColor = null, UiColor? transparentColor = null,
        bool antiAlias = true, float edgeWidth = 1f,
        bool enableBorder = false, float borderWidth = 1f, UiColor? borderColor = null,
        bool enableDashedBorder = false, float dashLength = 1f, float gapLength = 1f,
        RegisterImageOptions options = null
        )
    {
        Guard.IsCommunityEntityReady();
        Guard.IsNotNull(plugin);
        Guard.IsGreaterThanZero(size.Width);
        Guard.IsGreaterThanZero(size.Height);

        UiColor selectedFillColor = fillColor ?? UiColors.White;
        UiColor selectedTransparentColor = transparentColor ?? UiColors.Transparent;
        UiColor selectedBorderColor = borderColor ?? UiColors.Black;

        using BorderRadiusData data = BorderRadiusData.Get(plugin, size, radius, selectedFillColor, selectedTransparentColor, antiAlias, edgeWidth, enableBorder, borderWidth, selectedBorderColor, enableDashedBorder, dashLength, gapLength);
        ImageId id = _data.GetBorderRadius(BorderRadiusKeyCache.GetKey(data));
        if (id.IsValid && _db.Exists(id))
        {
            _db.OnImageRegistered(id);
            byte[] image = _db.Get(id);
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
        ImageId id = _data.GetBorderRadius(BorderRadiusKeyCache.GetKey(data));
        byte[] imageData;
        if (id.IsValid && _db.Exists(id))
        {
            _db.OnImageRegistered(id);
            imageData = _db.Get(id);
            return Singleton<RegisteredImageData>.Instance.AddExistingImageRequest(plugin.Id(), data.ToName(), imageData, id, options ?? RegisterImageOptions.Default);
        }

        if (ImageId.TryParse(image, out id) && _db.Exists(id))
        {
            imageData = _db.Get(id);
            return Singleton<RegisteredImageData>.Instance.AddRequest(plugin.Id(), imageData, data.New(), options ?? RegisterImageOptions.Default);
        }

        if (image.IsValidUrl())
        {
            id = _data.GetByUrl(image);
            if (id.IsValid && _db.Exists(id))
            {
                imageData = _db.Get(id);
                return Singleton<RegisteredImageData>.Instance.AddRequest(plugin.Id(), imageData, data.New(), options ?? RegisterImageOptions.Default);
            }

            return Singleton<RegisteredImageData>.Instance.AddRequest(plugin.Id(), image, data.New(), options ?? RegisterImageOptions.Default);
        }

        imageData = GetBytes(plugin, image);
        if (imageData != null)
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