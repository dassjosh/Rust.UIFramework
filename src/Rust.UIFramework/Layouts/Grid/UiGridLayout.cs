using System.Collections.Generic;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Layouts;

public class UiGridLayout : BaseUiLayout, IFixedElementsLayout
{
    public int NumRows;
    public int NumCols;
    public LayoutAlignment ColumnAlignment;
    public LayoutAlignment RowAlignment;
    public UiPadding Padding;
    public readonly List<GridElement> Elements = [];
    public int NumElements => NumRows * NumCols;

    public UiGridLayout Init(int numCols, int numRows, GridAlignment alignment, in UiPadding padding)
    {
        NumCols = numCols;
        NumRows = numRows;
        ColumnAlignment = alignment.ColumnAlignment;
        RowAlignment = alignment.RowAlignment;
        Padding = padding;
        return this;
    }

    public UiGridLayout SetNumRows(int numRows)
    {
        NumRows = numRows;
        return this;
    }

    public UiGridLayout SetNumCols(int numCols)
    {
        NumCols = numCols;
        return this;
    }

    public UiGridLayout SetColumnAlignment(LayoutAlignment alignment)
    {
        ColumnAlignment = alignment;
        return this;
    }

    public UiGridLayout SetRowAlignment(LayoutAlignment alignment)
    {
        RowAlignment = alignment;
        return this;
    }

    public UiGridLayout SetPadding(in UiPadding padding)
    {
        Padding = padding;
        return this;
    }

    public override void AddElement(BaseUiComponent element) => AddElement(element, 1f);

    public void AddElement(BaseUiComponent element, float elementSpan) => Elements.Add(new GridElement(element, elementSpan));

    public override void CalculateElementPositions()
    {
        List<GridRow> rows = GetRows();
        int numRows = rows.Count;
        float scale = GetScrollViewScale(numRows, NumRows);
        float currentRow = GetRowOffset(numRows) * scale;
        
        for (int i = 0; i < numRows; i++)
        {
            GridRow row = rows[i];
            float currentCol = GetColOffset(row.TotalSpan);
            for (int index = 0; index < row.Elements.Count; index++)
            {
                GridElement element = row.Elements[index];
                UiPosition pos = GetUiPosition(currentCol, currentRow, element.ColSpan, scale);
                element.Element.SetPosition(pos).SetOffsetPadding(Padding);
                currentCol += element.ColSpan;
            }

            currentRow += 1f;
        }
        
        ScaleScrollView(LayoutDirection.Vertical, scale);
        FreeGridRows(rows);
    }

    private float GetRowOffset(float numRows) => GetAlignmentOffset(RowAlignment, numRows, NumRows);

    private float GetColOffset(float numColumns) => GetAlignmentOffset(ColumnAlignment, numColumns, NumCols);

    private List<GridRow> GetRows()
    {
        List<GridRow> rows = PluginPool.GetList<GridRow>();

        float colSpan = 0f;
        List<GridElement> row = PluginPool.GetList<GridElement>();
        for (int index = 0; index < Elements.Count; index++)
        {
            GridElement state = Elements[index];
            if (row.Count != 0 && colSpan + state.ColSpan > NumCols)
            {
                rows.Add(new GridRow(colSpan, row));
                colSpan = 0f;
                row = PluginPool.GetList<GridElement>();
            }
            
            row.Add(state);
            colSpan += state.ColSpan;
        }
        
        rows.Add(new GridRow(colSpan, row));
        return rows;
    }

    private UiPosition GetUiPosition(float currentCol, float currentRow, float colSpan, float scale)
    {
        return new UiPosition(currentCol / NumCols,  1f - (currentRow + 1) * scale / NumRows,  (currentCol + colSpan) / NumCols, 1f - currentRow * scale / NumRows);
    }

    private void FreeGridRows(List<GridRow> rows)
    {
        for (int index = 0; index < rows.Count; index++)
        {
            GridRow row = rows[index];
            PluginPool.FreeList(row.Elements);
        }
        
        PluginPool.FreeList(rows);
    }
    
    public readonly struct GridElement(BaseUiComponent element, float elementSpan)
    {
        public readonly BaseUiComponent Element = element;
        public readonly float ColSpan = elementSpan;
    }

    private readonly struct GridRow(float totalSpan, List<GridElement> elements)
    {
        public readonly float TotalSpan = totalSpan;
        public readonly List<GridElement> Elements = elements;
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Elements.Clear();
        ColumnAlignment = default;
        RowAlignment = default;
        NumRows = default;
        NumCols = default;
        Padding = default;
    }
}