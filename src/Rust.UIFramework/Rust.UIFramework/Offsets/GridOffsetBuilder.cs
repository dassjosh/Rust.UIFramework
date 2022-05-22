using System;

namespace Oxide.Ext.UiFramework.Offsets
{
    public class GridOffsetBuilder
    {
        private readonly int _numCols;
        private readonly int _numRows;
        private readonly UiOffset _area;
        private int _rowHeight = 1;
        private int _rowOffset;
        private int _colWidth = 1;
        private int _colOffset;
        private int _xPad;
        private int _yPad;

        public GridOffsetBuilder(int size, UiOffset area) : this(size, size, area)
        {
            
        }

        public GridOffsetBuilder(int numCols, int numRows, UiOffset area)
        {
            if (numCols <= 0) throw new ArgumentOutOfRangeException(nameof(numCols));
            if (numRows <= 0) throw new ArgumentOutOfRangeException(nameof(numCols));
            _numCols = numCols;
            _numRows = numRows;
            _area = area;
        }

        public GridOffsetBuilder SetRowHeight(int height)
        {
            if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));
            _rowHeight = height;
            return this;
        }

        public GridOffsetBuilder SetRowOffset(int offset)
        {
            if (offset <= 0) throw new ArgumentOutOfRangeException(nameof(offset));
            _rowOffset = offset;
            return this;
        }

        public GridOffsetBuilder SetColWidth(int width)
        {
            if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
            _colWidth = width;
            return this;
        }

        public GridOffsetBuilder SetColOffset(int offset)
        {
            if (offset <= 0) throw new ArgumentOutOfRangeException(nameof(offset));
            _colOffset = offset;
            return this;
        }

        public GridOffsetBuilder SetPadding(int padding)
        {
            _xPad = padding;
            _yPad = padding;
            return this;
        }

        public GridOffsetBuilder SetPadding(int xPad, int yPad)
        {
            _xPad = xPad;
            _yPad = yPad;
            return this;
        }

        public GridOffsetBuilder SetRowPadding(int padding)
        {
            _xPad = padding;
            return this;
        }

        public GridOffsetBuilder SetColPadding(int padding)
        {
            _yPad = padding;
            return this;
        }

        public GridOffset Build()
        {
            int xMin = _area.Min.X;
            int yMin = _area.Max.Y - _rowHeight / _numRows  * (_area.Max.Y - _area.Min.Y);
            int xMax = _area.Min.X + _colWidth / _numCols * (_area.Max.X - _area.Min.X);
            int yMax = _area.Max.Y;

            if (_colOffset != 0)
            {
                int size = _colOffset / _numCols;
                xMin += size;
                xMax += size;
            }

            if (_rowOffset != 0)
            {
                int size = _rowOffset / _numRows;
                yMin += size;
                yMax += size;
            }

            xMin += _xPad;
            xMax -= _xPad;
            yMin += _yPad;
            yMax -= _yPad;

            return new GridOffset(xMin, yMin, xMax, yMax, _numCols, _numRows, _area);
        }
    }
}