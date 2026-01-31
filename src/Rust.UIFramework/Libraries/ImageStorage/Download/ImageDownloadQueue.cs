using System;
using System.Collections.Concurrent;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

/// <summary>
/// Handles concurrent downloading of images
/// </summary>
internal class ImageDownloadQueue
{
    internal readonly ConcurrentQueue<ImageDownloadRequest> RequestQueue = [];
    private readonly ConcurrentDictionary<string, ImageDownloadRequest> _urlRequests = new();
    private readonly IUiLogger<ImageDownloadQueue> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<ImageDownloadQueue>();
    private readonly ImageDownloader _downloader;
    
    public ImageDownloadQueue()
    {
        _downloader = new ImageDownloader(this);
    }

    /// <summary>
    /// Adds a new download request to the queue if it doesn't already exist or hasn't failed too many times
    /// </summary>
    /// <param name="pluginId">The plugin ID associated with the request</param>
    /// <param name="name">The name for the downloaded image</param>
    /// <param name="url">The URL to download the image from</param>
    /// <param name="options"></param>
    /// <returns>True if the request was added, false if it already existed or failed too many times</returns>
    internal RegisterImageRequest AddRequest(PluginId pluginId, string name, string url, RegisterImageOptions options)
    {
        if (!pluginId.IsValid) throw new ArgumentNullException(nameof(pluginId));
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));
        
        ImageDownloadRequest state = _urlRequests.GetOrAdd(url, u => new ImageDownloadRequest(u));
        RegisterImageRequest request = new(pluginId, name, state, options);
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

    internal RegisterImageRequest AddStoredImageRequest(PluginId pluginId, string name, string url, ImageId imageId, RegisterImageOptions options)
    {
        if (!pluginId.IsValid) throw new ArgumentNullException(nameof(pluginId));
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));
        
        ImageDownloadRequest state = _urlRequests.GetOrAdd(url, u => new ImageDownloadRequest(u));
        if (!state.IsCompleted)
        {
            state.OnImageStored(imageId);
        }
        
        RegisterImageRequest request = new(pluginId, name, state, options);
        request.ExecuteOnDownloadCompleted();
        return request;
    }

    internal bool IsDownloading(string url) => _urlRequests.TryGetValue(url, out ImageDownloadRequest state) && state.IsDownloading;
    internal void OnServerShutdown() => _downloader.OnServerShutdown();
}