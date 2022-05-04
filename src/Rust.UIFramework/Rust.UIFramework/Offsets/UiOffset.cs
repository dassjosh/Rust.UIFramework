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
        
        public override string ToString()
        {
            return $"({Min.X:0.####}, {Min.Y:0.####}) ({Max.X:0.####}, {Max.Y:0.####})";
        }
    }
}