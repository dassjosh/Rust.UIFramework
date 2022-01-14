using System.Text.RegularExpressions;
using UnityEngine;
using Color = UnityEngine.Color;

namespace UI.Framework.Rust.Colors
{
    public class UiColor
    {
        public string Color;
        public int Value;
        public float Red;
        public float Green;
        public float Blue;
        public float Alpha;

        /// <summary>
        /// Checks for format "0.0 0.0 0.0 0.0"
        /// Any permutation of normal rust color string will work
        /// </summary>
        private static readonly Regex _rustColorFormat = new Regex("\\d*.?\\d* \\d*.?\\d* \\d*.?\\d* \\d*.?\\d*", RegexOptions.Compiled | RegexOptions.ECMAScript);

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
            if (_rustColorFormat.IsMatch(color))
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

        public UiColor(string hexColor, int alpha = 255) : this(hexColor, alpha / 255f)
        {

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

        public UiColor(int red, int green, int blue, int alpha = 255) : this(red / 255f, green / 255f, blue / 255f, alpha / 255f)
        {

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
            return WithAlpha(alpha / 255f);
        }

        public UiColor WithAlpha(float alpha)
        {
            return new UiColor(Red, Green, Blue, Mathf.Clamp01(alpha));
        }

        private void SetValue(Color color)
        {
            SetValue(color.r, color.g, color.b, color.a);
        }

        private void SetValue(float red, float green, float blue, float alpha)
        {
            Color = $"{red:0.###} {green:0.###} {blue:0.###} {alpha:0.###}";
            Value = ((int)(red * 255) << 24) + ((int)(green * 255) << 16) + ((int)(blue * 255) << 8) + (int)(alpha * 255);
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        public static implicit operator UiColor(string value) => new UiColor(value);
        
        public static UiColor Lerp(UiColor start, UiColor end, float value)
        {
            value = Mathf.Clamp01(value);
            return new UiColor(start.Red + (end.Red - start.Red) * value, start.Green + (end.Green - start.Green) * value, start.Blue + (end.Blue - start.Blue) * value, start.Alpha + (end.Alpha - start.Alpha) * value);
        }
    }
}