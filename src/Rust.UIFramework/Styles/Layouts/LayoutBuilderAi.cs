using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Styles;

/// <summary>
/// Represents a cell in the layout grid.
/// </summary>
public readonly struct CellPosition : IEquatable<CellPosition>
{
    public readonly int RowIndex;
    public readonly int ColumnIndex;
    public readonly int RowSpan;
    public readonly int ColumnSpan;

    public CellPosition(int rowIndex, int columnIndex, int rowSpan, int columnSpan)
    {
        RowIndex = rowIndex;
        ColumnIndex = columnIndex;
        RowSpan = rowSpan;
        ColumnSpan = columnSpan;
    }

    public override bool Equals(object obj)
    {
        return obj is CellPosition position && Equals(position);
    }

    public bool Equals(CellPosition other)
    {
        return RowIndex == other.RowIndex &&
               ColumnIndex == other.ColumnIndex &&
               RowSpan == other.RowSpan &&
               ColumnSpan == other.ColumnSpan;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(RowIndex, ColumnIndex, RowSpan, ColumnSpan);
    }

    public static bool operator ==(CellPosition left, CellPosition right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(CellPosition left, CellPosition right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents a column span within a row.
/// </summary>
public readonly struct ColumnSpanInfo : IEquatable<ColumnSpanInfo>
{
    public readonly int ColumnIndex;
    public readonly int ColumnSpan;
    public readonly int ActualColumnSpan;

    public ColumnSpanInfo(int columnIndex, int columnSpan, int actualColumnSpan)
    {
        ColumnIndex = columnIndex;
        ColumnSpan = columnSpan;
        ActualColumnSpan = actualColumnSpan;
    }

    public override bool Equals(object obj)
    {
        return obj is ColumnSpanInfo info && Equals(info);
    }

    public bool Equals(ColumnSpanInfo other)
    {
        return ColumnIndex == other.ColumnIndex &&
               ColumnSpan == other.ColumnSpan &&
               ActualColumnSpan == other.ActualColumnSpan;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ColumnIndex, ColumnSpan, ActualColumnSpan);
    }

    public static bool operator ==(ColumnSpanInfo left, ColumnSpanInfo right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ColumnSpanInfo left, ColumnSpanInfo right)
    {
        return !(left == right);
    }
}

/// <summary>
/// A class to pregenerate layout UI positions and offsets in a grid-like structure.
/// </summary>
public class LayoutBuilderAi
{
    private readonly List<float> _rowPositions = [];
    private readonly List<float> _columnPositions = [];
    private readonly Dictionary<CellPosition, UiPadding> _cellPaddings = new();
    private readonly Dictionary<int, List<ColumnSpanInfo>> _rowColumnSpans = new(); // rowIndex -> List of column spans

    private readonly UiPadding _defaultPadding;
    private readonly UiPadding _defaultRowPadding;
    private readonly UiPadding _defaultColumnPadding;
    private readonly EmptyColumnAdjustments _emptyColumnAdjustment;

    /// <summary>
    /// Creates a new LayoutBuilder with the specified default paddings and empty column adjustment.
    /// </summary>
    /// <param name="defaultPadding">Default padding for all cells.</param>
    /// <param name="rowPadding">Default padding for rows.</param>
    /// <param name="columnPadding">Default padding for columns.</param>
    /// <param name="emptyColumnAdjustment">How to adjust columns when not all columns in a row are filled.</param>
    public LayoutBuilderAi(UiPadding defaultPadding, UiPadding rowPadding, UiPadding columnPadding, EmptyColumnAdjustments emptyColumnAdjustment)
    {
        _defaultPadding = defaultPadding;
        _defaultRowPadding = rowPadding;
        _defaultColumnPadding = columnPadding;
        _emptyColumnAdjustment = emptyColumnAdjustment;
    }

    /// <summary>
    /// Creates a new LayoutBuilder with no default padding and left alignment for empty columns.
    /// </summary>
    public LayoutBuilderAi() : this(UiPadding.None, UiPadding.None, UiPadding.None, EmptyColumnAdjustments.Left)
    {
    }

    /// <summary>
    /// Adds a row to the layout.
    /// </summary>
    /// <param name="size">The size of the row relative to the total layout (0-1).</param>
    /// <returns>The index of the added row.</returns>
    public int AddRow(float size)
    {
        if (size <= 0)
            throw new ArgumentException("Row size must be greater than 0.", nameof(size));

        float total = CalculateTotalSize(_rowPositions) + size;
        if (total > 1.0f)
            throw new ArgumentException($"Adding this row would exceed the maximum layout size (1.0). Current total: {CalculateTotalSize(_rowPositions)}, Requested: {size}");

        _rowPositions.Add(size);

        int rowIndex = _rowPositions.Count - 1;
        _rowColumnSpans[rowIndex] = new List<ColumnSpanInfo>();

        // Apply default row padding to all cells in this row
        for (int colIndex = 0; colIndex < _columnPositions.Count; colIndex++)
        {
            SetCellPadding(rowIndex, colIndex, 1, 1, _defaultRowPadding);
        }

        return rowIndex;
    }

    /// <summary>
    /// Adds a column to the layout.
    /// </summary>
    /// <param name="size">The size of the column relative to the total layout (0-1).</param>
    /// <returns>The index of the added column.</returns>
    public int AddColumn(float size)
    {
        if (size <= 0)
            throw new ArgumentException("Column size must be greater than 0.", nameof(size));

        float total = CalculateTotalSize(_columnPositions) + size;
        if (total > 1.0f)
            throw new ArgumentException($"Adding this column would exceed the maximum layout size (1.0). Current total: {CalculateTotalSize(_columnPositions)}, Requested: {size}");

        _columnPositions.Add(size);

        int colIndex = _columnPositions.Count - 1;

        // Apply default column padding to all cells in this column
        for (int rowIndex = 0; rowIndex < _rowPositions.Count; rowIndex++)
        {
            SetCellPadding(rowIndex, colIndex, 1, 1, _defaultColumnPadding);
        }

        return colIndex;
    }

    /// <summary>
    /// Adds a cell to the layout, optionally spanning multiple columns.
    /// </summary>
    /// <param name="rowIndex">The row index.</param>
    /// <param name="columnIndex">The column index.</param>
    /// <param name="columnSpan">The number of columns to span.</param>
    /// <returns>The column index of the added cell.</returns>
    public int AddCell(int rowIndex, int columnIndex, int columnSpan = 1)
    {
        if (rowIndex < 0 || rowIndex >= _rowPositions.Count)
            throw new ArgumentOutOfRangeException(nameof(rowIndex));
        if (columnIndex < 0 || columnIndex + columnSpan > _columnPositions.Count)
            throw new ArgumentOutOfRangeException(nameof(columnIndex));
        if (columnSpan <= 0)
            throw new ArgumentException("Column span must be greater than 0.", nameof(columnSpan));

        // Store the column span information for this row
        _rowColumnSpans[rowIndex].Add(new ColumnSpanInfo(columnIndex, columnSpan, columnSpan));

        return columnIndex;
    }

    /// <summary>
    /// Sets padding for a specific cell.
    /// </summary>
    /// <param name="rowIndex">The starting row index.</param>
    /// <param name="columnIndex">The starting column index.</param>
    /// <param name="rowSpan">The number of rows to span.</param>
    /// <param name="columnSpan">The number of columns to span.</param>
    /// <param name="padding">The padding to apply.</param>
    private void SetCellPadding(int rowIndex, int columnIndex, int rowSpan, int columnSpan, UiPadding padding)
    {
        if (rowIndex < 0 || rowIndex + rowSpan > _rowPositions.Count)
            throw new ArgumentOutOfRangeException(nameof(rowIndex));
        if (columnIndex < 0 || columnIndex + columnSpan > _columnPositions.Count)
            throw new ArgumentOutOfRangeException(nameof(columnIndex));
        if (rowSpan <= 0)
            throw new ArgumentException("Row span must be greater than 0.", nameof(rowSpan));
        if (columnSpan <= 0)
            throw new ArgumentException("Column span must be greater than 0.", nameof(columnSpan));

        CellPosition cellPosition = new(rowIndex, columnIndex, rowSpan, columnSpan);
        _cellPaddings[cellPosition] = padding;
    }

    /// <summary>
    /// Gets a layout section for a specific cell or group of cells, adjusted for empty columns.
    /// </summary>
    /// <param name="rowIndex">The starting row index.</param>
    /// <param name="columnIndex">The starting column index.</param>
    /// <param name="rowSpan">The number of rows to span.</param>
    /// <param name="columnSpan">The number of columns to span.</param>
    /// <returns>A LayoutSection with the position and offset for the specified cell(s).</returns>
    public LayoutSection GetSection(int rowIndex, int columnIndex, int rowSpan = 1, int columnSpan = 1)
    {
        if (rowIndex < 0 || rowIndex + rowSpan > _rowPositions.Count)
            throw new ArgumentOutOfRangeException(nameof(rowIndex));
        if (columnIndex < 0 || columnIndex + columnSpan > _columnPositions.Count)
            throw new ArgumentOutOfRangeException(nameof(columnIndex));
        if (rowSpan <= 0)
            throw new ArgumentException("Row span must be greater than 0.", nameof(rowSpan));
        if (columnSpan <= 0)
            throw new ArgumentException("Column span must be greater than 0.", nameof(columnSpan));

        // Get the adjusted column index and span
        (int adjustedColIndex, int adjustedColSpan) = GetAdjustedColumnValues(rowIndex, columnIndex, columnSpan);

        // Calculate the position
        float xMin = GetPositionStart(adjustedColIndex, _columnPositions);
        float yMin = GetPositionStart(rowIndex, _rowPositions);
        float xMax = GetPositionEnd(adjustedColIndex, adjustedColSpan, _columnPositions);
        float yMax = GetPositionEnd(rowIndex, rowSpan, _rowPositions);

        UiPosition position = new(xMin, yMin, xMax, yMax);

        // Get the padding for this cell or group of cells
        CellPosition cellPosition = new(rowIndex, columnIndex, rowSpan, columnSpan);
        UiPadding padding = _cellPaddings.GetValueOrDefault(cellPosition, _defaultPadding);

        // Convert padding to offset
        UiOffset offset = ConvertPaddingToOffset(padding);

        return new LayoutSection
        {
            Position = position,
            Offset = offset
        };
    }

    /// <summary>
    /// Calculates the adjusted column index and span based on the empty column adjustment.
    /// </summary>
    /// <param name="rowIndex">The row index.</param>
    /// <param name="columnIndex">The original column index.</param>
    /// <param name="columnSpan">The original column span.</param>
    /// <returns>A tuple containing the adjusted column index and span.</returns>
    private (int, int) GetAdjustedColumnValues(int rowIndex, int columnIndex, int columnSpan)
    {
        // If no cells have been added to this row or empty column adjustment is set to Left, return original values
        if (!_rowColumnSpans.ContainsKey(rowIndex) || _rowColumnSpans[rowIndex].Count == 0 || _emptyColumnAdjustment == EmptyColumnAdjustments.Left)
        {
            return (columnIndex, columnSpan);
        }

        // Calculate the total used column span in this row
        int totalUsedColumns = 0;
        foreach (ColumnSpanInfo spanInfo in _rowColumnSpans[rowIndex])
        {
            totalUsedColumns += spanInfo.ColumnSpan;
        }

        // Calculate the number of empty columns
        int emptyColumns = _columnPositions.Count - totalUsedColumns;

        // If no empty columns, return original values
        if (emptyColumns <= 0)
        {
            return (columnIndex, columnSpan);
        }

        // Calculate the adjusted column index based on the empty column adjustment
        int adjustedColumnIndex = columnIndex;

        switch (_emptyColumnAdjustment)
        {
            case EmptyColumnAdjustments.Middle:
                // Distribute empty columns evenly on both sides
                adjustedColumnIndex += emptyColumns / 2;
                break;

            case EmptyColumnAdjustments.Right:
                // Put all empty columns on the left
                adjustedColumnIndex += emptyColumns;
                break;
        }

        return (adjustedColumnIndex, columnSpan);
    }

    private static float CalculateTotalSize(List<float> sizes)
    {
        float total = 0;
        foreach (float size in sizes)
        {
            total += size;
        }

        return total;
    }

    private static float GetPositionStart(int index, List<float> sizes)
    {
        float pos = 0;
        for (int i = 0; i < index; i++)
        {
            pos += sizes[i];
        }

        return pos;
    }

    private static float GetPositionEnd(int index, int span, List<float> sizes)
    {
        float pos = GetPositionStart(index, sizes);
        for (int i = 0; i < span; i++)
        {
            pos += sizes[index + i];
        }

        return pos;
    }

    private static UiOffset ConvertPaddingToOffset(UiPadding padding)
    {
        return new UiOffset(padding.Left, padding.Bottom, -padding.Right, -padding.Top);
    }

    /// <summary>
    /// Builds the layout and returns a PrebuiltLayoutCuiStyle.
    /// </summary>
    /// <returns>A PrebuiltLayoutCuiStyle containing all the layout sections.</returns>
    public PrebuiltLayoutCuiStyle Build()
    {
        // Check if we have any rows and columns
        if (_rowPositions.Count == 0 || _columnPositions.Count == 0)
            throw new InvalidOperationException("Cannot build layout without any rows and columns.");

        // Check if the total size is too small
        float totalRowSize = CalculateTotalSize(_rowPositions);
        float totalColumnSize = CalculateTotalSize(_columnPositions);

        if (totalRowSize < 0.99f || totalColumnSize < 0.99f)
        {
            // This is not a hard error, just a warning
            Debug.LogWarning($"Layout does not fill the entire space. Total row size: {totalRowSize}, Total column size: {totalColumnSize}");
        }

        // Apply empty column adjustment to all rows

        A
            
        // Create the layout sections for each cell
        Dictionary<(int, int, int, int), LayoutSection> sections = new();

        for (int r = 0; r < _rowPositions.Count; r++)
        {
            for (int c = 0; c < _columnPositions.Count; c++)
            {
                sections[(r, c, 1, 1)] = GetSection(r, c);
            }
        }

        // Add sections for cells with custom padding (for spans)
        foreach (CellPosition key in _cellPaddings.Keys)
        {
            int rowIndex = key.RowIndex;
            int columnIndex = key.ColumnIndex;
            int rowSpan = key.RowSpan;
            int columnSpan = key.ColumnSpan;

            if (rowSpan > 1 || columnSpan > 1)
            {
                sections[(rowIndex, columnIndex, rowSpan, columnSpan)] = GetSection(rowIndex, columnIndex, rowSpan, columnSpan);
            }
        }

        return new PrebuiltLayoutCuiStyle(sections);
    }
}