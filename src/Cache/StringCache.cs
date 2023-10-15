using System;
using System.Collections.Generic;
using System.Globalization;

namespace Oxide.Ext.UiFramework.Cache
{
    public static class StringCache<T> where T : IFormattable
    {
        private static readonly Dictionary<T, string> Cache = new Dictionary<T, string>();
        private static readonly Dictionary<string, Dictionary<T, string>>  FormatCache = new Dictionary<string, Dictionary<T, string>>();

        public static string ToString(T value)
        {
            string text;
            if (!Cache.TryGetValue(value, out text))
            {
                text = value.ToString();
                Cache[value] = text;
            }

            return text;
        }
        
        public static string ToString(T value, string format)
        {
            Dictionary<T, string> values;
            if (!FormatCache.TryGetValue(format, out values))
            {
                values = new Dictionary<T, string>();
                FormatCache[format] = values;
            }

            string text;
            if (!values.TryGetValue(value, out text))
            {
                text = value.ToString(format, NumberFormatInfo.CurrentInfo);
                values[value] = text;
            }

            return text;
        }
    }
}