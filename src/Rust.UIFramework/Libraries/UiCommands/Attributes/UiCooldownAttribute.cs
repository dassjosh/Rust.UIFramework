using System;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

/// <summary>
/// Applies a cooldown to the command specifying the number of seconds between each call
/// </summary>
/// <param name="cooldown">Number of seconds between each call</param>
[AttributeUsage(AttributeTargets.Method)]
public class UiCooldownAttribute(float cooldown, string errorMessage = null) : Attribute
{
    public readonly float Cooldown = cooldown;
    public readonly string ErrorMessage = errorMessage;
}