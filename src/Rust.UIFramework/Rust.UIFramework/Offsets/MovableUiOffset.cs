namespace Oxide.Ext.UiFramework.Offsets
{
    public class MovableUiOffset : UiOffset
    {
        public int XMin;
        public int YMin;
        public int XMax;
        public int YMax;
        private readonly Vector2Short _initialMin;
        private readonly Vector2Short _initialMax;

        public MovableUiOffset(int x, int y, int width, int height)
        {
            XMin = x;
            YMin = y;
            XMax = x + width;
            YMax = y + height;
            _initialMin = new Vector2Short(XMin, YMin);
            _initialMax = new Vector2Short(XMax, YMax);
        }

        public void MoveX(int pixels)
        {
            XMin += pixels;
            XMax += pixels;
        }

        public void MoveY(int pixels)
        {
            YMin += pixels;
            YMax += pixels;
        }

        public void SetWidth(int width)
        {
            XMax = XMin + width;
        }

        public void SetHeight(int height)
        {
            YMax = YMin + height;
        }

        public override Offset ToOffset()
        {
            return new Offset(XMin, YMin, XMax, YMax);
        }

        public UiOffset ToStatic()
        {
            return new StaticUiOffset(XMin, YMin, XMax, YMax);
        }

        public void Reset()
        {
            XMin = _initialMin.X;
            YMin = _initialMin.Y;
            XMax = _initialMax.X;
            YMax = _initialMax.Y;
        }
    }
}