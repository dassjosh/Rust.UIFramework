using System;

namespace Oxide.Ext.UiFramework.Libraries;

public class ImageAnimationOptions : IImageAnimationOptions
{
    ///<inheritdoc/>
    public string DownloadingImage { get; set; }

    ///<inheritdoc/>
    public string TimeoutImage { get; set; }

    ///<inheritdoc/>
    public string FailedImage { get; set; }

    ///<inheritdoc/>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(10);

    [Obsolete($"Please use {nameof(DownloadingImage)} instead.")]
    public string DownloadingImageNameOrUrl
    {
        get => DownloadingImage;
        set => DownloadingImage = value;
    }

    [Obsolete($"Please use {nameof(TimeoutImage)} instead.")]
    public string TimeoutImageNameOrUrl
    {
        get => TimeoutImage;
        set => TimeoutImage = value;
    }

    [Obsolete($"Please use {nameof(FailedImage)} instead.")]
    public string FailedImageNameOrUrl
    {
        get => FailedImage;
        set => FailedImage = value;
    }
}