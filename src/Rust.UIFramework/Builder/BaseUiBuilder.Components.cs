using System;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Controls;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Padding;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Builder;

public partial class BaseUiBuilder
{
    #region Add Components
    public abstract void AddComponent(BaseUiComponent component, in UiReference parent);
        
    protected abstract void AddAnchor(BaseUiComponent component, in UiReference parent);

    public void AddControl(BaseUiControl control) => Controls.Add(control);

    public void AddLayout(BaseUiLayout layout) => Layouts.Add(layout);

    #endregion

    #region Base
    public T Element<T>(in UiReference parent) where T : BaseUiComponent, new()
    {
        T element = PluginPool.Get<T>();
        AddComponent(element, parent);
        return element;
    }
    
    public T Element<T>(BaseUiLayout layout) where T : BaseUiComponent, new()
    {
        T element = Element<T>(layout.Reference);
        layout.AddElement(element);
        return element;
    }
    #endregion
    
    #region Section
    public UiSection Section(in UiReference parent)
    {
        return Element<UiSection>(parent);
    }

    public UiSection Section(in UiReference parent, in UiPosition pos, in UiOffset offset = default)
    {
        return Section(parent).SetPosition(pos, offset);
    }
    
    public UiSection Section(BaseUiLayout layout)
    {
       return Element<UiSection>(layout);
    }

    public UiSection Padding(in UiReference parent, in UiPosition pos, in UiPadding padding = default)
    {
        return Section(parent, pos, padding);
    }
    #endregion
        
    #region Panel
    public UiPanel Panel(in UiReference parent)
    {
        return Element<UiPanel>(parent);
    }
    
    public UiPanel Panel(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor color)
    {
        return Panel(parent).SetPosition(pos, offset).SetColor(color);
    }

    public UiPanel Panel(BaseUiLayout layout, UiColor color)
    {
        return Element<UiPanel>(layout).SetColor(color);
    }
    #endregion

    #region Button
    public UiButton Button(in UiReference parent)
    {
        return Element<UiButton>(parent);
    }
    
    public UiButton Button(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor color, string command, ButtonType buttonType = ButtonType.Command)
    {
        return Button(parent).SetPosition(pos, offset).Init(color, command, buttonType);
    }
    
    public UiButton Button(BaseUiLayout layout, UiColor color, string command, ButtonType buttonType = ButtonType.Command)
    {
        return Element<UiButton>(layout).Init(color, command, buttonType);
    }
    #endregion

    #region Image
    public UiImage ImageSprite(in UiReference parent)
    {
        return Element<UiImage>(parent);
    }
    
    public UiImage ImageSprite(in UiReference parent, in UiPosition pos, in UiOffset offset, string sprite, UiColor? color = default)
    {
        return ImageSprite(parent).SetPosition(pos, offset).Init(sprite, color ?? UiColors.White);
    }
    
    public UiImage ImageSprite(BaseUiLayout layout, string sprite, UiColor? color = default)
    {
        return Element<UiImage>(layout).Init(sprite, color ?? UiColors.White);
    }
    #endregion

    #region PlayingCard
    public UiPlayingCard PlayingCard(in UiReference parent)
    {
        return Element<UiPlayingCard>(parent);
    }
    
    public UiPlayingCard PlayingCard(in UiReference parent, in UiPosition pos, in UiOffset offset, PlayingCardData card, UiCardType type = UiCardType.Normal, UiColor? color = default)
    {
        return PlayingCard(parent).SetPosition(pos, offset).Init(card, type, color ?? UiColors.White);
    }
    
    public UiPlayingCard PlayingCard(BaseUiLayout layout, PlayingCardData card, UiCardType type = UiCardType.Normal, UiColor? color = default)
    {
        return Element<UiPlayingCard>(layout).Init(card, type, color ?? UiColors.White);
    }
    #endregion

    #region Item Icon
    public UiItemIcon ItemIcon(in UiReference parent)
    {
        return Element<UiItemIcon>(parent);
    }
    
