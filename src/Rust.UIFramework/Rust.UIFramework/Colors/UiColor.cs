using System;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Exceptions;
using UnityEngine;
using Color = UnityEngine.Color;

namespace Oxide.Ext.UiFramework.Colors
{
    [JsonConverter(typeof(UiColorConverter))]
    public struct UiColor : IEquatable<UiColor>
    {
        #region Fields
        public readonly uint Value;
        public readonly Color Color;
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
        public UiColor(Color color)
        {
            Color = color;
            Value = ((uint)(color.r * 255) << 24) | ((uint)(color.g * 255) << 16) | ((uint)(color.b * 255) << 8) | (uint)(color.a * 255);
        }
        
        public UiColor(int red, int green, int blue, int alpha = 255) : this(red / 255f, green / 255f, blue / 255f, alpha / 255f)
        {

        }
        
        public UiColor(byte red, byte green, byte blue, byte alpha = 255) : this(red / 255f, green / 255f, blue / 255f, alpha / 255f)
        {

        }

        public UiColor(float red, float green, float blue, float alpha = 1f) : this(new Color(Mathf.Clamp01(red), Mathf.Clamp01(green), Mathf.Clamp01(blue), Mathf.Clamp01(alpha)))
        {
            
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

        public override string ToString()
        {
            return $"{Color.r} {Color.g} {Color.b} {Color.a}";
        }
        #endregion

        #region Formats
        public string ToHexRGB()
        {
            return ColorUtility.ToHtmlStringRGB(Color);
        }
        
        public string ToHexRGBA()
        {
            return ColorUtility.ToHtmlStringRGBA(Color);
        }
        
        public string ToHtmlColor()
        {
            return $"#{ColorUtility.ToHtmlStringRGBA(Color)}";
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
#if UiBenchmarks
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