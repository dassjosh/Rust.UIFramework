using System;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Positions;

public class GridPositionBuilder
{
    private float _numCols;
    private float _numRows;
    private int _rowHeight = 1;
    private int _rowOffset;
    private int _colWidth = 1;
    private int _colOffset;
    private float _horizontalPadding;
    private float _verticalPadding;
    
    public GridPositionBuilder(int size) : this(size, size) { }

    public GridPositionBuilder(int numCols, int numRows)
    {
        if (numCols <= 0) throw new ArgumentOutOfRangeException(nameof(numCols));
        if (numRows <= 0) throw new ArgumentOutOfRangeException(nameof(numCols));
        _numCols = numCols;
        _numRows = numRows;
    }
    
    public GridPositionBuilder() { }
    
    /// <summary>
    /// Sets the number of columns in the grid
    /// </summary>
    /// <param name="numCols"></param>
    /// <returns>This</returns>
    /// <exception cref="ArgumentOutOfRangeException">Throw if numCols is less than 1</exception>
    public GridPositionBuilder SetNumCols(int numCols)
    {
        if (numCols <= 0) throw new ArgumentOutOfRangeException(nameof(numCols));
        _numCols = numCols;
        return this;
    }
    
    /// <summary>
    /// Sets the number of rows in the grid
    /// </summary>
    /// <param name="numRows"></param>
    /// <returns>This</returns>
    /// <exception cref="ArgumentOutOfRangeException">Throw if numRows is less than 1</exception>
    public GridPositionBuilder SetNumRows(int numRows)
    {
        if (numRows <= 0) throw new ArgumentOutOfRangeException(nameof(numRows));
        _numRows = numRows;
        return this;
    }

    /// <summary>
    /// Sets how many rows the grid will span
    /// </summary>
    /// <param name="height"></param>
    /// <returns>This</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thow if height is less than 1</exception>
    public GridPositionBuilder SetRowHeight(int height)
    {
        if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));
        _rowHeight = height;
        return this;
    }

    /// <summary>
    /// Sets how many rows to offset into in the grid based on the total grid height
    /// </summary>
    /// <param name="offset"></param>
    /// <returns>This</returns>
    /// <exception cref="ArgumentOutOfRangeException">Throw if offset is less than 0</exception>
    public GridPositionBuilder SetRowOffset(int offset)
    {
        if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
        _rowOffset = offset;
        return this;
    }

    /// <summary>
    /// Sets how many columns the grid will span
    /// </summary>
    /// <param name="width"></param>
    /// <returns>This</returns>
    /// <exception cref="ArgumentOutOfRangeException">Throw if width is less than 1</exception>
    public GridPositionBuilder SetColWidth(int width)
    {
        if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
        _colWidth = width;
        return this;
    }

    /// <summary>
    /// Sets how many columns to offset into in the grid based on the total grid height
    /// </summary>
    /// <param name="offset"></param>
    /// <returns>This</returns>
    /// <exception cref="ArgumentOutOfRangeException">Throw if offset is less than 0</exception>
    public GridPositionBuilder SetColOffset(int offset)
    {
        if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
        _colOffset = offset;
        return this;
    }

    /// <summary>
    /// Sets the padding of the grid
    /// </summary>
    /// <param name="padding"></param>
    /// <returns>This</returns>
    public GridPositionBuilder SetPadding(float padding)
    {
        _horizontalPadding = padding;
        _verticalPadding = padding;
        return this;
    }

    /// <summary>
    /// Sets the padding of the grid
    /// </summary>
    /// <param name="horizontal"></param>
    /// <param name="vertical"></param>
    /// <returns></returns>
    public GridPositionBuilder SetPadding(float horizontal, float vertical)
    {
        _horizontalPadding = horizontal;
        _verticalPadding = vertical;
        return this;
    }

    [Obsolete("Use SetHorizontalPadding instead")]
    public GridPositionBuilder SetRowPadding(float padding) => SetVerticalPadding(padding);

    [Obsolete("Use SetVerticalPadding instead")]
    public GridPositionBuilder SetColPadding(float padding) => SetHorizontalPadding(padding);
    
    /// <summary>
    /// Sets the horizontal padding of the grid
    /// </summary>
    /// <param name="padding"></param>
    /// <returns>This</returns>
    public GridPositionBuilder SetHorizontalPadding(float padding)
    {
        _horizontalPadding = padding;
        return this;
    }
    
    /// <summary>
    /// Sets the vertical padding of the grid
    /// </summary>
    /// <param name="padding"></param>
    /// <returns>This</returns>
    public GridPositionBuilder SetVerticalPadding(float padding)
    {
        _verticalPadding = padding;
        return this;
    }
    
    private void GetPosition(out float xMin, out float yMin, out float xMax, out float yMax)
    {
        xMin = 0;
        yMin = 1 - _rowHeight / _numRows;
        xMax = _colWidth / _numCols;
        yMax = 1;

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
    }

    public GridPosition Build()
    {
        GetPosition(out float xMin, out float yMin, out float xMax, out float yMax);
        return new GridPosition(xMin, yMin, xMax, yMax, _horizontalPadding, _verticalPadding, _numCols / _colWidth, _numRows/ _rowHeight);
    }

    public UiPosition BuildPosition()
    {
        GetPosition(out float xMin, out float yMin, out float xMax, out float yMax);
        return new UiPosition(xMin, yMin, xMax, yMax).WithPadding(new UiPadding(_horizontalPadding, _verticalPadding));
    }
}