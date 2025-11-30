using System;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Json;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Colors;

[JsonConverter(typeof(UiOpacityConverter))]
public readonly struct UiOpacity(float value) : IEquatable<UiOpacity>
{
    public readonly float Value = Mathf.Clamp01(value);
    
    public static readonly UiOpacity None = new(0);
    public static readonly UiOpacity Full = new(1);
    
    public static UiOpacity Parse(string str) => new(float.Parse(str));
    public static UiOpacity Parse(ReadOnlySpan<char> span) => new(float.Parse(span));
    public static bool TryParse(string str, out UiOpacity rotation) => TryParse(str.AsSpan(), out rotation);
    
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
    public static UiOpacity operator *(UiOpacity lhs, UiOpacity rhs) => new(lhs.Value * (rhs.Value / Full.Value));
    public static UiOpacity operator /(UiOpacity lhs, float rhs) => new(lhs.Value / rhs);
    public static UiOpacity operator /(UiOpacity lhs, UiOpacity rhs) => new((lhs.Value * Full.Value) / rhs.Value);
    public static bool operator ==(UiOpacity lhs, UiOpacity rhs) => lhs.Equals(rhs);
    public static bool operator !=(UiOpacity lhs, UiOpacity rhs) => !(lhs == rhs);

    public bool Equals(UiOpacity other) => Value.Equals(other.Value);
    public override bool Equals(object obj) => obj is UiOpacity other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();
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