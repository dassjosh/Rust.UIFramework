using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class ThreadedImageDownloader : IImageDownloader
{
    private readonly HttpClient _httpClient;
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly object _taskLock = new();
    private int _activeWorkerCount;
    private readonly IUiLogger<ThreadedImageDownloader> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<ThreadedImageDownloader>();
    private ImageDownloadQueue _downloader;
    
    public ThreadedImageDownloader()
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
        
        _httpClient.DefaultRequestHeaders.Add("user-agent", $"Rust UiFramework (https://github.com/dassjosh/Rust.UIFramework, v{UiFrameworkExtension.Instance.Version})");
    }

    public void OnInit(ImageDownloadQueue queue)
    {
        _downloader = queue;
    }

    /// <summary>
    /// Ensures that worker tasks are running if needed
    /// </summary>
    public void OnDownloadQueued()
    {
        if (_cancellationTokenSource.IsCancellationRequested) return;
        
        lock (_taskLock)
        {
            // Only start new workers if we're below the maximum and queue has items
            if (_activeWorkerCount >= UiFrameworkConfig.Instance.ImageStorage.MaxConcurrentDownloads || _downloader.RequestQueue.IsEmpty)
            {
                return;
            }
            
            // Calculate how many new workers we need
            int workersToStart = Math.Min(UiFrameworkConfig.Instance.ImageStorage.MaxConcurrentDownloads - _activeWorkerCount, _downloader.RequestQueue.Count);
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
    private async ValueTask ProcessDownloadQueue(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested && _downloader.RequestQueue.TryDequeue(out ImageDownloadRequest request))
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
                    await DownloadImageAsync(request, cancellationToken).ConfigureAwait(false);
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
                    _downloader.RequestQueue.Enqueue(request);
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
    /// <returns>True if the download was successful, otherwise false</returns>
    private async ValueTask<bool> DownloadImageAsync(ImageDownloadRequest state, CancellationToken cancellationToken)
    {
        try
        {
            // Download the image
            state.OnDownloadStarted();
            using HttpResponseMessage response = await _httpClient.GetAsync(state.Url, cancellationToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                byte[] data = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                state.OnDownloadComplete(data);
                _logger.Debug("Image Downloaded Successfully. Plugin: {0} Url: {1} Attempt: {2}", state.GetFirstPluginId(), state.Url, state.Attempts);
                return true;
            }

            string message = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            _logger.Error($"Failed to download image. Plugin: {state.GetFirstPluginId()} Url: {state.Url}. Attempt: {state.Attempts} Status Code: {response.StatusCode}. Message:\n{message}");
            state.OnDownloadFailed(response.StatusCode, message);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.Exception($"An error occured downloading image. Plugin: {state.GetFirstPluginId()} Url: {state.Url} Attempt: {state.Attempts}", ex);
        }
        
        return false;
    }
    
    public void OnServerShutdown()
    {
        _cancellationTokenSource.Cancel();
    }
}