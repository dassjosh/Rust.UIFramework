using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Controls.NumberPicker;

public class UiIncrementalNumberPicker<T> : BaseNumberPicker<T> where T : struct, IConvertible, IFormattable, IComparable<T>
{
    public List<UiButton> Subtracts;
    public List<UiButton> Adds;

    public static UiIncrementalNumberPicker<T> Create(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, T value, IList<T> increments, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiColor disabledButtonColor, ICommandBuilder<InputArg> command, ICommandBuilder<T> buttonCommand, TextAnchor align, InputMode mode, T minValue, T maxValue, float buttonWidth, string incrementFormat, string numberFormat)
    {
        UiIncrementalNumberPicker<T> control = CreateControl<UiIncrementalNumberPicker<T>>(builder);
        int incrementCount = increments.Count;
            
        control.CreateLeftRightPicker(builder, parent, pos, offset, value, fontSize, textColor, backgroundColor, command, mode, buttonWidth * incrementCount, align, numberFormat);
        List<UiButton> subtracts = control.Subtracts;
        List<UiButton> adds = control.Adds;
        UiPanel background = control.Background;
            
        for (int i = 0; i < incrementCount; i++)
        {
            T increment = increments[i];
            UiPosition subtractSlice = UiPosition.Full.SliceHorizontal(i * buttonWidth, (i + 1) * buttonWidth);
            UiPosition addSlice = UiPosition.Full.SliceHorizontal(1 - (buttonWidth * incrementCount) + i * buttonWidth, 1 - (buttonWidth * incrementCount) + (i + 1) * buttonWidth);
                
            string displayIncrement = FormatCache<T>.ToString(increment, incrementFormat);

            T subtracted = GenericMath.Subtract(value, increment);
            if (subtracted.CompareTo(minValue) >= 0)
            {
                subtracts.Add(builder.TextButton(background, subtractSlice, default, $"-{displayIncrement}", fontSize, textColor, buttonColor, buttonCommand.Build(subtracted)));
            }
            else
            {
                subtracts.Add(builder.TextButton(background, subtractSlice, default, $"-{displayIncrement}", fontSize, textColor, disabledButtonColor, string.Empty));
            }

            T added = GenericMath.Add(value, increment);
            if (added.CompareTo(maxValue) <= 0)
            {
                adds.Add(builder.TextButton(background, addSlice, default, displayIncrement, fontSize, textColor, buttonColor, buttonCommand.Build(added)));
            }
            else
            {
                adds.Add(builder.TextButton(background, addSlice, default, displayIncrement, fontSize, textColor, disabledButtonColor, string.Empty));
            }
        }

        return control;
    }

    protected override void LeavePool()
    {
        Subtracts = PluginPool.GetList<UiButton>();
        Adds = PluginPool.GetList<UiButton>();
    }

    protected override void EnterPool()
    {
        PluginPool.FreeList(Subtracts);
        PluginPool.FreeList(Adds);
    }
}