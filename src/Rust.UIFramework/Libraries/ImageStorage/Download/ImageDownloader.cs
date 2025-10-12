using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
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
    private readonly HttpClient _httpClient;
    private readonly ConcurrentQueue<UrlDownloadState> _requestQueue = [];
    private readonly ConcurrentDictionary<string, UrlDownloadState> _urlRequests = new();
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly object _taskLock = new();
    private int _activeWorkerCount;
    private readonly IUiLogger<ImageDownloader> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<ImageDownloader>();

    /// <summary>
    /// Initializes a new instance of the ImageDownloader class
    /// </summary>
    public ImageDownloader()
    {
        HttpClientHandler handler = new()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            UseCookies = false
        };
        
        UiProxyConfig proxyConfig = UiFrameworkConfig.Instance.Proxy;
        if (proxyConfig.EnableProxy)
        {
            WebProxy proxy = new()
            {
                Credentials = new NetworkCredential(proxyConfig.Username, proxyConfig.Password),
                Address = new Uri(proxyConfig.Url),
            };
            handler.Proxy = proxy;
        }
        
        _httpClient = new HttpClient(handler)
        {
            Timeout = TimeSpan.FromSeconds(30)
        };
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
        _requestQueue.Enqueue(state);
        EnsureWorkersRunning();
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

    /// <summary>
    /// Ensures that worker tasks are running if needed
    /// </summary>
    private void EnsureWorkersRunning()
    {
        if (_cancellationTokenSource.IsCancellationRequested) return;
        
        lock (_taskLock)
        {
            // Only start new workers if we're below the maximum and queue has items
            if (_activeWorkerCount >= UiFrameworkConfig.Instance.ImageStorage.MaxConcurrentDownloads || _requestQueue.IsEmpty)
            {
                return;
            }
            
            // Calculate how many new workers we need
            int workersToStart = Math.Min(UiFrameworkConfig.Instance.ImageStorage.MaxConcurrentDownloads - _activeWorkerCount, _requestQueue.Count);
            for (int i = 0; i < workersToStart; i++)
            {
#pragma warning disable EPC13
                Task.Factory.StartNew(
                    () => ProcessDownloadQueue(_cancellationTokenSource.Token),
                    _cancellationTokenSource.Token,
                    TaskCreationOptions.None,
                    TaskScheduler.Default);
#pragma warning restore EPC13
                    
                Interlocked.Increment(ref _activeWorkerCount);
                _logger.Debug("Started new worker task. Active workers: {0}/{1}", _activeWorkerCount, UiFrameworkConfig.Instance.ImageStorage.MaxConcurrentDownloads);
            }
        }
    }

    /// <summary>
    /// Processes the download queue, handling requests as they come in
    /// </summary>
    /// <param name="cancellationToken">Token to monitor for cancellation</param>
    private async Task ProcessDownloadQueue(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested && _requestQueue.TryDequeue(out UrlDownloadState request))
            {
                if (request.IsCompleted)
                {
                    _logger.Debug("Skipping url: {0} as it has already been downloaded", request.Url);
                    continue;
                }
                
                if (request.IsOutOfAttempts)
                {
                    _logger.Debug("Skipping url: {0} as it is greater than max attempts {1}", request.Url, UiFrameworkConfig.Instance.ImageStorage.MaxDownloadAttempts);
                    continue;
                }
                
                try
                {
                    await DownloadImageAsync(request, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    // Expected when cancellation is requested
                    if(cancellationToken.IsCancellationRequested)
                    {
                        _logger.Debug("Worker task shutting down due to cancellation. Active workers: {0}", _activeWorkerCount);
                        return;
                    }

                    // Failed because of http timeout. Requeue the request
                    _requestQueue.Enqueue(request);
                    _logger.Debug("A timeout occured during download for request: {0}", request.Url);
                }
                catch (Exception ex)
                {
                    _logger.Exception("An unhandled exception occurred in the download worker", ex);
                }
            }
        }
        finally
        {
            _logger.Debug("Worker task shutting down due to empty queue. Active workers: {0}", _activeWorkerCount);
            Interlocked.Decrement(ref _activeWorkerCount);
        }
    }

    /// <summary>
    /// Downloads an image from the specified URL
    /// </summary>
    /// <param name="state">The download request</param>
    /// <param name="cancellationToken">Token to monitor for cancellation</param>
    /// <returns>True if download was successful, otherwise false</returns>
    private async ValueTask<bool> DownloadImageAsync(UrlDownloadState state, CancellationToken cancellationToken)
    {
        try
        {
            // Download the image
            state.OnDownloadStarted();
            using HttpResponseMessage response = await _httpClient.GetAsync(state.Url, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                byte[] data = await response.Content.ReadAsByteArrayAsync();
                state.OnDownloadComplete(data);
                return true;
            }
            
            _logger.Error($"Failed to download image. Plugin: {state.GetFirstPluginId()} Url: {state.Url}. Attempt: {state.Attempts} Status Code: {response.StatusCode}. Message: {await response.Content.ReadAsStringAsync()}");
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.Exception($"An error occured downloading image. Plugin: {state.GetFirstPluginId()} Url: {state.Url} Attempt: {state.Attempts}", ex);
        }

        state.OnDownloadFailed();
        return false;
    }
    
    internal void OnServerShutdown()
    {
        _cancellationTokenSource.Cancel();
    }
}