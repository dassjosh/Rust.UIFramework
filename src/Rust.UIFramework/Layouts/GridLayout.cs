using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Layouts;

public class GridLayout : BaseLayout
{
    public int NumRows;
    public int NumCols;
    public float CurrentRow;
    public float CurrentCol;
    public float RowSpacing;
    public float ColSpacing;
    public UiPadding? Padding;

    public static GridLayout Create(in UiReference reference, int numRows, int numCols, float rowSpacing, float colSpacing, in UiPadding? padding)
    {
        GridLayout layout = CreateBase<GridLayout>(reference);
        layout.NumRows = numRows;
        layout.NumCols = numCols;
        layout.RowSpacing = rowSpacing;
        layout.ColSpacing = colSpacing;
        layout.Padding = padding;
        return layout;
    }

    public void Add(BaseUiComponent component, float colSpan = 1f)
    {
        if (CurrentCol + colSpan > NumCols)
        {
            NextRow();
        }

        component.Position = GetPosition(colSpan);
        component.Offset = Padding?.ToOffset() ?? default;
    }
    
    public void NextRow(float rowSpan = 1f)
    {
        CurrentCol -= NumCols;
        CurrentRow += rowSpan;
    }

    public void OffsetColumn(float numCols)
    {
        CurrentCol += numCols;
    }
    
    public void OffsetRow(float numRows)
    {
        CurrentRow += numRows;
    }

    private UiPosition GetPosition(float colSpan)
    {
        UiPosition pos = new(CurrentCol / NumCols,  1f - (CurrentRow + 1) / NumRows, colSpan / NumCols, 1f - CurrentRow / NumRows);
        pos = pos.Shrink(ColSpacing, RowSpacing);
        return pos;
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        CurrentRow = 0f;
        CurrentCol = 0f;
        Padding = null;
    }
}