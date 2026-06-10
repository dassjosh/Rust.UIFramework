using System;
using System.Runtime.CompilerServices;

namespace Oxide.Ext.UiFramework.Guards;

public static partial class Guard
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsNullOrEmpty<T>(T[] array, [CallerArgumentExpression(nameof(array))] string name = null)
    {
        if (array != null && array.Length != 0) throw new ArgumentException(Message($"'{name}' should be null or empty"), name);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsNotNullOrEmpty<T>(T[] array, [CallerArgumentExpression(nameof(array))] string name = null)
    {
        if (array == null || array.Length == 0) throw new ArgumentException(Message($"'{name}' should not be null or empty"), name);
    }
}