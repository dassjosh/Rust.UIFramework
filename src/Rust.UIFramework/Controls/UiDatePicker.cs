using System;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Controls;

public class UiDatePicker : BaseUiControl
{
    public UiSection Anchor;
    public UiButton Command;
    public UiLabel Text;
    public UiLabel Icon; 
        
    public static UiDatePicker Create(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, DateTime date, int fontSize, UiColor textColor, UiColor backgroundColor, string openCommand, string displayFormat = "MM/dd/yyyy")
    {
        UiDatePicker control = CreateControl<UiDatePicker>();
            
        control.Anchor = builder.Anchor(parent, pos, offset);
        control.Command = builder.CommandButton(parent, pos, offset, backgroundColor, $"{openCommand} {control.Anchor.Reference.Name}");
        control.Text = builder.Label(control.Command, UiPosition.Full, new UiOffset(5, 0, 0, 0), date.ToString(displayFormat), fontSize, textColor, TextAnchor.MiddleLeft);
        control.Icon = builder.Label(control.Command, UiPosition.Right, new UiOffset(-fontSize - 4, 0, -4 , 0), "⏱", fontSize, textColor);

        return control;
    }
}