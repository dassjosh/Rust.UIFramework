using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Offsets
{
    public struct Offset
    {
        public Vector2Short Min;
        public Vector2Short Max;
        public readonly string MinString;
        public readonly string MaxString;

        public Offset(Vector2Short min, Vector2Short max)
        {
            Min = min;
            Max = max;
            MinString = VectorExt.ToString(Min);
            MaxString = VectorExt.ToString(Max);
        }
        
        public Offset(int xMin, int yMin, int xMax, int yMax) : this(new Vector2Short(xMin, yMin), new Vector2Short(xMax, yMax))
        {
            
        }
    }
}