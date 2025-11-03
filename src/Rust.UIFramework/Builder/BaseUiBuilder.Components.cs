using System;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Component<T>(BaseUiLayout layout) where T : BaseUiComponent, new()
    {
        T component = Component<T>(layout.Reference);
        layout.AddElement(component);
        return component;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Component<T>(BaseLayoutComponent layout) where T : BaseUiComponent, new() => Component<T>(layout.Reference);
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
    public UiSection Section(BaseUiLayout layout) => Component<UiSection>(layout);
    public UiSection Section(BaseLayoutComponent layout) => Component<UiSection>(layout);
    #endregion
        
    #region Panel
    public UiPanel Panel(in UiReference parent) => Component<UiPanel>(parent);

    public UiPanel Panel(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor color) => Panel(parent).SetPosition(pos, offset).SetColor(color);

    public UiPanel Panel(BaseUiLayout layout, UiColor color) => Component<UiPanel>(layout).SetColor(color);
    public UiPanel Panel(BaseLayoutComponent layout, UiColor color) => Component<UiPanel>(layout).SetColor(color);
    #endregion

    #region Button
    public UiButton Button(in UiReference parent)
    {
        return Component<UiButton>(parent);
    }
    
    public UiButton Button(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor color, string command, ButtonType buttonType = ButtonType.Command)
    {
        return Button(parent).SetPosition(pos, offset).Init(color, command, buttonType);
    }
    
    public UiButton Button(BaseUiLayout layout, UiColor color, string command, ButtonType buttonType = ButtonType.Command)
    {
        return Component<UiButton>(layout).Init(color, command, buttonType);
    }
    
    public UiButton Button(BaseLayoutComponent layout, UiColor color, string command, ButtonType buttonType = ButtonType.Command)
    {
        return Component<UiButton>(layout).Init(color, command, buttonType);
    }
    #endregion

    #region Image
    public UiImage ImageSprite(in UiReference parent)
    {
        return Component<UiImage>(parent);
    }
    
    public UiImage ImageSprite(in UiReference parent, in UiPosition pos, in UiOffset offset, string sprite, UiColor? color = default)
    {
        return ImageSprite(parent).SetPosition(pos, offset).Init(sprite, color ?? UiColors.White);
    }
    
    public UiImage ImageSprite(BaseUiLayout layout, string sprite, UiColor? color = default)
    {
        return Component<UiImage>(layout).Init(sprite, color ?? UiColors.White);
    }
    
    public UiImage ImageSprite(BaseLayoutComponent layout, string sprite, UiColor? color = default)
    {
        return Component<UiImage>(layout).Init(sprite, color ?? UiColors.White);
    }
    #endregion

    #region PlayingCard
    public UiPlayingCard PlayingCard(in UiReference parent)
    {
        return Component<UiPlayingCard>(parent);
    }
    
    public UiPlayingCard PlayingCard(in UiReference parent, in UiPosition pos, in UiOffset offset, PlayingCardData card, UiCardType type = UiCardType.Normal, UiColor? color = default)
    {
        return PlayingCard(parent).SetPosition(pos, offset).Init(card, type, color ?? UiColors.White);
    }
    
    public UiPlayingCard PlayingCard(BaseUiLayout layout, PlayingCardData card, UiCardType type = UiCardType.Normal, UiColor? color = default)
    {
        return Component<UiPlayingCard>(layout).Init(card, type, color ?? UiColors.White);
    }
    
    public UiPlayingCard PlayingCard(BaseLayoutComponent layout, PlayingCardData card, UiCardType type = UiCardType.Normal, UiColor? color = default)
    {
        return Component<UiPlayingCard>(layout).Init(card, type, color ?? UiColors.White);
    }
    #endregion

    #region Item Icon
    public UiItemIcon ItemIcon(in UiReference parent)
    {
        return Component<UiItemIcon>(parent);
    }
    
    public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, int itemId, ulong skinId = 0, UiColor? color = default)
    {
        return ItemIcon(parent).SetPosition(pos, offset).Init(itemId, skinId, color ?? UiColors.White);
    }
    
    public UiItemIcon ItemIcon(BaseUiLayout layout, int itemId, ulong skinId = 0, UiColor? color = default)
    {
        return Component<UiItemIcon>(layout).Init(itemId, skinId, color ?? UiColors.White);
    }
    
    public UiItemIcon ItemIcon(BaseLayoutComponent layout, int itemId, ulong skinId = 0, UiColor? color = default)
    {
        return Component<UiItemIcon>(layout).Init(itemId, skinId, color ?? UiColors.White);
    }
    
    public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, Item item, UiColor? color = default) => ItemIcon(parent, pos, offset, item.info.itemid, item.skin, color);
    public UiItemIcon ItemIcon(BaseUiLayout layout, Item item, UiColor? color = default) => ItemIcon(layout, item.info.itemid, item.skin, color);
    public UiItemIcon ItemIcon(BaseLayoutComponent layout, Item item, UiColor? color = default) => ItemIcon(layout, item.info.itemid, item.skin, color);
    #endregion
    
    #region Player Avatar
    public UiPlayerAvatar PlayerAvatar(in UiReference parent)
    {
        return Component<UiPlayerAvatar>(parent);
    }
    
    public UiPlayerAvatar PlayerAvatar(in UiReference parent, in UiPosition pos, in UiOffset offset, ulong steamId, AvatarType type = AvatarType.Medium, UiColor? color = default)
    {
        return PlayerAvatar(parent).SetPosition(pos, offset).Init(steamId, type, color ?? UiColors.White);
    }
    
    public UiPlayerAvatar PlayerAvatar(BaseUiLayout layout, ulong steamId, AvatarType type = AvatarType.Medium, UiColor? color = default)
    {
        return Component<UiPlayerAvatar>(layout).Init(steamId, type, color ?? UiColors.White);
    }
    
    public UiPlayerAvatar PlayerAvatar(BaseLayoutComponent layout, ulong steamId, AvatarType type = AvatarType.Medium, UiColor? color = default)
    {
        return Component<UiPlayerAvatar>(layout).Init(steamId, type, color ?? UiColors.White);
    }
    #endregion

    #region Raw Image
    public UiRawImage RawImage(in UiReference parent)
    {
        return Component<UiRawImage>(parent);
    }
    
    public UiRawImage RawImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string image, UiColor? color = default)
    {
        return RawImage(parent).SetPosition(pos, offset).Init(image, color ?? UiColors.White);
    }
    
    public UiRawImage RawImage(BaseUiLayout layout, string image, UiColor? color = default)
    {
        return Component<UiRawImage>(layout).Init(image, color ?? UiColors.White);
    }
    
    public UiRawImage RawImage(BaseLayoutComponent layout, string image, UiColor? color = default)
    {
        return Component<UiRawImage>(layout).Init(image, color ?? UiColors.White);
    }

    public UiRawImage WebImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string url, UiColor? color = default) => RawImage(parent, pos, offset, url, color);
    public UiRawImage WebImage(BaseUiLayout layout, string url, UiColor? color = default) => RawImage(layout, url, color);
    public UiRawImage WebImage(BaseLayoutComponent layout, string url, UiColor? color = default) => RawImage(layout, url, color);
    public UiRawImage TextureImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string texture, UiColor? color = default) => RawImage(parent, pos, offset, texture, color);
    public UiRawImage TextureImage(BaseUiLayout layout, string texture, UiColor? color = default) => RawImage(layout, texture, color);
    public UiRawImage TextureImage(BaseLayoutComponent layout, string texture, UiColor? color = default) => RawImage(layout, texture, color);
    public UiRawImage FileStorageImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string imageId, UiColor? color = default) => RawImage(parent, pos, offset, imageId, color);
    public UiRawImage FileStorageImage(BaseUiLayout layout, string imageId, UiColor? color = default) => RawImage(layout, imageId, color);
    public UiRawImage FileStorageImage(BaseLayoutComponent layout, string imageId, UiColor? color = default) => RawImage(layout, imageId, color);
    public UiRawImage ImageStorage(in UiReference parent, in UiPosition pos, in UiOffset offset, string nameOrUrl, ImageDownloadOptions options = null, UiColor? color = default)
        => RawImage(parent, pos, offset, Singleton<UiImageStorage>.Instance.Get(Plugin, nameOrUrl, options), color);

    public UiRawImage ImageStorage(BaseUiLayout layout, string nameOrUrl, ImageDownloadOptions options = null, UiColor? color = default)
        => RawImage(layout, Singleton<UiImageStorage>.Instance.Get(Plugin, nameOrUrl, options), color);
    
    public UiRawImage ImageStorage(BaseLayoutComponent layout, string nameOrUrl, ImageDownloadOptions options = null, UiColor? color = default)
        => RawImage(layout, Singleton<UiImageStorage>.Instance.Get(Plugin, nameOrUrl, options), color);
    #endregion
    
    #region Icon
    public UiIcon Icon(in UiReference parent)
    {
        return Component<UiIcon>(parent);
    }
    
    public UiIcon Icon<T>(in UiReference parent, in UiPosition pos, in UiOffset offset, T icon, UiColor? color = default) where T : struct, Enum
    {
        return Icon(parent).SetPosition(pos, offset).Init(icon, color ?? UiColors.White);
    }
    
    public UiIcon Icon<T>(BaseUiLayout layout, T icon, UiColor? color = default) where T : struct, Enum
    {
        return Component<UiIcon>(layout).Init(icon, color ?? UiColors.White);
    }
    
    public UiIcon Icon<T>(BaseLayoutComponent layout, T icon, UiColor? color = default) where T : struct, Enum
    {
        return Component<UiIcon>(layout).Init(icon, color ?? UiColors.White);
    }
    #endregion

    #region Label
    public UiLabel Label(in UiReference parent)
    {
        return Component<UiLabel>(parent);
    }
    
    public UiLabel Label(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int size, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter)
    {
        return Label(parent).SetPosition(pos, offset).Init(text, size, textColor, align, Font);
    }
    
    public UiLabel Label(BaseUiLayout layout, string text, int size, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter)
    {
        return Component<UiLabel>(layout).Init(text, size, textColor, align, Font);
    }
    
    public UiLabel Label(BaseLayoutComponent layout, string text, int size, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter)
    {
        return Component<UiLabel>(layout).Init(text, size, textColor, align, Font);
    }
    
    public UiTuple<UiPanel, UiLabel> Label(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int size, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter, in UiPadding? textPadding = null)
    {
        UiPanel panel = Panel(parent, pos, offset, backgroundColor);
        UiLabel label = Label(panel, UiPosition.Full, textPadding?.ToOffset() ?? JsonDefaults.Common.TextPadding, text, size, textColor, align);
        return UiTuple.Create(panel, label);
    }
    
    public UiTuple<UiPanel, UiLabel> Label(BaseUiLayout layout, string text, int size, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter, in UiPadding? textPadding = null)
    {
        UiPanel panel = Panel(layout, backgroundColor);
        UiLabel label = Label(panel, UiPosition.Full, textPadding?.ToOffset() ?? JsonDefaults.Common.TextPadding, text, size, textColor, align);
        return UiTuple.Create(panel, label);
    }
    
    public UiTuple<UiPanel, UiLabel> Label(BaseLayoutComponent layout, string text, int size, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter, in UiPadding? textPadding = null)
    {
        UiPanel panel = Panel(layout, backgroundColor);
        UiLabel label = Label(panel, UiPosition.Full, textPadding?.ToOffset() ?? JsonDefaults.Common.TextPadding, text, size, textColor, align);
        return UiTuple.Create(panel, label);
    }
    #endregion
        
    #region Input
    public UiInput Input(in UiReference parent)
    {
        return Component<UiInput>(parent);
    }
    
    public UiInput Input(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        return Input(parent).SetPosition(pos, offset).Init(text, fontSize, textColor, command, Font, align, charsLimit, mode, lineType);
    }
    
    public UiInput Input(BaseUiLayout layout, string text, int fontSize, UiColor textColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        return Component<UiInput>(layout).Init(text, fontSize, textColor, command, Font, align, charsLimit, mode, lineType);
    }
    
    public UiInput Input(BaseLayoutComponent layout, string text, int fontSize, UiColor textColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        return Component<UiInput>(layout).Init(text, fontSize, textColor, command, Font, align, charsLimit, mode, lineType);
    }
    
    public UiTuple<UiPanel, UiInput> Input(in UiReference parent, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null)
    {
        UiPanel background = Panel(parent).SetColor(backgroundColor);
        UiInput input = Input(background, UiPosition.Full, textPadding?.ToOffset() ?? JsonDefaults.Common.TextPadding, text, fontSize, textColor, command, align, charsLimit, mode, lineType);
        return UiTuple.Create(background, input);
    }
    
    public UiTuple<UiPanel, UiInput> Input(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null)
    {
        UiPanel panel = Panel(parent, pos, offset, backgroundColor);
        UiInput input = Input(panel, UiPosition.Full, textPadding?.ToOffset() ?? JsonDefaults.Common.TextPadding, text, fontSize, textColor, command, align, charsLimit, mode, lineType);
        return UiTuple.Create(panel, input);
    }
    
    public UiTuple<UiPanel, UiInput> Input(BaseUiLayout layout, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null)
    {
        UiPanel panel = Panel(layout, backgroundColor);
        UiInput input = Input(panel, UiPosition.Full, textPadding?.ToOffset() ?? JsonDefaults.Common.TextPadding, text, fontSize, textColor, command, align, charsLimit, mode, lineType);
        return UiTuple.Create(panel, input);
    }
    
    public UiTuple<UiPanel, UiInput> Input(BaseLayoutComponent layout, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null)
    {
        UiPanel panel = Panel(layout, backgroundColor);
        UiInput input = Input(panel, UiPosition.Full, textPadding?.ToOffset() ?? JsonDefaults.Common.TextPadding, text, fontSize, textColor, command, align, charsLimit, mode, lineType);
        return UiTuple.Create(panel, input);
    }
    
    public UiInput Input(in UiReference parent, string text, int fontSize, UiColor textColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        return Input(parent).Init(text, fontSize, textColor, command.Build(InputArg.Empty), Font, align, charsLimit, mode, lineType);
    }
    
    public UiInput Input(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        return Input(parent, pos, offset, text, fontSize, textColor, command.Build(InputArg.Empty), align, charsLimit, mode, lineType);
    }
    
    public UiInput Input(BaseUiLayout layout, string text, int fontSize, UiColor textColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        return Input(layout, text, fontSize, textColor, command.Build(InputArg.Empty), align, charsLimit, mode, lineType);
    }
    
    public UiInput Input(BaseLayoutComponent layout, string text, int fontSize, UiColor textColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        return Input(layout, text, fontSize, textColor, command.Build(InputArg.Empty), align, charsLimit, mode, lineType);
    }
    
    public UiTuple<UiPanel, UiInput> Input(in UiReference parent, string text, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null)
    {
        return Input(parent, text, fontSize, textColor, backgroundColor, command.Build(InputArg.Empty), align, charsLimit, mode, lineType, textPadding);
    }
    
    public UiTuple<UiPanel, UiInput> Input(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null)
    {
        return Input(parent, pos, offset, text, fontSize, textColor, backgroundColor, command.Build(InputArg.Empty), align, charsLimit, mode, lineType, textPadding);
    }
    
    public UiTuple<UiPanel, UiInput> Input(BaseUiLayout layout, string text, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null)
    {
        return Input(layout, text, fontSize, textColor, backgroundColor, command.Build(InputArg.Empty), align, charsLimit, mode, lineType, textPadding);
    }
    
    public UiTuple<UiPanel, UiInput> Input(BaseLayoutComponent layout, string text, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null)
    {
        return Input(layout, text, fontSize, textColor, backgroundColor, command.Build(InputArg.Empty), align, charsLimit, mode, lineType, textPadding);
    }
    #endregion

    #region Anchor
    public UiSection Anchor(in UiReference reference, string anchorName = null)
    {
        UiReferenceException.ThrowIfInvalidReference(reference);
        UiSection section = PluginPool.Get<UiSection>();
        Anchors.Add(section);
        Naming.SetAnchorName(section, reference, NamingMode, NamingCache, Components.Count);
        if (!string.IsNullOrEmpty(anchorName))
        {
            section.Name = anchorName;
        }
        return section;
    }
    
    public UiSection Anchor(in UiReference parent, in UiPosition pos, in UiOffset offset = default, string anchorName = null)
    {
        return Anchor(parent, anchorName).SetPosition(pos, offset);
    }
    #endregion

    #region ScrollView
    public UiScrollView ScrollView(in UiReference parent)
    {
        return Component<UiScrollView>(parent);
    }
    
    public UiScrollView ScrollView(in UiReference parent, in UiPosition pos, in UiOffset offset, 
        ScrollRect.MovementType movementType = JsonDefaults.ScrollView.MovementType, 
        float elasticity = JsonDefaults.ScrollView.Elasticity,
        bool inertia = JsonDefaults.ScrollView.Inertia, 
        float decelerationRate = JsonDefaults.ScrollView.DecelerationRate, 
        float scrollSensitivity = JsonDefaults.ScrollView.ScrollSensitivity)
    {
        return ScrollView(parent).SetPosition(pos, offset).Init(movementType, elasticity, inertia, decelerationRate, scrollSensitivity);
    }
    
    public UiScrollView ScrollView(BaseUiLayout layout, 
        ScrollRect.MovementType movementType = JsonDefaults.ScrollView.MovementType, 
        float elasticity = JsonDefaults.ScrollView.Elasticity,
        bool inertia = JsonDefaults.ScrollView.Inertia, 
        float decelerationRate = JsonDefaults.ScrollView.DecelerationRate, 
        float scrollSensitivity = JsonDefaults.ScrollView.ScrollSensitivity)
    {
        return Component<UiScrollView>(layout).Init(movementType, elasticity, inertia, decelerationRate, scrollSensitivity);
    }
    
    public UiScrollView ScrollView(BaseLayoutComponent layout, 
        ScrollRect.MovementType movementType = JsonDefaults.ScrollView.MovementType, 
        float elasticity = JsonDefaults.ScrollView.Elasticity,
        bool inertia = JsonDefaults.ScrollView.Inertia, 
        float decelerationRate = JsonDefaults.ScrollView.DecelerationRate, 
        float scrollSensitivity = JsonDefaults.ScrollView.ScrollSensitivity)
    {
        return Component<UiScrollView>(layout).Init(movementType, elasticity, inertia, decelerationRate, scrollSensitivity);
    }
    #endregion

    #region Nine Slice
    public UiNineSlice NineSlice(in UiReference parent)
    {
        return Component<UiNineSlice>(parent);
    }
    
    public UiNineSlice NineSlice(in UiReference parent, in UiPosition pos, in UiOffset offset, string png, in UiBorderWidth slice, bool fillCenter = false, UiColor? color = default, Image.Type type = Image.Type.Simple)
    {
        return NineSlice(parent).SetPosition(pos, offset).Init(png, slice, fillCenter, color ?? UiColors.White, type);
    }
    
    public UiNineSlice NineSlice(BaseUiLayout layout, string png, in UiBorderWidth slice, bool fillCenter = false, UiColor? color = default, Image.Type type = Image.Type.Simple)
    {
        return Component<UiNineSlice>(layout).Init(png, slice, fillCenter, color ?? UiColors.White, type);
    }
    
    public UiNineSlice NineSlice(BaseLayoutComponent layout, string png, in UiBorderWidth slice, bool fillCenter = false, UiColor? color = default, Image.Type type = Image.Type.Simple)
    {
        return Component<UiNineSlice>(layout).Init(png, slice, fillCenter, color ?? UiColors.White, type);
    }
    
    public UiNineSlice Border(in UiReference parent, in UiBorderWidth width, UiColor? color = default)
        => NineSlice(parent, UiPosition.Full, default, Singleton<UiImageStorage>.Instance.Get(UiFrameworkPlugin.Instance, UiImages.White1x1Name), width, false, color ?? UiColors.White, Image.Type.Sliced);
    public UiNineSlice Border(in UiReference parent, in UiPosition position, in UiOffset offset, in UiBorderWidth width, UiColor? color = default)
        => NineSlice(parent, position, offset, Singleton<UiImageStorage>.Instance.Get(UiFrameworkPlugin.Instance, UiImages.White1x1Name), width, false, color ?? UiColors.White, Image.Type.Sliced);
    #endregion
}