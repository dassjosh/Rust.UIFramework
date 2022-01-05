using System.Text.RegularExpressions;
using UnityEngine;

namespace UI.Framework.Rust.Colors
{
    public class UiColor
    {
        public string Color { get; private set; }
        public int Value { get; private set; }

        /// <summary>
        /// Checks for format "0.0 0.0 0.0 0.0"
        /// Any permutation of normal rust color string will work
        /// </summary>
        private static readonly Regex RustColorFormat = new Regex("\\d*.?\\d* \\d*.?\\d* \\d*.?\\d* \\d*.?\\d*", RegexOptions.Compiled | RegexOptions.ECMAScript);

        /// <summary>
        /// Valid Hex Color Formats
        /// #RGB
        /// #RRGGBB
        /// #RGBA
        /// #RRGGBBAA
        /// </summary>
        /// <param name="color"></param>
        public UiColor(string color)
        {
            Color colorValue;
            if (RustColorFormat.IsMatch(color))
            {
                colorValue = ColorEx.Parse(color);
            }
            else
            {
                if (!color.StartsWith("#"))
                {
                    color = "#" + color;
                }

                ColorUtility.TryParseHtmlString(color, out colorValue);
            }

            SetValue(colorValue);
        }

        public UiColor(Color color)
        {
            SetValue(color);
        }

        public UiColor(string hexColor, int alpha = 255)
        {
            if (!hexColor.StartsWith("#"))
            {
                hexColor = "#" + hexColor;
            }

            alpha = Mathf.Clamp(alpha, 0, 255);
            Color colorValue;
            ColorUtility.TryParseHtmlString(hexColor, out colorValue);
            colorValue.a = alpha / 255f;
            SetValue(colorValue);
        }

        public UiColor(string hexColor, float alpha = 1f)
        {
            if (!hexColor.StartsWith("#"))
            {
                hexColor = "#" + hexColor;
            }

            alpha = Mathf.Clamp01(alpha);
            Color colorValue;
            ColorUtility.TryParseHtmlString(hexColor, out colorValue);
            colorValue.a = alpha;
            SetValue(colorValue);
        }

        public UiColor(int red, int green, int blue, int alpha = 255)
        {
            red = Mathf.Clamp(red, 0, 255);
            green = Mathf.Clamp(green, 0, 255);
            blue = Mathf.Clamp(blue, 0, 255);
            alpha = Mathf.Clamp(alpha, 0, 255);

            SetValue(red / 255f, green / 255f, blue / 255f, alpha / 255f);
        }

        public UiColor(float red, float green, float blue, float alpha = 1f)
        {
            red = Mathf.Clamp01(red);
            green = Mathf.Clamp01(green);
            blue = Mathf.Clamp01(blue);
            alpha = Mathf.Clamp01(alpha);

            SetValue(red, green, blue, alpha);
        }

        public UiColor WithAlpha(string hex)
        {
            return WithAlpha(int.Parse(hex, System.Globalization.NumberStyles.HexNumber));
        }

        public UiColor WithAlpha(int alpha)
        {
            return WithAlpha(Mathf.Clamp(alpha, 0, 255) / 255f);
        }

        public UiColor WithAlpha(float alpha)
        {
            Color color = ColorEx.Parse(Color);
            color.a = Mathf.Clamp01(alpha);
            return new UiColor(color);
        }

        private void SetValue(Color color)
        {
            SetValue(color.r, color.g, color.b, color.a);
        }

        private void SetValue(float red, float green, float blue, float alpha)
        {
            Color = $"{red:0.###} {green:0.###} {blue:0.###} {alpha:0.###}";
            Value = ((int)(red * 255) << 24) + ((int)(green * 255) << 16) + ((int)(blue * 255) << 8) + (int)(alpha * 255);
        }

        public static implicit operator UiColor(string value) => new UiColor(value);
    }
}