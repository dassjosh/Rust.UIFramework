using System;
using System.Diagnostics.Contracts;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Json;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Types;

[JsonConverter(typeof(UiRotationConverter))]
public readonly record struct UiRotation(float Rotation)
{
    public static readonly UiRotation Zero = new(0);
    public static readonly UiRotation Twelfth = new(FullRotation / 12);
    public static readonly UiRotation Eleventh = new(FullRotation / 11);
    public static readonly UiRotation Tenth = new(FullRotation / 10);
    public static readonly UiRotation Ninth = new(FullRotation / 9);
    public static readonly UiRotation Eighth = new(FullRotation / 8);
    public static readonly UiRotation Seventh = new(FullRotation / 7);
    public static readonly UiRotation Sixth = new(FullRotation / 6);
    public static readonly UiRotation Fifth = new(FullRotation / 5);
    public static readonly UiRotation Fourth = new(FullRotation / 4);
    public static readonly UiRotation Third = new(FullRotation / 3);
    public static readonly UiRotation Half = new(FullRotation / 2);
    public static readonly UiRotation Full = new(FullRotation);

    private const float FullRotation = 360f;

    [Pure]
    public UiRotation RotateRight(float rotation) => new(Rotation + rotation);
    
    [Pure]
    public UiRotation RotateRight(UiRotation rotation) => this + rotation;
    
    [Pure]
    public UiRotation RotateLeft(float rotation) => new(Rotation - rotation);
    
    [Pure]
    public UiRotation RotateLeft(UiRotation rotation) => this - rotation;
    
    [Pure]
    public UiRotation Normalize() => new(Rotation % FullRotation);

    [Pure]
    public UiRotation Reverse() => (this + Half).Normalize();

    public static UiRotation Lerp(UiRotation a, UiRotation b, float t) => new(Mathf.Lerp(a.Rotation, b.Rotation, t));
    public static UiRotation LerpUnclamped(UiRotation a, UiRotation b, float t) => new(Mathf.LerpUnclamped(a.Rotation, b.Rotation, t));

    public static UiRotation Parse(string input) => Parse(input.AsSpan());
    public static UiRotation Parse(ReadOnlySpan<char> input) => TryParse(input, out UiRotation result) ? result : throw new FormatException($"Unable to parse '{input}' as {nameof(UiRotation)}");
    public static bool TryParse(string input, out UiRotation rotation) => TryParse(input.AsSpan(), out rotation);
    
    public static bool TryParse(ReadOnlySpan<char> input, out UiRotation rotation)
    {
        if (float.TryParse(input, out float rotationValue))
        {
            rotation = new UiRotation(rotationValue);
            return true;
        }

        rotation = default;
        return false;
    }

    public static UiRotation operator +(UiRotation lhs, UiRotation rhs) => new(lhs.Rotation + rhs.Rotation);
    public static UiRotation operator +(UiRotation lhs, float rhs) => new(lhs.Rotation + rhs);
    public static UiRotation operator -(UiRotation lhs, UiRotation rhs) => new(lhs.Rotation - rhs.Rotation);
    public static UiRotation operator -(UiRotation lhs, float rhs) => new(lhs.Rotation - rhs);
    public static UiRotation operator *(UiRotation lhs, float rhs) => new(lhs.Rotation * rhs);
    public static UiRotation operator *(UiRotation lhs, UiRotation rhs) => new(lhs.Rotation * (rhs.Rotation / Full.Rotation));
    public static UiRotation operator /(UiRotation lhs, float rhs) => new(lhs.Rotation / rhs);
    public static UiRotation operator /(UiRotation lhs, UiRotation rhs) => new(lhs.Rotation * Full.Rotation / rhs.Rotation);
    public static UiRotation operator -(UiRotation rotation) => new(-rotation.Rotation);
    public override string ToString() => $"{Rotation}°";
}

public static class UiRotationExt
{
    extension(int value)
    {
        public UiRotation Degrees() => new(value);
        public UiRotation Radians() => new(value * Mathf.Rad2Deg);
    }
    
    extension(float value)
    {
        public UiRotation Degrees() => new(value);
        public UiRotation Radians() => new(value * Mathf.Rad2Deg);
    }
    
    extension(double value)
    {
        public UiRotation Degrees() => new((float)value);
        public UiRotation Radians() => new((float)(value * Mathf.Rad2Deg));
    }
}