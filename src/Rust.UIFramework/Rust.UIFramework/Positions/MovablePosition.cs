using System;

namespace Oxide.Ext.UiFramework.Positions
{
    public class MovablePosition
    {
        public float XMin;
        public float YMin;
        public float XMax;
        public float YMax;
        private readonly UiPosition _initialState;

        public MovablePosition(float xMin, float yMin, float xMax, float yMax)
        {
            XMin = xMin;
            YMin = yMin;
            XMax = xMax;
            YMax = yMax;
            _initialState = new UiPosition(XMin, YMin, XMax, YMax);
        }

        public UiPosition ToPosition()
        {
            return new UiPosition(XMin, YMin, XMax, YMax);
        }

        public void Set(float xMin, float yMin, float xMax, float yMax)
        {
            SetX(xMin, xMax);
            SetY(yMin, yMax);
        }
        
        public void SetX(float xMin, float xMax)
        {
            XMin = xMin;
            XMax = xMax;
        }

        public void SetY(float yMin, float yMax)
        {
            YMin = yMin;
            YMax = yMax;
        }

        public void MoveX(float delta)
        {
            XMin += delta;
            XMax += delta;
        }

        public void MoveXPadded(float padding)
        {
            float spacing = (XMax - XMin + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
            XMin += spacing;
            XMax += spacing;
        }

        public void MoveY(float delta)
        {
            YMin += delta;
            YMax += delta;
        }

        public void MoveYPadded(float padding)
        {
            float spacing = (YMax - YMin + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
            YMin += spacing;
            YMax += spacing;
        }

        public void Expand(float amount)
        {
            ExpandHorizontal(amount);
            ExpandVertical(amount);
        }
        
        public void ExpandHorizontal(float amount)
        {
            XMin -= amount;
            XMax += amount;
        }
        
        public void ExpandVertical(float amount)
        {
            YMin -= amount;
            YMax += amount;
        }
        
        public void Shrink(float amount)
        {
            Expand(-amount);
        }
        
        public void ShrinkHorizontal(float amount)
        {
            ExpandHorizontal(-amount);
        }
        
        public void ShrinkVertical(float amount)
        {
            ExpandVertical(-amount);
        }

        public void Reset()
        {
            XMin = _initialState.Min.x;
            YMin = _initialState.Min.y;
            XMax = _initialState.Max.x;
            YMax = _initialState.Max.y;
        }

        public override string ToString()
        {
            return $"{XMin.ToString()} {YMin.ToString()} {XMax.ToString()} {YMax.ToString()}";
        }
        
        public static implicit operator UiPosition(MovablePosition pos) => pos.ToPosition();
    }
}