    public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, int itemId, ulong skinId = 0, UiColor? color = default)
    {
        return ItemIcon(parent).SetPosition(pos, offset).Init(itemId, skinId, color ?? UiColors.White);
    }
    
    public UiItemIcon ItemIcon(BaseUiLayout layout, int itemId, ulong skinId = 0, UiColor? color = default)
    {
        return Element<UiItemIcon>(layout).Init(itemId, skinId, color ?? UiColors.White);
    }
    
    public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, Item item, UiColor? color = default) => ItemIcon(parent, pos, offset, item.info.itemid, item.skin, color);
    public UiItemIcon ItemIcon(BaseUiLayout layout, Item item, UiColor? color = default) => ItemIcon(layout, item.info.itemid, item.skin, color);
    #endregion
    
    #region Player Avatar
    public UiPlayerAvatar PlayerAvatar(in UiReference parent)
    {
        return Element<UiPlayerAvatar>(parent);
    }
    
    public UiPlayerAvatar PlayerAvatar(in UiReference parent, in UiPosition pos, in UiOffset offset, ulong steamId, AvatarType type = AvatarType.Medium, UiColor? color = default)
    {
        return PlayerAvatar(parent).SetPosition(pos, offset).Init(steamId, type, color ?? UiColors.White);
    }
    
    public UiPlayerAvatar PlayerAvatar(BaseUiLayout layout, ulong steamId, AvatarType type = AvatarType.Medium, UiColor? color = default)
    {
        return Element<UiPlayerAvatar>(layout).Init(steamId, type, color ?? UiColors.White);
    }
    #endregion

    #region Raw Image
    public UiRawImage RawImage(in UiReference parent)
    {
        return Element<UiRawImage>(parent);
    }
    
    public UiRawImage RawImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string image, UiColor? color = default)
    {
        return RawImage(parent).SetPosition(pos, offset).Init(image, color ?? UiColors.White);
    }
    
    public UiRawImage RawImage(BaseUiLayout layout, string image, UiColor? color = default)
    {
        return Element<UiRawImage>(layout).Init(image, color ?? UiColors.White);
    }

    public UiRawImage WebImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string url, UiColor? color = default) => RawImage(parent, pos, offset, url, color);
    public UiRawImage WebImage(BaseUiLayout layout, string url, UiColor? color = default) => RawImage(layout, url, color);
    public UiRawImage TextureImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string texture, UiColor? color = default) => RawImage(parent, pos, offset, texture, color);
    public UiRawImage TextureImage(BaseUiLayout layout, string texture, UiColor? color = default) => RawImage(layout, texture, color);
    public UiRawImage FileStorageImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string imageId, UiColor? color = default) => RawImage(parent, pos, offset, imageId, color);
    public UiRawImage FileStorageImage(BaseUiLayout layout, string imageId, UiColor? color = default) => RawImage(layout, imageId, color);
    public UiRawImage ImageStorage(Plugin plugin, in UiReference parent, in UiPosition pos, in UiOffset offset, string nameOrUrl, UiColor? color = default) => RawImage(parent, pos, offset, Singleton<UiImageStorage>.Instance.Get(plugin, nameOrUrl), color);
    public UiRawImage ImageStorage(Plugin plugin, BaseUiLayout layout , string nameOrUrl, UiColor? color = default) => RawImage(layout, Singleton<UiImageStorage>.Instance.Get(plugin, nameOrUrl), color);
    #endregion
    
    #region Icon
    public UiIcon Icon(in UiReference parent)
    {
        return Element<UiIcon>(parent);
    }
    
    public UiIcon Icon<T>(in UiReference parent, in UiPosition pos, in UiOffset offset, T icon, UiColor? color = default) where T : struct, Enum
    {
        return Icon(parent).SetPosition(pos, offset).Init(icon, color ?? UiColors.White);
    }
    
    public UiIcon Icon<T>(BaseUiLayout layout, T icon, UiColor? color = default) where T : struct, Enum
    {
        return Element<UiIcon>(layout).Init(icon, color ?? UiColors.White);
    }
    #endregion

    #region Label
    public UiLabel Label(in UiReference parent)
    {
        return Element<UiLabel>(parent);
    }
    
    public UiLabel Label(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int size, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter)
    {
        return Label(parent).SetPosition(pos, offset).Init(text, size, textColor, align, Font);
    }
    
    public UiLabel Label(BaseUiLayout layout, string text, int size, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter)
    {
        return Element<UiLabel>(layout).Init(text, size, textColor, align, Font);
    }
    
    public UiTuple<UiPanel, UiLabel> Label(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int size, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter, in UiPadding? textPadding = null)
    {
        UiPanel panel = Panel(parent, pos, offset, backgroundColor);
        UiLabel label = Label(panel, UiPosition.Full, textPadding?.ToOffset() ?? JsonDefaults.Common.TextPadding, text, size, textColor, align);
        return new UiTuple<UiPanel, UiLabel>(panel, label);
    }
    
    public UiTuple<UiPanel, UiLabel> Label(BaseUiLayout layout, string text, int size, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter, in UiPadding? textPadding = null)
    {
        UiPanel panel = Panel(layout, backgroundColor);
        UiLabel label = Label(panel, UiPosition.Full, textPadding?.ToOffset() ?? JsonDefaults.Common.TextPadding, text, size, textColor, align);
        return new UiTuple<UiPanel, UiLabel>(panel, label);
    }
    #endregion
        
    #region Input
    public UiInput Input(in UiReference parent)
    {
        return Element<UiInput>(parent);
    }
    
    public UiInput Input(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        return Input(parent).SetPosition(pos, offset).Init(text, fontSize, textColor, command, Font, align, charsLimit, mode, lineType);
    }
    
    public UiInput Input(BaseUiLayout layout, string text, int fontSize, UiColor textColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        return Element<UiInput>(layout).Init(text, fontSize, textColor, command, Font, align, charsLimit, mode, lineType);
    }
    
    public UiTuple<UiPanel, UiInput> Input(in UiReference parent, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        UiPanel background = Panel(parent).SetColor(backgroundColor);
        UiInput input = Input(background, UiPosition.Full, default, text, fontSize, textColor, command, align, charsLimit, mode, lineType);
        return new UiTuple<UiPanel, UiInput>(background, input);
    }
    
    public UiTuple<UiPanel, UiInput> Input(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null)
    {
        UiPanel panel = Panel(parent, pos, offset, backgroundColor);
        UiInput input = Input(panel, UiPosition.Full, textPadding?.ToOffset() ?? JsonDefaults.Common.TextPadding, text, fontSize, textColor, command, align, charsLimit, mode, lineType);
        return new UiTuple<UiPanel, UiInput>(panel, input);
    }
    
    public UiTuple<UiPanel, UiInput> Input(BaseUiLayout layout, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null)
    {
        UiPanel panel = Panel(layout, backgroundColor);
        UiInput input = Input(panel, UiPosition.Full, textPadding?.ToOffset() ?? JsonDefaults.Common.TextPadding, text, fontSize, textColor, command, align, charsLimit, mode, lineType);
        return new UiTuple<UiPanel, UiInput>(panel, input);
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
    
    public UiTuple<UiPanel, UiInput> Input(in UiReference parent, string text, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        return Input(parent, text, fontSize, textColor, backgroundColor, command.Build(InputArg.Empty), align, charsLimit, mode, lineType);
    }
    
    public UiTuple<UiPanel, UiInput> Input(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null)
    {
        return Input(parent, pos, offset, text, fontSize, textColor, backgroundColor, command.Build(InputArg.Empty), align, charsLimit, mode, lineType);
    }
    
    public UiTuple<UiPanel, UiInput> Input(BaseUiLayout layout, string text, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null)
    {
        return Input(layout, text, fontSize, textColor, backgroundColor, command.Build(InputArg.Empty), align, charsLimit, mode, lineType);
    }
    #endregion

    #region Anchor
    public UiSection Anchor(in UiReference parent, string anchorName = null)
    {
        UiSection section = PluginPool.Get<UiSection>();
        AddAnchor(section, parent);
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
        return Element<UiScrollView>(parent);
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
        return Element<UiScrollView>(layout).Init(movementType, elasticity, inertia, decelerationRate, scrollSensitivity);
    }
    #endregion
}