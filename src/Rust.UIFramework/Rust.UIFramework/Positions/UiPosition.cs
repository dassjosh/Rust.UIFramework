using UnityEngine;

namespace Oxide.Ext.UiFramework.Positions
{
    public struct UiPosition
    {
        public static readonly UiPosition None = new UiPosition(0, 0, 0, 0);
        public static readonly UiPosition Full = new UiPosition(0, 0, 1, 1);
        public static readonly UiPosition HorizontalPaddedFull = SliceHorizontal(Full, 0.01f, 0.99f);
        public static readonly UiPosition VerticalPaddedFull = SliceVertical(Full, 0.01f, 0.99f);
        public static readonly UiPosition TopLeft = new UiPosition(0, 1, 0, 1);
        public static readonly UiPosition MiddleLeft = new UiPosition(0, .5f, 0, .5f);
        public static readonly UiPosition BottomLeft = new UiPosition(0, 0, 0, 0);
        public static readonly UiPosition TopMiddle = new UiPosition(.5f, 1, .5f, 1);
        public static readonly UiPosition MiddleMiddle = new UiPosition(.5f, .5f, .5f, .5f);
        public static readonly UiPosition BottomMiddle = new UiPosition(.5f, 0, .5f, 0);
        public static readonly UiPosition TopRight = new UiPosition(1, 1, 1, 1);
        public static readonly UiPosition MiddleRight = new UiPosition(1, .5f, 1, .5f);
        public static readonly UiPosition BottomRight = new UiPosition(1, 0, 1, 0);
        
        public static readonly UiPosition Top = new UiPosition(0, 1, 1, 1);
        public static readonly UiPosition Bottom = new UiPosition(0, 0, 1, 0);
        public static readonly UiPosition Left = new UiPosition(0, 0, 0, 1);
        public static readonly UiPosition Right = new UiPosition(1, 0, 1, 1);
        
        public static readonly UiPosition LeftHalf = new UiPosition(0, 0, 0.5f, 1);
        public static readonly UiPosition TopHalf = new UiPosition(0, 0.5f, 1, 1);
        public static readonly UiPosition RightHalf = new UiPosition(0.5f, 0, 1, 1);
        public static readonly UiPosition BottomHalf = new UiPosition(0, 0, 1, 0.5f);
        
        public Vector2 Min;
        public Vector2 Max;

        public UiPosition(float xMin, float yMin, float xMax, float yMax)
        {
            Min = new Vector2(xMin, yMin);
            Max = new Vector2(xMax, yMax);
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
        public static UiPosition Slice(UiPosition pos, float xMin, float yMin, float xMax, float yMax)
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
        public static UiPosition SliceHorizontal(UiPosition pos, float xMin, float xMax)
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
        public static UiPosition SliceVertical(UiPosition pos, float yMin, float yMax)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;   
            return new UiPosition(max.x, min.y + (max.y - min.y) * yMin, max.x, min.y + (max.y - min.y) * yMax);
        }
        
        public override string ToString()
        {
            return $"({Min.x:0.####}, {Min.y:0.####}) ({Max.x:0.####}, {Max.y:0.####})";
        }
    }
}