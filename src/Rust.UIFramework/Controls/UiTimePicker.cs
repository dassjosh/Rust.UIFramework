using System;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UI;
using UnityEngine;


namespace Oxide.Ext.UiFramework.Controls;

public class UiTimePicker : BaseUiControl
{
    public UiSection Anchor;
    public UiButton Command;
    public UiLabel Text;
    public UiIcon Icon;

    public static UiTimePicker Create(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, DateTime time, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<UiReference> openCommand, string displayFormat = "hh:mm:ss tt")
    {
        UiTimePicker control = CreateControl<UiTimePicker>();
            
        control.Anchor = builder.Anchor(parent, pos, offset);
        control.Command = builder.Button(parent, pos, offset, backgroundColor, openCommand.Build(control.Anchor.Reference));
        control.Text = builder.Label(control.Command, UiPosition.Full, new UiOffset(5, 0, 0, 0), time.ToString(displayFormat), fontSize, textColor, TextAnchor.MiddleLeft);
        control.Icon = builder.Icon(control.Command, UiPosition.Right, new UiOffset(-24, 0, -4 , 0), Icons.Clock, textColor);

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