namespace UI.Framework.Rust.Positions
{
    public class GridPosition : MovablePosition
    {
        public float NumCols { get; }
        public float NumRows { get; }
        private int _xSize = 1;
        private int _xOffset;
        private int _ySize = 1;
        private int _yOffset;
        private float _xPad;
        private float _yPad;

        public GridPosition(int size) : this(size, size)
        {
        }

        public GridPosition(int numCols, int numRows)
        {
            NumCols = numCols;
            NumRows = numRows;
        }

        public GridPosition SetRowLength(int size, int offset = 0)
        {
            _xSize = size;
            _xOffset = offset;
            return this;
        }

        public new GridPosition SetX(float xMin, float xMax)
        {
            XMin = xMin;
            XMax = xMax;
            return this;
        }

        public GridPosition SetColLength(int size, int offset = 0)
        {
            _ySize = size;
            _yOffset = offset;
            return this;
        }

        public new GridPosition SetY(float yMin, float yMax)
        {
            YMin = yMin;
            YMax = yMax;
            return this;
        }

        public GridPosition SetPadding(float padding)
        {
            _xPad = padding;
            _yPad = padding;
            return this;
        }

        public GridPosition SetPadding(float xPad, float yPad)
        {
            _xPad = xPad;
            _yPad = yPad;
            return this;
        }

        public GridPosition SetRowPadding(float padding)
        {
            _xPad = padding;
            return this;
        }

        public GridPosition SetColPadding(float padding)
        {
            _yPad = padding;
            return this;
        }

        public void MoveCols(int cols)
        {
            XMin += cols / NumCols;
            XMax += cols / NumCols;

#if UiDebug
                ValidatePositions();
#endif
        }

        public void MoveRows(int rows)
        {
            YMin += rows / NumRows;
            YMax += rows / NumRows;

#if UiDebug
                ValidatePositions();
#endif
        }

        public GridPosition Build()
        {
            if (_xSize != 0)
            {
                float size = _xSize / NumCols;
                XMax += size;
            }

            if (_xOffset != 0)
            {
                float size = _xOffset / NumCols;
                XMin += size;
                XMax += size;
            }

            if (_ySize != 0)
            {
                float size = _ySize / NumRows;
                YMax += size;
            }

            if (_yOffset != 0)
            {
                float size = _yOffset / NumRows;
                YMin += size;
                YMax += size;
            }

            XMin += _xPad;
            XMax -= _xPad;
            float yMin = YMin; //Need to save yMin before we overwrite it
            YMin = 1 - YMax + _yPad;
            YMax = 1 - yMin - _yPad;

#if UiDebug
                ValidatePositions();
#endif

            return this;
        }

        public new GridPosition Clone()
        {
            return (GridPosition)MemberwiseClone();
        }
    }
}