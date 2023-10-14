namespace Oxide.Ext.UiFramework.Offsets
{
    public abstract class BaseOffset
    {
        public float XMin;
        public float YMin;
        public float XMax;
        public float YMax;
        private readonly UiOffset _initialState;
        
        protected BaseOffset(float xMin, float yMin, float xMax, float yMax)
        {
            XMin = xMin;
            YMin = yMin;
            XMax = xMin + xMax;
            YMax = yMin + yMax;
            _initialState = new UiOffset(XMin, YMin, XMax, YMax);
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
        
        public static implicit operator UiOffset(BaseOffset offset) => offset.ToOffset();
    }
}