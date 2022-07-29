using UnityEngine;

namespace Oxide.Ext.UiFramework.Offsets
{
    public struct UiOffset
    {
        public static readonly UiOffset None = new UiOffset(0, 0, 0, 0);
        public static readonly UiOffset Scaled = new UiOffset(1280, 720);
        
        public readonly Vector2 Min;
        public readonly Vector2 Max;

        public UiOffset(int width, int height) : this(-width / 2f, -height / 2f, width / 2f, height / 2f) { }
        
        public UiOffset(float xMin, float yMin, float xMax, float yMax)
        {
            Min = new Vector2(xMin, yMin);
            Max = new Vector2(xMax, yMax);
        }

        public Vector2 Size => Max - Min;
        public float Width => Max.x - Min.x;
        public float Height => Max.y - Min.y;

        public override string ToString()
        {
            return $"({Min.x:0}, {Min.y:0}) ({Max.x:0}, {Max.y:0}) WxH:({Width} x {Height})";
        }
    }
}