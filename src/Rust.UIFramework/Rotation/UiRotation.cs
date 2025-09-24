using System.Diagnostics.Contracts;
using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Rotation;

public readonly struct UiRotation(float rotation)
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
    
    public readonly float Rotation = rotation;

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

    public static UiRotation Lerp(UiRotation a, UiRotation b, float t) => new(FloatExt.Lerp(a.Rotation, b.Rotation, t));
    public static UiRotation LerpUnclamped(UiRotation a, UiRotation b, float t) => new(FloatExt.LerpUnclamped(a.Rotation, b.Rotation, t));
    
    public static UiRotation operator +(UiRotation lhs, UiRotation rhs) => new(lhs.Rotation + rhs.Rotation);
    public static UiRotation operator -(UiRotation lhs, UiRotation rhs) => new(lhs.Rotation - rhs.Rotation);
    public static UiRotation operator *(UiRotation lhs, float rhs) => new(lhs.Rotation * rhs);
    public static UiRotation operator *(UiRotation lhs, UiRotation rhs) => new(lhs.Rotation * (rhs.Rotation / Full.Rotation));
    public static UiRotation operator /(UiRotation lhs, float rhs) => new(lhs.Rotation / rhs);
    public static UiRotation operator /(UiRotation lhs, UiRotation rhs) => new((lhs.Rotation * Full.Rotation) / rhs.Rotation);
}