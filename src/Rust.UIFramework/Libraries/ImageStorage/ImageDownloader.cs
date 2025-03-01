using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal readonly record struct DownloadRequest(PluginId PluginId, string Name, string Url);

/// <summary>
/// Handles concurrent downloading of images
/// </summary>
internal class ImageDownloader
{
    private readonly HttpClient _httpClient;
    private readonly ConcurrentQueue<DownloadRequest> _requestQueue = new();
    private readonly ConcurrentDictionary<string, DownloadState> _urlState = new();
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly int _maxConcurrentDownloads = UiFrameworkConfig.Instance.ImageStorage.MaxConcurrentDownloads;
    private readonly int _maxDownloadAttempts = UiFrameworkConfig.Instance.ImageStorage.MaxDownloadAttempts;
    private readonly object _taskLock = new();
    private int _activeWorkerCount;
    private readonly ILogger<ImageDownloader> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<ImageDownloader>();

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
    internal bool AddRequest(PluginId pluginId, string name, string url)
    {
        if (!pluginId.IsValid) throw new ArgumentNullException(nameof(pluginId));
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

        DownloadRequest request = new(pluginId, name, url);
        if (_urlState.TryGetValue(url, out DownloadState state))
        {
            if (state.InProgress || state.Attempts > _maxDownloadAttempts)
            {
                return false;
            }
        }
        else
        {
            _urlState.TryAdd(url, new DownloadState(1, true));
        }

        _requestQueue.Enqueue(request);
                
        // Start workers if needed
        EnsureWorkersRunning();
                
        return true;
    }

    internal void BulkAddRequests(PluginId pluginId, Dictionary<string, string> requests)
    {
        foreach ((string name, string url) in requests)
        {
            AddRequest(pluginId, name, url);
        }
    }

    /// <summary>
    /// Ensures that worker tasks are running if needed
    /// </summary>
    private void EnsureWorkersRunning()
    {
        if (_cancellationTokenSource.IsCancellationRequested) return;
        
        lock (_taskLock)
        {
            // Only start new workers if we're below the maximum and queue has items
            if (_activeWorkerCount >= _maxConcurrentDownloads || _requestQueue.IsEmpty)
            {
                return;
            }
            
            // Calculate how many new workers we need
            int workersToStart = Math.Min(_maxConcurrentDownloads - _activeWorkerCount, _requestQueue.Count);
            for (int i = 0; i < workersToStart; i++)
            {
#pragma warning disable EPC13
                Task.Factory.StartNew(
                    () => ProcessDownloadQueue(_cancellationTokenSource.Token),
                    _cancellationTokenSource.Token,
                    TaskCreationOptions.LongRunning,
                    TaskScheduler.Default);
#pragma warning restore EPC13
                    
                Interlocked.Increment(ref _activeWorkerCount);
                _logger.Debug("Started new worker task. Active workers: {0}", _activeWorkerCount);
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
            while (!cancellationToken.IsCancellationRequested)
            {
                // Try to dequeue a request
                if (!_requestQueue.TryDequeue(out DownloadRequest request))
                {
                    Interlocked.Decrement(ref _activeWorkerCount);
                    _logger.Debug("Worker task shutting down due to empty queue. Active workers: {0}", _activeWorkerCount);
                    return;
                }
                
                await DownloadImageAsync(request, cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            // Expected when cancellation is requested
        }
        catch (Exception ex)
        {
            _logger.Exception("An error occurred in the download worker", ex);
        }
        finally
        {
            Interlocked.Decrement(ref _activeWorkerCount);
        }
    }

    /// <summary>
    /// Downloads an image from the specified URL
    /// </summary>
    /// <param name="request">The download request</param>
    /// <param name="cancellationToken">Token to monitor for cancellation</param>
    /// <returns>True if download was successful, otherwise false</returns>
    private async ValueTask<bool> DownloadImageAsync(DownloadRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // Download the image
            using HttpResponseMessage response = await _httpClient.GetAsync(request.Url, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                // Successfully downloaded - remove from registry
                _urlState.TryRemove(request.Url, out _);
                byte[] data = await response.Content.ReadAsByteArrayAsync();
                Singleton<UiImageStorage>.Instance.OnImageDownloaded(request, data);
                return true;
            }
            
            _logger.Error($"Failed to download image. Url: {request.Url}. Attempt: {_urlState[request.Url].Attempts} Status Code: {response.StatusCode}. Message: {await response.Content.ReadAsStringAsync()}");
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.Exception($"An error occured downloading image. Url: {request.Url} Attempt: {_urlState[request.Url].Attempts}", ex);
        }
        finally
        {
            if (_urlState.TryGetValue(request.Url, out DownloadState state))
            {
                _urlState[request.Url] = new DownloadState(state.Attempts + 1, false);
            }
        }

        return false;
    }
    
    internal void OnServerShutdown()
    {
        _cancellationTokenSource.Cancel();
    }
    
    private readonly record struct DownloadState(int Attempts, bool InProgress);
}