using System.Collections.Concurrent;
using System.Text;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Cache
{
    public static class UiColorCache
    {
        private const string Format = "0.####";
        private const char Space = ' ';

        private static readonly ConcurrentDictionary<int, string> ColorCache = new();
        
        public static void WriteColor(JsonBinaryWriter writer, UiColor uiColor)
        {
            int hashCode = uiColor.GetHashCode();
            if (!ColorCache.TryGetValue(hashCode, out string color))
            {
                color = GetColor(uiColor);
                ColorCache[hashCode] = color;
            }

            writer.Write(color);
        }

        private static string GetColor(Color color)
        {
            StringBuilder builder = UiFrameworkPool.GetStringBuilder();
            builder.Append(StringCache<float>.ToString(color.r, Format));
            builder.Append(Space);
            builder.Append(StringCache<float>.ToString(color.g, Format));
            builder.Append(Space);
            builder.Append(StringCache<float>.ToString(color.b, Format));
            if (color.a < 1f)
            {
                builder.Append(Space);
                builder.Append(StringCache<float>.ToString(color.a, Format));
            }

            return builder.ToStringAndFree();
        }
    }
}