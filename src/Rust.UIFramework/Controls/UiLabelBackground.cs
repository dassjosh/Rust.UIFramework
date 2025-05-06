﻿using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Controls;

public class UiLabelBackground : BaseUiControl
{
    public UiLabel Label;
    public UiPanel Background;
        
    public static UiLabelBackground Create(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter)
    {
        UiLabelBackground control = CreateControl<UiLabelBackground>();
        control.Background = builder.Panel(parent, pos, offset, backgroundColor);
        control.Label = builder.Label(control.Background, UiPosition.HorizontalPaddedFull, text, fontSize, textColor, align);
        return control;
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Label = null;
        Background = null;
    }
}