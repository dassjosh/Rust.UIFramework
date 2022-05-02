using UnityEngine;

namespace Oxide.Ext.UiFramework.Positions
{
    public struct Position
    {
        public readonly Vector2 Min;
        public readonly Vector2 Max;

        public Position(float xMin, float yMin, float xMax, float yMax)
        {
            Min = new Vector2(Mathf.Clamp01(xMin), Mathf.Clamp01(yMin));
            Max = new Vector2(Mathf.Clamp01(xMax), Mathf.Clamp01(yMax));
        }
    }
}