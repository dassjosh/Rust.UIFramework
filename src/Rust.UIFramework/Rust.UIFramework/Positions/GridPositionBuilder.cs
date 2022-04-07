namespace Oxide.Ext.UiFramework.Positions
{
    public class GridPositionBuilder
    {
        private float _xMin;
        private float _yMin;
        private float _xMax;
        private float _yMax;
        
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
            if (_rowHeight != 0)
            {
                float size = _rowHeight / _numCols;
                _xMax += size;
            }

            if (_rowOffset != 0)
            {
                float size = _rowOffset / _numCols;
                _xMin += size;
                _xMax += size;
            }

            if (_colWidth != 0)
            {
                float size = _colWidth / _numRows;
                _yMax += size;
            }

            if (_colOffset != 0)
            {
                float size = _colOffset / _numRows;
                _yMin += size;
                _yMax += size;
            }

            _xMin += _xPad;
            _xMax -= _xPad;
            float yMin = _yMin; //Need to save yMin before we overwrite it
            _yMin = 1 - _yMax + _yPad;
            _yMax = 1 - yMin - _yPad;

#if UiDebug
            ValidatePositions();
#endif

            return new GridPosition(_xMin, _yMin, _xMax, _yMax, _numCols, _numRows);
        }
    }
}