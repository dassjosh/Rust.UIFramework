using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Rotation;

public readonly struct UiRotation(float rotation)
{
    public static readonly UiRotation Zero = new(0);
    public static readonly UiRotation Fourth = new(90);
    public static readonly UiRotation Third = new(120);
    public static readonly UiRotation Half = new(180);
    public static readonly UiRotation Full = new(360);
    
    public readonly float Rotation = rotation;
    
    public static UiRotation Lerp(in UiRotation a, in UiRotation b, float t) => new(FloatExt.Lerp(a.Rotation, b.Rotation, t));
    public static UiRotation LerpUnclamped(in UiRotation a, in UiRotation b, float t) => new(FloatExt.LerpUnclamped(a.Rotation, b.Rotation, t));
    
    public static UiRotation operator +(UiRotation lhs, UiRotation rhs) => new(lhs.Rotation + rhs.Rotation);
    public static UiRotation operator -(UiRotation lhs, UiRotation rhs) => new(lhs.Rotation - rhs.Rotation);
    public static UiRotation operator *(UiRotation lhs, float rhs) => new(lhs.Rotation * rhs);
    public static UiRotation operator *(UiRotation lhs, UiRotation rhs) => new(lhs.Rotation * (rhs.Rotation / Full.Rotation));
    public static UiRotation operator /(UiRotation lhs, float rhs) => new(lhs.Rotation / rhs);
    public static UiRotation operator /(UiRotation lhs, UiRotation rhs) => new((lhs.Rotation * Full.Rotation) / rhs.Rotation);
}