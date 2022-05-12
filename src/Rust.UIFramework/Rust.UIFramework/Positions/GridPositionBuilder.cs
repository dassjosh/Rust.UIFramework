namespace Oxide.Ext.UiFramework.Positions
{
    public class GridPositionBuilder
    {
        private readonly float _numCols;
        private readonly float _numRows;
        private int _rowHeight = 1;
        private int _rowOffset;
        private int _colWidth = 1;
        private int _colOffset;
        private float _xPad;
        private float _yPad;

        public GridPositionBuilder(int size) : this(size, size)
        {
        }

        public GridPositionBuilder(int numCols, int numRows)
        {
            _numCols = numCols;
            _numRows = numRows;
        }

        public GridPositionBuilder SetRowHeight(int height)
        {
            _rowHeight = height;
            return this;
        }

        public GridPositionBuilder SetRowOffset(int offset)
        {
            _rowOffset = offset;
            return this;
        }

        public GridPositionBuilder SetColWidth(int width)
        {
            _colWidth = width;
            return this;
        }

        public GridPositionBuilder SetColOffset(int offset)
        {
            _colOffset = offset;
            return this;
        }

        public GridPositionBuilder SetPadding(float padding)
        {
            _xPad = padding;
            _yPad = padding;
            return this;
        }

        public GridPositionBuilder SetPadding(float xPad, float yPad)
        {
            _xPad = xPad;
            _yPad = yPad;
            return this;
        }

        public GridPositionBuilder SetRowPadding(float padding)
        {
            _xPad = padding;
            return this;
        }

        public GridPositionBuilder SetColPadding(float padding)
        {
            _yPad = padding;
            return this;
        }

        public GridPosition Build()
        {
            float xMin = 0;
            float yMin = 0;
            float xMax = 0;
            float yMax = 0;

            if (_colWidth != 0)
            {
                float size = _colWidth / _numCols;
                xMax += size;
            }

            if (_colOffset != 0)
            {
                float size = _colOffset / _numCols;
                xMin += size;
                xMax += size;
            }

            if (_rowHeight != 0)
            {
                float size = _rowHeight / _numRows;
                yMax += size;
            }

            if (_rowOffset != 0)
            {
                float size = _rowOffset / _numRows;
                yMin += size;
                yMax += size;
            }

            xMin += _xPad;
            xMax -= _xPad;
            float yMinTemp = yMin; //Need to save yMin before we overwrite it
            yMin = 1 - yMax + _yPad;
            yMax = 1 - yMinTemp - _yPad;

#if UiDebug
                ValidatePositions();
#endif

            return new GridPosition(xMin, yMin, xMax, yMax, _numCols, _numRows);
        }
    }
}