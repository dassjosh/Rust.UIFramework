namespace Oxide.Ext.UiFramework.Libraries;

public interface IDownloadImageState
{
    DownloadState State { get; }
    int Attempts { get; }
    bool IsDownloading { get; }
    bool IsCompleted { get; }
    bool HadDownloadError { get; }
    bool IsOutOfAttempts { get; }
    void OnDownloadQueued();
    void OnDownloadStarted();
    void OnDownloadFailed();
    void OnDownloadCompleted();
}