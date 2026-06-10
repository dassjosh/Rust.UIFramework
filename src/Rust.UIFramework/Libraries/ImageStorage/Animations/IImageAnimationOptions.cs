using System;

namespace Oxide.Ext.UiFramework.Libraries;

public interface IImageAnimationOptions
{
    /// <summary>
    /// Image to use while the image is being downloaded
    /// Can be an image name, url, or image id.
    /// </summary>
    string DownloadingImage { get; set; }

    /// <summary>
    /// Image to use if the download fails to complete after a timeout
    /// Can be an image name, url, or image id.
    /// </summary>
    string TimeoutImage { get; set; }

    /// <summary>
    /// Image to use if the download fails for any reason other than timeout
    /// Can be an image name, url, or image id.
    /// </summary>
    string FailedImage { get; set; }

    /// <summary>
    /// The timeout for the download before using the timeout image
    /// </summary>
    TimeSpan Timeout { get; set; }
}