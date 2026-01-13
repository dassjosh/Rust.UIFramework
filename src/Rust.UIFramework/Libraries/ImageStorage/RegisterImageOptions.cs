namespace Oxide.Ext.UiFramework.Libraries;

public class RegisterImageOptions
{
    public bool EnableClientPrecache { get; init; }
    
    public static readonly RegisterImageOptions Default = new();
}