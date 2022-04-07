using System;

namespace Oxide.Ext.UiFramework.Positions
{
    public class MovablePosition : UiPosition
    {
        public float XMin;
        public float YMin;
        public float XMax;
        public float YMax;
        private readonly PositionState _state;

        public MovablePosition(float xMin, float yMin, float xMax, float yMax)
        {
            XMin = xMin;
            YMin = yMin;
            XMax = xMax;
            YMax = yMax;
            _state = new PositionState(XMin, YMin, XMax, YMax);
#if UiDebug
            ValidatePositions();
#endif
        }

        public override Position ToPosition()
        {
            return new Position(XMin, YMin, XMax, YMax);
        }

        public void Set(float xMin, float yMin, float xMax, float yMax)
        {
            SetX(xMin, xMax);
            SetY(yMin, yMax);
        }
        
        public void SetX(float xPos, float xMax)
        {
            XMin = xPos;
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

        public StaticUiPosition ToStatic()
        {
            return new StaticUiPosition(XMin, YMin, XMax, YMax);
        }

        public void Reset()
        {
            XMin = _state.XMin;
            YMin = _state.YMin;
            XMax = _state.XMax;
            YMax = _state.YMax;
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
    }
}