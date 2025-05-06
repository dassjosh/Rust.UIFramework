using System;

namespace Oxide.Ext.UiFramework.Offsets;

public class GridOffsetBuilder
{
    private readonly int _numCols;
    private readonly int _numRows;
    private readonly UiOffset _area;
    private readonly float _width;
    private readonly float _height;
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
        _width = area.Max.x - area.Min.x;
        _height = area.Max.y - area.Min.y;
    }
        
    public GridOffsetBuilder SetRowHeight(int height)
    {
        if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));
        _rowHeight = height;
        return this;
    }
        
    public GridOffsetBuilder SetRowOffset(int offset)
    {
        if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
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
        if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
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
        float xMin = _area.Min.x;
        float yMin = _area.Max.y - _rowHeight / (float)_numRows * _height;
        float xMax = _colWidth / (float)_numCols * _width;
        float yMax = _rowHeight / (float)_numRows * _height;
            
        if (_colOffset != 0)
        {
            int size = (int)(_colOffset / (float)_numCols * _width);
            xMin += size;
            xMax += size;
        }
            
        if (_rowOffset != 0)
        {
            int size = (int)(_rowOffset / (float)_numRows * _height);
            yMin += size;
            yMax += size;
        }
            
        xMin += _xPad;
        xMax -= _xPad * 2;
        yMin += _yPad;
        yMax -= _yPad * 2;
            
        return new GridOffset(xMin, yMin, xMax, yMax, _numCols, _numRows, _width, _height);
    }
}