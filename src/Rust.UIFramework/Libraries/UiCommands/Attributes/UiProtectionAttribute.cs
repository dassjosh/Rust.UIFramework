using System;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

/// <summary>
/// Protection for the command
/// </summary>
/// <param name="protection">Protection type for the command</param>
/// <param name="protectionKeyLifetime">For Advanced and Extreme protection modes. The lifetime the built command is valid for in seconds.</param>
[AttributeUsage(AttributeTargets.Method)]
public class UiProtectionAttribute(ProtectionType protection = ProtectionType.Simple, float protectionKeyLifetime = 30 * 60) : Attribute
{
    public readonly ProtectionType Protection = protection;
    public readonly float ProtectionKeyLifetime = protectionKeyLifetime;
}