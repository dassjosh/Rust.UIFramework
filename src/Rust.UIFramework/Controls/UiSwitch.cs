using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UI;

namespace Oxide.Ext.UiFramework.Controls;

public class UiSwitch : BaseUiControl
{
    public bool IsChecked;
    public UiButton Button;
    public UiIcon Icon;
        
    public static UiSwitch CreateSwitch(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, bool isChecked, string command, UiColor? checkedColor, UiColor? uncheckedColor, UiColor? buttonColor)
    {
        UiSwitch control = CreateControl<UiSwitch>();
        control.IsChecked = isChecked;
        control.Button = builder.CommandButton(parent, pos, offset, buttonColor ?? UiColor.Clear, command);
        control.Icon = builder.Icon(control.Button, UiPosition.Full, default, isChecked ? Icons.ToggleOn : Icons.ToggleOff, isChecked ? checkedColor ?? UiColors.Rust.Green : uncheckedColor ?? UiColors.Rust.Red);
        return control;
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        IsChecked = false;
        Button = null;
        Icon = null;
    }
}