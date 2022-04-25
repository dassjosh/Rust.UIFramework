using Oxide.Ext.UiFramework.Extensions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Offsets
{
    public struct Offset
    {
        public Vector2Int Min;
        public Vector2Int Max;
        public readonly string MinString;
        public readonly string MaxString;

        public Offset(Vector2Int min, Vector2Int max)
        {
            Min = min;
            Max = max;
            MinString = VectorExt.ToString(Min);
            MaxString = VectorExt.ToString(Max);
        }
        
        public Offset(int xMin, int yMin, int xMax, int yMax) : this(new Vector2Int(xMin, yMin), new Vector2Int(xMax, yMax))
        {
            
        }
    }
}