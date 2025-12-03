using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Colors;

[JsonConverter(typeof(UiColorConverter))]
[DebuggerDisplay("{ToHtmlColor()}")]
public readonly struct UiColor : IEquatable<UiColor>, IUiConvertable<UiColor, UiOpacity>
{
    #region Fields
    public readonly byte RedB;
    public readonly byte GreenB;
    public readonly byte BlueB;
    public readonly byte AlphaB;
    
    public float RedFloat => ToFloat(RedB);
    public float GreenFloat => ToFloat(GreenB);
    public float BlueFloat => ToFloat(BlueB);
    public float AlphaFloat => ToFloat(AlphaB);
    #endregion

    #region Static Colors
    [Obsolete("Use UiColors Instead")] public static readonly UiColor Black =  "#000000";
    [Obsolete("Use UiColors Instead")] public static readonly UiColor White = "#FFFFFF";
    [Obsolete("Use UiColors Instead")] public static readonly UiColor Silver =  "#C0C0C0";
    [Obsolete("Use UiColors Instead")] public static readonly UiColor Gray = "#808080";
    [Obsolete("Use UiColors Instead")] public static readonly UiColor Red = "#FF0000";
    [Obsolete("Use UiColors Instead")] public static readonly UiColor Maroon = "#800000";
    [Obsolete("Use UiColors Instead")] public static readonly UiColor Orange = "#FFA500";
    [Obsolete("Use UiColors Instead")] public static readonly UiColor Yellow = "#FFEB04";
    [Obsolete("Use UiColors Instead")] public static readonly UiColor Olive = "#808000";
    [Obsolete("Use UiColors Instead")] public static readonly UiColor Lime = "#00FF00";
    [Obsolete("Use UiColors Instead")] public static readonly UiColor Green = "#008000";
    [Obsolete("Use UiColors Instead")] public static readonly UiColor Teal = "#008080";
    [Obsolete("Use UiColors Instead")] public static readonly UiColor Cyan = "#00FFFF";
    [Obsolete("Use UiColors Instead")] public static readonly UiColor Blue = "#0000FF";
    [Obsolete("Use UiColors Instead")] public static readonly UiColor Navy = "#000080";
    [Obsolete("Use UiColors Instead")] public static readonly UiColor Magenta = "#FF00FF";
    [Obsolete("Use UiColors Instead")] public static readonly UiColor Purple = "#800080";
    [Obsolete("Use UiColors Instead")] public static readonly UiColor Clear = "#00000000";
    #endregion

    #region Constructors
    public UiColor() { }
    
    public UiColor(byte red, byte green, byte blue, byte alpha = 255)
    {
        RedB = red;
        GreenB = green;
        BlueB = blue;
        AlphaB = alpha;
    }
        
    public UiColor(int red, int green, int blue, int alpha = 255) : this(
        (byte)Mathf.Clamp(red, 0, byte.MaxValue), 
        (byte)Mathf.Clamp(green, 0, byte.MaxValue), 
        (byte)Mathf.Clamp(blue, 0, byte.MaxValue), 
        (byte)Mathf.Clamp(alpha, 0, byte.MaxValue)) { }
        
    public UiColor(Color color) : this(color.r, color.g, color.b, color.a) { }
    public UiColor(Color color, float alpha) : this(color.r, color.g, color.b, alpha) { }
        
    public UiColor(float red, float green, float blue, float alpha = 1f) : this(Mathf.RoundToInt(red * 255f), Mathf.RoundToInt(green * 255f), Mathf.RoundToInt(blue * 255f), Mathf.RoundToInt(alpha * 255f)) {}
    #endregion

    #region Operators
    public static implicit operator UiColor(string value) => ParseHexColor(value);
    public static implicit operator UiColor(Color value) => new(value);
    public static implicit operator Color(UiColor value) => new(value.RedFloat, value.GreenFloat, value.BlueFloat, value.AlphaFloat);
    public static bool operator ==(UiColor lhs, UiColor rhs) => lhs.RedB == rhs.RedB && lhs.GreenB == rhs.GreenB && lhs.BlueB == rhs.BlueB && lhs.AlphaB == rhs.AlphaB;
    public static bool operator !=(UiColor lhs, UiColor rhs) => !(lhs == rhs);
    public static UiColor operator *(UiColor color, UiColor multiplier)
    {
        return new UiColor( color.RedFloat * multiplier.RedFloat, color.GreenFloat * multiplier.GreenFloat, color.BlueFloat * multiplier.BlueFloat, color.AlphaFloat * multiplier.AlphaFloat);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static float ToFloat(byte value) => value / 255f;
        
    public bool Equals(UiColor other) => this == other;

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        return obj is UiColor color && Equals(color);
    }
        
    public override int GetHashCode()
    {
        return RedB << 24 | GreenB << 16 | BlueB << 8 | AlphaB;
    }

    public override string ToString() => $"{RedFloat} {GreenFloat} {BlueFloat} {AlphaFloat}";
    #endregion

    #region Modifiers
    [Pure]
    public UiColor WithAlpha(byte alpha) => new(RedB, GreenB, BlueB, alpha);

    [Pure]
    public UiColor WithAlpha(string hex) => WithAlpha(byte.Parse(hex, NumberStyles.HexNumber));

    [Pure]
    public UiColor WithAlpha(int alpha) => WithAlpha((byte)Mathf.Clamp(alpha, 0, byte.MaxValue));

    [Pure]
    public UiColor WithAlpha(float alpha) => WithAlpha((byte)Mathf.Clamp(alpha * 255f, 0, byte.MaxValue));
    
    [Pure]
    public UiColor WithOpacity(UiOpacity opacity) => MultiplyAlpha(opacity.Value);

    [Pure]
    public UiColor MultiplyAlpha(float alpha) => WithAlpha((byte)Mathf.Clamp(AlphaB * alpha, 0, byte.MaxValue));

    [Pure]
    public float GrayScaleFloat() => 0.299f * RedFloat + 0.587f * GreenFloat + 0.114f * BlueFloat;
    
    [Pure]
    public UiColor GrayScale() => GrayScale(1f);

    [Pure]
    public UiColor GrayScale(float amount)
    {
        amount = Mathf.Clamp01(amount);
        float gray = GrayScaleFloat();
        UiColor grayColor = new(gray, gray, gray, AlphaFloat);
        return Lerp(this, grayColor, amount);
    }
    
    [Pure]
    public UiColor Contrast(float amount)
    {
        amount = Mathf.Clamp(amount, 0, 2f);
        float factor = 259f * (amount + 255f) / (255f * (259f - amount));

        float r = ApplyContrast(RedFloat);
        float g = ApplyContrast(GreenFloat);
        float b = ApplyContrast(BlueFloat);
        
        return new UiColor(r, g, b, AlphaFloat);

        float ApplyContrast(float c)
        {
            c = Mathf.Clamp01(c);
            return Mathf.Clamp01((factor * (c - 0.5f)) + 0.5f);
        }
    }
    
    [Pure]
    public UiColor HueRotate(float degrees)
    {
        Color.RGBToHSV(this, out float h, out float s, out float v);
        h = (h * 360f + degrees) % 360f / 360f;
        if (h < 0) h += 1f;
        return new UiColor(Color.HSVToRGB(h, s, v), AlphaFloat);
    }
    
    [Pure]
    public UiColor Invert() => new(1f - RedFloat, 1f - GreenFloat, 1f - BlueFloat, AlphaFloat);

    [Pure]
    public UiColor Invert(float amount)
    {
        amount = Mathf.Clamp01(amount);
        if (amount <= 0f) return this;
        
        float r = Mathf.Lerp(RedFloat, 1f - RedFloat, amount);
        float g = Mathf.Lerp(GreenFloat, 1f - GreenFloat, amount);
        float b = Mathf.Lerp(BlueFloat, 1f - BlueFloat, amount);
        
        return new UiColor(r, g, b, AlphaFloat);
    }
    
    [Pure]
    public UiColor Saturate(float amount)
    {
        amount = Mathf.Clamp01(amount);
        Color.RGBToHSV(this, out float h, out float s, out float v);
        return new UiColor(Color.HSVToRGB(h, Mathf.Lerp(0f, s, amount), v), AlphaFloat);
    }
    
    [Pure]
    public UiColor Desaturate(float amount) => Saturate(1f - amount);
    
    [Pure]
    public UiColor Sepia() => Sepia(1f);
    
    [Pure]
    public UiColor Sepia(float amount)
    {
        amount = Mathf.Clamp01(amount);
        if (amount <= 0f) return this;
        
        // Calculate grayscale using standard weights
        float gray = GrayScaleFloat();
        
        // Apply sepia tone
        float r = Mathf.Lerp(RedFloat, Mathf.Clamp01(gray * 1.351f), amount);
        float g = Mathf.Lerp(GreenFloat, Mathf.Clamp01(gray * 0.954f), amount);
        float b = Mathf.Lerp(BlueFloat, Mathf.Clamp01(gray * 0.554f), amount);
        
        return new UiColor(r, g, b, AlphaFloat);
    }

    [Pure]
    public UiColor Darken(float percentage)
    {
        percentage = Mathf.Clamp01(percentage);
        byte red = (byte)Mathf.Clamp(RedB * (1 - percentage), 0, byte.MaxValue);
        byte green = (byte)Mathf.Clamp(GreenB * (1 - percentage), 0, byte.MaxValue);
        byte blue = (byte)Mathf.Clamp(BlueB * (1 - percentage), 0, byte.MaxValue);

        return new UiColor(red, green, blue, AlphaB);
    }

    [Pure]
    public UiColor Lighten(float percentage)
    {
        percentage = Mathf.Clamp01(percentage);
        byte red = (byte)Mathf.Clamp((byte.MaxValue - RedB) * percentage + RedB, 0, byte.MaxValue);
        byte green = (byte)Mathf.Clamp((byte.MaxValue - GreenB) * percentage + GreenB, 0, byte.MaxValue); 
        byte blue = (byte)Mathf.Clamp((byte.MaxValue - BlueB) * percentage + BlueB, 0, byte.MaxValue);

        return new UiColor(red, green, blue, AlphaB);
    }
        
    [Pure]
    public static UiColor Lerp(UiColor start, UiColor end, float value) => LerpUnclamped(start, end, Mathf.Clamp01(value));

    [Pure]
    public static UiColor LerpUnclamped(UiColor start, UiColor end, float value) 
        => new(Mathf.LerpUnclamped(start.RedFloat, end.RedFloat, value), 
            Mathf.LerpUnclamped(start.GreenFloat, end.GreenFloat, value), 
            Mathf.LerpUnclamped(start.BlueFloat, end.BlueFloat, value), 
            Mathf.LerpUnclamped(start.AlphaFloat, end.AlphaFloat, value));

    UiColor IUiConvertable<UiColor, UiOpacity>.Convert(UiOpacity from) => WithOpacity(from);
    #endregion

    #region Formats
    public string ToHexRGB() => $"{RedB:X2}{GreenB:X2}{BlueB:X2}";
    public string ToHexRGBA() => $"{RedB:X2}{GreenB:X2}{BlueB:X2}{AlphaB:X2}";
    public string ToHtmlColor() => $"#{RedB:X2}{GreenB:X2}{BlueB:X2}{AlphaB:X2}";
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