using System.Collections.Generic;
using System.Linq;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Layouts;

public class UiGridLayout : BaseLayout
{
    public int NumRows;
    public int NumCols;
    public LayoutAlignment ColumnAlignment;
    public LayoutAlignment RowAlignment;
    public LayoutPadding LayoutPadding;
    public UiPadding Padding;
    public readonly List<LayoutState> Elements = [];

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

    public void AddElement(BaseUiComponent element, float elementSpan)
    {
        Elements.Add(new LayoutState(element, elementSpan));
    }

    public override void CalculateElementPositions()
    {
        float numRows = CalculateNumRows();
        float scale = GetScrollViewScale(numRows, NumRows);
        float currentRow = GetRowOffset(numRows) * scale;
        
        UiOffset padding = Padding.ToOffset();

        int elementIndex = 0;
        for (int i = 0; i < numRows; i++)
        {
            GetColumnRange(elementIndex, out int maxIndex, out float numCols);
            float currentCol = GetColOffset(numCols) * scale;
            for (int index = elementIndex; index < maxIndex; index++)
            {
                LayoutState state = Elements[index];
                state.Element.SetPosition(GetUiPosition(currentCol, currentRow, state.ElementSpan, numRows, scale), padding);
                currentCol += state.ElementSpan;
                elementIndex++;
            }
        }
        
        ScaleScrollView(LayoutDirection.Vertical, scale);
    }

    private float GetRowOffset(float numRows) => GetAlignmentOffset(RowAlignment, numRows, NumRows);

    private float GetColOffset(float numColumns) => GetAlignmentOffset(ColumnAlignment, numColumns, NumCols);

    private int CalculateNumRows()
    {
        float totalSpan = Elements.Sum(e => e.ElementSpan);
        int numRows = Mathf.CeilToInt(totalSpan / NumCols);
        return ScrollView != null ? Mathf.Max(numRows, NumRows) : Mathf.Min(numRows, NumRows);
    }

    private void GetColumnRange(int startIndex, out int maxColIndex, out float colSpan)
    {
        float currentSpan = 0f;
        for(int i = startIndex; i < Elements.Count; i++)
        {
            float elementSpan = Elements[i].ElementSpan;
            if(currentSpan + elementSpan > NumCols)
            {
                maxColIndex = i;
                colSpan = currentSpan;
            }
            
            currentSpan += elementSpan;
        }
        
        maxColIndex = Elements.Count - 1;
        colSpan = currentSpan;
    }

    private UiPosition GetUiPosition(float currentCol, float currentRow, float colSpan, float totalRows, float scale)
    {
        UiPosition pos = new(currentCol / NumCols,  1f - (currentRow + 1) / totalRows * scale,  (currentCol + colSpan) / NumCols, 1f - currentRow / totalRows * scale);
        pos = pos.Shrink(LayoutPadding.Horizontal, LayoutPadding.Horizontal * scale);
        return pos;
    }
    
    public readonly struct LayoutState(BaseUiComponent element, float elementSpan)
    {
        public readonly BaseUiComponent Element = element;
        public readonly float ElementSpan = elementSpan;
    }
}