using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Oxide.Ext.UiFramework.Cache;

internal static class Utf8StringCache
{
    private static readonly Dictionary<string, byte[]> Cache = new();

    public static byte[] ToString(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return [];
        }
        
        if (!Cache.TryGetValue(value, out byte[] text))
        {
            text = Encoding.UTF8.GetBytes(value);
            Cache[value] = text;
        }

        return text;
    }
}