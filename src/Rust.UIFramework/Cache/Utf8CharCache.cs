using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Oxide.Ext.UiFramework.Cache;

internal static class Utf8CharCache
{
    private static readonly Dictionary<char, byte[]> Cache = new();

    public static byte[] ToUtf8String(char value)
    {
        if (!Cache.TryGetValue(value, out byte[] text))
        {
            text = Encoding.UTF8.GetBytes(value.ToString());
            Cache[value] = text;
        }

        return text;
    }
}