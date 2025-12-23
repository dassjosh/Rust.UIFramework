using System;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Interfaces.Types;
using Oxide.Ext.UiFramework.Json;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Colors;

[JsonConverter(typeof(UiOpacityConverter))]
public readonly struct UiOpacity(float value) : IEquatable<UiOpacity>, ICssString
{
    public readonly float Value = Mathf.Clamp01(value);
    
    public static readonly UiOpacity Transparent = new(0f);
    public static readonly UiOpacity Opaque = new(1f);

    public UiOpacity(byte value) : this(value / 255f) { }
    public UiOpacity(UiColor value) : this(value.Alpha) { }
    
    public static UiOpacity Parse(string input) => Parse(input.AsSpan());
    public static UiOpacity Parse(ReadOnlySpan<char> input) => TryParse(input, out UiOpacity result) ? result : throw FormatException.FailedParse<UiOpacity>(input);
    public static bool TryParse(string input, out UiOpacity rotation) => TryParse(input.AsSpan(), out rotation);
    
    public static bool TryParse(ReadOnlySpan<char> span, out UiOpacity rotation)
    {
        if (float.TryParse(span, out float rotationValue))
        {
            rotation = new UiOpacity(rotationValue);
            return true;
        }

        rotation = default;
        return false;
    }
    
    public static UiOpacity Lerp(UiOpacity start, UiOpacity end, float t) => new(Mathf.Lerp(start.Value, end.Value, t));
    
    public static UiOpacity operator +(UiOpacity lhs, UiOpacity rhs) => new(lhs.Value + rhs.Value);
    public static UiOpacity operator -(UiOpacity lhs, UiOpacity rhs) => new(lhs.Value - rhs.Value);
    public static UiOpacity operator *(UiOpacity lhs, float rhs) => new(lhs.Value * rhs);
    public static UiOpacity operator *(UiOpacity lhs, UiOpacity rhs) => new(lhs.Value * (rhs.Value / Opaque.Value));
    public static UiOpacity operator /(UiOpacity lhs, float rhs) => new(lhs.Value / rhs);
    public static UiOpacity operator /(UiOpacity lhs, UiOpacity rhs) => new((lhs.Value * Opaque.Value) / rhs.Value);
    public static bool operator ==(UiOpacity lhs, UiOpacity rhs) => lhs.Equals(rhs);
    public static bool operator !=(UiOpacity lhs, UiOpacity rhs) => !(lhs == rhs);

    public bool Equals(UiOpacity other) => Value.Equals(other.Value);
    public override bool Equals(object obj) => obj is UiOpacity other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value.ToString();

    public string ToCssString() => $"opacity: {Value};";
}

public static class UiOpacityExt
{
    extension(int value)
    {
        public UiOpacity Opacity() => new(value);
    }
    
    extension(float value)
    {
        public UiOpacity Opacity() => new(value);
    }
    
    extension(double value)
    {
        public UiOpacity Opacity() => new((float)value);
    }
}