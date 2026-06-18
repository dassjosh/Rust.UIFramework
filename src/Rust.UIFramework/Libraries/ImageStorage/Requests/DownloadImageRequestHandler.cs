using System.Net;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class DownloadImageRequestHandler(PluginId id, string url) : RegisterImageRequestHandler(id), IDownloadImageRequestHandler
{
    public string Url { get; } = url;
    public byte[] DownloadedImage { get; private set; }
    public ImageId DownloadedImageId { get; private set;  }
    public IDownloadImageState State { get; } = new DownloadImageState();

    public DownloadImageRequestHandler(PluginId id, string url, ImageId imageId) : this(id, url)
    {
        ImageId = imageId;
    }

    public void OnDownloadStarted()
    {
        State.OnDownloadStarted();
    }

    public void SetDownloadImageId(ImageId id)
    {
        DownloadedImageId = id;
    }

    public override void Success(RegisterSuccessEventArgs args)
    {
        base.Success(args);
        Singleton<ImageDownloadAnimationHandler>.Instance.OnDownloadCompleted(Url, true, ImageId);
    }

    public override void Failed(BaseImageStorageException exception)
    {
        base.Failed(exception);
        Singleton<ImageDownloadAnimationHandler>.Instance.OnDownloadCompleted(Url, false, default);
    }

    public void OnDownloadFailed(HttpStatusCode code, string message)
    {
        State.OnDownloadFailed();
        if (State.IsOutOfAttempts || !IsHttpCodeRetryable(code))
        {
            SetStep(ProcessStep.Failed);
            Failed(new DownloadFailedException(code, message));
        }
    }

    public void OnDownloadComplete(byte[] image)
    {
        SetImage(image);
        DownloadedImage = image;
        State.OnDownloadCompleted();
    }

    public bool IsHttpCodeRetryable(HttpStatusCode code)
    {
        return code is < HttpStatusCode.BadRequest or >= HttpStatusCode.InternalServerError;
    }
}