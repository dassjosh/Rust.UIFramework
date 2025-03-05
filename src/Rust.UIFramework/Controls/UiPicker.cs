using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UI;

namespace Oxide.Ext.UiFramework.Controls;

public class UiPicker : BaseUiControl
{
    public UiButton Previous;
    public UiLabel Value;
    public UiButton Next;

    public static UiPicker Create(BaseUiBuilder builder, in UiReference parent, in UiOffset pos, string value, int fontSize, UiColor textColor, UiColor backgroundColor, float height, string incrementCommand, string decrementCommand)
    {
        UiPicker control = CreateControl<UiPicker>();
            
        UiOffset slice = pos.SliceVertical(0, (int)height * 2);
        control.Next =  builder.IconButton(parent, UiPosition.BottomLeft, slice, backgroundColor, Icons.ChevronDown, decrementCommand, textColor);
        slice = slice.MoveY(height);
        control.Value = builder.Label(parent, UiPosition.BottomLeft, slice, value, fontSize, textColor);
        slice = slice.MoveY(height);
        control.Previous = builder.IconButton(parent, UiPosition.BottomLeft, slice, backgroundColor, Icons.ChevronUp, incrementCommand, textColor);
            
        return control;
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Previous = null;
        Value = null;
        Next = null;
    }
}