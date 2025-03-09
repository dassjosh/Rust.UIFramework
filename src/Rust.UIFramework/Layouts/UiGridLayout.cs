using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Layouts;

public class UiGridLayout : BaseLayout
{
    public int NumRows;
    public int NumCols;
    public float CurrentRow;
    public float CurrentCol;
    public float RowSpacing;
    public float ColSpacing;
    public UiPadding? Padding;

    public static UiGridLayout Create(in UiReference reference, int numCols, int numRows, float rowSpacing, float colSpacing, in UiPadding? padding)
    {
        UiGridLayout layout = CreateBase<UiGridLayout>(reference, numCols * numRows);
        layout.NumCols = numCols;
        layout.NumRows = numRows;
        layout.RowSpacing = rowSpacing;
        layout.ColSpacing = colSpacing;
        layout.Padding = padding;
        return layout;
    }

    public override void Add(BaseUiComponent component) => Add(component, 1f);

    public override void Add(BaseUiComponent component, float elementSpan)
    {
        if (CurrentCol + elementSpan > NumCols)
        {
            NextRow();
        }

        component.Position = GetPosition(elementSpan);
        component.Offset = Padding?.ToOffset() ?? default;
        CurrentCol += elementSpan;
    }
    
    public void NextRow(float rowSpan = 1f)
    {
        CurrentCol -= NumCols;
        CurrentRow += rowSpan;
    }

    public override void OffsetElements(float numElements) => OffsetColumn(numElements);

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
        UiPosition pos = new(CurrentCol / NumCols,  1f - (CurrentRow + 1) / NumRows,  (CurrentCol + colSpan) / NumCols, 1f - CurrentRow / NumRows);
        pos = pos.Shrink(ColSpacing, RowSpacing);
        return pos;
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        CurrentRow = 0f;
        CurrentCol = 0f;
    }
}