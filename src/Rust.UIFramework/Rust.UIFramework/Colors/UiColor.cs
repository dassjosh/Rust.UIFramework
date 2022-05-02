using System;
using Oxide.Ext.UiFramework.Exceptions;
using UnityEngine;
using Color = UnityEngine.Color;

namespace Oxide.Ext.UiFramework.Colors
{
    public struct UiColor : IEquatable<UiColor>
    {
        #region Fields
        public readonly uint Value;
        public readonly Color Color;
        #endregion

        #region Constructors
        public UiColor(Color color)
        {
            Color = color;
            Value = ((uint)(color.r * 255) << 24) + ((uint)(color.g * 255) << 16) + ((uint)(color.b * 255) << 8) + (uint)(color.a * 255);
        }
        
        public UiColor(int red, int green, int blue, int alpha = 255) : this(red / 255f, green / 255f, blue / 255f, alpha / 255f)
        {

        }

        public UiColor(float red, float green, float blue, float alpha = 1f) : this(new Color(Mathf.Clamp01(red), Mathf.Clamp01(green), Mathf.Clamp01(blue), Mathf.Clamp01(alpha)))
        {
            
        }
        #endregion
        
        #region Modifiers
        public static UiColor WithAlpha(UiColor color, string hex)
        {
            return WithAlpha(color, int.Parse(hex, System.Globalization.NumberStyles.HexNumber));
        }

        public static UiColor WithAlpha(UiColor color, int alpha)
        {
            return WithAlpha(color, alpha / 255f);
        }

        public static UiColor WithAlpha(UiColor color, float alpha)
        {
            return new UiColor(color.Color.WithAlpha(Mathf.Clamp01(alpha)));
        }

        public static UiColor Darken(UiColor color, float percentage)
        {
            percentage = Mathf.Clamp01(percentage);
            Color col = color.Color;
            float red = col.r * (1 - percentage);
            float green = col.g * (1 - percentage);
            float blue = col.b * (1 - percentage);

            return new UiColor(red, green, blue, col.a);
        }

        public static UiColor Lighten(UiColor color, float percentage)
        {
            percentage = Mathf.Clamp01(percentage);
            Color col = color.Color;
            float red = (1 - col.r) * percentage + col.r;
            float green = (1 - col.g) * percentage + col.g;
            float blue = (1 - col.b) * percentage + col.b;

            return new UiColor(red, green, blue, col.a);
        }
        #endregion

        #region Operators
        public static implicit operator UiColor(string value) => ParseHexColor(value);
        public static implicit operator UiColor(Color value) => new UiColor(value);
        public static implicit operator Color(UiColor value) => value.Color;
        public static bool operator ==(UiColor lhs, UiColor rhs) => lhs.Value == rhs.Value;
        public static bool operator !=(UiColor lhs, UiColor rhs) => !(lhs == rhs);
        
        public bool Equals(UiColor other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is UiColor && Equals((UiColor)obj);
        }
        
        public override int GetHashCode()
        {
            return (int)Value;
        }
        #endregion

        #region Parsing
        /// <summary>
        /// Valid Rust Color Formats
        /// 0 0 0
        /// 0.0 0.0 0.0 0.0
        /// 1.0 1.0 1.0
        /// 1.0 1.0 1.0 1.0
        /// </summary>
        /// <param name="color"></param>
        public static UiColor ParseRustColor(string color)
        {
            return new UiColor(ColorEx.Parse(color));
        }
        
        /// <summary>
        /// Valid Hex Color Formats
        /// #RGB
        /// #RRGGBB
        /// #RGBA
        /// #RRGGBBAA
        /// </summary>
        /// <param name="hexColor"></param>
        /// <returns></returns>
        /// <exception cref="UiFrameworkException"></exception>
        public static UiColor ParseHexColor(string hexColor)
        {
            if (hexColor[0] != '#')
            {
                throw new UiFrameworkException($"Invalid Hex Color: '{hexColor}' Color Must Start With '#'");
            }
            
#if UiBenchmarks
            Color colorValue = Color.black;
#else 
            Color colorValue;
            ColorUtility.TryParseHtmlString(hexColor, out colorValue);
#endif
            
            return new UiColor(colorValue);
        }
        #endregion
    }
}