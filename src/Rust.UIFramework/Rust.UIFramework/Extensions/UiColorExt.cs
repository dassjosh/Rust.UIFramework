using System.Globalization;
using Oxide.Ext.UiFramework.Colors;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Extensions
{
    //Define:ExtensionMethods
    public static class UiColorExt
    {
        public static UiColor WithAlpha(this UiColor color, byte alpha)
        {
            return new UiColor(color._red, color._green, color._blue, alpha);
        }
        
        public static UiColor WithAlpha(this UiColor color, string hex)
        {
            return color.WithAlpha(byte.Parse(hex, NumberStyles.HexNumber));
        }

        public static UiColor WithAlpha(this UiColor color, int alpha)
        {
            return color.WithAlpha((byte)alpha);
        }

        public static UiColor WithAlpha(this UiColor color, float alpha)
        {
            return color.WithAlpha((byte)Mathf.Clamp(alpha * 255f, 0, byte.MaxValue));
        }
        
        public static UiColor MultiplyAlpha(this UiColor color, float alpha)
        {
            return color.WithAlpha((byte)Mathf.Clamp(color._alpha * alpha, 0, byte.MaxValue));
        }
        
        public static UiColor ToGrayScale(this UiColor color)
        {
            float scale = ((Color)color).grayscale;
            return new UiColor(new Color(scale, scale, scale));
        }

        public static UiColor Darken(this UiColor color, float percentage)
        {
            percentage = Mathf.Clamp01(percentage);
            byte red = (byte)Mathf.Clamp(color._red * (1 - percentage), 0, byte.MaxValue);
            byte green = (byte)Mathf.Clamp(color._green * (1 - percentage), 0, byte.MaxValue);
            byte blue = (byte)Mathf.Clamp(color._blue * (1 - percentage), 0, byte.MaxValue);

            return new UiColor(red, green, blue, color._alpha);
        }

        public static UiColor Lighten(this UiColor color, float percentage)
        {
            percentage = Mathf.Clamp01(percentage);
            float red = (byte)Mathf.Clamp(byte.MaxValue - color._red * percentage + color._red, 0, byte.MaxValue);
            float green = (byte)Mathf.Clamp(byte.MaxValue - color._green * percentage + color._red, 0, byte.MaxValue);
            float blue = (byte)Mathf.Clamp(byte.MaxValue - color._blue * percentage + color._red, 0, byte.MaxValue);

            return new UiColor(red, green, blue, color._alpha);
        }
        
        public static UiColor Lerp(this UiColor start, UiColor end, float value)
        {
            return new UiColor(start._red + (end._red - start._red) * value, start._green + (end._green - start._green) * value, start._blue + (end._blue - start._blue) * value, start._alpha + (end._alpha - start._alpha) * value);
        }
    }
}