using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Layouts;

public class VerticalLayout : BaseLayout
{
    public int NumRows;
    public float CurrentRow;
    public float RowSpacing;
    public float ColPadding;
    public UiPadding? Padding;

    public static VerticalLayout Create(in UiReference reference, int numRows, float rowSpacing, float colPadding, in UiPadding? padding)
    {
        VerticalLayout layout = CreateBase<VerticalLayout>(reference);
        layout.NumRows = numRows;
        layout.CurrentRow = 0;
        layout.RowSpacing = rowSpacing;
        layout.Padding = padding;
        layout.ColPadding = colPadding;
        return layout;
    }

    public void Add(BaseUiComponent component, float rowSpan = 1f)
    {
        if (CurrentRow + rowSpan > NumRows)
        {
            // Cannot add more components to this column
            return;
        }

        component.Position = GetPosition(rowSpan);
        component.Offset = Padding?.ToOffset() ?? default;
        
        CurrentRow += rowSpan;
    }
    
    public void OffsetRow(float numRows)
    {
        CurrentRow += numRows;
    }

    private UiPosition GetPosition(float rowSpan)
    {
        UiPosition pos = new(ColPadding, 1f - (CurrentRow + rowSpan) / NumRows, 1 - ColPadding, rowSpan / NumRows);
        pos = pos.ShrinkVertical(RowSpacing);
        return pos;
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        CurrentRow = 0f;
        Padding = null;
    }
}