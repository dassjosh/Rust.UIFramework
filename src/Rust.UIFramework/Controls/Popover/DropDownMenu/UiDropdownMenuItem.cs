using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Controls.Popover;

public readonly struct UiDropdownMenuItem
{
    public readonly UiButton Button;
    public readonly UiLabel Label;

    public UiDropdownMenuItem(BaseUiBuilder builder, BaseUiLayout layout, in DropdownMenuData item, int fontSize, UiColor textColor, UiColor backgroundColor)
    {
        (Button, Label) = builder.TextButton(layout, item.DisplayName, fontSize, textColor, backgroundColor, item.Command);
    }
}