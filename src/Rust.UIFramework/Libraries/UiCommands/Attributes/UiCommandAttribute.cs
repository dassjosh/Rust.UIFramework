using System;

namespace Oxide.Ext.UiFramework.Libraries;

[AttributeUsage(AttributeTargets.Method)]
public class UiCommandAttribute(bool runOnCooldown = false, bool runOnNoPermission = false) : Attribute
{
    public readonly bool RunOnCooldown = runOnCooldown;
    public readonly bool RunOnNoPermission = runOnNoPermission;
}