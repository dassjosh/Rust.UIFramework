using System;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Controls;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Builder;

public partial class BaseUiBuilder
{
    #region Add Components

    public void AddControl(BaseUiControl control) => Controls.Add(control);

    public void AddLayout(BaseUiLayout layout) => Layouts.Add(layout);

    #endregion

    #region Component
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Component<T>(in UiReference reference) where T : BaseUiComponent, new()
    {
        UiReferenceException.ThrowIfInvalidReference(reference);
        T component = PluginPool.Get<T>();
        Components.Add(component);
        Naming.SetComponentName(component, reference, NamingMode, NamingCache, Components.Count);
        return component.SetUpdate(UpdateMode);
    }
    #endregion

    #region Update

    public T Update<T>(string name) where T : BaseUiComponent, new()
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
        T component = PluginPool.Get<T>().SetUpdate(UpdateMode.Update).SetName(name);
        Components.Add(component);
        return component;
    }

    public T Update<T>(in UiReference reference) where T : BaseUiComponent, new() => Update<T>(reference.Name);
    #endregion
    
    #region Section
    public UiSection Section(in UiReference parent) => Component<UiSection>(parent);
    public UiSection Section(in UiReference parent, in UiPosition pos, in UiOffset offset = default) => Section(parent).SetPosition(pos, offset);
    public UiSection Section(BaseUiLayout layout) => Section(layout.Reference).SetLayout(layout);
    #endregion
        
    #region Panel
    public UiPanel Panel(in UiReference parent) => Component<UiPanel>(parent);
    public UiPanel Panel(in UiReference parent, UiColor color) => Panel(parent).SetColor(color);
    public UiPanel Panel(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor color) => Panel(parent, color).SetPosition(pos, offset);
    public UiPanel Panel(BaseUiLayout layout, UiColor color) => Panel(layout.Reference, color).SetLayout(layout);
    #endregion

    #region Button
    public UiButton Button(in UiReference parent) => Component<UiButton>(parent);
    public UiButton Button(in UiReference parent, UiColor color, string command, ButtonType buttonType = ButtonType.Command) => Button(parent).Init(color, command, buttonType);
    public UiButton Button(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor color, string command, ButtonType buttonType = ButtonType.Command) => Button(parent, color, command, buttonType).SetPosition(pos, offset);
    public UiButton Button(BaseUiLayout layout, UiColor color, string command, ButtonType buttonType = ButtonType.Command) => Button(layout.Reference, color, command, buttonType).SetLayout(layout);

    #endregion

    #region Image
    public UiImage ImageSprite(in UiReference parent) => Component<UiImage>(parent);
    public UiImage ImageSprite(in UiReference parent, string sprite, UiColor? color = default) => ImageSprite(parent).Init(sprite, color ?? UiColors.White);
    public UiImage ImageSprite(in UiReference parent, in UiPosition pos, in UiOffset offset, string sprite, UiColor? color = default) => ImageSprite(parent, sprite, color).SetPosition(pos, offset);
    public UiImage ImageSprite(BaseUiLayout layout, string sprite, UiColor? color = default) => ImageSprite(layout.Reference, sprite, color).SetLayout(layout);
    #endregion

    #region PlayingCard
    public UiPlayingCard PlayingCard(in UiReference parent) => Component<UiPlayingCard>(parent);
    public UiPlayingCard PlayingCard(in UiReference parent, PlayingCardData card, UiCardType type = UiCardType.Normal, UiColor? color = default) => PlayingCard(parent).Init(card, type, color ?? UiColors.White);
    public UiPlayingCard PlayingCard(in UiReference parent, in UiPosition pos, in UiOffset offset, PlayingCardData card, UiCardType type = UiCardType.Normal, UiColor? color = default) => PlayingCard(parent, card, type, color).SetPosition(pos, offset);
    public UiPlayingCard PlayingCard(BaseUiLayout layout, PlayingCardData card, UiCardType type = UiCardType.Normal, UiColor? color = default) => PlayingCard(layout.Reference, card, type, color).SetLayout(layout);

    #endregion

    #region Item Icon
    public UiItemIcon ItemIcon(in UiReference parent) => Component<UiItemIcon>(parent);
    public UiItemIcon ItemIcon(in UiReference parent, int itemId, ulong skinId = 0, UiColor? color = default) => ItemIcon(parent).Init(itemId, skinId, color ?? UiColors.White);
    public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, int itemId, ulong skinId = 0, UiColor? color = default) => ItemIcon(parent, itemId, skinId, color).SetPosition(pos, offset);
    public UiItemIcon ItemIcon(BaseUiLayout layout, int itemId, ulong skinId = 0, UiColor? color = default) => ItemIcon(layout.Reference, itemId, skinId, color).SetLayout(layout);
    
    public UiItemIcon ItemIcon(in UiReference parent, Item item, UiColor? color = default) => ItemIcon(parent, item.info.itemid, item.skin, color);
    public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, Item item, UiColor? color = default) => ItemIcon(parent, pos, offset, item.info.itemid, item.skin, color);
    public UiItemIcon ItemIcon(BaseUiLayout layout, Item item, UiColor? color = default) => ItemIcon(layout, item.info.itemid, item.skin, color);
    #endregion
    
    #region Player Avatar
    public UiPlayerAvatar PlayerAvatar(in UiReference parent) => Component<UiPlayerAvatar>(parent);
    public UiPlayerAvatar PlayerAvatar(in UiReference parent, ulong steamId, AvatarType type = AvatarType.Medium, UiColor? color = default) => PlayerAvatar(parent).Init(steamId, type, color ?? UiColors.White);
    public UiPlayerAvatar PlayerAvatar(in UiReference parent, in UiPosition pos, in UiOffset offset, ulong steamId, AvatarType type = AvatarType.Medium, UiColor? color = default) => PlayerAvatar(parent, steamId, type, color).SetPosition(pos, offset);
    public UiPlayerAvatar PlayerAvatar(BaseUiLayout layout, ulong steamId, AvatarType type = AvatarType.Medium, UiColor? color = default) => PlayerAvatar(layout.Reference, steamId, type, color).SetLayout(layout);
    #endregion

    #region Raw Image
    public UiRawImage RawImage(in UiReference parent) => Component<UiRawImage>(parent);
    public UiRawImage RawImage(in UiReference parent, string image, UiColor? color = default) => RawImage(parent).Init(image, color ?? UiColors.White);
    public UiRawImage RawImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string image, UiColor? color = default) => RawImage(parent, image, color).SetPosition(pos, offset);
    public UiRawImage RawImage(BaseUiLayout layout, string image, UiColor? color = default) => RawImage(layout.Reference,image, color).SetLayout(layout);
    

    public UiRawImage WebImage(in UiReference parent, string url, UiColor? color = default) => RawImage(parent, url, color);
    public UiRawImage WebImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string url, UiColor? color = default) => RawImage(parent, pos, offset, url, color);
    public UiRawImage WebImage(BaseUiLayout layout, string url, UiColor? color = default) => RawImage(layout, url, color);
    
    public UiRawImage TextureImage(in UiReference parent, string texture, UiColor? color = default) => RawImage(parent, texture, color);
    public UiRawImage TextureImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string texture, UiColor? color = default) => RawImage(parent, pos, offset, texture, color);
    public UiRawImage TextureImage(BaseUiLayout layout, string texture, UiColor? color = default) => RawImage(layout, texture, color);

    
    public UiRawImage FileStorageImage(in UiReference parent, string imageId, UiColor? color = default) => RawImage(parent, imageId, color);
    public UiRawImage FileStorageImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string imageId, UiColor? color = default) => RawImage(parent, pos, offset, imageId, color);
    public UiRawImage FileStorageImage(BaseUiLayout layout, string imageId, UiColor? color = default) => RawImage(layout, imageId, color);
    
    public UiRawImage ImageStorage(in UiReference parent, string nameOrUrl, ImageDownloadOptions options = null, UiColor? color = default) 
        => RawImage(parent, Singleton<UiImageStorage>.Instance.Get(Plugin, nameOrUrl, options), color);
    public UiRawImage ImageStorage(in UiReference parent, in UiPosition pos, in UiOffset offset, string nameOrUrl, ImageDownloadOptions options = null, UiColor? color = default) 
        => RawImage(parent, pos, offset, Singleton<UiImageStorage>.Instance.Get(Plugin, nameOrUrl, options), color);
    public UiRawImage ImageStorage(BaseUiLayout layout, string nameOrUrl, ImageDownloadOptions options = null, UiColor? color = default)
        => RawImage(layout, Singleton<UiImageStorage>.Instance.Get(Plugin, nameOrUrl, options), color);
    #endregion
    
    #region Icon
    public UiIcon Icon(in UiReference parent) => Component<UiIcon>(parent);
    public UiIcon Icon<T>(in UiReference parent, T icon, UiColor? color = default) where T : struct, Enum => Icon(parent).Init(icon, color ?? UiColors.White);
    public UiIcon Icon<T>(in UiReference parent, in UiPosition pos, in UiOffset offset, T icon, UiColor? color = default) where T : struct, Enum => Icon(parent, icon, color).SetPosition(pos, offset);
    public UiIcon Icon<T>(BaseUiLayout layout, T icon, UiColor? color = default) where T : struct, Enum => Icon(layout.Reference, icon, color);
    #endregion

    #region Label
    public UiLabel Label(in UiReference parent) => Component<UiLabel>(parent);
    public UiLabel Label(in UiReference parent, string text, int size, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter) => Label(parent).Init(text, size, textColor, align, Font);
    public UiLabel Label(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int size, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter) => Label(parent, text, size, textColor, align).SetPosition(pos, offset);
    public UiLabel Label(BaseUiLayout layout, string text, int size, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter) => Label(layout.Reference, text, size, textColor, align).SetLayout(layout);

    public UiTuple<UiPanel, UiLabel> Label(in UiReference parent, string text, int size, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter, in UiPadding? textPadding = null)
    {
        UiPanel panel = Panel(parent, backgroundColor);
        UiLabel label = Label(panel, UiPosition.Full, default, text, size, textColor, align).SetOffsetPadding(textPadding ?? JsonDefaults.Common.TextPadding);
        return UiTuple.Create(panel, label);
    }
    
    public UiTuple<UiPanel, UiLabel> Label(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int size, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter, in UiPadding? textPadding = null)
    {
        UiPanel panel = Panel(parent, pos, offset, backgroundColor);
        UiLabel label = Label(panel, UiPosition.Full, default, text, size, textColor, align).SetOffsetPadding(textPadding ?? JsonDefaults.Common.TextPadding);
        return UiTuple.Create(panel, label);
    }
    
    public UiTuple<UiPanel, UiLabel> Label(BaseUiLayout layout, string text, int size, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter, in UiPadding? textPadding = null)
    {
        UiPanel panel = Panel(layout, backgroundColor);
        UiLabel label = Label(panel, UiPosition.Full, default, text, size, textColor, align).SetOffsetPadding(textPadding ?? JsonDefaults.Common.TextPadding);
        return UiTuple.Create(panel, label);
    }
    #endregion
        
    #region Input
    public UiInput Input(in UiReference parent) => Component<UiInput>(parent);
    public UiInput Input(in UiReference parent, string text, int fontSize, UiColor textColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
        => Input(parent).Init(text, fontSize, textColor, command, Font, align, charsLimit, mode, lineType);
    public UiInput Input(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
        => Input(parent, text, fontSize, textColor, command, align, charsLimit, mode, lineType).SetPosition(pos, offset);
    public UiInput Input(BaseUiLayout layout, string text, int fontSize, UiColor textColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
        => Input(layout.Reference, text, fontSize, textColor, command, align, charsLimit, mode, lineType).SetLayout(layout);
    
    public UiTuple<UiPanel, UiInput> Input(in UiReference parent, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null)
    {
        UiPanel background = Panel(parent).SetColor(backgroundColor);
        UiInput input = Input(background, UiPosition.Full, default, text, fontSize, textColor, command, align, charsLimit, mode, lineType).SetOffsetPadding(textPadding ?? JsonDefaults.Common.TextPadding);
        return UiTuple.Create(background, input);
    }
    
    public UiTuple<UiPanel, UiInput> Input(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null)
    {
        UiPanel panel = Panel(parent, pos, offset, backgroundColor);
        UiInput input = Input(panel, UiPosition.Full, default, text, fontSize, textColor, command, align, charsLimit, mode, lineType).SetOffsetPadding(textPadding ?? JsonDefaults.Common.TextPadding);
        return UiTuple.Create(panel, input);
    }
    
    public UiTuple<UiPanel, UiInput> Input(BaseUiLayout layout, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null)
    {
        UiPanel panel = Panel(layout, backgroundColor);
        UiInput input = Input(panel, UiPosition.Full, default, text, fontSize, textColor, command, align, charsLimit, mode, lineType).SetOffsetPadding(textPadding ?? JsonDefaults.Common.TextPadding);
        return UiTuple.Create(panel, input);
    }
    
    public UiInput Input(in UiReference parent, string text, int fontSize, UiColor textColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine) 
        => Input(parent, text, fontSize, textColor, command.Build(InputArg.Empty), align, charsLimit, mode, lineType);
    public UiInput Input(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
        => Input(parent, pos, offset, text, fontSize, textColor, command.Build(InputArg.Empty), align, charsLimit, mode, lineType);
    public UiInput Input(BaseUiLayout layout, string text, int fontSize, UiColor textColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine) 
        => Input(layout, text, fontSize, textColor, command.Build(InputArg.Empty), align, charsLimit, mode, lineType);
    
    public UiTuple<UiPanel, UiInput> Input(in UiReference parent, string text, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null) 
        => Input(parent, text, fontSize, textColor, backgroundColor, command.Build(InputArg.Empty), align, charsLimit, mode, lineType, textPadding);
    public UiTuple<UiPanel, UiInput> Input(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null) 
        => Input(parent, pos, offset, text, fontSize, textColor, backgroundColor, command.Build(InputArg.Empty), align, charsLimit, mode, lineType, textPadding);
    public UiTuple<UiPanel, UiInput> Input(BaseUiLayout layout, string text, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null)
        => Input(layout, text, fontSize, textColor, backgroundColor, command.Build(InputArg.Empty), align, charsLimit, mode, lineType, textPadding);
    #endregion

    #region Anchor
    public UiSection Anchor(in UiReference reference, string anchorName = null)
    {
        UiReferenceException.ThrowIfInvalidReference(reference);
        UiSection section = PluginPool.Get<UiSection>();
        Anchors.Add(section);
        if (string.IsNullOrEmpty(anchorName))
        {
            Naming.SetAnchorName(section, reference, NamingMode, NamingCache, Components.Count);
        }
        else
        {
            section.Reference = new UiReference(reference.Parent, anchorName);
        }
        return section;
    }
    
    public UiSection Anchor(in UiReference parent, in UiPosition pos, in UiOffset offset = default, string anchorName = null)
    {
        return Anchor(parent, anchorName).SetPosition(pos, offset);
    }

    public UiSection AnchorV2(BaseUiComponent component, string anchorName = null)
    {
        if (component == null) throw new ArgumentNullException(nameof(component));
        UiSection section = PluginPool.Get<UiSection>();
        Anchors.Add(section);
        if (string.IsNullOrEmpty(anchorName))
        {
            Naming.SetAnchorName(section, component.Reference, NamingMode, NamingCache, Components.Count);
        }
        else
        {
            section.Reference = new UiReference(component.Parent, anchorName);
        }
        section.ShareSubComponent(component.RectTransform);
        return section;
    }
    #endregion

    #region ScrollView
    public UiScrollView ScrollView(in UiReference parent) => Component<UiScrollView>(parent);

    public UiScrollView ScrollView(in UiReference parent,
        ScrollRect.MovementType movementType = JsonDefaults.ScrollView.MovementType, 
        float elasticity = JsonDefaults.ScrollView.Elasticity,
        bool inertia = JsonDefaults.ScrollView.Inertia, 
        float decelerationRate = JsonDefaults.ScrollView.DecelerationRate, 
        float scrollSensitivity = JsonDefaults.ScrollView.ScrollSensitivity) =>
        ScrollView(parent).Init(movementType, elasticity, inertia, decelerationRate, scrollSensitivity);
    
    public UiScrollView ScrollView(in UiReference parent, in UiPosition pos, in UiOffset offset, 
        ScrollRect.MovementType movementType = JsonDefaults.ScrollView.MovementType, 
        float elasticity = JsonDefaults.ScrollView.Elasticity,
        bool inertia = JsonDefaults.ScrollView.Inertia, 
        float decelerationRate = JsonDefaults.ScrollView.DecelerationRate, 
        float scrollSensitivity = JsonDefaults.ScrollView.ScrollSensitivity) =>
        ScrollView(parent, movementType, elasticity, inertia, decelerationRate, scrollSensitivity).SetPosition(pos, offset);

    public UiScrollView ScrollView(BaseUiLayout layout, 
        ScrollRect.MovementType movementType = JsonDefaults.ScrollView.MovementType, 
        float elasticity = JsonDefaults.ScrollView.Elasticity,
        bool inertia = JsonDefaults.ScrollView.Inertia, 
        float decelerationRate = JsonDefaults.ScrollView.DecelerationRate, 
        float scrollSensitivity = JsonDefaults.ScrollView.ScrollSensitivity) =>
        ScrollView(layout.Reference, movementType, elasticity, inertia, decelerationRate, scrollSensitivity).SetLayout(layout);
    #endregion

    #region Nine Slice
    public UiNineSlice NineSlice(in UiReference parent) => Component<UiNineSlice>(parent);
    public UiNineSlice NineSlice(in UiReference parent, string png, in UiBorderWidth slice, bool fillCenter = false, UiColor? color = default, Image.Type type = Image.Type.Simple)
        => NineSlice(parent).Init(png, slice, fillCenter, color ?? UiColors.White, type);
    public UiNineSlice NineSlice(in UiReference parent, in UiPosition pos, in UiOffset offset, string png, in UiBorderWidth slice, bool fillCenter = false, UiColor? color = default, Image.Type type = Image.Type.Simple)
        => NineSlice(parent, png, slice, fillCenter, color, type).SetPosition(pos, offset);
    public UiNineSlice NineSlice(BaseUiLayout layout, string png, in UiBorderWidth slice, bool fillCenter = false, UiColor? color = default, Image.Type type = Image.Type.Simple)
        => NineSlice(layout.Reference, png, slice, fillCenter, color, type).SetLayout(layout);

    public UiNineSlice Border(in UiReference parent, in UiBorderWidth width, UiColor? color = default)
        => NineSlice(parent, UiPosition.Full, default, Singleton<UiImageStorage>.Instance.Get(UiFrameworkPlugin.Instance, UiImages.White1x1Name), width, false, color ?? UiColors.White, Image.Type.Sliced);
    public UiNineSlice Border(in UiReference parent, in UiPosition position, in UiOffset offset, in UiBorderWidth width, UiColor? color = default)
        => NineSlice(parent, position, offset, Singleton<UiImageStorage>.Instance.Get(UiFrameworkPlugin.Instance, UiImages.White1x1Name), width, false, color ?? UiColors.White, Image.Type.Sliced);
    #endregion
}