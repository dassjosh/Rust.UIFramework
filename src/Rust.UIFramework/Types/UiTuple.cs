using System.Runtime.CompilerServices;

namespace Oxide.Ext.UiFramework.Types;

public readonly struct UiTuple<T0, T1>(T0 item1, T1 item2)
{
    public readonly T0 Item1 = item1;
    public readonly T1 Item2 = item2;

    /// <summary>
    /// Implicitly converts from the ValueTuple to the first item's type.
    /// </summary>
    /// <param name="tuple">The tuple to convert</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator T0(UiTuple<T0, T1> tuple) => tuple.Item1;

    /// <summary>
    /// Implicitly converts from the ValueTuple to the second item's type.
    /// </summary>
    /// <param name="tuple">The tuple to convert</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator T1(UiTuple<T0, T1> tuple) => tuple.Item2;
    
    /// <summary>
    /// Implicitly converts from a pair of values to a ValueTuple.
    /// </summary>
    /// <param name="tuple">The tuple to convert</param>
    public static implicit operator UiTuple<T0, T1>((T0, T1) tuple) => new(tuple.Item1, tuple.Item2);
    
    /// <summary>
    /// Implicitly converts from a ValueTuple to a standard tuple.
    /// </summary>
    /// <param name="tuple">The ValueTuple to convert</param>
    public static implicit operator (T0, T1)(UiTuple<T0, T1> tuple) => (tuple.Item1, tuple.Item2);
    
    /// <summary>
    /// Deconstructs the ValueTuple into its individual items.
    /// </summary>
    /// <param name="item1">The first item of the tuple</param>
    /// <param name="item2">The second item of the tuple</param>
    public void Deconstruct(out T0 item1, out T1 item2)
    {
        item1 = Item1;
        item2 = Item2;
    }
}