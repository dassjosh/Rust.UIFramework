namespace Oxide.Ext.UiFramework.Offsets
{
    public struct UiOffset
    {
        public static readonly UiOffset None = new UiOffset(0, 0, 0, 0);
        public static readonly UiOffset Scaled = new UiOffset(1280, 720);
        
        public readonly Vector2Short Min;
        public readonly Vector2Short Max;

        public UiOffset(int width, int height) : this(-width / 2, -height / 2, width / 2, height / 2) { }
        
        public UiOffset(int xMin, int yMin, int xMax, int yMax)
        {
            Min = new Vector2Short(xMin, yMin);
            Max = new Vector2Short(xMax, yMax);
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
            Vector2Short min = pos.Min;
            Vector2Short max = pos.Max;
            return new UiOffset(min.X + left, min.Y + bottom, max.X - right, max.Y - top);
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
            Vector2Short min = pos.Min;
            Vector2Short max = pos.Max;   
            return new UiOffset(min.X + left, min.Y, max.X - right,max.Y);
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
            Vector2Short min = pos.Min;
            Vector2Short max = pos.Max;   
            return new UiOffset(max.X, min.Y + bottom, max.X, max.Y - top);
        }
        
        public override string ToString()
        {
            return $"({Min.X:0.####}, {Min.Y:0.####}) ({Max.X:0.####}, {Max.Y:0.####})";
        }
    }
}