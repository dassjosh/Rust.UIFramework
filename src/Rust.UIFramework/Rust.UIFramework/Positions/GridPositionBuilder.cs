using System;

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
            if (numCols <= 0) throw new ArgumentOutOfRangeException(nameof(numCols));
            if (numRows <= 0) throw new ArgumentOutOfRangeException(nameof(numCols));
            _numCols = numCols;
            _numRows = numRows;
        }

        public GridPositionBuilder SetRowHeight(int height)
        {
            if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));
            _rowHeight = height;
            return this;
        }

        public GridPositionBuilder SetRowOffset(int offset)
        {
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
            _rowOffset = offset;
            return this;
        }

        public GridPositionBuilder SetColWidth(int width)
        {
            if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
            _colWidth = width;
            return this;
        }

        public GridPositionBuilder SetColOffset(int offset)
        {
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
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
            float yMin = 1 - _rowHeight / _numRows;
            float xMax = _colWidth / _numCols;
            float yMax = 1;

            if (_colOffset != 0)
            {
                float size = _colOffset / _numCols;
                xMin += size;
                xMax += size;
            }

            if (_rowOffset != 0)
            {
                float size = _rowOffset / _numRows;
                yMin -= size;
                yMax -= size;
            }

            xMin += _xPad;
            xMax -= _xPad;
            yMin += _yPad;
            yMax -= _yPad;

            return new GridPosition(xMin, yMin, xMax, yMax, _numCols, _numRows);
        }
    }
}