using System;
using System.Collections.Generic;
using System.Globalization;

namespace Oxide.Ext.UiFramework.Cache
{
    public static class StringCache<T> where T : IFormattable
    {
        private static readonly Dictionary<T, string> Cache = new();
        private static readonly Dictionary<string, Dictionary<T, string>>  FormatCache = new();

        public static string ToString(T value)
        {
            if (!Cache.TryGetValue(value, out string text))
            {
                text = value.ToString();
                Cache[value] = text;
            }

            return text;
        }
        
        public static string ToString(T value, string format)
        {
            if (!FormatCache.TryGetValue(format, out Dictionary<T, string> values))
            {
                values = new Dictionary<T, string>();
                FormatCache[format] = values;
            }

            if (!values.TryGetValue(value, out string text))
            {
                text = value.ToString(format, NumberFormatInfo.CurrentInfo);
                values[value] = text;
            }

            return text;
        }
    }
}