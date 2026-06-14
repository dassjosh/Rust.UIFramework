using System;
using System.Runtime.CompilerServices;
using Oxide.Core;

namespace Oxide.Ext.UiFramework.Guards;

public static partial class Guard
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsGreaterThanZero(int value, [CallerArgumentExpression(nameof(value))] string name = null)
    {
        if(value <= 0) throw new ArgumentOutOfRangeException(name, value, "Value must be greater than zero.");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsGreaterThanZero(float value, [CallerArgumentExpression(nameof(value))] string name = null)
    {
        if(value <= 0) throw new ArgumentOutOfRangeException(name, value, "Value must be greater than zero.");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsGreaterThanOrEqualToZero(int value, [CallerArgumentExpression(nameof(value))] string name = null)
    {
        if(value < 0) throw new ArgumentOutOfRangeException(name, value, "Value must be greater than or equal to zero.");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsGreaterThanOrEqualToZero(float value, [CallerArgumentExpression(nameof(value))] string name = null)
    {
        if(value < 0) throw new ArgumentOutOfRangeException(name, value, "Value must be greater than or equal to zero.");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsValidPlayerId(ulong value, [CallerArgumentExpression(nameof(value))] string name = null)
    {
        if(!value.IsSteamId()) throw new ArgumentOutOfRangeException(name, value, "Value is not a valid steam ID.");
    }
}