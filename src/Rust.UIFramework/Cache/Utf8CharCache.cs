using System.Collections.Generic;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Cache;

internal static class Utf8CharCache
{
    private static readonly Dictionary<char, Utf8String> Cache = new();

    public static Utf8String ToUtf8String(char value)
    {
        if (!Cache.TryGetValue(value, out Utf8String text))
        {
            Cache[value] = text = value.ToString();
        }

        return text;
    }
}