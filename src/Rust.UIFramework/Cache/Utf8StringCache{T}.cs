using System.Collections.Generic;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Cache;

internal static class Utf8StringCache<T> where T : struct
{
    private static readonly Dictionary<T, Utf8String> Cache = new();

    public static Utf8String ToString(T value)
    {
        if (!Cache.TryGetValue(value, out Utf8String text))
        {
            Cache[value] = text = value.ToString();
        }

        return text;
    }
}