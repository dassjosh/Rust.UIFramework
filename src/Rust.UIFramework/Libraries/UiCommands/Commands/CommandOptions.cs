namespace Oxide.Ext.UiFramework.Libraries;

public class CommandOptions
{
    internal static readonly CommandOptions Default = new();
    
    public OnPlayerNoPermission OnPlayerNoPermission { get; set; }
    public OnPlayerCooldown OnPlayerCooldown { get; set; }
    public OnPlayerProtectionFailed OnPlayerProtectionFailed { get; set; }
}