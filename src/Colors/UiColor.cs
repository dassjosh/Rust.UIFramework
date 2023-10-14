using System;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Exceptions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Colors
{
    [JsonConverter(typeof(UiColorConverter))]
    public struct UiColor : IEquatable<UiColor>
    {
        #region Fields
        internal readonly byte _red;
        internal readonly byte _green;
        internal readonly byte _blue;
        internal readonly byte _alpha;
        
        //public readonly uint Value;
        //public readonly Color Color;
        #endregion

        #region Static Colors
        public static readonly UiColor Black =  "#000000";
        public static readonly UiColor White = "#FFFFFF";
        public static readonly UiColor Silver =  "#C0C0C0";
        public static readonly UiColor Gray = "#808080";
        public static readonly UiColor Red = "#FF0000";
        public static readonly UiColor Maroon = "#800000";
        public static readonly UiColor Orange = "#FFA500";
        public static readonly UiColor Yellow = "#FFEB04";
        public static readonly UiColor Olive = "#808000";
        public static readonly UiColor Lime = "#00FF00";
        public static readonly UiColor Green = "#008000";
        public static readonly UiColor Teal = "#008080";
        public static readonly UiColor Cyan = "#00FFFF";
        public static readonly UiColor Blue = "#0000FF";
        public static readonly UiColor Navy = "#000080";
        public static readonly UiColor Magenta = "#FF00FF";
        public static readonly UiColor Purple = "#800080";
        public static readonly UiColor Clear = "#00000000";
        #endregion

        #region Constructors
        public UiColor(byte red, byte green, byte blue, byte alpha = 255)
        {
            _red = red;
            _green = green;
            _blue = blue;
            _alpha = alpha;
        }
        
        public UiColor(int red, int green, int blue, int alpha = 255) : this(
            (byte)Mathf.Clamp(red, 0, byte.MaxValue), 
            (byte)Mathf.Clamp(green, 0, byte.MaxValue), 
            (byte)Mathf.Clamp(blue, 0, byte.MaxValue), 
            (byte)Mathf.Clamp(alpha, 0, byte.MaxValue)) { }
        
        public UiColor(Color color) : this(color.r, color.g, color.b, color.a) { }
        
        public UiColor(float red, float green, float blue, float alpha = 1f) : this(Mathf.RoundToInt(red * 255f), Mathf.RoundToInt(green * 255f), Mathf.RoundToInt(blue * 255f), Mathf.RoundToInt(alpha * 255f)) {}
        #endregion

        #region Operators
        public static implicit operator UiColor(string value) => ParseHexColor(value);
        public static implicit operator UiColor(Color value) => new UiColor(value);
        public static implicit operator Color(UiColor value) => new Color(ToFloat(value._red), ToFloat(value._green), ToFloat(value._blue), ToFloat(value._alpha));
        public static bool operator ==(UiColor lhs, UiColor rhs) => lhs._red == rhs._red && lhs._green == rhs._green && lhs._blue == rhs._blue && lhs._alpha == rhs._alpha;
        public static bool operator !=(UiColor lhs, UiColor rhs) => !(lhs == rhs);

        private static float ToFloat(byte value) => value / 255f;
        
        public bool Equals(UiColor other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is UiColor && Equals((UiColor)obj);
        }
        
        public override int GetHashCode()
        {
            int red = _red << 24;
            int green = _green << 16;
            int blue = _blue << 8;
            return red | green | blue | _alpha;
        }

        public override string ToString() => $"{ToFloat(_red)} {ToFloat(_green)} {ToFloat(_blue)} {ToFloat(_alpha)}";
        #endregion

        #region Formats
        public string ToHexRGB() => ColorUtility.ToHtmlStringRGB(this);
        public string ToHexRGBA() => ColorUtility.ToHtmlStringRGBA(this);
        public string ToHtmlColor() => $"#{ColorUtility.ToHtmlStringRGBA(this)}";
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
        public static UiColor ParseRustColor(string color) => new UiColor(ColorEx.Parse(color));

        /// <summary>
        /// <a href="https://docs.unity3d.com/ScriptReference/ColorUtility.TryParseHtmlString.html">Unity ColorUtility.TryParseHtmlString API reference</a>
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
#if BENCHMARKS
            Color colorValue = Color.black;
#else 
            Color colorValue;
            if (!ColorUtility.TryParseHtmlString(hexColor, out colorValue))
            {
                throw new UiFrameworkException($"Invalid Color: '{hexColor}' Hex colors must start with a '#'");
            }
#endif
            
            return new UiColor(colorValue);
        }
        #endregion
    }
}