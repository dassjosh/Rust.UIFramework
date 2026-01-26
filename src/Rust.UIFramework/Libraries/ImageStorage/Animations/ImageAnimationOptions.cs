using System;

namespace Oxide.Ext.UiFramework.Libraries;

public class ImageAnimationOptions : IImageAnimationOptions
{
    public string DownloadingImageNameOrUrl { get; set; }
    public string TimeoutImageNameOrUrl { get; set; }
    public string FailedImageNameOrUrl { get; set; }
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(10);
}