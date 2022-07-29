using Oxide.Ext.UiFramework.Colors;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Extensions
{
    //Define:ExtensionMethods
    public static class UiColorExt
    {
        public static UiColor WithAlpha(this UiColor color, string hex)
        {
            return WithAlpha(color, int.Parse(hex, System.Globalization.NumberStyles.HexNumber));
        }

        public static UiColor WithAlpha(this UiColor color, int alpha)
        {
            return color.WithAlpha(alpha / 255f);
        }

        public static UiColor WithAlpha(this UiColor color, float alpha)
        {
            return new UiColor(color.Color.WithAlpha(Mathf.Clamp01(alpha)));
        }
        
        public static UiColor MultiplyAlpha(this UiColor color, float alpha)
        {
            return new UiColor(color.Color.WithAlpha(Mathf.Clamp01(color.Color.a * alpha)));
        }
        
        public static UiColor ToGrayScale(this UiColor color)
        {
            float scale = color.Color.grayscale;
            return new UiColor(new Color(scale, scale, scale));
        }

        public static UiColor Darken(this UiColor color, float percentage)
        {
            percentage = Mathf.Clamp01(percentage);
            Color col = color.Color;
            float red = col.r * (1 - percentage);
            float green = col.g * (1 - percentage);
            float blue = col.b * (1 - percentage);

            return new UiColor(red, green, blue, col.a);
        }

        public static UiColor Lighten(this UiColor color, float percentage)
        {
            percentage = Mathf.Clamp01(percentage);
            Color col = color.Color;
            float red = (1 - col.r) * percentage + col.r;
            float green = (1 - col.g) * percentage + col.g;
            float blue = (1 - col.b) * percentage + col.b;

            return new UiColor(red, green, blue, col.a);
        }
        
        public static UiColor Lerp(this UiColor start, UiColor end, float value)
        {
            return Color.Lerp(start, end, value);
        }
    }
}