using System;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Controls.NumberPicker
{
    public abstract class BaseNumberPicker<T> : BaseUiControl where T : struct, IConvertible, IFormattable, IComparable<T>
    {
        public UiPanel Background;
        public UiInput Input;

        protected void CreateLeftRightPicker(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, T value, int fontSize, UiColor textColor, UiColor backgroundColor, string command, InputMode mode, float buttonWidth, TextAnchor align, string numberFormat)
        {
            Background = builder.Panel(parent, pos, offset, backgroundColor);
            string displayValue = string.IsNullOrEmpty(numberFormat) ? StringCache<T>.ToString(value) : StringCache<T>.ToString(value, numberFormat);
            Input = builder.Input(Background, UiPosition.Full.SliceHorizontal(buttonWidth, 1 - buttonWidth), displayValue, fontSize, textColor, command, align, mode: mode);
        }
        
        protected void CreateUpDownPicker(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, T value, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align, InputMode mode, string numberFormat)
        {
            Background = builder.Panel(parent, pos, offset, backgroundColor);
            string displayValue = string.IsNullOrEmpty(numberFormat) ? StringCache<T>.ToString(value) : StringCache<T>.ToString(value, numberFormat);
            Input = builder.Input(Background, UiPosition.HorizontalPaddedFull, displayValue, fontSize, textColor, command, mode: mode, align: align);
        }

        protected override void EnterPool()
        {
            Background = null;
            Input = null;
        }
    }
}