using Oxide.Ext.UiFramework.Config;

namespace Oxide.Ext.UiFramework.Libraries;

internal class DownloadImageState : IDownloadImageState
{
    public DownloadState State { get; private set; }
    public int Attempts { get; private set; }
    public bool IsDownloading => State is DownloadState.InProgress or DownloadState.Queued;
    public bool IsCompleted => State is DownloadState.Completed;
    public bool HadDownloadError => State is DownloadState.Failed || Attempts > 0;
    public bool IsOutOfAttempts => Attempts >= UiFrameworkConfig.Instance.ImageStorage.MaxDownloadAttempts;

    public void OnDownloadQueued() => State = DownloadState.Queued;
    public void OnDownloadStarted() => State = DownloadState.InProgress;
    public void OnDownloadFailed()
    {
        State = DownloadState.Failed;
        Attempts++;
    }

    public void OnDownloadCompleted() => State = DownloadState.Completed;

}