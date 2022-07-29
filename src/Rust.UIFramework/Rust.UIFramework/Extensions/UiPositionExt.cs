using System;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Extensions
{
    //Define:ExtensionMethods
    public static class UiPositionExt
    {
        public static UiPosition SetX(this UiPosition pos, float xMin, float xMax)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(xMin, min.y, xMax, max.y);
        }
        
        public static UiPosition SetY(this UiPosition pos, float yMin, float yMax)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x, yMin, max.x, yMax);
        }
        
        public static UiPosition MoveX(this UiPosition pos, float delta)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x + delta, min.y, max.x + delta, max.y);
        }
        
        public static UiPosition MoveXPadded(this UiPosition pos, float padding)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            float spacing = (max.x - min.x + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
            return new UiPosition(min.x + spacing, min.y, max.x + spacing, max.y);
        }
        
        public static UiPosition MoveY(this UiPosition pos, float delta)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x, min.y + delta, max.x, max.y + delta);
        }
        
        public static UiPosition MoveYPadded(this UiPosition pos, float padding)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            float spacing = (max.y - min.y + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
            return new UiPosition(min.x, min.y + spacing, max.x, max.y + spacing);
        }
        
        public static UiPosition Expand(this UiPosition pos, float amount)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x - amount, min.y - amount, max.x + amount, max.y + amount);
        }
        
        public static UiPosition ExpandHorizontal(this UiPosition pos, float amount)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x - amount, min.y, max.x + amount, max.y);
        }
        
        public static UiPosition ExpandVertical(this UiPosition pos, float amount)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x, min.y - amount, max.x, max.y + amount);
        }
        
        public static UiPosition Shrink(this UiPosition pos, float amount)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x + amount, min.y + amount, max.x - amount, max.y - amount);
        }
        
        public static UiPosition ShrinkHorizontal(this UiPosition pos, float amount)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x + amount, min.y, max.x - amount, max.y);
        }
        
        public static UiPosition ShrinkVertical(this UiPosition pos, float amount)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x, min.y + amount, max.x, max.y - amount);
        }
        
        /// <summary>
        /// Returns a slice of the position
        /// </summary>
        /// <param name="pos">Position to slice</param>
        /// <param name="xMin">% of the xMax - xMin distance added to xMin</param>
        /// <param name="yMin">% of the yMax - yMin distance added to yMin</param>
        /// <param name="xMax">>% of the xMax - xMin distance added to xMin</param>
        /// <param name="yMax">% of the yMax - yMin distance added to yMin</param>
        /// <returns>Sliced <see cref="UiPosition"/></returns>
        public static UiPosition Slice(this UiPosition pos, float xMin, float yMin, float xMax, float yMax)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            Vector2 distance = max - min;
            return new UiPosition(min.x + distance.x * xMin, min.y + distance.y * yMin, min.x + distance.x * xMax, min.y + distance.y * yMax);
        }
        
        /// <summary>
        /// Returns a horizontal slice of the position
        /// </summary>
        /// <param name="pos">Position to slice</param>
        /// <param name="xMin">% of the xMax - xMin distance added to xMin</param>
        /// <param name="xMax">>% of the xMax - xMin distance added to xMin</param>
        /// <returns>Sliced <see cref="UiPosition"/></returns>
        public static UiPosition SliceHorizontal(this UiPosition pos, float xMin, float xMax)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;   
            return new UiPosition(min.x + (max.x - min.x) * xMin, min.y, min.x + (max.x - min.x) * xMax, max.y);
        }
        
        /// <summary>
        /// Returns a vertical slice of the position
        /// </summary>
        /// <param name="pos">Position to slice</param>
        /// <param name="yMin">% of the yMax - yMin distance added to yMin</param>
        /// <param name="yMax">% of the yMax - yMin distance added to yMin</param>
        /// <returns>Sliced <see cref="UiPosition"/></returns>
        public static UiPosition SliceVertical(this UiPosition pos, float yMin, float yMax)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;   
            return new UiPosition(min.x, min.y + (max.y - min.y) * yMin, max.x, min.y + (max.y - min.y) * yMax);
        }
    }
}