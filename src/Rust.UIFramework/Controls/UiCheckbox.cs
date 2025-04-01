using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UI;


namespace Oxide.Ext.UiFramework.Controls;

public class UiCheckbox : BaseUiControl
{
    public bool IsChecked;
    public UiButton Button;
    public UiIcon Icon;
        
    public static UiCheckbox CreateCheckbox(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, bool isChecked, string command, UiColor? checkedColor, UiColor? uncheckedColor, UiColor? buttonColor)
    {
        UiCheckbox control = CreateControl<UiCheckbox>();
        control.IsChecked = isChecked;
        control.Button = builder.Button(parent, pos, offset, buttonColor ?? UiColor.Clear, command);
        if (isChecked)
        {
            control.Icon = builder.Icon(control.Button, UiPosition.Full, default, Icons.CheckSquare, checkedColor ?? UiColors.Rust.Green);
        }
        else
        {
            control.Icon = builder.Icon(control.Button, UiPosition.Full, default, FontAwesomeRegularIcons.Square, uncheckedColor ?? UiColors.Rust.Red);
        }

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