using System.Net;

namespace Oxide.Ext.UiFramework.Libraries;

internal interface IDownloadImageRequestHandler : IRegisterImageRequestHandler
{
    string Url { get; }
    byte[] DownloadedImage { get; }
    ImageId DownloadedImageId { get; }
    IDownloadImageState State { get; }

    void OnDownloadStarted();
    void OnDownloadFailed(HttpStatusCode code, string message);
    void OnDownloadComplete(byte[] image);
}