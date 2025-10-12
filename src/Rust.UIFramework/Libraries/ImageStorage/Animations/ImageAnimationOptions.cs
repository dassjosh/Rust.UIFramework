using System;

namespace Oxide.Ext.UiFramework.Libraries;

public class ImageAnimationOptions
{
    public string DownloadingImageNameOrUrl;
    public string TimeoutImageNameOrUrl;
    public string FailedImageNameOrUrl;
    public TimeSpan Timeout = TimeSpan.FromSeconds(5);
}