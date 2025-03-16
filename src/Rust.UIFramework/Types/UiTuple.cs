namespace Oxide.Ext.UiFramework.Types;

public readonly struct UiTuple<T0, T1>(T0 item1, T1 item2)
{
    public readonly T0 Item1 = item1;
    public readonly T1 Item2 = item2;

    /// <summary>
    /// Implicitly converts from the ValueTuple to the first item's type.
    /// </summary>
    /// <param name="tuple">The tuple to convert</param>
    public static implicit operator T0(UiTuple<T0, T1> tuple) => tuple.Item1;

    /// <summary>
    /// Implicitly converts from the ValueTuple to the second item's type.
    /// </summary>
    /// <param name="tuple">The tuple to convert</param>
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

public readonly struct UiTuple<T0, T1, T2>(T0 item1, T1 item2, T2 item3)
{
    public readonly T0 Item1 = item1;
    public readonly T1 Item2 = item2;
    public readonly T2 Item3 = item3;

    /// <summary>
    /// Implicitly converts from the ValueTuple to the first item's type.
    /// </summary>
    /// <param name="tuple">The tuple to convert</param>
    public static implicit operator T0(UiTuple<T0, T1, T2> tuple) => tuple.Item1;

    /// <summary>
    /// Implicitly converts from the ValueTuple to the second item's type.
    /// </summary>
    /// <param name="tuple">The tuple to convert</param>
    public static implicit operator T1(UiTuple<T0, T1, T2> tuple) => tuple.Item2;

    /// <summary>
    /// Implicitly converts from the ValueTuple to the third item's type.
    /// </summary>
    /// <param name="tuple">The tuple to convert</param>
    public static implicit operator T2(UiTuple<T0, T1, T2> tuple) => tuple.Item3;
    
    /// <summary>
    /// Implicitly converts from a pair of values to a ValueTuple.
    /// </summary>
    /// <param name="tuple">The tuple to convert</param>
    public static implicit operator UiTuple<T0, T1, T2>((T0, T1, T2) tuple) => new(tuple.Item1, tuple.Item2, tuple.Item3);
    
    /// <summary>
    /// Implicitly converts from a ValueTuple to a standard tuple.
    /// </summary>
    /// <param name="tuple">The ValueTuple to convert</param>
    public static implicit operator (T0, T1, T2)(UiTuple<T0, T1, T2> tuple) => (tuple.Item1, tuple.Item2, tuple.Item3);
    
    /// <summary>
    /// Deconstructs the ValueTuple into its individual items.
    /// </summary>
    /// <param name="item1">The first item of the tuple</param>
    /// <param name="item2">The second item of the tuple</param>
    /// <param name="item3">The third item of the tuple</param>
    public void Deconstruct(out T0 item1, out T1 item2, out T2 item3)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
    }
}