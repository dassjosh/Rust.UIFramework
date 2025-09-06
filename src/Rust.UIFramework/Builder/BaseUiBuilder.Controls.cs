using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Controls;
using Oxide.Ext.UiFramework.Controls.Data;
using Oxide.Ext.UiFramework.Controls.NumberPicker;
using Oxide.Ext.UiFramework.Controls.Popover;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Layouts.GridPositions;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Padding;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Builder;

public partial class BaseUiBuilder
{
    #region Buttons
    #region Sprite
    public UiTuple<UiButton, UiImage> SpriteButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, string sprite, string command, ButtonType type = ButtonType.Command, UiColor? spriteColor = null)
    {
        UiButton button = Button(parent, pos, offset, buttonColor, command, type);
        UiImage image = ImageSprite(button, UiPosition.Full, default, sprite, spriteColor);
        return new UiTuple<UiButton, UiImage>(button, image);
    }
    
    public UiTuple<UiButton, UiImage> SpriteButton(BaseUiLayout layout, UiColor buttonColor, string sprite, string command, ButtonType type = ButtonType.Command, UiColor? spriteColor = null)
    {
        UiButton button = Button(layout, buttonColor, command, type);
        UiImage image = ImageSprite(button, UiPosition.Full, default, sprite, spriteColor);
        return new UiTuple<UiButton, UiImage>(button, image);
    }
    #endregion
    
    #region PlayingCard
    public UiTuple<UiButton, UiPlayingCard> PlayingCardButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, PlayingCardData card, string command, UiCardType type = UiCardType.Normal, ButtonType buttonType = ButtonType.Command, UiColor? spriteColor = null)
    {
        UiButton button = Button(parent, pos, offset, buttonColor, command, buttonType);
        UiPlayingCard image = PlayingCard(button, UiPosition.Full, default, card, type, spriteColor);
        return new UiTuple<UiButton, UiPlayingCard>(button, image);
    }
    
    public UiTuple<UiButton, UiPlayingCard> PlayingCardButton(BaseUiLayout layout, UiColor buttonColor, PlayingCardData card, string command, UiCardType type = UiCardType.Normal, ButtonType buttonType = ButtonType.Command, UiColor? spriteColor = null)
    {
        UiButton button = Button(layout, buttonColor, command, buttonType);
        UiPlayingCard image = PlayingCard(button, UiPosition.Full, default, card, type, spriteColor);
        return new UiTuple<UiButton, UiPlayingCard>(button, image);
    }
    #endregion

    #region ItemIcon
    public UiTuple<UiButton, UiItemIcon> ItemIconButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, int itemId, string command, ButtonType type = ButtonType.Command, UiColor? iconColor = null)
    {
        UiButton button = Button(parent, pos, offset, buttonColor, command, type);
        UiItemIcon icon = ItemIcon(button, UiPosition.Full, default, itemId, color: iconColor);
        return new UiTuple<UiButton, UiItemIcon>(button, icon);
    }
        
    public UiTuple<UiButton, UiItemIcon> ItemIconButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, int itemId, ulong skinId, string command, ButtonType type = ButtonType.Command, UiColor? iconColor = null)
    {
        UiButton button = Button(parent, pos, offset, buttonColor, command, type);
        UiItemIcon icon = ItemIcon(button, UiPosition.Full, default, itemId, skinId, iconColor ?? UiColors.White);
        return new UiTuple<UiButton, UiItemIcon>(button, icon);
    }
    
    public UiTuple<UiButton, UiItemIcon> ItemIconButton(BaseUiLayout layout, UiColor buttonColor, int itemId, string command, ButtonType type = ButtonType.Command, UiColor? iconColor = null)
    {
        UiButton button = Button(layout, buttonColor, command, type);
        UiItemIcon icon = ItemIcon(button, UiPosition.Full, default, itemId, color: iconColor);
        return new UiTuple<UiButton, UiItemIcon>(button, icon);
    }
        
    public UiTuple<UiButton, UiItemIcon> ItemIconButton(BaseUiLayout layout, UiColor buttonColor, int itemId, ulong skinId, string command, ButtonType type = ButtonType.Command, UiColor? iconColor = null)
    {
        UiButton button = Button(layout, buttonColor, command, type);
        UiItemIcon icon = ItemIcon(button, UiPosition.Full, default, itemId, skinId, iconColor);
        return new UiTuple<UiButton, UiItemIcon>(button, icon);
    }
    
    public UiTuple<UiButton, UiItemIcon> ItemIconButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, Item item, string command, ButtonType type = ButtonType.Command, UiColor? iconColor = null) 
        => ItemIconButton(parent, pos, offset, buttonColor, item.info.itemid, item.skin, command, type, iconColor);
    public UiTuple<UiButton, UiItemIcon> ItemIconButton(BaseUiLayout layout, UiColor buttonColor, Item item, string command, ButtonType type = ButtonType.Command, UiColor? iconColor = null)
        => ItemIconButton(layout, buttonColor, item.info.itemid, item.skin, command, type, iconColor);
    #endregion

    #region Player Avatar
    public UiTuple<UiButton, UiPlayerAvatar> PlayerAvatarButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, ulong steamId, string command, AvatarType avatarType = AvatarType.Medium, ButtonType type = ButtonType.Command, UiColor? iconColor = null)
    {
        UiButton button = Button(parent, pos, offset, buttonColor, command, type);
        UiPlayerAvatar icon = PlayerAvatar(button, UiPosition.Full, default, steamId, avatarType, iconColor ?? UiColors.White);
        return new UiTuple<UiButton, UiPlayerAvatar>(button, icon);
    }
    
    public UiTuple<UiButton, UiPlayerAvatar> PlayerAvatarButton(BaseUiLayout layout, UiColor buttonColor, ulong steamId, string command, AvatarType avatarType = AvatarType.Medium, ButtonType type = ButtonType.Command, UiColor? iconColor = null)
    {
        UiButton button = Button(layout, buttonColor, command, type);
        UiPlayerAvatar icon = PlayerAvatar(button, UiPosition.Full, default, steamId, avatarType, iconColor ?? UiColors.White);
        return new UiTuple<UiButton, UiPlayerAvatar>(button, icon);
    }
    #endregion

    #region RawImage
    public UiTuple<UiButton, UiRawImage> RawImageButton(in UiReference parent, in UiPosition pos, in UiOffset offset, string image, string command, UiColor? buttonColor = default, ButtonType type = ButtonType.Command, UiColor? imageColor = default)
    {
        UiButton button = Button(parent, pos, offset, buttonColor ?? UiColors.White, command, type);
        UiRawImage rawImage = RawImage(button, UiPosition.Full, default, image, imageColor ?? UiColors.White);
        return new UiTuple<UiButton, UiRawImage>(button, rawImage);
    }
    
    public UiTuple<UiButton, UiRawImage> RawImageButton(BaseUiLayout layout, string image, string command, UiColor? buttonColor = default, ButtonType type = ButtonType.Command, UiColor? imageColor = default)
    {
        UiButton button = Button(layout, buttonColor ?? UiColors.White, command, type);
        UiRawImage rawImage = RawImage(button, UiPosition.Full, default, image, imageColor ?? UiColors.White);
        return new UiTuple<UiButton, UiRawImage>(button, rawImage);
    }
    
    public UiTuple<UiButton, UiRawImage> WebImageButton(in UiReference parent, in UiPosition pos, in UiOffset offset, string url, string command, UiColor? buttonColor = default, ButtonType type = ButtonType.Command, UiColor? imageColor = default)
    => RawImageButton(parent, pos, offset, url, command, buttonColor, type, imageColor);
    public UiTuple<UiButton, UiRawImage> WebImageButton(BaseUiLayout layout, string url, string command, UiColor? buttonColor = default, ButtonType type = ButtonType.Command, UiColor? imageColor = default)
        => RawImageButton(layout, url, command, buttonColor, type, imageColor);
    
    public UiTuple<UiButton, UiRawImage> TextureImageButton(in UiReference parent, in UiPosition pos, in UiOffset offset, string texture, string command, UiColor? buttonColor = default, ButtonType type = ButtonType.Command, UiColor? imageColor = default)
        => RawImageButton(parent, pos, offset, texture, command, buttonColor, type, imageColor);
    public UiTuple<UiButton, UiRawImage> TextureImageButton(BaseUiLayout layout, string texture, string command, UiColor? buttonColor = default, ButtonType type = ButtonType.Command, UiColor? imageColor = default)
        => RawImageButton(layout, texture, command, buttonColor, type, imageColor);
    
    public UiTuple<UiButton, UiRawImage> FileStorageImageButton(in UiReference parent, in UiPosition pos, in UiOffset offset, string imageId, string command, UiColor? buttonColor = default, ButtonType type = ButtonType.Command, UiColor? imageColor = default)
        => RawImageButton(parent, pos, offset, imageId, command, buttonColor, type, imageColor);
    public UiTuple<UiButton, UiRawImage> FileStorageImageButton(BaseUiLayout layout, string imageId, string command, UiColor? buttonColor = default, ButtonType type = ButtonType.Command, UiColor? imageColor = default)
        => RawImageButton(layout, imageId, command, buttonColor, type, imageColor);
    
    public UiTuple<UiButton, UiRawImage> ImageStorageButton(Plugin plugin, in UiReference parent, in UiPosition pos, in UiOffset offset, string nameOrUrl, string command, UiColor? buttonColor = default, ButtonType type = ButtonType.Command, string fallbackNameOrUrl = null, UiColor? imageColor = default)
        => RawImageButton(parent, pos, offset, Singleton<UiImageStorage>.Instance.Get(plugin, nameOrUrl, fallbackNameOrUrl), command, buttonColor, type, imageColor);
    public UiTuple<UiButton, UiRawImage> ImageStorageButton(Plugin plugin, BaseUiLayout layout, string nameOrUrl, string command, UiColor? buttonColor = default, ButtonType type = ButtonType.Command, string fallbackNameOrUrl = null, UiColor? imageColor = default)
        => RawImageButton(layout, Singleton<UiImageStorage>.Instance.Get(plugin, nameOrUrl, fallbackNameOrUrl), command, buttonColor, type, imageColor);
    #endregion

    #region Icon
    public UiTuple<UiButton, UiIcon> IconButton<T>(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, T icon, string command, UiColor? iconColor = null, ButtonType type = ButtonType.Command) where T : struct, Enum
    {
        UiButton button = Button(parent, pos, offset, buttonColor, command, type);
        UiIcon image = Icon(button, UiPosition.Full, default, icon, iconColor ?? UiColors.White);
        return new UiTuple<UiButton, UiIcon>(button, image);
    }
    
    public UiTuple<UiButton, UiIcon> IconButton<T>(BaseUiLayout layout, UiColor buttonColor, T icon, string command, UiColor? iconColor = null, ButtonType type = ButtonType.Command) where T : struct, Enum
    {
        UiButton button = Button(layout, buttonColor, command, type);
        UiIcon image = Icon(button, UiPosition.Full, default, icon, iconColor ?? UiColors.White);
        return new UiTuple<UiButton, UiIcon>(button, image);
    }
    #endregion

    #region Text
    public UiTuple<UiButton, UiLabel> TextButton(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int textSize, UiColor textColor, UiColor buttonColor, string command, TextAnchor align = TextAnchor.MiddleCenter, in UiPadding? textPadding = null, ButtonType buttonType = ButtonType.Command)
    {
        UiButton button = Button(parent, pos, offset, buttonColor, command, buttonType);
        UiLabel label = Label(button, UiPosition.Full, textPadding?.ToOffset()?? JsonDefaults.Common.TextPadding, text, textSize, textColor, align);
        return new UiTuple<UiButton, UiLabel>(button, label);
    }

    public UiTuple<UiButton, UiLabel> TextButton(BaseUiLayout layout, string text, int textSize, UiColor textColor, UiColor buttonColor, string command, TextAnchor align = TextAnchor.MiddleCenter, in UiPadding? textPadding = null, ButtonType buttonType = ButtonType.Command)
    {
        UiButton button = Button(layout, buttonColor, command, buttonType);
        UiLabel label = Label(button, UiPosition.Full, textPadding?.ToOffset()?? JsonDefaults.Common.TextPadding, text, textSize, textColor, align);
        return new UiTuple<UiButton, UiLabel>(button, label);
    }
    #endregion
    #endregion

    #region Checkbox
    public UiCheckbox Checkbox(in UiReference parent, in UiPosition pos, in UiOffset offset, bool isChecked, string command, UiColor? checkedColor = null, UiColor? uncheckedColor = null, UiColor? buttonColor = null)
    {
        UiCheckbox checkbox = UiCheckbox.CreateCheckbox(this, parent, pos, offset, isChecked, command, checkedColor, uncheckedColor, buttonColor);
        AddControl(checkbox);
        return checkbox;
    }

    public UiSwitch Switch(in UiReference parent, in UiPosition pos, in UiOffset offset, bool isChecked, string command, UiColor? checkedColor = null, UiColor? uncheckedColor = null, UiColor? buttonColor = null)
    {
        UiSwitch checkbox = UiSwitch.CreateSwitch(this, parent, pos, offset, isChecked, command, checkedColor, uncheckedColor, buttonColor);
        AddControl(checkbox);
        return checkbox;
    }
    #endregion

    #region ProgressBar
    public UiTuple<UiPanel, UiPanel> ProgressBar(in UiReference parent, in UiPosition pos, in UiOffset offset, float percentage, UiColor barColor, UiColor backgroundColor, ProgressBarDirection direction = ProgressBarDirection.LeftToRight)
    {
        UiPosition barPos = direction switch
        {
            ProgressBarDirection.LeftToRight => UiPosition.Full.SliceHorizontal(0, percentage),
            ProgressBarDirection.RightToLeft => UiPosition.Full.SliceHorizontal(1 - percentage, 1),
            ProgressBarDirection.TopToBottom => UiPosition.Full.SliceVertical(0, percentage),
            ProgressBarDirection.BottomToTop => UiPosition.Full.SliceVertical(1 - percentage, 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };

        UiPanel background = Panel(parent, pos, offset, backgroundColor);
        UiPanel progress = Panel(background, barPos, default, barColor);
        return new UiTuple<UiPanel, UiPanel>(background, progress);
    }
    #endregion

    #region Button Groups
    public UiButtonGroup ButtonGroup(in UiReference parent, in UiPosition pos, in UiOffset offset, List<ButtonGroupData> buttons, int textSize, UiColor textColor, UiColor buttonColor, UiColor currentButtonColor)
    {
        UiButtonGroup control = UiButtonGroup.Create(this, parent, pos, offset, buttons, textSize, textColor, buttonColor, currentButtonColor);
        AddControl(control);
        return control;
    }
    
    public UiButtonGroup NumericButtonGroup(in UiReference parent, in UiPosition pos, in UiOffset offset, int value, int minValue, int maxValue, int textSize, UiColor textColor, UiColor buttonColor, UiColor currentButtonColor, string command)
        => NumericButtonGroup(parent, pos, offset, value, minValue, maxValue, textSize, textColor, buttonColor, currentButtonColor, PartialCommand.Create<int>(command));

    public UiButtonGroup NumericButtonGroup(in UiReference parent, in UiPosition pos, in UiOffset offset, int value, int minValue, int maxValue, int textSize, UiColor textColor, UiColor buttonColor, UiColor currentButtonColor, ICommandBuilder<int> command)
    {
        UiButtonGroup control = UiButtonGroup.CreateNumeric(this, parent, pos, offset, value, minValue, maxValue, textSize, textColor, buttonColor, currentButtonColor, command);
        AddControl(control);
        return control;
    }
    #endregion

    #region Number Picker
    public UiNumberPicker NumberPicker(in UiReference parent, in UiPosition pos, in UiOffset offset, int value, int fontSize, int buttonFontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiColor disabledButtonColor, ICommandBuilder<InputArg> inputCommand, ICommandBuilder<int> incDecCommand, int minValue = int.MinValue, int maxValue = int.MaxValue, float buttonWidth = 0.1f, TextAnchor align = TextAnchor.MiddleRight, InputMode mode = InputMode.Default, NumberPickerMode numberMode = NumberPickerMode.LeftRight, string numberFormat = null)
    {
        UiNumberPicker control = UiNumberPicker.Create(this, parent, pos, offset, value, fontSize, buttonFontSize, textColor, backgroundColor, buttonColor, disabledButtonColor, inputCommand, incDecCommand, minValue, maxValue, buttonWidth, align, mode, numberMode, numberFormat);
        AddControl(control);
        return control;
    }

    public UiIncrementalNumberPicker<T> IncrementalNumberPicker<T>(in UiReference parent, in UiPosition pos, in UiOffset offset, T value, IList<T> increments, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiColor disabledButtonColor, string command, T minValue, T maxValue, InputMode mode = InputMode.Default,
        float buttonWidth = 0.1f, TextAnchor align = TextAnchor.MiddleRight, string incrementFormat = "0", string numberFormat = null) where T : struct, IConvertible, IFormattable, IComparable<T>
        => IncrementalNumberPicker(in parent, in pos, in offset, value, increments, fontSize, textColor, backgroundColor, buttonColor, disabledButtonColor, PartialCommand.Create<InputArg>(command), PartialCommand.Create<T>(command), minValue, maxValue, mode, buttonWidth, align, incrementFormat, numberFormat);

    public UiIncrementalNumberPicker<T> IncrementalNumberPicker<T>(in UiReference parent, in UiPosition pos, in UiOffset offset, T value, IList<T> increments, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiColor disabledButtonColor, ICommandBuilder<InputArg> inputCommand, ICommandBuilder<T> buttonCommand, T minValue , T maxValue, InputMode mode = InputMode.Default, float buttonWidth = 0.1f, TextAnchor align = TextAnchor.MiddleRight, string incrementFormat = "0", string numberFormat = null)  where T : struct, IConvertible, IFormattable, IComparable<T>
    {
        UiIncrementalNumberPicker<T> control = UiIncrementalNumberPicker<T>.Create(this, parent, pos, offset, value, increments, fontSize, textColor, backgroundColor, buttonColor, disabledButtonColor, inputCommand, buttonCommand, align, mode, minValue, maxValue, buttonWidth, incrementFormat, numberFormat);
        AddControl(control);
        return control;
    }
    #endregion

    #region Paginator
    public UiPaginator Paginator(BaseUiLayout layout, int numElements, int currentPage, int maxPage, int fontSize, UiColor textColor, UiColor buttonColor, UiColor activePageColor, string command, UiColor? disabledColorMultiplier = null)
    {
        return Paginator(layout, numElements, currentPage, maxPage, fontSize, textColor, buttonColor, activePageColor, PartialCommand.Create<int>(command), disabledColorMultiplier);
    }
    
    public UiPaginator Paginator(BaseUiLayout layout, int numElements, int currentPage, int maxPage, int fontSize, UiColor textColor, UiColor buttonColor, UiColor activePageColor, ICommandBuilder<int> command, UiColor? disabledColorMultiplier = null)
    {
        UiPaginator control = UiPaginator.Create(this, layout, numElements, currentPage, maxPage, fontSize, textColor, buttonColor, activePageColor, command, disabledColorMultiplier);
        AddControl(control);
        return control;
    }
    
    public UiPaginator Paginator(IFixedElementsLayout layout, int currentPage, int maxPage, int fontSize, UiColor textColor, UiColor buttonColor, UiColor activePageColor, string command, UiColor? disabledColorMultiplier = null)
    {
        return Paginator(layout, currentPage, maxPage, fontSize, textColor, buttonColor, activePageColor, PartialCommand.Create<int>(command), disabledColorMultiplier);
    }

    public UiPaginator Paginator(IFixedElementsLayout layout, int currentPage, int maxPage, int fontSize, UiColor textColor, UiColor buttonColor, UiColor activePageColor, ICommandBuilder<int> command, UiColor? disabledColorMultiplier = null)
        => Paginator((BaseUiLayout)layout, layout.NumElements, currentPage, maxPage, fontSize, textColor, buttonColor, activePageColor, command, disabledColorMultiplier);
    
    public UiPaginator Paginator(in UiReference parent, GridPosition grid, int currentPage, int maxPage, int fontSize, UiColor textColor, UiColor buttonColor, UiColor activePageColor, string command, UiColor? disabledColorMultiplier = null)
    {
        return Paginator(parent, grid, currentPage, maxPage, fontSize, textColor, buttonColor, activePageColor, PartialCommand.Create<int>(command), disabledColorMultiplier);
    }
    
    public UiPaginator Paginator(in UiReference parent, GridPosition grid, int currentPage, int maxPage, int fontSize, UiColor textColor, UiColor buttonColor, UiColor activePageColor, ICommandBuilder<int> command, UiColor? disabledColorMultiplier = null)
    {
        UiGridPositionLayout layout = GridPositionLayout(parent, UiPosition.Full, default, grid);
        return Paginator(layout, currentPage, maxPage, fontSize, textColor, buttonColor, activePageColor, command, disabledColorMultiplier);
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