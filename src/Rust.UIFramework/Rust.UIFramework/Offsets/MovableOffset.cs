namespace Oxide.Ext.UiFramework.Offsets
{
    public class MovableOffset
    {
        public float XMin;
        public float YMin;
        public float XMax;
        public float YMax;
        private readonly UiOffset _initialState;
        
        public MovableOffset(float xMin, float yMin, float xMax, float yMax)
        {
            XMin = xMin;
            YMin = yMin;
            XMax = xMin + xMax;
            YMax = yMin + yMax;
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
            XMin = _initialState.Min.x;
            YMin = _initialState.Min.y;
            XMax = _initialState.Max.x;
            YMax = _initialState.Max.y;
        }
        
        public static implicit operator UiOffset(MovableOffset offset) => offset.ToOffset();
    }
}