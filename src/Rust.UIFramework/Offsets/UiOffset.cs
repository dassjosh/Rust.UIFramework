using System;
using System.Diagnostics.Contracts;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Offsets;

public readonly struct UiOffset(float xMin, float yMin, float xMax, float yMax) : IEquatable<UiOffset>
{
    public static readonly UiOffset None = new(0, 0, 0, 0);
    public static readonly UiOffset Scaled = new(1280, 720);

    public readonly Vector2 Min = new(xMin, yMin);
    public readonly Vector2 Max = new(xMax, yMax);

    public Vector2 Size => Max - Min;
    public float Width => Max.x - Min.x;
    public float Height => Max.y - Min.y;

    public UiOffset(float size) : this(size, size) { }
    
    public UiOffset(float width, float height) : this(-width / 2f, -height / 2f, width / 2f, height / 2f) { }
    public UiOffset(Vector2 min, Vector2 max) : this(min.x, min.y, max.x, max.y) { }

    public static UiOffset CreateRect(int x, int y, int width, int height)
    {
        return new UiOffset(x, y, x + width, y + height);
    }

    #region Modifiers
    [Pure]
    public UiOffset SetX(float xMin, float xMax)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiOffset(xMin, min.y, xMax, max.y);
    }
        
    [Pure]
    public UiOffset SetY(float yMin, float yMax)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiOffset(min.x, yMin, max.x, yMax);
    }
        
    [Pure]
    public UiOffset SetWidth(float width)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiOffset(min.x, min.y,  min.x + width, max.y);
    }
    
    [Pure]   
    public UiOffset SetHeight(float height)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiOffset(min.x, min.y, max.x, min.y + height);
    }
        
    [Pure]
    public UiOffset MoveX(float delta)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiOffset(min.x + delta, min.y, max.x + delta, max.y);
    }
        
    [Pure]
    public UiOffset MoveXPadded(float padding)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        float spacing = (max.x - min.x + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
        return new UiOffset(min.x + spacing, min.y, max.x + spacing, max.y);
    }
        
    [Pure]
    public UiOffset MoveXPaddedWithWidth(float padding, float width)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        float spacing = (max.x - min.x + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
        return new UiOffset(min.x + spacing, min.y, min.x + spacing + width, max.y);
    }
        
    [Pure]
    public UiOffset MoveXPaddedWithHeight(float padding, float height)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        float spacing = (max.x - min.x + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
        return new UiOffset(min.x + spacing, min.y, max.x + spacing, min.y + height);
    }
        
    [Pure]
    public UiOffset MoveY(float delta)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiOffset(min.x, min.y + delta, max.x, max.y + delta);
    }
     
    [Pure]
    public UiOffset MoveYPadded(float padding)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        float spacing = (max.y - min.y + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
        return new UiOffset(min.x, min.y + spacing, max.x, max.y + spacing);
    }

    [Pure]
    public UiOffset MoveYPaddedWithWidth(float padding, float width)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        float spacing = (max.y - min.y + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
        return new UiOffset(min.x, min.y + spacing, min.x + width, max.y + spacing);
    }
        
    [Pure]
    public UiOffset MoveYPaddedWithHeight(float padding, float height)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        int multiplier = padding < 0 ? -1 : 1;
        float spacing = (max.y - min.y + Math.Abs(padding)) * multiplier;
        return new UiOffset(min.x, min.y + spacing, max.x, min.y + spacing + height * multiplier);
    }
        
    [Pure]
    public UiOffset Expand(float amount)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiOffset(min.x - amount, min.y - amount, max.x + amount, max.y + amount);
    }
        
    [Pure]
    public UiOffset ExpandHorizontal(float amount)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiOffset(min.x - amount, min.y, max.x + amount, max.y);
    }
        
    [Pure]
    public UiOffset ExpandVertical(float amount)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiOffset(min.x, min.y - amount, max.x, max.y + amount);
    }
        
    [Pure]
    public UiOffset Shrink(float amount)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiOffset(min.x + amount, min.y + amount, max.x - amount, max.y - amount);
    }
        
    [Pure]
    public UiOffset ShrinkHorizontal(float amount)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiOffset(min.x + amount, min.y, max.x - amount, max.y);
    }
        
    [Pure]
    public UiOffset ShrinkVertical(float amount)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiOffset(min.x, min.y + amount, max.x, max.y - amount);
    }
        
    /// <summary>
    /// Returns a slice of the position
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="left">Pixels to remove from the left</param>
    /// <param name="bottom">Pixels to remove from the bottom</param>
    /// <param name="right">>Pixels to remove from the right</param>
    /// <param name="top">Pixels to remove from the top</param>
    /// <returns>Sliced <see cref="UiOffset"/></returns>
    [Pure]
    public UiOffset Slice(float left, float bottom, float right, float top)
    {
        Vector2 min = Min;
        Vector2 max = Max;
        return new UiOffset(min.x + left, min.y + bottom, max.x - right, max.y - top);
    }

    /// <summary>
    /// Returns a horizontal slice of the position
    /// </summary>
    /// <param name="pos">Offset to slice</param>
    /// <param name="left">Pixels to remove from the left</param>
    /// <param name="right">>Pixels to remove from the right</param>
    /// <returns>Sliced <see cref="UiOffset"/></returns>
    [Pure]
    public UiOffset SliceHorizontal(float left, float right)
    {
        Vector2 min = Min;
        Vector2 max = Max;   
        return new UiOffset(min.x + left, min.y, max.x - right,max.y);
    }

    /// <summary>
    /// Returns a vertical slice of the position
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="bottom">Pixels to remove from the bottom</param>
    /// <param name="top">Pixels to remove from the top</param>
    /// <returns>Sliced <see cref="UiOffset"/></returns>
    [Pure]
    public UiOffset SliceVertical(float bottom, float top)
    {
        Vector2 min = Min;
        Vector2 max = Max;   
        return new UiOffset(min.x, min.y + bottom, max.x, max.y - top);
    }
    
    public static UiOffset Lerp(in UiOffset a, in UiOffset b, float t) => new(Vector2.Lerp(a.Min, b.Min, t), Vector2.Lerp(a.Max, b.Max, t));
    #endregion

    #region Operators

    public static UiOffset operator +(UiOffset lhs, UiOffset rhs) => new(lhs.Min.x + rhs.Min.x, lhs.Min.y + rhs.Min.y, lhs.Max.x + rhs.Max.x, lhs.Max.y + rhs.Max.y);
    public static UiOffset operator -(UiOffset lhs, UiOffset rhs) => new(lhs.Min.x - rhs.Min.x, lhs.Min.y - rhs.Min.y, lhs.Max.x - rhs.Max.x, lhs.Max.y - rhs.Max.y);

    #endregion
    
    public override string ToString()
    {
        return $"({Min.x:0}, {Min.y:0}) ({Max.x:0}, {Max.y:0}) WxH:({Width} x {Height})";
    }

    public bool Equals(UiOffset other) => Min.Equals(other.Min) && Max.Equals(other.Max);

    public override bool Equals(object obj) => obj is UiOffset other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(Min, Max);
    
    public static bool operator ==(UiOffset left, UiOffset right) => left.Equals(right);
    public static bool operator !=(UiOffset left, UiOffset right) => !(left == right);
}