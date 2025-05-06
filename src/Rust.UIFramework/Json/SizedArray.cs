namespace Oxide.Ext.UiFramework.Json;

public readonly struct SizedArray<T>
{
    public readonly T[] Array;
    public readonly int Size;

    public SizedArray(T[] array, int size)
    {
        Array = array;
        Size = size;
    }
}