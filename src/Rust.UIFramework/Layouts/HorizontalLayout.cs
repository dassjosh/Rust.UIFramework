using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Layouts;

public class HorizontalLayout : BaseLayout
{
    public int NumCols;
    public float CurrentCol;
    public float ColSpacing;
    public float RowPadding;
    public UiPadding? Padding;

    public static HorizontalLayout Create(in UiReference reference, int numCols, float colSpacing, float rowPadding, in UiPadding? padding)
    {
        HorizontalLayout layout = CreateBase<HorizontalLayout>(reference);
        layout.NumCols = numCols;
        layout.CurrentCol = 0;
        layout.ColSpacing = colSpacing;
        layout.Padding = padding;
        layout.RowPadding = rowPadding;
        return layout;
    }

    public void Add(BaseUiComponent component, float colSpan = 1f)
    {
        if (CurrentCol + colSpan > NumCols)
        {
            // Cannot add more components to this row
            return;
        }

        component.Position = GetPosition(colSpan);
        component.Offset = Padding?.ToOffset() ?? default;
        
        CurrentCol += colSpan;
    }
    
    public void OffsetColumn(float numCols)
    {
        CurrentCol += numCols;
    }

    private UiPosition GetPosition(float colSpan)
    {
        UiPosition pos = new(CurrentCol / NumCols, RowPadding, colSpan / NumCols, 1 - RowPadding);
        pos = pos.ShrinkHorizontal(ColSpacing);
        return pos;
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        CurrentCol = 0;
        Padding = null;
    }
}