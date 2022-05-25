using UnityEngine;

namespace Oxide.Ext.UiFramework.Offsets
{
    public struct UiOffset
    {
        public static readonly UiOffset None = new UiOffset(0, 0, 0, 0);
        public static readonly UiOffset Scaled = new UiOffset(1280, 720);
        
        public readonly Vector2 Min;
        public readonly Vector2 Max;

        public UiOffset(int width, int height) : this(-width / 2f, -height / 2f, width / 2f, height / 2f) { }
        
        public UiOffset(float xMin, float yMin, float xMax, float yMax)
        {
            Min = new Vector2(xMin, yMin);
            Max = new Vector2(xMax, yMax);
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
        public static UiOffset Slice(UiOffset pos, int left, int bottom, int right, int top)
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
        public static UiOffset SliceHorizontal(UiOffset pos, int left, int right)
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
        public static UiOffset SliceVertical(UiOffset pos, int bottom, int top)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;   
            return new UiOffset(max.x, min.y + bottom, max.x, max.y - top);
        }
        
        public override string ToString()
        {
            return $"({Min.x:0}, {Min.y:0}) ({Max.x:0}, {Max.y:0})";
        }
    }
}