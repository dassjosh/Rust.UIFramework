namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

/// <summary>
/// Mode of required permissions
/// </summary>
public enum PermissionMode
{
    /// <summary>
    /// All permissions are required to execute the command
    /// </summary>
    RequireAll,
    
    /// <summary>
    /// At least one permission is required to execute the command
    /// </summary>
    RequireAny
}