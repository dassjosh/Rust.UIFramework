namespace Oxide.Ext.UiFramework.Libraries;

public interface IDownloadImageRequest : IRegisterImageRequest
{
    string Url { get; }
    IDownloadImageState State { get; }
}