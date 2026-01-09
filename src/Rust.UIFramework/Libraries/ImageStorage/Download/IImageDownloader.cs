namespace Oxide.Ext.UiFramework.Libraries;

internal interface IImageDownloader
{
    void OnInit(ImageDownloader downloader);
    void OnDownloadQueued();
    void OnServerShutdown();
}