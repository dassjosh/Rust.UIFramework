using System.Collections;
using System.Net;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;
using UnityEngine.Networking;

namespace Oxide.Ext.UiFramework.Libraries;

internal class BehaviorImageDownloader : FacepunchBehaviour, IImageDownloader
{
    private ImageDownloadQueue _queue;
    private readonly IUiLogger<BehaviorImageDownloader> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<BehaviorImageDownloader>();
    private Coroutine _downloadRoutine;
    
    public void OnInit(ImageDownloadQueue queue)
    {
        _queue = queue;
    }

    /// <summary>
    /// Ensures that worker tasks are running if needed
    /// </summary>
    public void OnDownloadQueued()
    {
        enabled = true;
    }

    /// <summary>
    /// Processes the download queue, handling requests as they come in
    /// </summary>
    private void Update()
    {
        if(_downloadRoutine == null && _queue.RequestQueue.TryDequeue(out UrlDownloadState request))
        {
            if (request.IsCompleted)
            {
                _logger.Debug("Skipping url: {0} as it has already been downloaded", request.Url);
                return;
            }
                
            if (request.IsOutOfAttempts)
            {
                _logger.Debug("Skipping url: {0} as it is greater than max attempts {1}", request.Url, UiFrameworkConfig.Instance.ImageStorage.MaxDownloadAttempts);
                return;
            }

            _downloadRoutine = StartCoroutine(DownloadImageAsync(request));
        }
    }

    /// <summary>
    /// Downloads an image from the specified URL
    /// </summary>
    /// <param name="state">The download request</param>
    /// <returns>True if the download was successful, otherwise false</returns>
    private IEnumerator DownloadImageAsync(UrlDownloadState state)
    {
        try
        {
            // Download the image
            state.OnDownloadStarted();
            using UnityWebRequest request = UnityWebRequest.Get(state.Url);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                byte[] data = request.downloadHandler.data;
                state.OnDownloadComplete(data);
                _logger.Debug("Image Downloaded Successfully. Plugin: {0} Url: {1} Attempt: {2}", state.GetFirstPluginId(), state.Url, state.Attempts);
            }
            else
            {
                string message = request.error;
                _logger.Error($"Failed to download image. Plugin: {state.GetFirstPluginId()} Url: {state.Url}. Attempt: {state.Attempts} Status Code: {request.responseCode}. Message:\n{message}");
                state.OnDownloadFailed((HttpStatusCode)request.responseCode, message);
            }
        }
        finally
        {
            _downloadRoutine = null;
        }
    }
    
    public void OnServerShutdown() { }
}