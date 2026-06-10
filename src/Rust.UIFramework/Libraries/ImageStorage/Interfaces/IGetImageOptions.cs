namespace Oxide.Ext.UiFramework.Libraries;

public interface IGetImageOptions
{
    /// <summary>
    /// Image to use if the requested images fails to load.
    /// Can be an image name, url, or image id.
    /// </summary>
    string FallbackImage { get; set; }
}