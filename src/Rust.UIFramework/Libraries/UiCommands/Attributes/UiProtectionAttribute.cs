using System;

namespace Oxide.Ext.UiFramework.Libraries;

/// <summary>
/// Protection for the command
/// </summary>
/// <param name="protection">Protection type for the command</param>
/// <param name="protectionKeyLifetime">For Advanced and Extreme protection modes. The lifetime the built command is valid for in seconds.</param>
/// <param name="multiUse">For Advanced and Extreme protection modes. Can the command be used multiple times.</param>
[AttributeUsage(AttributeTargets.Method)]
public class UiProtectionAttribute(ProtectionType protection = ProtectionType.Simple, float protectionKeyLifetime = 30 * 60, bool multiUse = false) : Attribute
{
    public readonly ProtectionType Protection = protection;
    public readonly float ProtectionKeyLifetime = protectionKeyLifetime;
    public readonly bool MultiUse = multiUse;
}