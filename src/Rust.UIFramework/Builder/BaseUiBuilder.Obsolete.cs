using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
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
    [Obsolete] public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, int itemId, ulong skinId) => ItemIcon(parent, pos, offset, itemId, skinId, UiColor.White);
    [Obsolete] public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, int itemId, UiColor color) => ItemIcon(parent, pos, offset, itemId, 0, color);
    [Obsolete] public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, int itemId) => ItemIcon(parent, pos, offset, itemId, UiColor.White);
    [Obsolete] public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, int itemId, ulong skinId) => ItemIcon(parent, pos, default, itemId, skinId);
    [Obsolete] public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, int itemId, UiColor color) => ItemIcon(parent, pos, default, itemId, color);
    [Obsolete] public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, int itemId) => ItemIcon(parent, pos, default, itemId);
    [Obsolete] public UiPlayerAvatar PlayerAvatar(in UiReference parent, in UiPosition pos, in UiOffset offset, ulong steamId) => PlayerAvatar(parent, pos, offset, steamId, AvatarType.Medium, UiColor.White);
    [Obsolete] public UiPlayerAvatar PlayerAvatar(in UiReference parent, in UiPosition pos, ulong steamId) => PlayerAvatar(parent, pos, default, steamId);
    [Obsolete] public UiPlayerAvatar PlayerAvatar(in UiReference parent, in UiPosition pos, ulong steamId, UiColor color) => PlayerAvatar(parent, pos, default, steamId, AvatarType.Medium, color);
    [Obsolete] public UiRawImage WebImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string url) => WebImage(parent, pos, offset, url, UiColor.White);
    [Obsolete] public UiRawImage WebImage(in UiReference parent, in UiPosition pos, string url, UiColor color) => WebImage(parent, pos, default, url, color);
    [Obsolete] public UiRawImage WebImage(in UiReference parent, in UiPosition pos, string url) => WebImage(parent, pos, url, UiColor.White);
    [Obsolete] public UiRawImage TextureImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string texture) => TextureImage(parent, pos, offset, texture, UiColor.White);
    [Obsolete] public UiRawImage TextureImage(in UiReference parent, in UiPosition pos, string texture, UiColor color) => TextureImage(parent, pos, default, texture, color);
    [Obsolete] public UiRawImage TextureImage(in UiReference parent, in UiPosition pos, string texture) => TextureImage(parent, pos, texture, UiColor.White);
    [Obsolete] public UiRawImage ImageFileStorage(in UiReference parent, in UiPosition pos, string png, UiColor color) => ImageFileStorage(parent, pos, default, png, color);
    [Obsolete] public UiRawImage ImageFileStorage(in UiReference parent, in UiPosition pos, in UiOffset offset, string png) => ImageFileStorage(parent, pos, offset, png, UiColor.White);
    [Obsolete] public UiRawImage ImageFileStorage(in UiReference parent, in UiPosition pos, string png) => ImageFileStorage(parent, pos, default, png, UiColor.White);
    [Obsolete] public UiLabel Label(in UiReference parent, in UiPosition pos, string text, int fontSize, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter) => Label(parent, pos, default, text, fontSize, textColor, align);
    [Obsolete] public UiInput Input(in UiReference parent, in UiPosition pos, string text, int fontSize, UiColor textColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine) => Input(parent, pos, default, text, fontSize, textColor, command, align, charsLimit, mode, lineType);
    [Obsolete] public UiTuple<UiButton, UiLabel> TextButton(in UiReference parent, in UiPosition pos, string text, int textSize, UiColor textColor, UiColor buttonColor, string command, TextAnchor align = TextAnchor.MiddleCenter) => TextButton(parent, pos, default, text, textSize, textColor, buttonColor, command, align);
    [Obsolete] public UiTuple<UiButton, UiRawImage> ImageFileStorageButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, string png, string command, UiColor? imageColor = null) => ImageFileStorageButton(parent, pos, default, buttonColor, png, command, imageColor);
    [Obsolete] public UiTuple<UiButton, UiImage> ImageSpriteButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, string sprite, string command, UiColor? spriteColor = null) => ImageSpriteButton(parent, pos, default, buttonColor, sprite, command, spriteColor);
    [Obsolete] public UiTuple<UiButton, UiRawImage> WebImageButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, string url, string command, UiColor? imageColor = null) => WebImageButton(parent, pos, default, buttonColor, url, command, imageColor);
    [Obsolete] public UiTuple<UiButton, UiItemIcon> ItemIconButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, int itemId, string command) => ItemIconButton(parent, pos, default, buttonColor, itemId, command);
    [Obsolete] public UiTuple<UiButton, UiItemIcon> ItemIconButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, int itemId, ulong skinId, string command) => ItemIconButton(parent, pos, default, buttonColor, itemId, skinId, command);
    [Obsolete] public UiTuple<UiButton, UiLabel> CloseTextButton(in UiReference parent, in UiPosition pos, string text, int textSize, UiColor textColor, UiColor buttonColor, string close, TextAnchor align = TextAnchor.MiddleCenter) => CloseTextButton(parent, pos, default, text, textSize, textColor, buttonColor, close, align);
    [Obsolete] public UiTuple<UiButton, UiRawImage> CloseImageFileStorageButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, string png, string close, UiColor? imageColor = null) => CloseImageFileStorageButton(parent, pos, default, buttonColor, png, close, imageColor);
    [Obsolete] public UiTuple<UiButton, UiImage> CloseImageSpriteButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, string sprite, string close, UiColor? spriteColor = null) => CloseImageSpriteButton(parent, pos, default, buttonColor, sprite, close, spriteColor);
    [Obsolete] public UiTuple<UiButton, UiRawImage> CloseWebImageButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, string url, string close, UiColor? imageColor = null) => CloseWebImageButton(parent, pos, default, buttonColor, url, close, imageColor);
    [Obsolete] public UiTuple<UiButton, UiItemIcon> CloseItemIconButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, int itemId, string close) => CloseItemIconButton(parent, pos, default, buttonColor, itemId, close);
    [Obsolete] public UiTuple<UiButton, UiItemIcon> CloseItemIconButton(in UiReference parent, in UiPosition pos, UiColor buttonColor, int itemId, ulong skinId, string close) => CloseItemIconButton(parent, pos, default, buttonColor, itemId, skinId, close);
    [Obsolete] public UiTuple<UiPanel, UiLabel> LabelBackground(in UiReference parent, in UiPosition pos, string text, int fontSize, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter) => LabelBackground(parent, pos, default, text, fontSize, textColor, backgroundColor, align);
    [Obsolete] public UiTuple<UiPanel, UiInput> InputBackground(in UiReference parent, in UiPosition pos, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine) =>
        InputBackground(parent, pos, UiOffset.None, text, fontSize, textColor, backgroundColor, command, align, charsLimit, mode, lineType);
}