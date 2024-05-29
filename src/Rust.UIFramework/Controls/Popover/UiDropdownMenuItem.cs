using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Controls.Data;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Controls.Popover;

public class UiDropdownMenuItem : BaseUiControl
{
    public UiButton Button;
    public UiLabel Label;

    public static UiDropdownMenuItem Create(UiBuilder builder, in UiOffset position, in DropdownMenuData item, int fontSize, UiColor textColor, UiColor backgroundColor, string selectedCommand)
    {
        UiDropdownMenuItem control = CreateControl<UiDropdownMenuItem>();
            
        control.Button = builder.CommandButton(builder.Root, UiPosition.TopLeft, position, backgroundColor, $"{selectedCommand} {item.CommandArgs}");
        control.Label = builder.Label(control.Button, UiPosition.Full, item.DisplayName, fontSize, textColor);

        return control;
    }
}