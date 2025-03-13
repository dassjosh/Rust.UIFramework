using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Controls;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Builder;

public partial class BaseUiBuilder
{
    #region Buttons
    public UiTuple<UiButton, UiLabel> TextButton(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int textSize, UiColor textColor, UiColor buttonColor, string command, TextAnchor align = TextAnchor.MiddleCenter)
    {
        UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
        UiLabel label = Label(button, UiPosition.Full, JsonDefaults.Common.TextPadding, text, textSize, textColor , align);
        return new UiTuple<UiButton, UiLabel>(button, label);
    }

    public UiTuple<UiButton, UiLabel> TextButton(in LayoutPosition layout, string text, int textSize, UiColor textColor, UiColor buttonColor, string command, TextAnchor align = TextAnchor.MiddleCenter) =>
        TextButton(layout.Reference, layout.Position, layout.Offset, text, textSize, textColor, buttonColor, command, align);
        
    public UiTuple<UiButton, UiRawImage> ImageFileStorageButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, string png, string command, UiColor? spriteColor = null)
    {
        UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
        UiRawImage image = ImageFileStorage(button, UiPosition.Full, default, png, spriteColor);
        return new UiTuple<UiButton, UiRawImage>(button, image);
    }

    public UiTuple<UiButton, UiImage> ImageSpriteButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, string sprite, string command, UiColor? spriteColor = null)
    {
        UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
        UiImage image = ImageSprite(button, UiPosition.Full, default, sprite, spriteColor);
        return new UiTuple<UiButton, UiImage>(button, image);
    }
        
    public UiTuple<UiButton, UiRawImage> WebImageButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, string url, string command, UiColor? imageColor = null)
    {
        UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
        UiRawImage image = WebImage(button, UiPosition.Full, default, url, imageColor);
        return new UiTuple<UiButton, UiRawImage>(button, image);
    }
        
    public UiTuple<UiButton, UiItemIcon> ItemIconButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, int itemId, string command)
    {
        UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
        UiItemIcon icon = ItemIcon(button, UiPosition.Full, default, itemId);
        return new UiTuple<UiButton, UiItemIcon>(button, icon);
    }
        
    public UiTuple<UiButton, UiItemIcon> ItemIconButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, int itemId, ulong skinId, string command)
    {
        UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
        UiItemIcon icon = ItemIcon(button, UiPosition.Full, default, itemId, skinId);
        return new UiTuple<UiButton, UiItemIcon>(button, icon);
    }
    
    public UiTuple<UiButton, UiIcon> IconButton<T>(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, T icon, string command, UiColor? iconColor = null) where T : struct, Enum
    {
        UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
        UiIcon image = Icon(button, UiPosition.Full, default, icon, iconColor);
        return new UiTuple<UiButton, UiIcon>(button, image);
    }
    
    public UiTuple<UiButton, UiIcon> IconButton<T>(in LayoutPosition layout, UiColor buttonColor, T icon, string command, UiColor? iconColor = null) where T : struct, Enum
    {
        UiButton button = CommandButton(layout, buttonColor, command);
        UiIcon image = Icon(button, UiPosition.Full, default, icon, iconColor);
        return new UiTuple<UiButton, UiIcon>(button, image);
    }
        
    public UiTuple<UiButton, UiLabel> CloseTextButton(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int textSize, UiColor textColor, UiColor buttonColor, string close, TextAnchor align = TextAnchor.MiddleCenter)
    {
        UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
        UiLabel label = Label(button, UiPosition.Full, JsonDefaults.Common.TextPadding, text, textSize, textColor , align);
        return new UiTuple<UiButton, UiLabel>(button, label);
    }
        
    public UiTuple<UiButton, UiRawImage> CloseImageFileStorageButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, string png, string close, UiColor? imageColor = null)
    {
        UiButton button = CommandButton(parent, pos, offset, buttonColor, close);
        UiRawImage image = ImageFileStorage(button, UiPosition.Full, default, png, imageColor);
        return new UiTuple<UiButton, UiRawImage>(button, image);
    }
        
    public UiTuple<UiButton, UiImage> CloseImageSpriteButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, string sprite, string close, UiColor? spriteColor = null)
    {
        UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
        UiImage image = ImageSprite(button, UiPosition.Full, default, sprite, spriteColor);
        return new UiTuple<UiButton, UiImage>(button, image);
    }
        
    public UiTuple<UiButton, UiRawImage> CloseWebImageButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, string url, string close, UiColor? imageColor = null)
    {
        UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
        UiRawImage image = WebImage(button, UiPosition.Full, default, url, imageColor);
        return new UiTuple<UiButton, UiRawImage>(button, image);
    }
        
    public UiTuple<UiButton, UiItemIcon> CloseItemIconButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, int itemId, string close)
    {
        UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
        UiItemIcon icon = ItemIcon(button, UiPosition.Full, default, itemId);
        return new UiTuple<UiButton, UiItemIcon>(button, icon);
    }
        
    public UiTuple<UiButton, UiItemIcon> CloseItemIconButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, int itemId, ulong skinId, string close)
    {
        UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
        UiItemIcon icon = ItemIcon(button, UiPosition.Full, default, itemId, skinId);
        return new UiTuple<UiButton, UiItemIcon>(button, icon);
    }
    
    public UiTuple<UiButton, UiIcon> CloseIconButton<T>(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, T icon, string close, UiColor? iconColor = null) where T : struct, Enum
    {
        UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
        UiIcon image = Icon(button, UiPosition.Full, default, icon, iconColor);
        return new UiTuple<UiButton, UiIcon>(button, image);
    }
    #endregion

    #region Label Background
    public UiTuple<UiPanel, UiLabel> LabelBackground(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter)
    {
        UiPanel background = Panel(parent, pos, offset, backgroundColor);
        UiLabel label = Label(background, UiPosition.Full, JsonDefaults.Common.TextPadding, text, fontSize, textColor, align);
        return new UiTuple<UiPanel, UiLabel>(background, label);
    }
    #endregion

    #region Input Background
    public UiTuple<UiPanel, UiInput> InputBackground(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        UiPanel background = Panel(parent,  pos, offset, backgroundColor);
        UiInput input = Input(background, UiPosition.Full, JsonDefaults.Common.TextPadding, text, fontSize, textColor, command, align, charsLimit, mode, lineType);
        return new UiTuple<UiPanel, UiInput>(background, input);
    }
    #endregion

    #region Checkbox
    public UiCheckbox Checkbox(in UiReference parent, in UiPosition pos, in UiOffset offset, bool isChecked, string command, UiColor? checkedColor = null, UiColor? uncheckedColor = null, UiColor? buttonColor = null)
    {
        UiCheckbox checkbox = UiCheckbox.CreateCheckbox(this, parent, pos, offset, isChecked, command, checkedColor, uncheckedColor, buttonColor);
        AddControl(checkbox);
        return checkbox;
    }
    #endregion
    
    #region Checkbox
    public UiSwitch Switch(in UiReference parent, in UiPosition pos, in UiOffset offset, bool isChecked, string command, UiColor? checkedColor = null, UiColor? uncheckedColor = null, UiColor? buttonColor = null)
    {
        UiSwitch checkbox = UiSwitch.CreateSwitch(this, parent, pos, offset, isChecked, command, checkedColor, uncheckedColor, buttonColor);
        AddControl(checkbox);
        return checkbox;
    }
    #endregion

    #region ProgressBar
    public UiTuple<UiPanel, UiPanel> ProgressBar(in UiReference parent, in UiPosition pos, in UiOffset offset, float percentage, UiColor barColor, UiColor backgroundColor)
    {
        UiPanel background = Panel(parent, pos, offset, backgroundColor);
        UiPanel progress = Panel(background, UiPosition.Full.SliceHorizontal(0, percentage), default, barColor);
        return new UiTuple<UiPanel, UiPanel>(background, progress);
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
    public UiPaginator Paginator(BaseLayout layout, int currentPage, int maxPage, int fontSize, UiColor textColor, UiColor buttonColor, UiColor activePageColor, string command, UiColor? disabledColorMultiplier = null)
    {
        return Paginator(layout, currentPage, maxPage, fontSize, textColor, buttonColor, activePageColor, PartialCommand.Create<int>(command), disabledColorMultiplier);
    }
    
    public UiPaginator Paginator(BaseLayout layout, int currentPage, int maxPage, int fontSize, UiColor textColor, UiColor buttonColor, UiColor activePageColor, ICommandBuilder<int> command, UiColor? disabledColorMultiplier = null)
    {
        UiPaginator control = UiPaginator.Create(this, layout, currentPage, maxPage, fontSize, textColor, buttonColor, activePageColor, command, disabledColorMultiplier);
        AddControl(control);
        return control;
    }
    #endregion
        
    #region Scroll Bar
    public UiScrollBar ScrollBar(in UiReference parent, in UiPosition position, in UiOffset offset, int currentPage, int maxPage, UiColor barColor, UiColor backgroundColor, string command, ScrollbarDirection direction = ScrollbarDirection.Vertical, string sprite = UiSprites.Content.Ui.UiBackgroundRounded)
    {
        return ScrollBar(parent, position, offset, currentPage, maxPage, barColor, backgroundColor, PartialCommand.Create<int>(command), direction, sprite);
    }
    
    public UiScrollBar ScrollBar(in UiReference parent, in UiPosition position, in UiOffset offset, int currentPage, int maxPage, UiColor barColor, UiColor backgroundColor, ICommandBuilder<int> command, ScrollbarDirection direction = ScrollbarDirection.Vertical, string sprite = UiSprites.Content.Ui.UiBackgroundRounded)
    {
        UiScrollBar control = UiScrollBar.Create(this, parent, position, offset, currentPage, maxPage, barColor, backgroundColor, command, direction, sprite);
        AddControl(control);
        return control;
    }
    #endregion

    #region Dropdown
    public UiDropdown Dropdown(in UiReference parent, in UiPosition pos, in UiOffset offset, string displayValue, int fontSize, UiColor textColor, UiColor backgroundColor, string openCommand)
    {
        UiDropdown control = UiDropdown.Create(this, parent, pos, offset, displayValue, fontSize, textColor, backgroundColor, PartialCommand.Create<UiReference>(openCommand));
        AddControl(control);
        return control;
    }
    
    public UiDropdown Dropdown(in UiReference parent, in UiPosition pos, in UiOffset offset, string displayValue, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<UiReference> openCommand)
    {
        UiDropdown control = UiDropdown.Create(this, parent, pos, offset, displayValue, fontSize, textColor, backgroundColor, openCommand);
        AddControl(control);
        return control;
    }
        
    public UiDropdownMenu DropdownMenu(in UiReference reference, in UiPosition pos, in UiOffset offset, List<DropdownMenuData> items, int fontSize, UiColor textColor, UiColor backgroundColor, DropdownMenuScrollMode scrollMode, string pageCommand = null, int page = 0, int maxValuesPerPage = 100, in UiPadding? padding = null)
    {
        return DropdownMenu(reference, pos, offset, items, fontSize, textColor, backgroundColor, scrollMode, PartialCommand.Create<int>(pageCommand), page, maxValuesPerPage, padding);
    }
    
    public UiDropdownMenu DropdownMenu(in UiReference reference, in UiPosition pos, in UiOffset offset, List<DropdownMenuData> items, int fontSize, UiColor textColor, UiColor backgroundColor, DropdownMenuScrollMode scrollMode, ICommandBuilder<int> pageCommand = null, int page = 0, int maxValuesPerPage = 100, in UiPadding? padding = null,
        PopoverPosition position = PopoverPosition.Bottom, string menuSprite = UiSprites.Content.Ui.UiBackgroundRounded)
    {
        UiDropdownMenu control = UiDropdownMenu.Create(this, reference, pos, offset, items, fontSize, textColor, backgroundColor, scrollMode, pageCommand, page, maxValuesPerPage, padding);
        AddControl(control);
        return control;
    }
    #endregion

    #region Time Picker
    public UiTimePicker TimePicker(in UiReference parent, in UiPosition pos, in UiOffset offset, DateTime time, int fontSize, UiColor textColor, UiColor backgroundColor, string openCommand, string displayFormat = "hh:mm:ss tt")
    {
        UiTimePicker control = UiTimePicker.Create(this, parent, pos, offset, time, fontSize, textColor, backgroundColor, PartialCommand.Create<UiReference>(openCommand), displayFormat);
        AddControl(control);
        return control;
    }
    
    public UiTimePicker TimePicker(in UiReference parent, in UiPosition pos, in UiOffset offset, DateTime time, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<UiReference> openCommand, string displayFormat = "hh:mm:ss tt")
    {
        UiTimePicker control = UiTimePicker.Create(this, parent, pos, offset, time, fontSize, textColor, backgroundColor, openCommand, displayFormat);
        AddControl(control);
        return control;
    }
        
    public UiTimePickerMenu TimePickerMenu(in UiReference reference, in UiPosition pos, in UiOffset offset, TimePickerData time, int fontSize, UiColor textColor, UiColor backgroundColor, string changeCommand, TimePickerDisplayModes displayMode = TimePickerDisplayModes.All, ClockMode clockMode = ClockMode.Hour12)
    {
        return TimePickerMenu(reference, pos, offset, time, fontSize, textColor, backgroundColor, PartialCommand.Create<TimePickerData>(changeCommand), displayMode, clockMode);
    }
    
    public UiTimePickerMenu TimePickerMenu(in UiReference reference, in UiPosition pos, in UiOffset offset, TimePickerData time, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<TimePickerData> changeCommand, TimePickerDisplayModes displayMode = TimePickerDisplayModes.All, ClockMode clockMode = ClockMode.Hour12)
    {
        UiTimePickerMenu picker = UiTimePickerMenu.Create(this, reference, pos, offset, time, fontSize, textColor, backgroundColor, changeCommand, displayMode, clockMode);
        AddControl(picker);
        return picker;
    }
    #endregion

    #region Date Picker
    public UiDatePicker DatePicker(in UiReference parent, in UiPosition pos, in UiOffset offset, DateTime date, int fontSize, UiColor textColor, UiColor backgroundColor, string openCommand)
    {
        UiDatePicker picker = UiDatePicker.Create(this, parent, pos,offset, date, fontSize, textColor, backgroundColor, PartialCommand.Create<UiReference>(openCommand));
        return picker;
    }
    
    public UiDatePicker DatePicker(in UiReference parent, in UiPosition pos, in UiOffset offset, DateTime date, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<UiReference> openCommand)
    {
        UiDatePicker picker = UiDatePicker.Create(this, parent, pos,offset, date, fontSize, textColor, backgroundColor, openCommand);
        return picker;
    }
        
    public UiCalenderPicker DateCalenderMenu(in UiReference reference, in UiPosition pos, in UiOffset offset, DateTime date, int fontSize, UiColor textColor, UiColor buttonColor, UiColor selectedDateColor, string changeCommand, string buttonSprite = UiSprites.Content.Ui.UiRounded)
    {
        UiCalenderPicker picker = UiCalenderPicker.Create(this, reference, in pos, in offset, date, fontSize, textColor, buttonColor, selectedDateColor, PartialCommand.Create<DateTime>(changeCommand), buttonSprite);
        return picker;
    }
    
    public UiCalenderPicker DateCalenderMenu(in UiReference reference, in UiPosition pos, in UiOffset offset, DateTime date, int fontSize, UiColor textColor, UiColor buttonColor, UiColor selectedDateColor, ICommandBuilder<DateTime> changeCommand, string buttonSprite = UiSprites.Content.Ui.UiRounded)
    {
        UiCalenderPicker picker = UiCalenderPicker.Create(this, reference, in pos, in offset, date, fontSize, textColor, buttonColor, selectedDateColor, changeCommand, buttonSprite);
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
    // public static UiColorPickerMenu ColorPickerMenu(UiReference reference, UiColor selectedColor, int fontSize, UiColor textColor, UiColor buttonColor, UiColor backgroundColor, UiColor pickerBackgroundColor, UiColor pickerDisabledColor, string command, ColorPickerMode mode, PopoverPosition position, string menuSprite = UiSprites.Assets.Content.Ui.UiBackgroundRounded, InputMode inputMode = InputMode.NeedsKeyboard)
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