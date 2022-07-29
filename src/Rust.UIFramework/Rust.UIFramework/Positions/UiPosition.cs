using Oxide.Ext.UiFramework.Extensions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Positions
{
    public struct UiPosition
    {
        public static readonly UiPosition None = new UiPosition(0, 0, 0, 0);
        public static readonly UiPosition Full = new UiPosition(0, 0, 1, 1);
        public static readonly UiPosition HorizontalPaddedFull = Full.SliceHorizontal(0.01f, 0.99f);
        public static readonly UiPosition VerticalPaddedFull = Full.SliceVertical(0.01f, 0.99f);
        public static readonly UiPosition TopLeft = new UiPosition(0, 1, 0, 1);
        public static readonly UiPosition MiddleLeft = new UiPosition(0, .5f, 0, .5f);
        public static readonly UiPosition BottomLeft = new UiPosition(0, 0, 0, 0);
        public static readonly UiPosition TopMiddle = new UiPosition(.5f, 1, .5f, 1);
        public static readonly UiPosition MiddleMiddle = new UiPosition(.5f, .5f, .5f, .5f);
        public static readonly UiPosition BottomMiddle = new UiPosition(.5f, 0, .5f, 0);
        public static readonly UiPosition TopRight = new UiPosition(1, 1, 1, 1);
        public static readonly UiPosition MiddleRight = new UiPosition(1, .5f, 1, .5f);
        public static readonly UiPosition BottomRight = new UiPosition(1, 0, 1, 0);
        
        public static readonly UiPosition Top = new UiPosition(0, 1, 1, 1);
        public static readonly UiPosition Bottom = new UiPosition(0, 0, 1, 0);
        public static readonly UiPosition Left = new UiPosition(0, 0, 0, 1);
        public static readonly UiPosition Right = new UiPosition(1, 0, 1, 1);
        
        public static readonly UiPosition LeftHalf = new UiPosition(0, 0, 0.5f, 1);
        public static readonly UiPosition TopHalf = new UiPosition(0, 0.5f, 1, 1);
        public static readonly UiPosition RightHalf = new UiPosition(0.5f, 0, 1, 1);
        public static readonly UiPosition BottomHalf = new UiPosition(0, 0, 1, 0.5f);
        
        public readonly Vector2 Min;
        public readonly Vector2 Max;

        public UiPosition(float xMin, float yMin, float xMax, float yMax)
        {
            Min = new Vector2(xMin, yMin);
            Max = new Vector2(xMax, yMax);
        }

        public override string ToString()
        {
            return $"({Min.x:0.####}, {Min.y:0.####}) ({Max.x:0.####}, {Max.y:0.####})";
        }
    }
}