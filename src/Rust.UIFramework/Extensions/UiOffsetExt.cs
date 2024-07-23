using System;
using Oxide.Ext.UiFramework.Offsets;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Extensions;

public static class UiOffsetExt
{
    public static UiOffset SetX(in this UiOffset pos, float xMin, float xMax)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;
        return new UiOffset(xMin, min.y, xMax, max.y);
    }
        
    public static UiOffset SetY(in this UiOffset pos, float yMin, float yMax)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;
        return new UiOffset(min.x, yMin, max.x, yMax);
    }
        
    public static UiOffset SetWidth(in this UiOffset pos, float width)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;
        return new UiOffset(min.x, min.y,  min.x + width, max.y);
    }
        
    public static UiOffset SetHeight(in this UiOffset pos, float height)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;
        return new UiOffset(min.x, min.y, max.x, min.y + height);
    }
        
    public static UiOffset MoveX(in this UiOffset pos, float delta)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;
        return new UiOffset(min.x + delta, min.y, max.x + delta, max.y);
    }
        
    public static UiOffset MoveXPadded(in this UiOffset pos, float padding)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;
        float spacing = (max.x - min.x + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
        return new UiOffset(min.x + spacing, min.y, max.x + spacing, max.y);
    }
        
    public static UiOffset MoveXPaddedWithWidth(in this UiOffset pos, float padding, float width)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;
        float spacing = (max.x - min.x + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
        return new UiOffset(min.x + spacing, min.y, min.x + spacing + width, max.y);
    }
        
    public static UiOffset MoveXPaddedWithHeight(in this UiOffset pos, float padding, float height)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;
        float spacing = (max.x - min.x + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
        return new UiOffset(min.x + spacing, min.y, max.x + spacing, min.y + height);
    }
        
    public static UiOffset MoveY(in this UiOffset pos, float delta)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;
        return new UiOffset(min.x, min.y + delta, max.x, max.y + delta);
    }
        
    public static UiOffset MoveYPadded(in this UiOffset pos, float padding)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;
        float spacing = (max.y - min.y + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
        return new UiOffset(min.x, min.y + spacing, max.x, max.y + spacing);
    }

    public static UiOffset MoveYPaddedWithWidth(in this UiOffset pos, float padding, float width)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;
        float spacing = (max.y - min.y + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
        return new UiOffset(min.x, min.y + spacing, min.x + width, max.y + spacing);
    }
        
    public static UiOffset MoveYPaddedWithHeight(in this UiOffset pos, float padding, float height)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;
        int multiplier = padding < 0 ? -1 : 1;
        float spacing = (max.y - min.y + Math.Abs(padding)) * multiplier;
        return new UiOffset(min.x, min.y + spacing, max.x, min.y + spacing + height * multiplier);
    }
        
    public static UiOffset Expand(in this UiOffset pos, float amount)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;
        return new UiOffset(min.x - amount, min.y - amount, max.x + amount, max.y + amount);
    }
        
    public static UiOffset ExpandHorizontal(in this UiOffset pos, float amount)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;
        return new UiOffset(min.x - amount, min.y, max.x + amount, max.y);
    }
        
    public static UiOffset ExpandVertical(in this UiOffset pos, float amount)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;
        return new UiOffset(min.x, min.y - amount, max.x, max.y + amount);
    }
        
    public static UiOffset Shrink(in this UiOffset pos, float amount)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;
        return new UiOffset(min.x + amount, min.y + amount, max.x - amount, max.y - amount);
    }
        
    public static UiOffset ShrinkHorizontal(in this UiOffset pos, float amount)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;
        return new UiOffset(min.x + amount, min.y, max.x - amount, max.y);
    }
        
    public static UiOffset ShrinkVertical(in this UiOffset pos, float amount)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;
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
    public static UiOffset Slice(in this UiOffset pos, int left, int bottom, int right, int top)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;
        return new UiOffset(min.x + left, min.y + bottom, max.x - right, max.y - top);
    }

    /// <summary>
    /// Returns a horizontal slice of the position
    /// </summary>
    /// <param name="pos">Offset to slice</param>
    /// <param name="left">Pixels to remove from the left</param>
    /// <param name="right">>Pixels to remove from the right</param>
    /// <returns>Sliced <see cref="UiOffset"/></returns>
    public static UiOffset SliceHorizontal(in this UiOffset pos, int left, int right)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;   
        return new UiOffset(min.x + left, min.y, max.x - right,max.y);
    }

    /// <summary>
    /// Returns a vertical slice of the position
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="bottom">Pixels to remove from the bottom</param>
    /// <param name="top">Pixels to remove from the top</param>
    /// <returns>Sliced <see cref="UiOffset"/></returns>
    public static UiOffset SliceVertical(in this UiOffset pos, int bottom, int top)
    {
        Vector2 min = pos.Min;
        Vector2 max = pos.Max;   
        return new UiOffset(min.x, min.y + bottom, max.x, max.y - top);
    }
}