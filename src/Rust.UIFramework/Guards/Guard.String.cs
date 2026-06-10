using System;
using System.Runtime.CompilerServices;

namespace Oxide.Ext.UiFramework.Guards;

public static partial class Guard
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsNullOrEmpty(string text, [CallerArgumentExpression(nameof(text))] string name = null)
    {
        if (!string.IsNullOrEmpty(text)) throw new ArgumentException(Message($"'{name}' should be null or empty but got '{text}'"), name);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsNotNullOrEmpty(string text, [CallerArgumentExpression(nameof(text))] string name = null)
    {
        if (string.IsNullOrEmpty(text)) throw new ArgumentException(Message($"{name} should not be null or empty"), name);
    }
}