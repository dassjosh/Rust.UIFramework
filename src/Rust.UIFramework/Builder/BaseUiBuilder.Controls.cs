using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Controls;
using Oxide.Ext.UiFramework.Controls.Data;
using Oxide.Ext.UiFramework.Controls.NumberPicker;
using Oxide.Ext.UiFramework.Controls.Popover;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Builder;

public partial class BaseUiBuilder
{
    #region Buttons
    public UiButton TextButton(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int textSize, UiColor textColor, UiColor buttonColor, string command, TextAnchor align = TextAnchor.MiddleCenter)
    {
        UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
        Label(button, UiPosition.HorizontalPaddedFull, text, textSize, textColor , align);
        return button;
    }

    public UiButton TextButton(in UiReference parent, in UiPosition pos, string text, int textSize, UiColor textColor, UiColor buttonColor, string command, TextAnchor align = TextAnchor.MiddleCenter) 
        => TextButton(parent, pos, default, text, textSize, textColor, buttonColor, command, align);
        
    public UiButton ImageFileStorageButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, string png, string command)
    {
        UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
        ImageFileStorage(button, UiPosition.Full, png);
        return button;
    }

    public UiButton ImageFileStorageButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, string png, string command) => ImageFileStorageButton(parent, pos, default, buttonColor, png, command);
        
    public UiButton ImageSpriteButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, string sprite, string command)
    {
        UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
        ImageSprite(button, UiPosition.Full, sprite);
        return button;
    }

    public UiButton ImageSpriteButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, string sprite, string command) => ImageSpriteButton(parent, pos, default, buttonColor, sprite, command);
        
    public UiButton WebImageButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, string url, string command)
    {
        UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
        WebImage(button, UiPosition.Full, url);
        return button;
    }

    public UiButton WebImageButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, string url, string command) => WebImageButton(parent, pos, default, buttonColor, url, command);
        
    public UiButton ItemIconButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, int itemId, string command)
    {
        UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
        ItemIcon(button, UiPosition.Full, itemId);
        return button;
    }

    public UiButton ItemIconButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, int itemId, string command) => ItemIconButton(parent, pos, default, buttonColor, itemId, command);
        
    public UiButton ItemIconButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, int itemId, ulong skinId, string command)
    {
        UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
        ItemIcon(button, UiPosition.Full, itemId, skinId);
        return button;
    }

    public UiButton ItemIconButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, int itemId, ulong skinId, string command) => ItemIconButton(parent, pos, default, buttonColor, itemId, skinId, command);
        
    public UiButton CloseTextButton(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int textSize, UiColor textColor, UiColor buttonColor, string close, TextAnchor align = TextAnchor.MiddleCenter)
    {
        UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
        Label(button, UiPosition.HorizontalPaddedFull, text, textSize, textColor , align);
        return button;
    }

    public UiButton CloseTextButton(in UiReference parent, in UiPosition pos, string text, int textSize, UiColor textColor, UiColor buttonColor, string close, TextAnchor align = TextAnchor.MiddleCenter) 
        => CloseTextButton(parent, pos, default, text, textSize, textColor, buttonColor, close, align);
        
    public UiButton CloseImageFileStorageButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, string png, string close)
    {
        UiButton button = CommandButton(parent, pos, offset, buttonColor, close);
        ImageFileStorage(button, UiPosition.Full, png);
        return button;
    }

    public UiButton CloseImageFileStorageButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, string png, string close) => CloseImageFileStorageButton(parent, pos, default, buttonColor, png, close);
        
    public UiButton CloseImageSpriteButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, string sprite, string close)
    {
        UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
        ImageSprite(button, UiPosition.Full, sprite);
        return button;
    }

    public UiButton CloseImageSpriteButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, string sprite, string close) => CloseImageSpriteButton(parent, pos, default, buttonColor, sprite, close);
        
    public UiButton CloseWebImageButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, string url, string close)
    {
        UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
        WebImage(button, UiPosition.Full, url);
        return button;
    }

    public UiButton CloseWebImageButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, string url, string close) => CloseWebImageButton(parent, pos, default, buttonColor, url, close);
        
    public UiButton CloseItemIconButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, int itemId, string close)
    {
        UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
        ItemIcon(button, UiPosition.Full, itemId);
        return button;
    }

    public UiButton CloseItemIconButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, int itemId, string close) => CloseItemIconButton(parent, pos, default, buttonColor, itemId, close);
        
    public UiButton CloseItemIconButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, int itemId, ulong skinId, string close)
    {
        UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
        ItemIcon(button, UiPosition.Full, itemId, skinId);
        return button;
    }

    public UiButton CloseItemIconButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, int itemId, ulong skinId, string close) => CloseItemIconButton(parent, pos, default, buttonColor, itemId, skinId, close);
    #endregion

    #region Input Background
    public UiInputBackground InputBackground(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        UiInputBackground control = UiInputBackground.Create(this, parent, pos, offset, text, fontSize, textColor, backgroundColor, command, align, charsLimit, mode, lineType);
        AddControl(control);
        return control;
    }

    public UiInputBackground InputBackground(in UiReference parent, in UiPosition pos, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine) =>
        InputBackground(parent, pos, UiOffset.None, text, fontSize, textColor, backgroundColor, command, align, charsLimit, mode, lineType);
    #endregion

    #region Checkbox
    public UiCheckbox Checkbox(in UiReference parent, in UiPosition pos, in UiOffset offset, bool isChecked, int textSize, UiColor textColor, UiColor backgroundColor, string command)
    {
        UiCheckbox checkbox = UiCheckbox.CreateCheckbox(this, parent, pos, offset, isChecked, textSize, textColor, backgroundColor, command);
        AddControl(checkbox);
        return checkbox;
    }
    #endregion

    #region ProgressBar
    public UiProgressBar ProgressBar(in UiReference parent, in UiPosition pos, in UiOffset offset, float percentage, UiColor barColor, UiColor backgroundColor)
    {
        UiProgressBar control = UiProgressBar.Create(this, parent, pos, offset, percentage, barColor, backgroundColor);
        AddControl(control);
        return control;
    }
    #endregion

    #region Button Groups
    public UiButtonGroup ButtonGroup(in UiReference parent, in UiPosition pos, in UiOffset offset, List<ButtonGroupData> buttons, int textSize, UiColor textColor, UiColor buttonColor, UiColor currentButtonColor, string command)
    {
        UiButtonGroup control = UiButtonGroup.Create(this, parent, pos, offset, buttons, textSize, textColor, buttonColor, currentButtonColor, command);
        AddControl(control);
        return control;
    }

    public UiButtonGroup NumericButtonGroup(in UiReference parent, in UiPosition pos, in UiOffset offset, int value, int minValue, int maxValue, int textSize, UiColor textColor, UiColor buttonColor, UiColor currentButtonColor, string command)
    {
        UiButtonGroup control = UiButtonGroup.CreateNumeric(this, parent, pos, offset, value, minValue, maxValue, textSize, textColor, buttonColor, currentButtonColor, command);
        AddControl(control);
        return control;
    }
    #endregion

    #region Number Picker
    public UiNumberPicker NumberPicker(in UiReference parent, in UiPosition pos, in UiOffset offset, int value, int fontSize, int buttonFontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiColor disabledButtonColor, string command, string incrementCommand, string decrementCommand, int minValue = int.MinValue, int maxValue = int.MaxValue, float buttonWidth = 0.1f, TextAnchor align = TextAnchor.MiddleRight, InputMode mode = InputMode.Default, NumberPickerMode numberMode = NumberPickerMode.LeftRight, string numberFormat = null)
    {
        UiNumberPicker control = UiNumberPicker.Create(this, parent, pos, offset, value, fontSize, buttonFontSize, textColor, backgroundColor, buttonColor, disabledButtonColor, command, incrementCommand, decrementCommand, minValue, maxValue, buttonWidth, align, mode, numberMode, numberFormat);
        AddControl(control);
        return control;
    }

    public UiIncrementalNumberPicker<T> IncrementalNumberPicker<T>(in UiReference parent, in UiPosition pos, in UiOffset offset, T value, IList<T> increments, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiColor disabledButtonColor, string command, T minValue , T maxValue, InputMode mode = InputMode.Default, float buttonWidth = 0.1f, TextAnchor align = TextAnchor.MiddleRight, string incrementFormat = "0", string numberFormat = null)  where T : struct, IConvertible, IFormattable, IComparable<T>
    {
        UiIncrementalNumberPicker<T> control = UiIncrementalNumberPicker<T>.Create(this, parent, pos, offset, value, increments, fontSize, textColor, backgroundColor, buttonColor, disabledButtonColor, command, align, mode, minValue, maxValue, buttonWidth, incrementFormat, numberFormat);
        AddControl(control);
        return control;
    }
    #endregion

    #region Paginator
    public UiPaginator Paginator(in UiReference parent, GridPosition grid, int currentPage, int maxPage, int fontSize, UiColor textColor, UiColor buttonColor, UiColor activePageColor, string command)
    {
        UiPaginator control = UiPaginator.Create(this, parent, grid, currentPage, maxPage, fontSize, textColor, buttonColor, activePageColor, command);
        AddControl(control);
        return control;
    }
    #endregion
        
    #region Scroll Bar

    public UiScrollBar ScrollBar(in UiPosition position, in UiOffset offset, int currentPage, int maxPage, UiColor barColor, UiColor backgroundColor, string command, ScrollbarDirection direction = ScrollbarDirection.Vertical, string sprite = UiConstants.Sprites.RoundedBackground2)
    {
        return ScrollBar(new UiReference(), position, offset, currentPage, maxPage, barColor, backgroundColor, command, direction, sprite);
    }

    public UiScrollBar ScrollBar(in UiReference parent, in UiPosition position, in UiOffset offset, int currentPage, int maxPage, UiColor barColor, UiColor backgroundColor, string command, ScrollbarDirection direction = ScrollbarDirection.Vertical, string sprite = UiConstants.Sprites.RoundedBackground2)
    {
        UiScrollBar control = UiScrollBar.Create(this, parent, position, offset, currentPage, maxPage, barColor, backgroundColor, command, direction, sprite);
        AddControl(control);
        return control;
    }
    #endregion

    #region Dropdown
    public UiDropdown Dropdown(in UiReference parent, in UiPosition pos, in UiOffset offset, string displayValue, int fontSize, UiColor textColor, UiColor backgroundColor, string openCommand)
    {
        UiDropdown control = UiDropdown.Create(this, parent, pos, offset, displayValue, fontSize, textColor, backgroundColor, openCommand);
        AddControl(control);
        return control;
    }
        
    public static UiDropdownMenu DropdownMenu(in UiReference reference, List<DropdownMenuData> items, int fontSize, UiColor textColor, UiColor backgroundColor, string selectedCommand, string pageCommand = null, int page = 0, int maxValuesPerPage = 100, int minWidth = 100,
        PopoverPosition position = PopoverPosition.Bottom, string menuSprite = UiConstants.Sprites.RoundedBackground2)
    {
        UiDropdownMenu control = UiDropdownMenu.Create(reference, items, fontSize, textColor, backgroundColor, selectedCommand, pageCommand, page, maxValuesPerPage, minWidth, position, menuSprite);
        return control;
    }
    #endregion

    #region Time Picker
    public UiTimePicker TimePicker(in UiReference parent, in UiPosition pos, in UiOffset offset, DateTime time, int fontSize, UiColor textColor, UiColor backgroundColor, string openCommand, string displayFormat = "hh:mm:ss tt")
    {
        UiTimePicker control = UiTimePicker.Create(this, parent, pos, offset, time, fontSize, textColor, backgroundColor, openCommand, displayFormat);
        AddControl(control);
        return control;
    }
        
    public static UiTimePickerMenu TimePickerMenu(in UiReference reference, TimePickerData time, int fontSize, UiColor textColor, UiColor backgroundColor, string changeCommand, TimePickerDisplayMode displayMode = TimePickerDisplayMode.All, ClockMode clockMode = ClockMode.Hour12,
        PopoverPosition position = PopoverPosition.Bottom, string menuSprite = UiConstants.Sprites.RoundedBackground2)
    {
        UiTimePickerMenu picker = UiTimePickerMenu.Create(reference, time, fontSize, textColor, backgroundColor, changeCommand, displayMode, clockMode, position, menuSprite);
        return picker;
    }
    #endregion

    #region Date Picker
    public UiDatePicker DatePicker(in UiReference parent, in UiPosition pos, in UiOffset offset, DateTime date, int fontSize, UiColor textColor, UiColor backgroundColor, string openCommand)
    {
        UiDatePicker picker = UiDatePicker.Create(this, parent, pos,offset, date, fontSize, textColor, backgroundColor, openCommand);
        return picker;
    }
        
    public static UiCalenderPicker DateCalenderMenu(in UiReference reference, DateTime date, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiColor selectedDateColor, string changeCommand, PopoverPosition position, string menuSprite = UiConstants.Sprites.RoundedBackground2, string buttonSprite = UiConstants.Sprites.RoundedBackground1)
    {
        UiCalenderPicker picker = UiCalenderPicker.Create(reference, date, fontSize, textColor, backgroundColor, buttonColor, selectedDateColor, changeCommand, position, menuSprite, buttonSprite);
        return picker;
    }
    #endregion
        
    #region Color Picker
    // public UiColorPicker ColorPicker(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor selectedColor, int fontSize, UiColor textColor, UiColor backgroundColor, string openCommand)
    // {
    //     UiColorPicker control = UiColorPicker.Create(this, parent, pos, offset, selectedColor, fontSize, textColor, backgroundColor, openCommand);
    //     AddControl(control);
    //     return control;
    // }
    //
    // public static UiColorPickerMenu ColorPickerMenu(UiReference reference, UiColor selectedColor, int fontSize, UiColor textColor, UiColor buttonColor, UiColor backgroundColor, UiColor pickerBackgroundColor, UiColor pickerDisabledColor, string command, ColorPickerMode mode, PopoverPosition position, string menuSprite = UiConstants.Sprites.RoundedBackground2, InputMode inputMode = InputMode.NeedsKeyboard)
    // {
    //     UiColorPickerMenu picker = UiColorPickerMenu.Create(reference, selectedColor, fontSize, textColor, buttonColor, backgroundColor, pickerBackgroundColor, pickerDisabledColor, command, mode, position, menuSprite, inputMode);
    //     return picker;
    // }
    #endregion
        
    #region Border
    public UiBorder Border(in UiReference parent, UiColor color, in UiBorderWidth width, BorderMode border = BorderMode.All)
    {
        UiBorder control = UiBorder.Create(this, parent, color, width, border);
        AddControl(control);
        return control;
    }
    #endregion
}