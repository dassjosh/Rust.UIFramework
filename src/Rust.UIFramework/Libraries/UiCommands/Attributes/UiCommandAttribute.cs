using System;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

[AttributeUsage(AttributeTargets.Method)]
public class UiCommandAttribute(ProtectionType protectionType = ProtectionType.Simple, string permission = null, float cooldown = 0) : Attribute
{
    public readonly ProtectionType ProtectionType = protectionType;
    public readonly string Permission = permission;
    public readonly float Cooldown = cooldown;
}