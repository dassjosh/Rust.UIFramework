using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Oxide.Ext.UiFramework.Guards;

public static partial class Guard
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsNull<T>(T value, [CallerArgumentExpression(nameof(value))] string name = null) where T : class
    {
        if (value is not null) throw new ArgumentException(Message($"'{name}' should be null"), name);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsNull<T>(T? value, [CallerArgumentExpression(nameof(value))] string name = null) where T : struct
    {
        if (value is not null) throw new ArgumentException(Message($"'{name}' should be null"), name);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsNotNull<T>([NotNull] T value, [CallerArgumentExpression(nameof(value))] string name = null) where T : class
    {
        if (value is null) throw new ArgumentException(Message($"'{name}' should not be null"), name);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsNotNull<T>([NotNull] T? value, [CallerArgumentExpression(nameof(value))] string name = null) where T : struct
    {
        if (value is null) throw new ArgumentException(Message($"'{name}' should not be null"), name);
    }
}