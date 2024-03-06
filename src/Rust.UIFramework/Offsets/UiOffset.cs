using UnityEngine;

namespace Oxide.Ext.UiFramework.Offsets
{
    public readonly struct UiOffset
    {
        public static readonly UiOffset None = new(0, 0, 0, 0);
        public static readonly UiOffset Scaled = new(1280, 720);
        
        public readonly Vector2 Min;
        public readonly Vector2 Max;
        
        public Vector2 Size => Max - Min;
        public float Width => Max.x - Min.x;
        public float Height => Max.y - Min.y;

        public UiOffset(int width, int height) : this(-width / 2f, -height / 2f, width / 2f, height / 2f) { }
        
        public UiOffset(float xMin, float yMin, float xMax, float yMax)
        {
            Min = new Vector2(xMin, yMin);
            Max = new Vector2(xMax, yMax);
        }

        public static UiOffset CreateRect(int x, int y, int width, int height)
        {
            return new UiOffset(x, y, x + width, y + height);
        }

        public override string ToString()
        {
            return $"({Min.x:0}, {Min.y:0}) ({Max.x:0}, {Max.y:0}) WxH:({Width} x {Height})";
        }
    }
}