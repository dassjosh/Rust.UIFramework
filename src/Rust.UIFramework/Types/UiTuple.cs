using System;
using System.Runtime.CompilerServices;

namespace Oxide.Ext.UiFramework.Types;

public readonly struct UiTuple<T0, T1>(T0 item1, T1 item2)
{
    public readonly T0 Item1 = item1;
    public readonly T1 Item2 = item2;

    /// <summary>
    /// Implicitly converts from the <see cref="UiTuple{T0, T1}"/> to <see cref="T0"/>.
    /// </summary>
    /// <param name="tuple">The tuple to convert</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator T0(UiTuple<T0, T1> tuple) => tuple.Item1;

    /// <summary>
    /// Implicitly converts from the <see cref="UiTuple{T0, T1}"/> to <see cref="T1"/>.
    /// </summary>
    /// <param name="tuple">The tuple to convert</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator T1(UiTuple<T0, T1> tuple) => tuple.Item2;
    
    /// <summary>
    /// Implicitly converts from a pair of values to a <see cref="UiTuple{T0, T1}"/>.
    /// </summary>
    /// <param name="tuple">The tuple to convert</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator UiTuple<T0, T1>((T0, T1) tuple) => new(tuple.Item1, tuple.Item2);
    
    /// <summary>
    /// Implicitly converts from a <see cref="UiTuple{T0, T1}"/> to a standard <see cref="ValueTuple{T0, T1}"/>.
    /// </summary>
    /// <param name="tuple">The <see cref="UiTuple{T0, T1}"/> to convert</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator (T0, T1)(UiTuple<T0, T1> tuple) => (tuple.Item1, tuple.Item2);
    
    /// <summary>
    /// Deconstructs the <see cref="UiTuple{T0, T1}"/> into its individual items.
    /// </summary>
    /// <param name="item1">The first item of the tuple</param>
    /// <param name="item2">The second item of the tuple</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out T0 item1, out T1 item2)
    {
        item1 = Item1;
        item2 = Item2;
    }
}

public static class UiTuple
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UiTuple<T0, T1> Create<T0, T1>(T0 item1, T1 item2) => new(item1, item2);
}