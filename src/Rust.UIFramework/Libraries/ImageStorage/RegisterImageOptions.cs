namespace Oxide.Ext.UiFramework.Libraries;

/// <summary>
/// Options for registering an image.
/// </summary>
public class RegisterImageOptions : IRegisterImageOptions
{
    ///<inheritdoc/>
    public bool EnableClientPrecache { get; init; }
    
    public static readonly RegisterImageOptions Default = new();
}