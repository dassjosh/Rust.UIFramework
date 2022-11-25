using Oxide.Core;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Controls.NumberPicker;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Controls.Popover
{
    //Define:ExcludeFile
    public class UiColorPickerMenu : BasePopoverControl
    {
        public UiPanel HexInputBackground;
        public UiInput HexInput;
        public UiNumberPicker RedPicker;
        public UiNumberPicker GreenPicker;
        public UiNumberPicker BluePicker;
        public UiNumberPicker AlphaPicker;
        
        private const int MenuPadding = 4;
        private const int ItemPadding = 2;

        private int _width;
        private int _height;

        //private int _colorLabelWidth;
        private int _colorInputWidth;
        private int _hexInputWidth;
        
        public static UiColorPickerMenu Create(string parentName, UiColor selectedColor, int fontSize, UiColor textColor, UiColor buttonColor, UiColor backgroundColor, UiColor pickerBackgroundColor, UiColor pickerDisabledColor, string command, ColorPickerMode mode, PopoverPosition position, string menuSprite, InputMode inputMode)
        {
            UiColorPickerMenu control = CreateControl<UiColorPickerMenu>();

            int labelHeight = UiHelpers.TextOffsetHeight(fontSize);
            int rgbaTextHeight = labelHeight + 2;

            control._colorInputWidth = UiHelpers.TextOffsetWidth(6, fontSize);
            control._hexInputWidth = UiHelpers.TextOffsetWidth(10, fontSize, 4);
            int numColors = mode == ColorPickerMode.RGBA ? 4 : 3;

            control._width = MenuPadding * 2 + control._hexInputWidth + control._colorInputWidth * numColors + ItemPadding * numColors;
            control._height = MenuPadding * 2 + rgbaTextHeight * 3 + ItemPadding * 2;
            
            CreateBuilder(control, parentName, new Vector2Int(control._width, control._height), backgroundColor, position, menuSprite);
            UiBuilder builder = control.Builder;
            
            float colorYMin = -MenuPadding - rgbaTextHeight * 2;
            float colorYMax = -MenuPadding - rgbaTextHeight;

            UiOffset input = new UiOffset(MenuPadding, colorYMin, MenuPadding + control._hexInputWidth, colorYMax);
            string color = mode == ColorPickerMode.RGBA ? selectedColor.ToHexRGBA() : selectedColor.ToHexRGB();
            int charLimit = mode == ColorPickerMode.RGBA ? 8 : 6;
            control.HexInputBackground = builder.Panel(builder.Root, UiPosition.TopLeft, input, pickerBackgroundColor);
            control.HexInput = builder.Input(control.HexInputBackground, UiPosition.Full, new UiOffset(4, 0, 0, 0), color, fontSize, textColor, command, TextAnchor.MiddleLeft, charLimit, inputMode);
            builder.Label(builder.Root, UiPosition.TopLeft, control.GetLabelPosition(input, labelHeight), "Hex", fontSize, textColor);
            input = input.MoveXPadded(ItemPadding);
            
            input = input.SetWidth(control._colorInputWidth);
            //control.RedPicker = builder.NumberPicker(builder.Root, UiPosition.TopLeft, input, (int)(selectedColor.Color.r * 255f), fontSize, fontSize / 2, textColor, pickerBackgroundColor, buttonColor, pickerDisabledColor, command, 0, 255, 0, TextAnchor.MiddleCenter, inputMode, NumberPickerMode.UpDown);
            builder.Label(builder.Root, UiPosition.TopLeft, control.GetLabelPosition(control.RedPicker.Background.Offset, labelHeight), "R", fontSize, textColor);
            input = input.MoveXPadded(ItemPadding);
            
            //control.GreenPicker = builder.NumberPicker(builder.Root, UiPosition.TopLeft, input, (int)(selectedColor.Color.g * 255f), fontSize, fontSize / 2, textColor, pickerBackgroundColor, buttonColor, pickerDisabledColor, null, 0, 255, 0, TextAnchor.MiddleCenter, inputMode, NumberPickerMode.UpDown);
            builder.Label(builder.Root, UiPosition.TopLeft, control.GetLabelPosition(control.GreenPicker.Background.Offset, labelHeight), "G", fontSize, textColor);
            input = input.MoveXPadded(ItemPadding);
            
            //control.BluePicker = builder.NumberPicker(builder.Root, UiPosition.TopLeft, input, (int)(selectedColor.Color.b * 255f), fontSize, fontSize / 2, textColor, pickerBackgroundColor, buttonColor, pickerDisabledColor, null, 0, 255, 0, TextAnchor.MiddleCenter, inputMode, NumberPickerMode.UpDown);
            builder.Label(builder.Root, UiPosition.TopLeft, control.GetLabelPosition(control.BluePicker.Background.Offset, labelHeight), "B", fontSize, textColor);
            
            if (mode == ColorPickerMode.RGBA)
            {
                input = input.MoveXPadded(ItemPadding);
                //control.AlphaPicker = builder.NumberPicker(builder.Root, UiPosition.TopLeft, input, (int)(selectedColor.Color.a * 255f), fontSize, fontSize / 2, textColor, pickerBackgroundColor, buttonColor, pickerDisabledColor, null, 0, 255, 0, TextAnchor.MiddleCenter, inputMode, NumberPickerMode.UpDown);
                builder.Label(builder.Root, UiPosition.TopLeft, control.GetLabelPosition(control.AlphaPicker.Background.Offset, labelHeight), "A", fontSize, textColor);
            }

            builder.Panel(builder.Root, UiPosition.Bottom, new UiOffset(MenuPadding, MenuPadding + 2, -MenuPadding, rgbaTextHeight + ItemPadding + 2), selectedColor);

            return control;
        }

        private UiOffset GetLabelPosition(UiOffset offset, int textHeight)
        {
            return new UiOffset(offset.Min.x, offset.Min.y + textHeight + ItemPadding, offset.Max.x, offset.Max.y + textHeight + ItemPadding);
        }

        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
}