using System;
using System.Diagnostics.Contracts;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Positions;

[JsonConverter(typeof(UiPositionConverter))]
public readonly struct UiPosition(float xMin, float yMin, float xMax, float yMax) : IEquatable<UiPosition>
{
    public static readonly UiPosition None = new(0, 0, 0, 0);
    public static readonly UiPosition Full = new(0, 0, 1, 1);
    public static readonly UiPosition HorizontalPaddedFull = Full.SliceHorizontal(0.01f, 0.99f);
    public static readonly UiPosition VerticalPaddedFull = Full.SliceVertical(0.01f, 0.99f);
    public static readonly UiPosition TopLeft = new(0, 1, 0, 1);
    public static readonly UiPosition MiddleLeft = new(0, .5f, 0, .5f);
    public static readonly UiPosition BottomLeft = new(0, 0, 0, 0);
    public static readonly UiPosition TopMiddle = new(.5f, 1, .5f, 1);
    public static readonly UiPosition MiddleMiddle = new(.5f, .5f, .5f, .5f);
    public static readonly UiPosition BottomMiddle = new(.5f, 0, .5f, 0);
    public static readonly UiPosition TopRight = new(1, 1, 1, 1);
    public static readonly UiPosition MiddleRight = new(1, .5f, 1, .5f);
    public static readonly UiPosition BottomRight = new(1, 0, 1, 0);
        
    public static readonly UiPosition Top = new(0, 1, 1, 1);
    public static readonly UiPosition Bottom = new(0, 0, 1, 0);
    public static readonly UiPosition Left = new(0, 0, 0, 1);
    public static readonly UiPosition Right = new(1, 0, 1, 1);
        
    public static readonly UiPosition LeftHalf = new(0, 0, 0.5f, 1);
    public static readonly UiPosition TopHalf = new(0, 0.5f, 1, 1);
    public static readonly UiPosition RightHalf = new(0.5f, 0, 1, 1);
    public static readonly UiPosition BottomHalf = new(0, 0, 1, 0.5f);
    
    public readonly Vector2 Min = new(xMin, yMin);
    public readonly Vector2 Max = new(xMax, yMax);
    
    public float XMin => Min.x;
    public float YMin => Min.y;
    public float XMax => Max.x;
    public float YMax => Max.y;
    
    public Vector2 Size => Max - Min;
    public Vector2 Center => (Min + Max) * 0.5f;
    public float Width => Max.x - Min.x;
    public float Height => Max.y - Min.y;

    public UiPosition(Vector2 min, Vector2 max) : this(min.x, min.y, max.x, max.y) { }

    [Pure]
    public UiPosition WithXMin(float xMin) => new(xMin, Min.y, Max.x, Max.y);
    
    [Pure]
    public UiPosition WithXMax(float xMax) => new(Min.x, Min.y, xMax, Max.y);    
    
    [Pure]
    public UiPosition WithYMin(float yMin) => new(Min.x, yMin, Max.x, Max.y);
    
    [Pure]
    public UiPosition WithYMax(float yMax) => new(Min.x, Min.y, Max.x, yMax);
    
    [Pure]
    public UiPosition SetX(float xMin, float xMax)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiPosition(xMin, min.y, xMax, max.y);
    }
        
    [Pure]
    public UiPosition SetY(float yMin, float yMax)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiPosition(min.x, yMin, max.x, yMax);
    }
        
    [Pure]
    public UiPosition MoveX(float delta)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiPosition(min.x + delta, min.y, max.x + delta, max.y);
    }
     
    [Pure]
    public UiPosition MoveXPadded(float padding)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        float spacing = (max.x - min.x + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
        return new UiPosition(min.x + spacing, min.y, max.x + spacing, max.y);
    }
        
    [Pure]
    public UiPosition MoveY(float delta)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiPosition(min.x, min.y + delta, max.x, max.y + delta);
    }
        
    [Pure]
    public UiPosition MoveYPadded(float padding)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        float spacing = (max.y - min.y + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
        return new UiPosition(min.x, min.y + spacing, max.x, max.y + spacing);
    }
        
    [Pure]
    public UiPosition Expand(float amount)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiPosition(min.x - amount, min.y - amount, max.x + amount, max.y + amount);
    }
    
    [Pure]
    public UiPosition Expand(float horizontal, float vertical)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiPosition(min.x - horizontal, min.y - vertical, max.x + horizontal, max.y + vertical);
    }
        
    [Pure]
    public UiPosition ExpandHorizontal(float amount)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiPosition(min.x - amount, min.y, max.x + amount, max.y);
    }
        
    [Pure]
    public UiPosition ExpandVertical(float amount)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiPosition(min.x, min.y - amount, max.x, max.y + amount);
    }
        
    [Pure]
    public UiPosition Shrink(float amount)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiPosition(min.x + amount, min.y + amount, max.x - amount, max.y - amount);
    }
    
    [Pure]
    public UiPosition Shrink(float horizontal, float vertical)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiPosition(min.x + horizontal, min.y + vertical, max.x - horizontal, max.y - vertical);
    }
        
    [Pure]
    public UiPosition ShrinkHorizontal(float amount)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiPosition(min.x + amount, min.y, max.x - amount, max.y);
    }
        
    [Pure]
    public UiPosition ShrinkVertical(float amount)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiPosition(min.x, min.y + amount, max.x, max.y - amount);
    }
        
    /// <summary>
    /// Returns a slice of the position
    /// </summary>
    /// <param name="xMin">% of the xMax - xMin distance added to xMin</param>
    /// <param name="yMin">% of the yMax - yMin distance added to yMin</param>
    /// <param name="xMax">>% of the xMax - xMin distance added to xMin</param>
    /// <param name="yMax">% of the yMax - yMin distance added to yMin</param>
    /// <returns>Sliced <see cref="UiPosition"/></returns>
    [Pure]
    public UiPosition Slice(float xMin, float yMin, float xMax, float yMax)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        Vector2 distance = max - min;
        return new UiPosition(min.x + distance.x * xMin, min.y + distance.y * yMin, min.x + distance.x * xMax, min.y + distance.y * yMax);
    }

    /// <summary>
    /// Returns a horizontal slice of the position
    /// </summary>
    /// <param name="xMin">% of the xMax - xMin distance added to xMin</param>
    /// <param name="xMax">>% of the xMax - xMin distance added to xMin</param>
    /// <returns>Sliced <see cref="UiPosition"/></returns>
    [Pure]
    public UiPosition SliceHorizontal(float xMin, float xMax)
    {
        Vector2 min = Min;
        Vector2 max = Max;   
        return new UiPosition(min.x + (max.x - min.x) * xMin, min.y, min.x + (max.x - min.x) * xMax, max.y);
    }

    /// <summary>
    /// Returns a vertical slice of the position
    /// </summary>
    /// <param name="yMin">% of the yMax - yMin distance added to yMin</param>
    /// <param name="yMax">% of the yMax - yMin distance added to yMin</param>
    /// <returns>Sliced <see cref="UiPosition"/></returns>
    [Pure]
    public UiPosition SliceVertical(float yMin, float yMax)
    {
        Vector2 min = Min;
        Vector2 max = Max;   
        return new UiPosition(min.x, min.y + (max.y - min.y) * yMin, max.x, min.y + (max.y - min.y) * yMax);
    }

    [Pure]
    public UiPosition WithPadding(in UiPadding padding)
    {
        return this + padding;
    }
    
    [Pure]
    public UiPosition Scale(in UiScale scale)
    {
        if (!scale.HasScale)
        {
            return this;
        }

        Vector2 center = Center;
        float centerX = center.x;
        float centerY = center.y;
        
        float scaledWidth = Width * 0.5f * scale.Horizontal;
        float scaledHeight = Height * 0.5f * scale.Vertical;
        
        return new UiPosition(
            centerX - scaledWidth,
            centerY - scaledHeight,
            centerX + scaledWidth,
            centerY + scaledHeight
        );
    }

    public static UiPosition Lerp(in UiPosition a, in UiPosition b, float t) => new(Vector2.Lerp(a.Min, b.Min, t), Vector2.Lerp(a.Max, b.Max, t));
    public static UiPosition LerpUnclamped(in UiPosition a, in UiPosition b, float t) => new(Vector2.LerpUnclamped(a.Min, b.Min, t), Vector2.LerpUnclamped(a.Max, b.Max, t));
    
