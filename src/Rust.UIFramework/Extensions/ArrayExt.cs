namespace Oxide.Ext.UiFramework.Extensions;

public static class ArrayExt
{
    public static void Clear<T>(this T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = default;
        }
    }
}