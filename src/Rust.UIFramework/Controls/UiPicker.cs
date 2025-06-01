using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UI;


namespace Oxide.Ext.UiFramework.Controls;

public class UiPicker : BaseUiControl
{
    public UiButton Previous;
    public UiLabel Value;
    public UiButton Next;

    public static UiPicker Create(BaseUiBuilder builder, BaseUiLayout layout, string value, int fontSize, UiColor textColor, UiColor backgroundColor, string incrementCommand, string decrementCommand)
    {
        UiPicker control = CreateControl<UiPicker>();
        
        control.Next =  builder.IconButton(layout, backgroundColor, Icons.ChevronDown, decrementCommand, textColor);
        control.Value = builder.Label(layout, value, fontSize, textColor);
        control.Previous = builder.IconButton(layout, backgroundColor, Icons.ChevronUp, incrementCommand, textColor);
            
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