using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Controls;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable CS0612 // Type or member is obsolete

namespace Oxide.Ext.UiFramework.Builder;

public abstract partial class BaseUiBuilder
{
    #region Panel
    [Obsolete] public UiPanel Panel(in UiReference parent, in UiPosition pos, UiColor color) => Panel(parent, pos, default, color);
    #endregion

    #region Button

    [Obsolete("Use Button() Instead")] public UiButton CommandButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor color, string command)
        => Button(parent, pos, offset, color, command);

    [Obsolete("Use Button() Instead")] public UiButton CommandButton(in UiReference parent, in UiPosition pos, UiColor color, string command) => CommandButton(parent, pos, default, color, command);

    [Obsolete("Use Button() Instead")] public UiButton CloseButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor color, string close)
        => Button(parent, pos, offset, color, close, ButtonType.Close);

    [Obsolete("Use Button() Instead")] public UiButton CloseButton(in UiReference parent, in UiPosition pos, UiColor color, string close) => CloseButton(parent, pos, default, color, close);
    #endregion

    #region Image
    //[Obsolete] public UiImage ImageSprite(in UiReference parent, in UiPosition pos, in UiOffset offset, string sprite) => ImageSprite(parent, pos, offset, sprite, UiColors.White);
    [Obsolete] public UiImage ImageSprite(in UiReference parent, in UiPosition pos, string sprite, UiColor color) => ImageSprite(parent, pos, default, sprite, color);
    [Obsolete] public UiImage ImageSprite(in UiReference parent, in UiPosition pos, string sprite) => ImageSprite(parent, pos, sprite, UiColors.White);
    #endregion

    #region Item Icon
    [Obsolete] public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, int itemId, ulong skinId) => ItemIcon(parent, pos, offset, itemId, skinId, UiColors.White);
    [Obsolete] public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, int itemId, UiColor color) => ItemIcon(parent, pos, offset, itemId, 0, color);
    //[Obsolete] public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, int itemId) => ItemIcon(parent, pos, offset, itemId, UiColors.White);
    [Obsolete] public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, int itemId, ulong skinId) => ItemIcon(parent, pos, default, itemId, skinId);
    [Obsolete] public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, int itemId, UiColor color) => ItemIcon(parent, pos, default, itemId, color);
    [Obsolete] public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, int itemId) => ItemIcon(parent, pos, default, itemId);
    #endregion
    
    #region Player Avatar

    [Obsolete] public UiPlayerAvatar PlayerAvatar(in UiReference parent, in UiPosition pos, in UiOffset offset, ulong steamId, UiColor color)
        => PlayerAvatar(parent, pos, offset, steamId, AvatarType.Medium, color);
    //[Obsolete] public UiPlayerAvatar PlayerAvatar(in UiReference parent, in UiPosition pos, in UiOffset offset, ulong steamId) => PlayerAvatar(parent, pos, offset, steamId, UiColors.White);
    [Obsolete] public UiPlayerAvatar PlayerAvatar(in UiReference parent, in UiPosition pos, ulong steamId) => PlayerAvatar(parent, pos, default, steamId);
    [Obsolete] public UiPlayerAvatar PlayerAvatar(in UiReference parent, in UiPosition pos, ulong steamId, UiColor color) => PlayerAvatar(parent, pos, default, steamId, color);
    #endregion

    #region Raw Image
    //[Obsolete] public UiRawImage WebImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string url) => WebImage(parent, pos, offset, url, UiColors.White);
    [Obsolete] public UiRawImage WebImage(in UiReference parent, in UiPosition pos, string url, UiColor color) => WebImage(parent, pos, default, url, color);
    [Obsolete] public UiRawImage WebImage(in UiReference parent, in UiPosition pos, string url) => WebImage(parent, pos, url, UiColors.White);
    //[Obsolete] public UiRawImage TextureImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string texture) => TextureImage(parent, pos, offset, texture, UiColors.White);
    [Obsolete] public UiRawImage TextureImage(in UiReference parent, in UiPosition pos, string texture, UiColor color) => TextureImage(parent, pos, default, texture, color);
    [Obsolete] public UiRawImage TextureImage(in UiReference parent, in UiPosition pos, string texture) => TextureImage(parent, pos, texture, UiColors.White);
    [Obsolete] public UiRawImage ImageFileStorage(in UiReference parent, in UiPosition pos, in UiOffset offset, string png, UiColor color) 
        => FileStorageImage(parent, pos, offset, png, color);
    [Obsolete] public UiRawImage ImageFileStorage(in UiReference parent, in UiPosition pos, string png, UiColor color) => FileStorageImage(parent, pos, default, png, color);
    [Obsolete] public UiRawImage ImageFileStorage(in UiReference parent, in UiPosition pos, in UiOffset offset, string png) => FileStorageImage(parent, pos, offset, png, UiColors.White);
    [Obsolete] public UiRawImage ImageFileStorage(in UiReference parent, in UiPosition pos, string png) => FileStorageImage(parent, pos, default, png, UiColors.White);
    #endregion

    #region Label
    [Obsolete] public UiLabel Label(in UiReference parent, in UiPosition pos, string text, int fontSize, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter) => Label(parent, pos, default, text, fontSize, textColor, align);

    [Obsolete] public UiLabelBackground LabelBackground(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter)
    {
        UiLabelBackground control = UiLabelBackground.Create(this, parent, pos, offset, text, fontSize, textColor, backgroundColor, align);
        AddControl(control);
        return control;
    }

    [Obsolete] public UiLabelBackground LabelBackground(in UiReference parent, in UiPosition pos, string text, int fontSize, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter) => LabelBackground(parent, pos, default, text, fontSize, textColor, backgroundColor, align);
    #endregion
        
    #region Input
    [Obsolete] public UiInput Input(in UiReference parent, in UiPosition pos, string text, int fontSize, UiColor textColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine) 
        => Input(parent, pos, default, text, fontSize, textColor, command, align, charsLimit, mode, lineType);
    #endregion

    #region Countdown
    [Obsolete("This method is obsolete. Use Countdown(UiLabel, float, float, string, float, float, TimerFormat, string, bool) instead.")]
    public UiLabel Countdown(UiLabel label, int startTime, int endTime, int step, string command)
    {
        Countdown(label, startTime, endTime, command, step);
        return label;
    }
    #endregion

    #region Outline
    [Obsolete] public T Outline<T>(T outline, UiColor color) where T : BaseUiComponent
    {
        outline.AddOutline(color);
        return outline;
    }

    [Obsolete] public T Outline<T>(T outline, UiColor color, Vector2 distance, bool useGraphicAlpha = false) where T : BaseUiComponent
    {
        outline.AddOutline(color, distance, useGraphicAlpha);
        return outline;
    }
    #endregion

    #region ScrollView
    [Obsolete] public UiScrollView ScrollView(in UiReference parent, in UiPosition pos, in UiOffset offset, bool horizontal = false, bool vertical = false, ScrollRect.MovementType movementType = ScrollRect.MovementType.Clamped, float elasticity = 0.1f,
        bool inertia = false, float decelerationRate = 0.135f, float scrollSensitivity = 1f)
        => ScrollView(in parent, in pos, in offset, movementType, elasticity, inertia, decelerationRate, scrollSensitivity);
    #endregion
    
    #region Buttons
    [Obsolete] public UiButton TextButton(in UiReference parent, in UiPosition pos, string text, int textSize, UiColor textColor, UiColor buttonColor, string command, TextAnchor align = TextAnchor.MiddleCenter) 
        => TextButton(parent, pos, default, text, textSize, textColor, buttonColor, command, align);
        
    [Obsolete] public UiButton ImageFileStorageButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, string png, string command, UiColor? imageColor = null)
    {
        UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
        ImageFileStorage(button, UiPosition.Full, png, imageColor ?? UiColor.White);
        return button;
    }

    [Obsolete] public UiButton ImageFileStorageButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, string png, string command, UiColor? imageColor = null) => ImageFileStorageButton(parent, pos, default, buttonColor, png, command, imageColor);
        
    [Obsolete] public UiButton ImageSpriteButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, string sprite, string command, UiColor? imageColor = null)
    {
        UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
        ImageSprite(button, UiPosition.Full, sprite, imageColor ?? UiColor.White);
        return button;
    }

    [Obsolete] public UiButton ImageSpriteButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, string sprite, string command, UiColor? imageColor = null) => ImageSpriteButton(parent, pos, default, buttonColor, sprite, command, imageColor);
        
    [Obsolete] public UiButton WebImageButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, string url, string command, UiColor? imageColor = null)
    {
        UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
        WebImage(button, UiPosition.Full, url, imageColor ?? UiColor.White);
        return button;
    }

    [Obsolete] public UiButton WebImageButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, string url, string command, UiColor? imageColor = null) => WebImageButton(parent, pos, default, buttonColor, url, command, imageColor);
        
    // [Obsolete] public UiButton ItemIconButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, int itemId, string command)
    // {
    //     UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
    //     ItemIcon(button, UiPosition.Full, itemId);
    //     return button;
    // }

    [Obsolete] public UiButton ItemIconButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, int itemId, string command) => ItemIconButton(parent, pos, default, buttonColor, itemId, command);
        
    // [Obsolete] public UiButton ItemIconButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, int itemId, ulong skinId, string command)
    // {
    //     UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
    //     ItemIcon(button, UiPosition.Full, itemId, skinId);
    //     return button;
    // }

    [Obsolete] public UiButton ItemIconButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, int itemId, ulong skinId, string command) => ItemIconButton(parent, pos, default, buttonColor, itemId, skinId, command);
        
    [Obsolete] public UiButton CloseTextButton(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int textSize, UiColor textColor, UiColor buttonColor, string close, TextAnchor align = TextAnchor.MiddleCenter)
    {
        UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
        Label(button, UiPosition.HorizontalPaddedFull, text, textSize, textColor , align);
        return button;
    }

    [Obsolete] public UiButton CloseTextButton(in UiReference parent, in UiPosition pos, string text, int textSize, UiColor textColor, UiColor buttonColor, string close, TextAnchor align = TextAnchor.MiddleCenter) 
        => CloseTextButton(parent, pos, default, text, textSize, textColor, buttonColor, close, align);
        
    [Obsolete] public UiButton CloseImageFileStorageButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, string png, string close, UiColor? imageColor = null)
    {
        UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
        ImageFileStorage(button, UiPosition.Full, png, imageColor ?? UiColor.White);
        return button;
    }

    [Obsolete] public UiButton CloseImageFileStorageButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, string png, string close, UiColor? imageColor = null) => CloseImageFileStorageButton(parent, pos, default, buttonColor, png, close, imageColor);
        
    [Obsolete] public UiButton CloseImageSpriteButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, string sprite, string close, UiColor? imageColor = null)
    {
        UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
        ImageSprite(button, UiPosition.Full, sprite, imageColor ?? UiColor.White);
        return button;
    }

    [Obsolete] public UiButton CloseImageSpriteButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, string sprite, string close, UiColor? imageColor = null) => CloseImageSpriteButton(parent, pos, default, buttonColor, sprite, close, imageColor);
        
    [Obsolete] public UiButton CloseWebImageButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, string url, string close, UiColor? imageColor = null)
    {
        UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
        WebImage(button, UiPosition.Full, url, imageColor ?? UiColor.White);
        return button;
    }

    [Obsolete] public UiButton CloseWebImageButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, string url, string close, UiColor? imageColor = null) => CloseWebImageButton(parent, pos, default, buttonColor, url, close, imageColor);
        
    [Obsolete] public UiButton CloseItemIconButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, int itemId, string close)
    {
        UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
        ItemIcon(button, UiPosition.Full, itemId);
        return button;
    }

    [Obsolete] public UiButton CloseItemIconButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, int itemId, string close) => CloseItemIconButton(parent, pos, default, buttonColor, itemId, close);
        
    [Obsolete] public UiButton CloseItemIconButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, int itemId, ulong skinId, string close)
    {
        UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
        ItemIcon(button, UiPosition.Full, itemId, skinId);
        return button;
    }

    public UiButton CloseItemIconButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, int itemId, ulong skinId, string close) => CloseItemIconButton(parent, pos, default, buttonColor, itemId, skinId, close);
    #endregion

    #region Input Background
    [Obsolete] public UiInputBackground InputBackground(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        UiInputBackground control = UiInputBackground.Create(this, parent, pos, offset, text, fontSize, textColor, backgroundColor, command, align, charsLimit, mode, lineType);
        AddControl(control);
        return control;
    }

    [Obsolete] public UiInputBackground InputBackground(in UiReference parent, in UiPosition pos, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine) =>
        InputBackground(parent, pos, UiOffset.None, text, fontSize, textColor, backgroundColor, command, align, charsLimit, mode, lineType);
    #endregion

    #region Checkbox

    [Obsolete] public UiCheckbox Checkbox(in UiReference parent, in UiPosition pos, in UiOffset offset, bool isChecked, int textSize, UiColor textColor, UiColor backgroundColor, string command)
        => Checkbox(parent, pos, offset, isChecked, command, textColor, textColor, backgroundColor);
    #endregion

    #region ProgressBar
    // [Obsolete] public UiProgressBar ProgressBar(in UiReference parent, in UiPosition pos, in UiOffset offset, float percentage, UiColor barColor, UiColor backgroundColor)
    // {
    //     UiProgressBar control = UiProgressBar.Create(this, parent, pos, offset, percentage, barColor, backgroundColor);
    //     AddControl(control);
    //     return control;
    // }
    #endregion

    // #region Button Groups
    // public UiButtonGroup ButtonGroup(in UiReference parent, in UiPosition pos, in UiOffset offset, List<ButtonGroupData> buttons, int textSize, UiColor textColor, UiColor buttonColor, UiColor currentButtonColor, string command)
    // {
    //     for (int index = 0; index < buttons.Count; index++)
    //     {
    //         ButtonGroupData button = buttons[index];
    //         button.Command = $"{command} {button.}"
    //     }
    //
    //     UiButtonGroup control = UiButtonGroup.Create(this, parent, pos, offset, buttons, textSize, textColor, buttonColor, currentButtonColor, command);
    //     AddControl(control);
    //     return control;
    // }
    // #endregion
    //
    // #region Number Picker
    // public UiNumberPicker NumberPicker(in UiReference parent, in UiPosition pos, in UiOffset offset, int value, int fontSize, int buttonFontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiColor disabledButtonColor, string command, string incrementCommand, string decrementCommand, int minValue = int.MinValue, int maxValue = int.MaxValue, float buttonWidth = 0.1f, TextAnchor align = TextAnchor.MiddleRight, InputMode mode = InputMode.Default, NumberPickerMode numberMode = NumberPickerMode.LeftRight, string numberFormat = null)
    // {
    //     UiNumberPicker control = UiNumberPicker.Create(this, parent, pos, offset, value, fontSize, buttonFontSize, textColor, backgroundColor, buttonColor, disabledButtonColor, command, incrementCommand, decrementCommand, minValue, maxValue, buttonWidth, align, mode, numberMode, numberFormat);
    //     AddControl(control);
    //     return control;
    // }
    // #endregion
    //
}