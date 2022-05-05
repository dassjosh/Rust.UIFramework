using UnityEngine;

namespace Oxide.Ext.UiFramework.Positions
{
    public struct UiPosition
    {
        public static readonly UiPosition FullPosition = new UiPosition(0, 0, 1, 1);
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
        
        public readonly Vector2 Min;
        public readonly Vector2 Max;

        public UiPosition(float xMin, float yMin, float xMax, float yMax)
        {
            Min = new Vector2(Mathf.Clamp01(xMin), Mathf.Clamp01(yMin));
            Max = new Vector2(Mathf.Clamp01(xMax), Mathf.Clamp01(yMax));
        }

        /// <summary>
        /// Returns a slice of the position
        /// </summary>
        /// <param name="xMin">% of the xMax - xMin distance added to xMin</param>
        /// <param name="yMin">% of the yMax - yMin distance added to yMin</param>
        /// <param name="xMax">>% of the xMax - xMin distance added to xMin</param>
        /// <param name="yMax">% of the yMax - yMin distance added to yMin</param>
        /// <returns>Sliced <see cref="UiPosition"/></returns>
        public UiPosition Slice(float xMin, float yMin, float xMax, float yMax)
        {
            Vector2 distance = Max - Min;
            return new UiPosition(Min.x + distance.x * xMin, Min.y + distance.y * yMin, Min.x + distance.x * xMax, Min.y + distance.y * yMax);
        }
        
        /// <summary>
        /// Returns a horizontal slice of the position
        /// </summary>
        /// <param name="xMin">% of the xMax - xMin distance added to xMin</param>
        /// <param name="xMax">>% of the xMax - xMin distance added to xMin</param>
        /// <returns>Sliced <see cref="UiPosition"/></returns>
        public UiPosition SliceHorizontal(float xMin, float xMax)
        {
            return new UiPosition(Min.x + (Max.x - Min.x) * xMin, Min.y, Min.x + (Max.x - Min.x) * xMax, Max.y);
        }
        
        /// <summary>
        /// Returns a vertical slice of the position
        /// </summary>
        /// <param name="yMin">% of the yMax - yMin distance added to yMin</param>
        /// <param name="yMax">% of the yMax - yMin distance added to yMin</param>
        /// <returns>Sliced <see cref="UiPosition"/></returns>
        public UiPosition SliceVertical(float yMin, float yMax)
        {
            return new UiPosition(Min.x, Min.y + (Max.y - Min.y) * yMin, Max.x, Min.y + (Max.y - Min.y) * yMax);
        }
        
        public override string ToString()
        {
            return $"({Min.x:0.####}, {Min.y:0.####}) ({Max.x:0.####}, {Max.y:0.####})";
        }
    }
}