#pragma warning disable EPS05
    public static UiPosition Lerp(UiPosition a, UiPosition b, float t) => new(Vector2.Lerp(a.Min, b.Min, t), Vector2.Lerp(a.Max, b.Max, t));
    public static UiPosition LerpUnclamped(UiPosition a, UiPosition b, float t) => new(Vector2.LerpUnclamped(a.Min, b.Min, t), Vector2.LerpUnclamped(a.Max, b.Max, t));
#pragma warning restore EPS05

    public static UiPosition Parse(string str, string token = " ") => Parse(str.AsSpan(), token);

    public static UiPosition Parse(ReadOnlySpan<char> span, ReadOnlySpan<char> token = " ")
    {
        (float xMin, float yMin, float xMax, float yMax) = span.ParseFourFloats(token);
        return new UiPosition(xMin, yMin, xMax, yMax);
    }

    public static bool TryParse(string str, out UiPosition position, string token = " ") => TryParse(str.AsSpan(), out position, token);

    public static bool TryParse(ReadOnlySpan<char> span, out UiPosition position, ReadOnlySpan<char> token = " ")
    {
        bool success = span.TryParseFourFloats(token, out (float xMin, float yMin, float xMax, float yMax) parsed);
        position = success ? new UiPosition(parsed.xMin, parsed.yMin, parsed.xMax, parsed.yMax) : default;
        return success;
    }
    
    public static bool operator ==(UiPosition left, UiPosition right) => left.Equals(right);
    public static bool operator !=(UiPosition left, UiPosition right) => !(left == right);
    public static UiPosition operator +(UiPosition lhs, UiPosition rhs) => new(lhs.Min.x + rhs.Min.x, lhs.Min.y + rhs.Min.y, lhs.Max.x - rhs.Max.x, lhs.Max.y - rhs.Max.y);
    public static UiPosition operator -(UiPosition lhs, UiPosition rhs) => new(lhs.Min.x - rhs.Min.x, lhs.Min.y - rhs.Min.y, lhs.Max.x + rhs.Max.x, lhs.Max.y + rhs.Max.y);

    public bool Equals(UiPosition other) => Min.Equals(other.Min) && Max.Equals(other.Max);
    public override bool Equals(object obj) => obj is UiPosition other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(Min, Max);
    public override string ToString() => $"({Min.x:0.####}, {Min.y:0.####}) ({Max.x:0.####}, {Max.y:0.####})";
    public string ToJsonString() => $"{Min.x} {Min.y} {Max.x} {Max.y}";
}