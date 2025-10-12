using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public sealed class UrlDownloadState(string url)
{
    public readonly string Url = url;
    public int Attempts { get; private set; }
    public DownloadState State { get; private set; }

    public byte[] Image { get; private set; }
    internal ImageId ImageId { get; private set; }
    
    private readonly ConcurrentList<DownloadImageRequest> _urlRequests = [];

    public bool IsDownloading => State is DownloadState.InProgress or DownloadState.Queued;
    public bool IsCompleted => State is DownloadState.Completed or DownloadState.Stored;
    public bool HadDownloadError => State == DownloadState.Failed || Attempts > 0;
    public bool IsOutOfAttempts => Attempts >= UiFrameworkConfig.Instance.ImageStorage.MaxDownloadAttempts;
    internal PluginId GetFirstPluginId() => _urlRequests.Count != 0 ? _urlRequests[0].PluginId : default;
    internal void AddRequest(DownloadImageRequest request) => _urlRequests.TryAdd(request);
    public void OnDownloadStarted() => State = DownloadState.InProgress;

    public void OnDownloadFailed()
    {
        Attempts += 1;
        State = DownloadState.Failed;
        foreach (DownloadImageRequest request in _urlRequests)
        {
            request.ExecuteOnDownloadFailed();
        }
        Singleton<ImageUpdateAnimations>.Instance.OnDownloadCompleted(Url, false, default);
        _urlRequests.Clear();
    }

    public void OnDownloadComplete(byte[] image)
    {
        State = DownloadState.Completed;
        Image = image;
    }

    internal void OnImageStored(ImageId imageId)
    {
        ImageId = imageId;
        State = DownloadState.Stored;
        foreach (DownloadImageRequest request in _urlRequests)
        {
            request.ExecuteOnDownloadCompleted(imageId.ToString());
        }
        _urlRequests.Clear();
    }
}