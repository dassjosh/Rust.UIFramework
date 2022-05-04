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
#if UiDebug
            ValidatePositions();
#endif
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
#if UiDebug
            ValidatePositions();
#endif
        }

        public void SetY(float yMin, float yMax)
        {
            YMin = yMin;
            YMax = yMax;
#if UiDebug
            ValidatePositions();
#endif
        }

        public void MoveX(float delta)
        {
            XMin += delta;
            XMax += delta;
#if UiDebug
            ValidatePositions();
#endif
        }

        public void MoveXPadded(float padding)
        {
            float spacing = (XMax - XMin + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
            XMin += spacing;
            XMax += spacing;
#if UiDebug
            ValidatePositions();
#endif
        }

        public void MoveY(float delta)
        {
            YMin += delta;
            YMax += delta;
#if UiDebug
            ValidatePositions();
#endif
        }

        public void MoveYPadded(float padding)
        {
            float spacing = (YMax - YMin + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
            YMin += spacing;
            YMax += spacing;
#if UiDebug
            ValidatePositions();
#endif
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
            XMax = _initialState.Min.x;
            YMax = _initialState.Max.y;
        }

#if UiDebug
        protected void ValidatePositions()
        {
            if (XMin < 0 || XMin > 1)
            {
                PrintError($"[{GetType().Name}] XMin is out or range at: {XMin}");
            }

            if (XMax > 1 || XMax < 0)
            {
                PrintError($"[{GetType().Name}] XMax is out or range at: {XMax}");
            }

            if (YMin < 0 || YMin > 1)
            {
                PrintError($"[{GetType().Name}] YMin is out or range at: {YMin}");
            }

            if (YMax > 1 || YMax < 0)
            {
                PrintError($"[{GetType().Name}] YMax is out or range at: {YMax}");
            }
        }

        private void PrintError(string format)
        {
            _ins.PrintError(format);
        }
#endif

        public override string ToString()
        {
            return $"{XMin.ToString()} {YMin.ToString()} {XMax.ToString()} {YMax.ToString()}";
        }
        
        public static implicit operator UiPosition(MovablePosition pos) => pos.ToPosition();
    }
}