using System;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

/// <summary>
/// Specifies the required permissions to execute the command
/// </summary>
/// <param name="permissions">Array of required permissions</param>
/// <param name="mode">Mode of required permissions</param>
[AttributeUsage(AttributeTargets.Method)]
public class UiPermissionAttribute(string[] permissions, PermissionMode mode = PermissionMode.RequireAll, string errorMessage = null) : Attribute
{
    public readonly string[] Permissions = permissions;
    public readonly PermissionMode Mode = mode;
    public readonly string ErrorMessage = errorMessage;
}