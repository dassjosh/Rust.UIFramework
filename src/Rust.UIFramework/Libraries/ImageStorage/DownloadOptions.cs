using System;

namespace Oxide.Ext.UiFramework.Libraries;

public class ImageDownloadOptions
{
    internal static readonly ImageDownloadOptions Default = new();
    
    public string FailedImageNameOrUrl;
    public ImageAutomaticUpdateOptions AutomaticUpdate;
}

public class ImageAutomaticUpdateOptions
{
    public string DownloadingImageNameOrUrl;
    public string TimeoutImageNameOrUrl;
    public bool EnableAutoImageUpdate;
    public TimeSpan Timeout = TimeSpan.FromSeconds(5);
}