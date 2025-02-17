using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Data;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Libraries;

internal class ImageStorage : BaseUiFrameworkLibrary, ISingleton
{
    private readonly ConcurrentDictionary<PluginImage, ImageId> _pluginImages = new();
    private readonly ConcurrentDictionary<string, ImageId> _downloadedImages = new();
    private readonly ConcurrentDictionary<string, int> _downloadAttempts = new();
    private readonly ImageStorageData _data = ImageStorageData.Instance;
    private readonly HttpClient _client;
    
    private static readonly byte[] SignaturePNG = [137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82];

    private ImageStorage()
    {
        HttpClientHandler handler = new()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            UseCookies = false
        };
        _client = new HttpClient(handler)
        {
            Timeout = TimeSpan.FromSeconds(30)
        };
    }
    
    public string Get(string url)
    {
        if (_downloadedImages.TryGetValue(url, out ImageId png) && png.IsValid)
        {
            return png.Id;
        }

        if (!_downloadAttempts.TryGetValue(url, out int attempts))
        {
            _downloadAttempts[url] = attempts = 0;
        }

        if (attempts > 3)
        {
            return url;
        }

        Task.Run(() => DownloadImageInternal(url));
        
        return url;
    }
    
    public UiRawImage Get(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, string url, UiColor color)
    {
        string png = Get(url);
        return png.StartsWith("http") ? builder.WebImage(parent, pos, offset, png, color) : builder.ImageFileStorage(parent, pos, offset, png, color);
    }

    public string Get(Plugin plugin, string name)
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        if (name == null) throw new ArgumentNullException(nameof(name));
        if (_pluginImages.TryGetValue(new PluginImage(plugin, name), out ImageId png) && png.IsValid)
        {
            return png.Id;
        }

        throw new ImageNotFoundException(plugin, name);
    }
    
    public UiRawImage Get(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, Plugin plugin, string name, UiColor color)
    {
        string png = Get(plugin, name);
        return builder.ImageFileStorage(parent, pos, offset, png, color);
    }

    public bool RegisterNamedImage(Plugin plugin, string name, byte[] image, out string error)
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
        if (image == null) throw new ArgumentNullException(nameof(image));

        PluginImage pluginImage = new(plugin, name);
        if (_pluginImages.TryGetValue(pluginImage, out ImageId png) && png.IsValid)
        {
            error = "Image already registered";
            return false;
        }
        
        ImageId imageId = ProcessImage(image, out error);
        if (!imageId.IsValid)
        {
            return false;
        }
        
        _pluginImages[pluginImage] = imageId;
        _data.AddPluginImage(pluginImage, imageId);
        return true;
    }
    
    public bool RegisterNamedImage(Plugin plugin, string name, string base64, out string error)
    {
        return RegisterNamedImage(plugin, name, Convert.FromBase64String(base64), out error);
    }

    protected override void OnServerInitialized()
    {
        ImageStorageData data = ImageStorageData.Instance;
        foreach (UrlImage urlImage in data.URLImages)
        {
            _downloadedImages[urlImage.Url] = urlImage.ImageId;
        }
        
        foreach (NamedImage pluginImage in data.PluginImages)
        {
            _pluginImages[new PluginImage(pluginImage.PluginId, pluginImage.Name)] = pluginImage.ImageId;
        }
    }

    private async ValueTask DownloadImageInternal(string url)
    {
        try
        {
            byte[] image = await GetImageInternal(url);
            ImageId imageId = ProcessImage(image, out string error);
            if (!imageId.IsValid)
            {
                Interface.Oxide.LogWarning($"[UiFramework] Failed to download image from url: {url} Error: {error}");
                IncrementAttempt(url);
                return;
            }
            
            _downloadedImages[url] = imageId;
            _data.AddUrlImage(url, imageId);
        }
        catch (Exception ex)
        {
            Interface.Oxide.LogException($"[UiFramework] an error downloading image: {url}", ex);
            IncrementAttempt(url);
        }
    }

    private static ImageId ProcessImage(byte[] image, out string error)
    {
        if (image == null || image.Length == 0)
        {
            error = "Image byte[] is empty";
            return default;
        }

        if (!IsValidRustPng(image))
        {
            if (!IsValidJpegImage(image))
            {
                error = "Image is not a valid PNG or JPEG";
                return default;
            }
                
            image = ConvertToPng(image);
        }
        
        error = null;
        return StoreImage(image);
    }
    
    private async ValueTask<byte[]> GetImageInternal(string url)
    {
        HttpResponseMessage response = await _client.GetAsync(new Uri(url));
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsByteArrayAsync();
        }
        
        return [];
    }

    private void IncrementAttempt(string url)
    {
        _downloadAttempts.TryGetValue(url, out int attempts);
        _downloadAttempts[url] = attempts + 1;
    }

    private static bool IsValidRustPng(byte[] image) => image.AsSpan()[..16] == SignaturePNG;
    private static bool IsValidJpegImage(byte[] image) => image is [0xFF, 0xD8, ..];
    private static ImageId StoreImage(byte[] image) => new(FileStorage.server.Store(image, FileStorage.Type.png, CommunityEntity.ServerInstance.net.ID).ToString());

    private static byte[] ConvertToPng(byte[] jpeg)
    {
        using MemoryStream pngStream = new();
        using Image image = Image.FromStream(new MemoryStream(jpeg));
        image.Save(pngStream, ImageFormat.Png);
        return pngStream.ToArray();
    }
}