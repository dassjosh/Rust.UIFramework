using System;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Interfaces.Types;
using Oxide.Ext.UiFramework.Json;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Types;

[JsonConverter(typeof(UiScaleConverter))]
public readonly record struct UiScale(float Horizontal, float Vertical) : ICssString
{
    public bool HasScale => !Mathf.Approximately(Horizontal, 1f) || !Mathf.Approximately(Vertical, 1f);
    
    public static readonly UiScale Zero = new(0);
    public static readonly UiScale One = new(1);

    public UiScale(float scale) : this(scale, scale) { }
    
    public static UiScale X(float scale) => new(scale, 1);
    public static UiScale Y(float scale) => new(1, scale);
    
    public static UiScale Parse(string input, string token = " ") => Parse(input.AsSpan(), token);
    public static UiScale Parse(ReadOnlySpan<char> input, ReadOnlySpan<char> token = " ") => TryParse(input, out UiScale result, token) ? result : throw FormatException.FailedParse<UiScale>(input);
    public static bool TryParse(string input, out UiScale scale, string token = " ") => TryParse(input.AsSpan(), out scale, token);

    public static bool TryParse(ReadOnlySpan<char> input, out UiScale scale, ReadOnlySpan<char> token = " ")
    {
        bool success = input.TryParseTwoFloats(token, out (float horizontal, float vertical) parsed);
        scale = success ? new UiScale(parsed.horizontal, parsed.vertical) : default;
        return success;
    }
    
    public static UiScale Lerp(UiScale start, UiScale end, float progress)
    {
        return new UiScale(Mathf.Lerp(start.Horizontal, end.Horizontal, progress), Mathf.Lerp(start.Vertical, end.Vertical, progress));
    }
    
    public static UiScale operator -(UiScale scale) => new(-scale.Horizontal, -scale.Vertical);
    
    public static UiScale operator *(UiScale scale, float value) => new(scale.Horizontal * value, scale.Vertical * value);
    public static UiScale operator /(UiScale scale, float value) => new(scale.Horizontal / value, scale.Vertical / value);

    public override string ToString() => $"{Horizontal} {Vertical}";
    public string ToCssString()
    {
        if (!HasScale)
        {
            return "transform: scale(1);";
        }

        if (Mathf.Approximately(Horizontal, Vertical))
        {
            return $"transform: scale({Horizontal});";
        }

        if (Mathf.Approximately(Horizontal, 1f))
        {
            return $"transform: scaleY({Vertical});";
        }
        
        if (Mathf.Approximately(Vertical, 1f))
        {
            return $"transform: scaleX({Horizontal});";
        }
        
        return $"transform: scale({Horizontal} {Vertical})";
    }
}

public static class UiScaleExt
{
    extension(UiScale scale)
    {
        public UiScale Inverse() => new(1 / scale.Horizontal, 1 / scale.Vertical);
        public UiScale X() => new(scale.Horizontal, 1);
        public UiScale Y() => new(1, scale.Vertical);
    }
    
    extension(int value)
    {
        public UiScale Scale() => new(value);
    }
    
    extension(float value)
    {
        public UiScale Scale() => new(value);
    }
    
    extension(double value)
    {
        public UiScale Scale() => new((float)value);
    }
}