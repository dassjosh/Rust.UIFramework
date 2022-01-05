namespace UI.Framework.Rust.Positions
{
    public class MovableUiOffset : UiOffset
    {
        public int XMin;
        public int YMin;
        public int XMax;
        public int YMax;

        public MovableUiOffset(int x, int y, int width, int height)
        {
            XMin = x;
            YMin = y;
            XMax = x + width;
            YMax = y + height;
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
    }
}