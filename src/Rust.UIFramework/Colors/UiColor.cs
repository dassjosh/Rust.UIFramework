using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Exceptions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Colors;

[JsonConverter(typeof(UiColorConverter))]
public readonly struct UiColor : IEquatable<UiColor>
{
    #region Fields
    private readonly byte _red;
    private readonly byte _green;
    private readonly byte _blue;
    private readonly byte _alpha;
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
    public UiColor()
    {
        
    }
    
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
    public static implicit operator UiColor(Color value) => new(value);
    public static implicit operator Color(UiColor value) => new(ToFloat(value._red), ToFloat(value._green), ToFloat(value._blue), ToFloat(value._alpha));
    public static bool operator ==(UiColor lhs, UiColor rhs) => lhs._red == rhs._red && lhs._green == rhs._green && lhs._blue == rhs._blue && lhs._alpha == rhs._alpha;
    public static bool operator !=(UiColor lhs, UiColor rhs) => !(lhs == rhs);
    public static UiColor operator *(UiColor color, UiColor multiplier)
    {
        return new UiColor(ToFloat(color._red) * ToFloat(multiplier._red), ToFloat(color._green) * ToFloat(multiplier._green), ToFloat(color._blue) * ToFloat(multiplier._blue), ToFloat(color._alpha) * ToFloat(multiplier._alpha));
    }

    private static float ToFloat(byte value) => value / 255f;
        
    public bool Equals(UiColor other) => this == other;

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        return obj is UiColor color && Equals(color);
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

    #region Modifiers
    [Pure]
    public UiColor WithAlpha(byte alpha)
    {
        return new UiColor(_red, _green, _blue, alpha);
    }
        
    [Pure]
    public UiColor WithAlpha(string hex)
    {
        return WithAlpha(byte.Parse(hex, NumberStyles.HexNumber));
    }

    [Pure]
    public UiColor WithAlpha(int alpha)
    {
        return WithAlpha((byte)alpha);
    }

    [Pure]
    public UiColor WithAlpha(float alpha)
    {
        return WithAlpha((byte)Mathf.Clamp(alpha * 255f, 0, byte.MaxValue));
    }
        
    [Pure]
    public UiColor MultiplyAlpha(float alpha)
    {
        return WithAlpha((byte)Mathf.Clamp(_alpha * alpha, 0, byte.MaxValue));
    }
        
    [Pure]
    public UiColor ToGrayScale()
    {
        float scale = ((Color)this).grayscale;
        return new UiColor(new Color(scale, scale, scale));
    }

    [Pure]
    public UiColor Darken(float percentage)
    {
        percentage = Mathf.Clamp01(percentage);
        byte red = (byte)Mathf.Clamp(_red * (1 - percentage), 0, byte.MaxValue);
        byte green = (byte)Mathf.Clamp(_green * (1 - percentage), 0, byte.MaxValue);
        byte blue = (byte)Mathf.Clamp(_blue * (1 - percentage), 0, byte.MaxValue);

        return new UiColor(red, green, blue, _alpha);
    }

    [Pure]
    public UiColor Lighten(float percentage)
    {
        percentage = Mathf.Clamp01(percentage);
        float red = (byte)Mathf.Clamp(byte.MaxValue - _red * percentage + _red, 0, byte.MaxValue);
        float green = (byte)Mathf.Clamp(byte.MaxValue - _green * percentage + _red, 0, byte.MaxValue);
        float blue = (byte)Mathf.Clamp(byte.MaxValue - _blue * percentage + _red, 0, byte.MaxValue);

        return new UiColor(red, green, blue, _alpha);
    }
        
    [Pure]
    public static UiColor Lerp(UiColor start, UiColor end, float value)
    {
        return new UiColor(start._red + (end._red - start._red) * value, start._green + (end._green - start._green) * value, start._blue + (end._blue - start._blue) * value, start._alpha + (end._alpha - start._alpha) * value);
    }
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
    public static UiColor ParseRustColor(string color) => new(ColorEx.Parse(color));

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
        ReadOnlySpan<char> span = hexColor.AsSpan();
        if (span[0] == '#')
        {
            span = span[1..];
        }
        
        byte red = byte.Parse(span[..2], NumberStyles.HexNumber);
        byte green = byte.Parse(span[2..4], NumberStyles.HexNumber);
        byte blue = byte.Parse(span[4..6], NumberStyles.HexNumber);
        byte alpha = 255;
        if (span.Length == 8)
        {
            alpha = byte.Parse(span[6..8], NumberStyles.HexNumber);
        }
        return new UiColor(red, green, blue, alpha);
    }
    #endregion
}