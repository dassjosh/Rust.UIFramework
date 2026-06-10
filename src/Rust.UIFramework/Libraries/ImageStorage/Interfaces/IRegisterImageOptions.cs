namespace Oxide.Ext.UiFramework.Libraries;

public interface IRegisterImageOptions
{
    /// <summary>
    /// Enable client-side precaching of the image.
    /// This will send the image to the client after they have connected to the server
    /// </summary>
    bool EnableClientPrecache { get; init; }
}