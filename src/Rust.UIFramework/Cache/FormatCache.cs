using System;
using System.Collections.Concurrent;
using System.Globalization;

namespace Oxide.Ext.UiFramework.Cache;

public class FormatCache<T> where T : IFormattable
{
    private static readonly ConcurrentDictionary<FormatKey, string> Cache = new();
    
    public static string ToString(T value, string format)
    {
        FormatKey key = new(format, value);
        if (!Cache.TryGetValue(key, out string text))
        {
            Cache[key] = text = value.ToString(format, NumberFormatInfo.CurrentInfo);
        }

        return text;
    }
    
    private readonly record struct FormatKey(string Format, T Value);
}