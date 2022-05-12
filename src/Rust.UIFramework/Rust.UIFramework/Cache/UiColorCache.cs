using System.Collections.Generic;
using System.Text;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Color = UnityEngine.Color;

namespace Oxide.Ext.UiFramework.Cache
{
    public static class UiColorCache
    {
        private const string Format = "0.####";
        private const char Space = ' ';

        private static readonly Dictionary<uint, string> ColorCache = new Dictionary<uint, string>();
        
        public static void WriteColor(JsonBinaryWriter writer, UiColor uiColor)
        {
            string color;
            if (!ColorCache.TryGetValue(uiColor.Value, out color))
            {
                color = GetColor(uiColor);
                ColorCache[uiColor.Value] = color;
            }

            writer.Write(color);
        }
        
        public static string GetColor(Color color)
        {
            StringBuilder builder = UiFrameworkPool.GetStringBuilder();
            builder.Append(color.r.ToString(Format));
            builder.Append(Space);
            builder.Append(color.g.ToString(Format));
            builder.Append(Space);
            builder.Append(color.b.ToString(Format));
            if (color.a != 1f)
            {
                builder.Append(Space);
                builder.Append(color.a.ToString(Format));
            }
            return UiFrameworkPool.ToStringAndFreeStringBuilder(ref builder);
        }
        
        public static void OnUnload()
        {
            ColorCache.Clear();
        }
    }
}