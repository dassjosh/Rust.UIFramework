using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Oxide.Ext.UiFramework.Cache;

internal static class Utf8StringCache<T> where T : IFormattable
{
    private static readonly Dictionary<T, byte[]> Cache = new();
    private static readonly Dictionary<string, Dictionary<T, byte[]>> FormatCache = new();

    public static byte[] ToString(T value)
    {
        if (!Cache.TryGetValue(value, out byte[] text))
        {
            text = Encoding.UTF8.GetBytes(value.ToString());
            Cache[value] = text;
        }

        return text;
    }
        
    public static byte[] ToString(T value, string format)
    {
        if (!FormatCache.TryGetValue(format, out Dictionary<T, byte[]> values))
        {
            values = new Dictionary<T, byte[]>();
            FormatCache[format] = values;
        }

        if (!values.TryGetValue(value, out byte[] text))
        {
            text = Encoding.UTF8.GetBytes(value.ToString(format, NumberFormatInfo.CurrentInfo));
            values[value] = text;
        }

        return text;
    }
}