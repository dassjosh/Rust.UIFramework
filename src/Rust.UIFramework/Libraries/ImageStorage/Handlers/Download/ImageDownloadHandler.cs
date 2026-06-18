using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using Cysharp.Threading.Tasks;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Threading;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class ImageDownloadHandler : ISingleton, IUiChannelAsyncProcess<DownloadImageRequestHandler>, IUiChannelProcessResult<DownloadImageRequestHandler>, IUiChannelException<DownloadImageRequestHandler>
{
    private readonly HttpClient _httpClient;
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly IUiLogger<ImageDownloadHandler> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<ImageDownloadHandler>();
    private readonly UiChannel<DownloadImageRequestHandler> _channel;

    private ImageDownloadHandler()
    {
        _channel = Singleton<UiChannels>.Instance.Create(UiFrameworkPlugin.Instance, new UiChannelOptions(ThreadingHelper.ImageDownloadMultiThreaded, UiFrameworkConfig.Instance.ImageStorage.MaxConcurrentDownloads), this);

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
                Address = new Uri(proxyConfig.Url)
            };
            handler.Proxy = proxy;
        }

        _httpClient = new HttpClient(handler)
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        _httpClient.DefaultRequestHeaders.Add("user-agent", $"Rust UiFramework (https://github.com/dassjosh/Rust.UIFramework, v{UiFrameworkExtension.Instance.Version})");
    }

    public void Enqueue(DownloadImageRequestHandler request)
    {
        request.SetStep(ProcessStep.Download);
        _channel.Enqueue(request);
        _logger.Debug("Enqueued Request: ID: {0} Plugin: {1} URL: {2} Name: {3}", request.Id, request.PluginCreator, request.Url, request.Requests.FirstOrDefault()?.Name ?? "No Requests");
    }

    public async UniTask<ProcessResult> Process(DownloadImageRequestHandler request)
    {
        _logger.Debug("Processing Request: ID: {0} Plugin: {1} URL: {2}", request.Id, request.PluginCreator, request.Url);
        if (request.State.IsCompleted)
        {
            _logger.Debug("Skipping ID: {0} URL: {1} as it has already been downloaded", request.Id, request.Url);
            return ProcessResult.Success;
        }

        if (request.State.IsOutOfAttempts)
        {
            _logger.Debug("Skipping ID: {0} URL: {1} as it is greater than max attempts {2}", request.Id, request.Url, UiFrameworkConfig.Instance.ImageStorage.MaxDownloadAttempts);
            return ProcessResult.Failed;
        }

        CancellationToken cancellationToken = _cancellationTokenSource.Token;

        try
        {
            return await DownloadImageAsync(request, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            // Expected when cancellation is requested
            if(cancellationToken.IsCancellationRequested)
            {
                _logger.Verbose("Worker task shutting down due to cancellation.");
                return ProcessResult.Failed;
            }

            // Failed because of http timeout. Requeue the request
            _logger.Debug("A timeout occured during download for request: ID: {0} Plugin: {1} URL: {2}", request.Id, request.PluginCreator, request.Url);
            request.OnDownloadFailed(HttpStatusCode.RequestTimeout, "Download timed out");
        }
        catch (Exception ex)
        {
            _logger.Exception("An unhandled exception occurred in the download worker. ID: {0} Plugin: {1} URL: {2}", request.Id, request.PluginCreator, request.Url, ex);
        }

        return ProcessResult.Failed;
    }

    /// <summary>
    /// Downloads an image from the specified URL
    /// </summary>
    /// <param name="request">The download request</param>
    /// <param name="cancellationToken">Token to monitor for cancellation</param>
    /// <returns>True if the download was successful, otherwise false</returns>
    private async UniTask<ProcessResult> DownloadImageAsync(DownloadImageRequestHandler request, CancellationToken cancellationToken)
    {
        try
        {
            // Download the image
            request.State.OnDownloadStarted();
            _logger.Debug("Download Request: ID: {0} Plugin: {1} URL: {2}", request.Id, request.PluginCreator, request.Url);
            using HttpResponseMessage response = await _httpClient.GetAsync(request.Url, cancellationToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                byte[] data = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                request.OnDownloadComplete(data);
                _logger.Debug("Download Request Completed Successfully. ID: {0} Plugin: {1} Url: {2} Attempts: {3}", request.Id, request.PluginCreator, request.Url, request.State.Attempts);
                return ProcessResult.Success;
            }

            string message = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            _logger.Error("Download Request Failed. ID: {0} Plugin: {1} Url: {2}. Attempt: {3} Status Code: {4}. Message:\n{5}",
                request.Id, request.PluginCreator, request.Url, request.State.Attempts, response.StatusCode, message);
            request.OnDownloadFailed(response.StatusCode, message);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.Exception("An error occured downloading image. ID: {0} Plugin: {1} Url: {2} Attempt: {3}", request.Id, request.PluginCreator, request.Url, request.State.Attempts, ex);
        }
        
        return ProcessResult.Failed;
    }

    public void OnSuccess(DownloadImageRequestHandler request)
    {
        if (!request.Redirect())
        {
            Singleton<DefaultImageProcessor>.Instance.Enqueue(request);
        }
    }

    public void OnFailed(DownloadImageRequestHandler request)
    {
        if (!request.State.IsOutOfAttempts && !_cancellationTokenSource.IsCancellationRequested && request.Step == ProcessStep.Download)
        {
            Enqueue(request);
        }
    }

    public void OnServerShutdown()
    {
        _cancellationTokenSource.Cancel();
    }

    public void OnException(DownloadImageRequestHandler request, Exception ex)
    {
        request.Failed(new RegisterException(ex));
        _logger.Exception("Process Request Failed: ID: {0}", request.Id, ex);
    }
}