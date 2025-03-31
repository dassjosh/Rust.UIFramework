using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Controls;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Builder;

public abstract partial class BaseUiBuilder
{
    [Obsolete] public UiPanel Panel(in UiReference parent, in UiPosition pos, UiColor color) => Panel(parent, pos, default, color);
    [Obsolete] public UiButton CommandButton(in UiReference parent, in UiPosition pos, UiColor color, string command) => CommandButton(parent, pos, default, color, command);
    [Obsolete] public UiButton CloseButton(in UiReference parent, in UiPosition pos, UiColor color, string close) => CloseButton(parent, pos, default, color, close);
    [Obsolete] public UiImage ImageSprite(in UiReference parent, in UiPosition pos, in UiOffset offset, string sprite) => ImageSprite(parent, pos, offset, sprite, UiColor.White);
    [Obsolete] public UiImage ImageSprite(in UiReference parent, in UiPosition pos, string sprite, UiColor color) => ImageSprite(parent, pos, default, sprite, color);
    [Obsolete] public UiImage ImageSprite(in UiReference parent, in UiPosition pos, string sprite) => ImageSprite(parent, pos, sprite, UiColor.White);
    [Obsolete] public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, int itemId, UiColor color) => ItemIcon(parent, pos, offset, itemId, 0, color);
    [Obsolete] public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, int itemId) => ItemIcon(parent, pos, offset, itemId, UiColor.White);
    [Obsolete] public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, int itemId, ulong skinId) => ItemIcon(parent, pos, default, itemId, skinId);
    [Obsolete] public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, int itemId, UiColor color) => ItemIcon(parent, pos, default, itemId, color);
    [Obsolete] public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, int itemId) => ItemIcon(parent, pos, default, itemId);
    [Obsolete] public UiPlayerAvatar PlayerAvatar(in UiReference parent, in UiPosition pos, in UiOffset offset, ulong steamId) => PlayerAvatar(parent, pos, offset, steamId, AvatarType.Medium, UiColor.White);
    [Obsolete] public UiPlayerAvatar PlayerAvatar(in UiReference parent, in UiPosition pos, ulong steamId) => PlayerAvatar(parent, pos, default, steamId);
    [Obsolete] public UiPlayerAvatar PlayerAvatar(in UiReference parent, in UiPosition pos, ulong steamId, UiColor color) => PlayerAvatar(parent, pos, default, steamId, AvatarType.Medium, color);
    [Obsolete] public UiLabel Label(in UiReference parent, in UiPosition pos, string text, int fontSize, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter) => Label(parent, pos, default, text, fontSize, textColor, align);
    [Obsolete] public UiLabelBackground LabelBackground(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter)
    {
        UiLabelBackground control = UiLabelBackground.Create(this, parent, pos, offset, text, fontSize, textColor, backgroundColor, align);
        AddControl(control);
        return control;
    }

    [Obsolete] public UiLabelBackground LabelBackground(in UiReference parent, in UiPosition pos, string text, int fontSize, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter) => LabelBackground(parent, pos, default, text, fontSize, textColor, backgroundColor, align);
    [Obsolete] public UiRawImage ImageFileStorage(in UiReference parent, in UiPosition pos, in UiOffset offset, string png, UiColor? color = null) => RawImage(parent, pos, offset, png, color);
    
    [Obsolete] public UiButton CommandButton(in UiReference parent, UiColor color, string command) => Button(parent, color, command, ButtonType.Command);
    [Obsolete] public UiButton CommandButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor color, string command) => Button(parent, pos, offset, color, command, ButtonType.Command);
    [Obsolete] public UiButton CommandButton(BaseLayout layout, UiColor color, string command) => Button(layout, color, command, ButtonType.Command);
    [Obsolete] public UiButton CloseButton(in UiReference parent, UiColor color, string command) => Button(parent, color, command, ButtonType.Close);
    [Obsolete] public UiButton CloseButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor color, string close) => Button(parent, pos, offset, color, close, ButtonType.Close);
    [Obsolete] public UiButton CloseButton(BaseLayout layout, UiColor color, string command) => Button(layout, color, command, ButtonType.Close);

    [Obsolete] public UiButton TextButton(in UiReference parent, in UiPosition pos, string text, int textSize, UiColor textColor, UiColor buttonColor, string command, TextAnchor align = TextAnchor.MiddleCenter) 
        => TextButton(parent, pos, default, text, textSize, textColor, buttonColor, command, align);
        
    [Obsolete] public UiButton ImageFileStorageButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, string png, string command, UiColor? imageColor = null)
    {
        UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
        ImageFileStorage(button, UiPosition.Full, default, png, imageColor ?? UiColor.White);
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
        WebImage(button, UiPosition.Full, default, url, imageColor ?? UiColor.White);
        return button;
    }

    [Obsolete] public UiButton WebImageButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, string url, string command, UiColor? imageColor = null) => WebImageButton(parent, pos, default, buttonColor, url, command, imageColor);

    [Obsolete] public UiButton ItemIconButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, int itemId, string command) => ItemIconButton(parent, pos, default, buttonColor, itemId, command);
        
    [Obsolete] public UiButton ItemIconButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor buttonColor, int itemId, ulong skinId, string command)
    {
        UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
        ItemIcon(button, UiPosition.Full, itemId, skinId);
        return button;
    }
        
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
        ImageFileStorage(button, UiPosition.Full, default, png, imageColor ?? UiColor.White);
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
        WebImage(button, UiPosition.Full, default, url, imageColor ?? UiColor.White);
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

    [Obsolete] public UiButton CloseItemIconButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, int itemId, ulong skinId, string close) => CloseItemIconButton(parent, pos, default, buttonColor, itemId, skinId, close);
    
    [Obsolete] public UiInputBackground InputBackground(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        UiInputBackground control = UiInputBackground.Create(this, parent, pos, offset, text, fontSize, textColor, backgroundColor, command, align, charsLimit, mode, lineType);
        AddControl(control);
        return control;
    }

    [Obsolete] public UiInputBackground InputBackground(in UiReference parent, in UiPosition pos, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine) =>
        InputBackground(parent, pos, UiOffset.None, text, fontSize, textColor, backgroundColor, command, align, charsLimit, mode, lineType);
}