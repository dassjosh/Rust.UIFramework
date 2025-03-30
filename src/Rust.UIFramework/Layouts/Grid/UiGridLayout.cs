using System.Collections.Generic;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Layouts;

public class UiGridLayout : BaseLayout
{
    public int NumRows;
    public int NumCols;
    public LayoutAlignment ColumnAlignment;
    public LayoutAlignment RowAlignment;
    public LayoutPadding LayoutPadding;
    public UiPadding Padding;
    public readonly List<GridElement> Elements = [];

    public static UiGridLayout Create(in UiReference reference, int numCols, int numRows, GridAlignment alignment, LayoutPadding layoutPadding, in UiPadding padding)
    {
        UiGridLayout layout = CreateBase<UiGridLayout>(reference);
        layout.NumCols = numCols;
        layout.NumRows = numRows;
        layout.ColumnAlignment = alignment.ColumnAlignment;
        layout.RowAlignment = alignment.RowAlignment;
        layout.LayoutPadding = layoutPadding;
        layout.Padding = padding;
        return layout;
    }

    public override void AddElement(BaseUiComponent element) => AddElement(element, 1f);

    public void AddElement(BaseUiComponent element, float elementSpan) => Elements.Add(new GridElement(element, elementSpan));

    public override void CalculateElementPositions()
    {
        List<GridRow> rows = GetRows();
        int numRows = rows.Count;
        float scale = GetScrollViewScale(numRows, NumRows);
        float currentRow = GetRowOffset(numRows) * scale;
        
        UiOffset padding = Padding.ToOffset();

        int elementIndex = 0;
        for (int i = 0; i < numRows; i++)
        {
            GridRow row = rows[i];
            float currentCol = GetColOffset(row.TotalSpan);
            for (int index = 0; index < row.Elements.Count; index++)
            {
                GridElement element = row.Elements[index];
                UiPosition pos = GetUiPosition(currentCol, currentRow, element.ColSpan, scale);
                element.Element.SetPosition(pos, padding);
                currentCol += element.ColSpan;
                elementIndex++;
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
        List<GridRow> rows = UiFrameworkPool.GetList<GridRow>();

        float colSpan = 0f;
        List<GridElement> row = UiFrameworkPool.GetList<GridElement>();
        for (int index = 0; index < Elements.Count; index++)
        {
            GridElement state = Elements[index];
            if (row.Count != 0 && colSpan + state.ColSpan > NumCols)
            {
                rows.Add(new GridRow(colSpan, row));
                colSpan = 0f;
                row = UiFrameworkPool.GetList<GridElement>();
            }
            
            row.Add(state);
            colSpan += state.ColSpan;
        }
        
        rows.Add(new GridRow(colSpan, row));
        return rows;
    }

    private UiPosition GetUiPosition(float currentCol, float currentRow, float colSpan, float scale)
    {
        UiPosition pos = new(currentCol / NumCols,  1f - (currentRow + 1) * scale / NumRows,  (currentCol + colSpan) / NumCols, 1f - currentRow * scale / NumRows);
        pos = pos.Shrink(LayoutPadding.Horizontal, LayoutPadding.Vertical * scale);
        return pos;
    }

    private static void FreeGridRows(List<GridRow> rows)
    {
        for (int index = 0; index < rows.Count; index++)
        {
            GridRow row = rows[index];
            UiFrameworkPool.FreeList(row.Elements);
        }
        
        UiFrameworkPool.FreeList(rows);
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
    }
}