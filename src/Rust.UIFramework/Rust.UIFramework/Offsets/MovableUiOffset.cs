namespace Oxide.Ext.UiFramework.Offsets
{
    public class MovableUiOffset
    {
        public int XMin;
        public int YMin;
        public int XMax;
        public int YMax;
        private readonly UiOffset _initialState;

        public MovableUiOffset(int x, int y, int width, int height)
        {
            XMin = x;
            YMin = y;
            XMax = x + width;
            YMax = y + height;
            _initialState = new UiOffset(XMin, YMin, XMax, YMax);
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

        public UiOffset ToOffset()
        {
            return new UiOffset(XMin, YMin, XMax, YMax);
        }

        public void Reset()
        {
            XMin = _initialState.Min.X;
            YMin = _initialState.Min.Y;
            XMax = _initialState.Max.X;
            YMax = _initialState.Max.Y;
        }
        
        public static implicit operator UiOffset(MovableUiOffset offset) => offset.ToOffset();
    }
}