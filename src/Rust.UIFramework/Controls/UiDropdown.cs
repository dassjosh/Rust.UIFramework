using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Controls;

public class UiDropdown : BaseUiControl
{
    public UiSection Anchor;
    public UiButton Command;
    public UiLabel Text;
    public UiLabel Icon;

    public static UiDropdown Create(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, string displayValue, int fontSize, UiColor textColor, UiColor backgroundColor, string openCommand)
    {
        UiDropdown control = CreateControl<UiDropdown>();
        control.Anchor = builder.Anchor(parent, pos);
        control.Command = builder.CommandButton(parent, pos, offset, backgroundColor, $"{openCommand} {control.Anchor.Reference.Name}");
        control.Text = builder.Label(control.Command, UiPosition.Full, new UiOffset(5, 0, 0, 0), displayValue, fontSize, textColor, TextAnchor.MiddleLeft);
        control.Icon = builder.Label(control.Command, UiPosition.Right, new UiOffset(-UiHelpers.TextOffsetWidth(1, fontSize) - 4, 0, -4 , 0), "▼", fontSize, textColor);
        return control;
    }
        
    protected override void EnterPool()
    {
        base.EnterPool();
        Anchor = null;
        Command = null;
        Text = null;
        Icon = null;
    }
}