using System.Collections.Generic;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Cache;

internal static class Utf8StringCache
{
    private static readonly Dictionary<string, Utf8String> Cache = new();

    public static Utf8String ToString(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return default;
        }
        
        if (!Cache.TryGetValue(value, out Utf8String text))
        {
            Cache[value] = text = value;
        }

        return text;
    }
}