using System.Net;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class DownloadImageRequestHandler(PluginId id, string url) : RegisterImageRequestHandler(id), IDownloadImageRequestHandler
{
    public string Url { get; } = url;
    public IDownloadImageState State { get; } = new DownloadImageState();

    public DownloadImageRequestHandler(PluginId id, string url, ImageId imageId) : this(id, url)
    {
        ImageId = imageId;
    }

    public void OnDownloadStarted()
    {
        State.OnDownloadStarted();
    }

    public override void Success(RegisterSuccessEventArgs args)
    {
        base.Success(args);
        Singleton<ImageDownloadAnimationHandler>.Instance.OnDownloadCompleted(Url, true, ImageId);
    }

    public override void Failed(IRegisterImageFailureResult args)
    {
        base.Failed(args);
        Singleton<ImageDownloadAnimationHandler>.Instance.OnDownloadCompleted(Url, false, default);
    }

    public void OnDownloadFailed(HttpStatusCode code, string message)
    {
        State.OnDownloadFailed();
        if (State.IsOutOfAttempts)
        {
            SetStep(ProcessStep.Failed);
            Failed(new DownloadFailedEventArgs(code, message));
        }
    }

    public void OnDownloadComplete(byte[] image)
    {
        Image = image;
        State.OnDownloadCompleted();
    }
}