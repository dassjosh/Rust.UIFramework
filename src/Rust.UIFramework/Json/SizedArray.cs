namespace Oxide.Ext.UiFramework.Json;

public readonly struct SizedArray<T>(T[] array, int size)
{
    public readonly T[] Array = array;
    public readonly int Size = size;
}