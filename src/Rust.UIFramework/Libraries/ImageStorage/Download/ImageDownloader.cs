using System;
using System.Collections.Concurrent;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

/// <summary>
/// Handles concurrent downloading of images
/// </summary>
internal class ImageDownloader
{
    internal readonly ConcurrentQueue<UrlDownloadState> RequestQueue = [];
    private readonly ConcurrentDictionary<string, UrlDownloadState> _urlRequests = new();
    private readonly IUiLogger<ImageDownloader> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<ImageDownloader>();
    private readonly IImageDownloader _downloader;
    
    public ImageDownloader()
    {
        if (UiFrameworkConfig.Instance.Threading.EnableImageDownloadThread)
        {
            _downloader = new ThreadedImageDownloader();
        }
        else
        {
            _downloader = SingletonBehavior<BehaviorImageDownloader>.Instance;
        }
        
        _downloader.OnInit(this);
    }

    /// <summary>
    /// Adds a new download request to the queue if it doesn't already exist or hasn't failed too many times
    /// </summary>
    /// <param name="pluginId">The plugin ID associated with the request</param>
    /// <param name="name">The name for the downloaded image</param>
    /// <param name="url">The URL to download the image from</param>
    /// <returns>True if the request was added, false if it already existed or failed too many times</returns>
    internal DownloadImageRequest AddRequest(PluginId pluginId, string name, string url)
    {
        if (!pluginId.IsValid) throw new ArgumentNullException(nameof(pluginId));
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));
        
        UrlDownloadState state = _urlRequests.GetOrAdd(url, u => new UrlDownloadState(u));
        DownloadImageRequest request = new(pluginId, name, state);
        if (state.IsCompleted || state.IsOutOfAttempts)
        {
            return request;
        }
        
        state.AddRequest(request);
        if (!state.IsDownloading)
        {
            state.OnDownloadQueued();
            RequestQueue.Enqueue(state);
            _downloader.OnDownloadQueued();
        }

        return request;
    }

    internal DownloadImageRequest AddStoredImageRequest(PluginId pluginId, string name, string url, ImageId imageId)
    {
        if (!pluginId.IsValid) throw new ArgumentNullException(nameof(pluginId));
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));
        
        UrlDownloadState state = _urlRequests.GetOrAdd(url, u => new UrlDownloadState(u));
        if (!state.IsCompleted)
        {
            state.OnImageStored(imageId);
        }
        
        return new DownloadImageRequest(pluginId, name, state);
    }

    internal bool IsDownloading(string url) => _urlRequests.TryGetValue(url, out UrlDownloadState state) && state.IsDownloading;
    
    internal void OnServerShutdown()
    {
        _downloader.OnServerShutdown();
    }
}