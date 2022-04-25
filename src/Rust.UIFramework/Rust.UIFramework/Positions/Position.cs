using Oxide.Ext.UiFramework.Extensions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Positions
{
    public struct Position
    {
        public readonly Vector2 Min;
        public readonly Vector2 Max;
        public readonly string MinString;
        public readonly string MaxString;

        public Position(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
            MinString = VectorExt.ToString(min);
            MaxString = VectorExt.ToString(max);
        }

        public Position(float xMin, float yMin, float xMax, float yMax) : this(new Vector2(xMin, yMin), new Vector2(xMax, yMax))
        {
            
        }
    }
}