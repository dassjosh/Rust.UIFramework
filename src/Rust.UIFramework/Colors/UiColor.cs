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
    public readonly byte Red;
    public readonly byte Green;
    public readonly byte Blue;
    public readonly byte Alpha;
    
    public float RedFloat => ToFloat(Red);
    public float GreenFloat => ToFloat(Green);
    public float BlueFloat => ToFloat(Blue);
    public float AlphaFloat => ToFloat(Alpha);
    #endregion

    #region Static Colors
    public static readonly UiColor Black =  "#000000";
    public static readonly UiColor White = "#FFFFFF";
    public static readonly UiColor Clear = "#00000000";
    #endregion

    #region Constructors
    public UiColor()
    {
        
    }
    
    public UiColor(byte red, byte green, byte blue, byte alpha = 255)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
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
    public static implicit operator Color(UiColor value) => new(ToFloat(value.Red), ToFloat(value.Green), ToFloat(value.Blue), ToFloat(value.Alpha));
    public static bool operator ==(UiColor lhs, UiColor rhs) => lhs.Red == rhs.Red && lhs.Green == rhs.Green && lhs.Blue == rhs.Blue && lhs.Alpha == rhs.Alpha;
    public static bool operator !=(UiColor lhs, UiColor rhs) => !(lhs == rhs);
    public static UiColor operator *(UiColor color, UiColor multiplier)
    {
        return new UiColor(ToFloat(color.Red) * ToFloat(multiplier.Red), ToFloat(color.Green) * ToFloat(multiplier.Green), ToFloat(color.Blue) * ToFloat(multiplier.Blue), ToFloat(color.Alpha) * ToFloat(multiplier.Alpha));
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
        int red = Red << 24;
        int green = Green << 16;
        int blue = Blue << 8;
        return red | green | blue | Alpha;
    }

    public override string ToString() => $"{ToFloat(Red)} {ToFloat(Green)} {ToFloat(Blue)} {ToFloat(Alpha)}";
    #endregion

    #region Modifiers
    [Pure]
    public UiColor WithAlpha(byte alpha)
    {
        return new UiColor(Red, Green, Blue, alpha);
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
        return WithAlpha((byte)Mathf.Clamp(Alpha * alpha, 0, byte.MaxValue));
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
        byte red = (byte)Mathf.Clamp(Red * (1 - percentage), 0, byte.MaxValue);
        byte green = (byte)Mathf.Clamp(Green * (1 - percentage), 0, byte.MaxValue);
        byte blue = (byte)Mathf.Clamp(Blue * (1 - percentage), 0, byte.MaxValue);

        return new UiColor(red, green, blue, Alpha);
    }

    [Pure]
    public UiColor Lighten(float percentage)
    {
        percentage = Mathf.Clamp01(percentage);
        float red = (byte)Mathf.Clamp(byte.MaxValue - Red * percentage + Red, 0, byte.MaxValue);
        float green = (byte)Mathf.Clamp(byte.MaxValue - Green * percentage + Red, 0, byte.MaxValue);
        float blue = (byte)Mathf.Clamp(byte.MaxValue - Blue * percentage + Red, 0, byte.MaxValue);

        return new UiColor(red, green, blue, Alpha);
    }
        
    [Pure]
    public static UiColor Lerp(UiColor start, UiColor end, float value)
    {
        value = Mathf.Clamp01(value);
        return new UiColor(LerpField(start.Red, end.Red, value), LerpField(start.Green, end.Green, value), LerpField(start.Blue, end.Blue, value), LerpField(start.Alpha, end.Alpha, value));
    }

    private static byte LerpField(byte start, byte end, float value)
    {
        return (byte)Mathf.RoundToInt(start + (end - start) * value);
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
    public static UiColor ParseHexColor(string hexColor) => ParseHexColor(hexColor.AsSpan());
    
    public static UiColor ParseHexColor(ReadOnlySpan<char> span)
    {
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