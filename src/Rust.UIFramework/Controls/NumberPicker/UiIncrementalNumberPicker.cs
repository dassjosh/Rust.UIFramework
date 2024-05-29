using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Controls.NumberPicker;

public class UiIncrementalNumberPicker<T> : BaseNumberPicker<T> where T : struct, IConvertible, IFormattable, IComparable<T>
{
    public List<UiButton> Subtracts;
    public List<UiButton> Adds;

    public static UiIncrementalNumberPicker<T> Create(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, T value, IList<T> increments, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiColor disabledButtonColor, string command, TextAnchor align, InputMode mode, T minValue, T maxValue, float buttonWidth, string incrementFormat, string numberFormat)
    {
        UiIncrementalNumberPicker<T> control = CreateControl<UiIncrementalNumberPicker<T>>();
        int incrementCount = increments.Count;
            
        control.CreateLeftRightPicker(builder, parent, pos, offset, value, fontSize, textColor, backgroundColor, command, mode, buttonWidth * incrementCount, align, numberFormat);
        List<UiButton> subtracts = control.Subtracts;
        List<UiButton> adds = control.Adds;
        UiPanel background = control.Background;
            
        for (int i = 0; i < incrementCount; i++)
        {
            T increment = increments[i];
            string incrementValue = StringCache<T>.ToString(increment);
            UiPosition subtractSlice = UiPosition.Full.SliceHorizontal(i * buttonWidth, (i + 1) * buttonWidth);
            UiPosition addSlice = UiPosition.Full.SliceHorizontal(1 - (buttonWidth * incrementCount) + i * buttonWidth, 1 - (buttonWidth * incrementCount) + (i + 1) * buttonWidth);
                
            string displayIncrement = StringCache<T>.ToString(increment, incrementFormat);
                
            if (GenericMath.Subtract(value, increment).CompareTo(minValue) >= 0)
            {
                subtracts.Add(builder.TextButton(background, subtractSlice,  $"-{displayIncrement}", fontSize, textColor, buttonColor, $"{command} -{incrementValue}"));
            }
            else
            {
                subtracts.Add(builder.TextButton(background, subtractSlice, $"-{displayIncrement}", fontSize, textColor, disabledButtonColor, string.Empty));
            }

            if (GenericMath.Add(value, increment).CompareTo(maxValue) <= 0)
            {
                adds.Add(builder.TextButton(background, addSlice, displayIncrement, fontSize, textColor, buttonColor, $"{command} {incrementValue}"));
            }
            else
            {
                adds.Add(builder.TextButton(background, addSlice, displayIncrement, fontSize, textColor, disabledButtonColor, string.Empty));
            }
        }

        return control;
    }

    protected override void LeavePool()
    {
        Subtracts = UiFrameworkPool.GetList<UiButton>();
        Adds = UiFrameworkPool.GetList<UiButton>();
    }

    protected override void EnterPool()
    {
        UiFrameworkPool.FreeList(Subtracts);
        UiFrameworkPool.FreeList(Adds);
    }
}