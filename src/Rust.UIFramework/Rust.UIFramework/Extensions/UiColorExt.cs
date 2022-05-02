using System.Text;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Plugins;
using Color = UnityEngine.Color;

namespace Oxide.Ext.UiFramework.Extensions
{
    public static class UiColorExt
    {
        private const string Format = "0.####";
        private const string RGBFormat = "{0} ";
        private const string AFormat = "{0}";

        private static readonly Hash<uint, string> ColorCache = new Hash<uint, string>();
        
        public static void WriteColor(StringBuilder writer, UiColor uiColor)
        {
            string color = ColorCache[uiColor.Value];
            if (color == null)
            {
                color = GetColor(uiColor);
                ColorCache[uiColor.Value] = color;
            }

            writer.Append(color);
        }
        
        public static string GetColor(Color color)
        {
            StringBuilder builder = UiFrameworkPool.GetStringBuilder();
            builder.AppendFormat(RGBFormat, color.r.ToString(Format));
            builder.AppendFormat(RGBFormat, color.g.ToString(Format));
            builder.AppendFormat(RGBFormat, color.b.ToString(Format));
            builder.AppendFormat(AFormat, color.a.ToString(Format));
            return UiFrameworkPool.ToStringAndFreeStringBuilder(ref builder);
        }
    }
}