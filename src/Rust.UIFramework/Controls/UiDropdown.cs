using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UI;
using UnityEngine;


namespace Oxide.Ext.UiFramework.Controls;

public class UiDropdown : BaseUiControl
{
    public UiSection Anchor;
    public UiButton Command;
    public UiLabel Text;
    public UiIcon Icon;

    public static UiDropdown Create(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, string displayValue, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<UiReference> openCommand)
    {
        UiDropdown control = CreateControl<UiDropdown>(builder.PluginPool);
        control.Anchor = builder.Anchor(parent, pos);
        control.Command = builder.Button(parent, pos, offset, backgroundColor, openCommand.Build(control.Anchor.Reference));
        control.Text = builder.Label(control.Command, UiPosition.Full, new UiOffset(5, 0, 0, 0), displayValue, fontSize, textColor, TextAnchor.MiddleLeft);
        control.Icon = builder.Icon(control.Command, UiPosition.Right, new UiOffset(-20, 0, -4 , 0), Icons.CaretDown, textColor);
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