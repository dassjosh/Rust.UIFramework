using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Controls.NumberPicker
{
    public class UiNumberPicker : BaseNumberPicker<int>
    {
        public UiButton Subtract;
        public UiButton Add;

        public static UiNumberPicker Create(UiBuilder builder, BaseUiComponent parent, UiPosition pos, UiOffset offset, int value, int fontSize, int buttonFontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiColor disabledButtonColor, string command, string incrementCommand, string decrementCommand, int minValue, int maxValue, float buttonWidth, TextAnchor align, InputMode mode, NumberPickerMode numberMode, string numberFormat)
        {
            UiNumberPicker control = CreateControl<UiNumberPicker>();

            if (numberMode == NumberPickerMode.LeftRight)
            {
                control.CreateLeftRightPicker(builder, parent, pos, offset, value, fontSize, textColor, backgroundColor, command, mode, buttonWidth, align, numberFormat);
                UiPosition subtractPosition = UiPosition.Full.SliceHorizontal(0, buttonWidth);
                UiPosition addPosition = UiPosition.Full.SliceHorizontal(1 - buttonWidth, 1);
                control.CreateAdd(builder, value, maxValue, addPosition, default(UiOffset), "+", buttonFontSize, textColor, buttonColor, disabledButtonColor, incrementCommand);
                control.CreateSubtract(builder, value, minValue, subtractPosition, default(UiOffset), "-", buttonFontSize, textColor, buttonColor, disabledButtonColor, decrementCommand);
            }
            else
            {
                int width = UiHelpers.TextOffsetWidth(1, buttonFontSize, 4);
                UiOffset pickerOffset = offset.SliceHorizontal(0, width);
                control.CreateUpDownPicker(builder, parent, pos, pickerOffset, value, fontSize, textColor, backgroundColor, command, align, mode, numberFormat);
                UiOffset buttonOffset = new UiOffset(0, 0, width, 0);
                control.CreateAdd(builder, value, maxValue, new UiPosition(1, 0.5f, 1, 1), buttonOffset, "<b>˄</b>", buttonFontSize, textColor, buttonColor, disabledButtonColor, incrementCommand);
                control.CreateSubtract(builder, value, minValue, new UiPosition(1, 0, 1, 0.5f), buttonOffset, "<b>˅</b>", buttonFontSize, textColor, buttonColor, disabledButtonColor, decrementCommand);
            }
            
            return control;
        }

        private void CreateSubtract(UiBuilder builder, int value, int minValue, UiPosition position, UiOffset offset, string text, int fontSize, UiColor textColor, UiColor buttonColor, UiColor disabledButtonColor, string command)
        {
            if (value > minValue)
            {
                Subtract = builder.TextButton(Background, position, offset, text, fontSize, textColor, buttonColor, command);
            }
            else
            {
                Subtract = builder.TextButton(Background, position, offset, text, fontSize, textColor.MultiplyAlpha(0.5f), disabledButtonColor, null);
            }
        }

        private void CreateAdd(UiBuilder builder, int value, int maxValue, UiPosition position, UiOffset offset, string text, int fontSize, UiColor textColor, UiColor buttonColor, UiColor disabledButtonColor, string command)
        {
            if (value < maxValue)
            {
                Add = builder.TextButton(Background, position, offset, text, fontSize, textColor, buttonColor,  command);
            }
            else
            {
                Add = builder.TextButton(Background, position, offset, text, fontSize, textColor.MultiplyAlpha(0.5f), disabledButtonColor, null);
            }
        }

        protected override void EnterPool()
        {
            Subtract = null;
            Add = null;
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
}