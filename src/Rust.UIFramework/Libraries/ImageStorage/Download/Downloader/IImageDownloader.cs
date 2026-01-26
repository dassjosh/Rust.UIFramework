namespace Oxide.Ext.UiFramework.Libraries;

internal interface IImageDownloader
{
    void OnInit(ImageDownloadQueue queue);
    void OnDownloadQueued();
    void OnServerShutdown();
}