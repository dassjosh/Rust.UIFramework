namespace Oxide.Ext.UiFramework.Offsets
{
    public struct Offset
    {
        public Vector2Short Min;
        public Vector2Short Max;

        public Offset(int xMin, int yMin, int xMax, int yMax)
        {
            Min = new Vector2Short(xMin, yMin);
            Max = new Vector2Short(xMax, yMax);
        }
    }
}