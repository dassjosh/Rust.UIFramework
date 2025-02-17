using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Data;

internal class UrlImage(string url, ImageId imageId)
{
    public string Url { get; set; } = url;
    public ImageId ImageId { get; set; } = imageId;
}