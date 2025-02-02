using System.Collections.Generic;
using System.Text;

namespace Oxide.Ext.UiFramework.Cache;

internal static class Utf8StringCache<T> where T : struct
{
    private static readonly Dictionary<T, byte[]> Cache = new();

    public static byte[] ToString(T value)
    {
        if (!Cache.TryGetValue(value, out byte[] text))
        {
            text = Encoding.UTF8.GetBytes(value.ToString());
            Cache[value] = text;
        }

        return text;
    }
}