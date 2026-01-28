using System.Net;
using Cysharp.Threading.Tasks;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public sealed class ImageDownloadRequest(string url)
{
    public readonly string Url = url;
    public int Attempts { get; private set; }
    public DownloadState State { get; private set; }

    public byte[] Image { get; private set; }
    internal ImageId ImageId { get; private set; }

    public HttpStatusCode StatusCode { get; private set; }
    public string Message { get; private set; }
    public RegisterImageErrorCode ErrorCode { get; private set; }
    
    internal readonly ConcurrentList<RegisterImageRequest> URLRequests = [];

    public bool IsDownloading => State is DownloadState.InProgress or DownloadState.Queued;
    public bool IsCompleted => State is DownloadState.Completed or DownloadState.Stored;
    public bool HadDownloadError => State == DownloadState.Failed || Attempts > 0;
    public bool IsOutOfAttempts => Attempts >= UiFrameworkConfig.Instance.ImageStorage.MaxDownloadAttempts;
    internal PluginId GetFirstPluginId() => URLRequests.Count != 0 ? URLRequests[0].PluginId : default;
    internal void AddRequest(RegisterImageRequest request) => URLRequests.TryAdd(request);
    public void OnDownloadQueued() => State = DownloadState.Queued;
    public void OnDownloadStarted() => State = DownloadState.InProgress;

    public void OnDownloadFailed(HttpStatusCode code, string message)
    {
        Attempts += 1;
        State = DownloadState.Failed;
        StatusCode = code;
        Message = message;
        foreach (RegisterImageRequest request in URLRequests.GetPooledEnumerator(UiFrameworkPlugin.Instance))
        {
            request.ExecuteOnDownloadFailed();
        }
        Singleton<ImageDownloadAnimationHandler>.Instance.OnDownloadCompleted(Url, false, default);
        URLRequests.Clear();
    }

    public async UniTask OnDownloadComplete(byte[] image)
    {
        State = DownloadState.Completed;
        Image = image;
        StatusCode = HttpStatusCode.OK;
        Message = null;
        await Singleton<UiImageStorage>.Instance.OnDownloadCompleted(this);
    }

    internal void OnImageStored(ImageId imageId)
    {
        ImageId = imageId;
        State = DownloadState.Stored;
        URLRequests.Clear();
    }

    internal void OnInvalidImage(RegisterImageErrorCode code)
    {
        ErrorCode = code;
        foreach (RegisterImageRequest request in URLRequests.GetPooledEnumerator(UiFrameworkPlugin.Instance))
        {
            request.ExecuteOnInvalidImage();
        }
        Singleton<ImageDownloadAnimationHandler>.Instance.OnDownloadCompleted(Url, false, default);
        URLRequests.Clear();
    }
}