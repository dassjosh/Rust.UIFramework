using System;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Colors;
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
    #region Add Components
    public abstract void AddComponent(BaseUiComponent component, in UiReference parent);
        
    protected abstract void AddAnchor(BaseUiComponent component, in UiReference parent);

    public void AddControl(BaseUiControl control)
    {
        Controls.Add(control);
    }

    public void AddLayout(BaseLayout layout)
    {
        Layouts.Add(layout);
    }
    #endregion

    #region Base
    public T Base<T>(in UiReference parent) where T : BaseUiComponent, new()
    {
        T @base = BaseUiComponent.CreateBase<T>();
        AddComponent(@base, parent);
        return @base;
    }
    
    public T Base<T>(in UiReference parent, in UiPosition pos, in UiOffset offset = default) where T : BaseUiComponent, new()
    {
        T @base = BaseUiComponent.CreateBase<T>(pos, offset);
        AddComponent(@base, parent);
        return @base;
    }
    #endregion
    
    #region Section
    public UiSection Section(in UiReference parent)
    {
        UiSection section = UiSection.Create();
        AddComponent(section, parent);
        return section;
    }

    public UiSection Section(in UiReference parent, in UiPosition pos, in UiOffset offset = default)
    {
        UiSection section = Section(parent);
        section.SetPosition(pos, offset);
        return section;
    }
    
    public UiSection Section(BaseLayout layout)
    {
        UiSection section = Section(layout.Reference);
        layout.AddElement(section);
        return section;
    }

    public UiSection Padding(in UiReference parent, in UiPosition pos, in UiPadding padding = default)
    {
        return Section(parent, pos, padding);
    }
    #endregion
        
    #region Panel
    public UiPanel Panel(in UiReference parent, UiColor color)
    {
        UiPanel panel = UiPanel.Create(color);
        AddComponent(panel, parent);
        return panel;
    }
    
    public UiPanel Panel(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor color)
    {
        UiPanel panel = Panel(parent, color);
        panel.SetPosition(pos, offset);
        return panel;
    }

    public UiPanel Panel(BaseLayout layout, UiColor color)
    {
        UiPanel panel = Panel(layout.Reference, color);
        layout.AddElement(panel);
        return panel;
    }
    #endregion

    #region Button
    public UiButton Button(in UiReference parent, UiColor color, string command, ButtonType buttonType = ButtonType.Command)
    {
        UiButton button = UiButton.Create(color, command, buttonType);
        AddComponent(button, parent);
        return button;
    }
    
    public UiButton Button(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor color, string command, ButtonType buttonType = ButtonType.Command)
    {
        UiButton button = Button(parent, color, command, buttonType);
        button.SetPosition(pos, offset);
        return button;
    }
    
    public UiButton Button(BaseLayout layout, UiColor color, string command, ButtonType buttonType = ButtonType.Command)
    {
        UiButton button = Button(layout.Reference, color, command, buttonType);
        layout.AddElement(button);
        return button;
    }
    #endregion

    #region Image
    public UiImage ImageSprite(in UiReference parent, string sprite, UiColor? color = default)
    {
        UiImage image = UiImage.CreateSpriteImage(sprite, color ?? UiColor.White);
        AddComponent(image, parent);
        return image;
    }
    
    public UiImage ImageSprite(in UiReference parent, in UiPosition pos, in UiOffset offset, string sprite, UiColor? color = default)
    {
        UiImage image = ImageSprite(parent, sprite, color ?? UiColor.White);
        image.SetPosition(pos, offset);
        return image;
    }
    
    public UiImage ImageSprite(BaseLayout layout, string sprite, UiColor? color = default)
    {
        UiImage image = ImageSprite(layout.Reference, sprite, color ?? UiColor.White);
        layout.AddElement(image);
        return image;
    }
    #endregion

    #region PlayingCard
    public UiPlayingCard PlayingCard(in UiReference parent, PlayingCardData card, UiCardType type = UiCardType.Normal, UiColor? color = default)
    {
        UiPlayingCard image = UiPlayingCard.Create(card, type, color ?? UiColor.White);
        AddComponent(image, parent);
        return image;
    }
    
    public UiPlayingCard PlayingCard(in UiReference parent, in UiPosition pos, in UiOffset offset, PlayingCardData card, UiCardType type = UiCardType.Normal, UiColor? color = default)
    {
        UiPlayingCard image = PlayingCard(parent, card, type, color ?? UiColor.White);
        image.SetPosition(pos, offset);
        return image;
    }
    
    public UiPlayingCard PlayingCard(BaseLayout layout, PlayingCardData card, UiCardType type = UiCardType.Normal, UiColor? color = default)
    {
        UiPlayingCard image = PlayingCard(layout.Reference, card, type, color ?? UiColor.White);
        layout.AddElement(image);
        return image;
    }
    #endregion

    #region Item Icon
    public UiItemIcon ItemIcon(in UiReference parent, int itemId, ulong skinId = 0, UiColor? color = default)
    {
        UiItemIcon image = UiItemIcon.Create(itemId, skinId, color ?? UiColor.White);
        AddComponent(image, parent);
        return image;
    }
    
    public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, int itemId, ulong skinId = 0, UiColor? color = default)
    {
        UiItemIcon image = ItemIcon(parent, itemId, skinId, color ?? UiColor.White);
        image.SetPosition(pos, offset);
        return image;
    }
    
    public UiItemIcon ItemIcon(BaseLayout layout, int itemId, ulong skinId = 0, UiColor? color = default)
    {
        UiItemIcon image = ItemIcon(layout.Reference, itemId, skinId, color ?? UiColor.White);
        layout.AddElement(image);
        return image;
    }
    
    public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, Item item, UiColor? color = default) => ItemIcon(parent, pos, offset, item.info.itemid, item.skin, color);
    public UiItemIcon ItemIcon(BaseLayout layout, Item item, UiColor? color = default) => ItemIcon(layout, item.info.itemid, item.skin, color);
    #endregion
    
    #region Player Avatar
    public UiPlayerAvatar PlayerAvatar(in UiReference parent, ulong steamId, AvatarType type = AvatarType.Medium, UiColor? color = default)
    {
        UiPlayerAvatar image = UiPlayerAvatar.Create(steamId, type, color ?? UiColor.White);
        AddComponent(image, parent);
        return image;
    }
    
    public UiPlayerAvatar PlayerAvatar(in UiReference parent, in UiPosition pos, in UiOffset offset, ulong steamId, AvatarType type = AvatarType.Medium, UiColor? color = default)
    {
        UiPlayerAvatar image = PlayerAvatar(parent, steamId, type, color ?? UiColor.White);
        image.SetPosition(pos, offset);
        return image;
    }
    
    public UiPlayerAvatar PlayerAvatar(BaseLayout layout, ulong steamId, AvatarType type = AvatarType.Medium, UiColor? color = default)
    {
        UiPlayerAvatar image = PlayerAvatar(layout.Reference, steamId, type, color ?? UiColor.White);
        layout.AddElement(image);
        return image;
    }
    #endregion

    #region Raw Image
    public UiRawImage RawImage(in UiReference parent, string image, UiColor? color = default)
    {
        UiRawImage rawImage = UiRawImage.Create(image, color ?? UiColor.White);
        AddComponent(rawImage, parent);
        return rawImage;
    }
    
    public UiRawImage RawImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string image, UiColor? color = default)
    {
        UiRawImage rawImage = RawImage(parent, image, color ?? UiColor.White);
        rawImage.SetPosition(pos, offset);
        return rawImage;
    }
    
    public UiRawImage RawImage(BaseLayout layout, string image, UiColor? color = default)
    {
        UiRawImage rawImage = RawImage(layout.Reference, image, color ?? UiColor.White);
        layout.AddElement(rawImage);
        return rawImage;
    }

    public UiRawImage WebImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string url, UiColor? color = default) => RawImage(parent, pos, offset, url, color);
    public UiRawImage WebImage(BaseLayout layout, string url, UiColor? color = default) => RawImage(layout, url, color);
    public UiRawImage TextureImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string texture, UiColor? color = default) => RawImage(parent, pos, offset, texture, color);
    public UiRawImage TextureImage(BaseLayout layout, string texture, UiColor? color = default) => RawImage(layout, texture, color);
    public UiRawImage FileStorageImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string imageId, UiColor? color = default) => RawImage(parent, pos, offset, imageId, color);
    public UiRawImage FileStorageImage(BaseLayout layout, string imageId, UiColor? color = default) => RawImage(layout, imageId, color);
    public UiRawImage ImageStorage(Plugin plugin, in UiReference parent, in UiPosition pos, in UiOffset offset, string nameOrUrl, UiColor? color = default) => RawImage(parent, pos, offset, Singleton<UiImageStorage>.Instance.Get(plugin, nameOrUrl), color);
    public UiRawImage ImageStorage(Plugin plugin, BaseLayout layout , string nameOrUrl, UiColor? color = default) => RawImage(layout, Singleton<UiImageStorage>.Instance.Get(plugin, nameOrUrl), color);
    #endregion
    
    #region Icon
    public UiIcon Icon<T>(in UiReference parent, T icon, UiColor? color = default) where T : struct, Enum
    {
        UiIcon image = UiIcon.CreateIcon(icon, color ?? UiColor.White);
        AddComponent(image, parent);
        return image;
    }
    
    public UiIcon Icon<T>(in UiReference parent, in UiPosition pos, in UiOffset offset, T icon, UiColor? color = default) where T : struct, Enum
    {
        UiIcon image = Icon(parent, icon, color ?? UiColor.White);
        image.SetPosition(pos, offset);
        return image;
    }
    
    public UiIcon Icon<T>(BaseLayout layout, T icon, UiColor? color = default) where T : struct, Enum
    {
        UiIcon image = Icon(layout.Reference, icon, color ?? UiColor.White);
        layout.AddElement(image);
        return image;
    }
    #endregion

    #region Label
    public UiLabel Label(in UiReference parent, string text, int size, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter)
    {
        UiLabel label = UiLabel.Create(textColor, text, size, Font, align);
        AddComponent(label, parent);
        return label;
    }
    
    public UiLabel Label(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int size, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter)
    {
        UiLabel label = Label(parent, text, size, textColor, align);
        label.SetPosition(pos, offset);
        return label;
    }
    
    public UiLabel Label(BaseLayout layout, string text, int size, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter)
    {
        UiLabel label = Label(layout.Reference, text, size, textColor, align);
        layout.AddElement(label);
        return label;
    }
    
    public UiTuple<UiPanel, UiLabel> Label(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int size, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter, in UiPadding? textPadding = null)
    {
        UiPanel panel = Panel(parent, pos, offset, backgroundColor);
        UiLabel label = Label(panel, UiPosition.Full, textPadding?.ToOffset() ?? JsonDefaults.Common.TextPadding, text, size, textColor, align);
        return new UiTuple<UiPanel, UiLabel>(panel, label);
    }
    
    public UiTuple<UiPanel, UiLabel> Label(BaseLayout layout, string text, int size, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter, in UiPadding? textPadding = null)
    {
        UiPanel panel = Panel(layout, backgroundColor);
        UiLabel label = Label(panel, UiPosition.Full, textPadding?.ToOffset() ?? JsonDefaults.Common.TextPadding, text, size, textColor, align);
        return new UiTuple<UiPanel, UiLabel>(panel, label);
    }
    #endregion
        
    #region Input
    public UiInput Input(in UiReference parent, string text, int fontSize, UiColor textColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        UiInput input = UiInput.Create(text, fontSize, textColor, command, Font, align, charsLimit, mode, lineType);
        AddComponent(input, parent);
        return input;
    }
    
    public UiInput Input(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        UiInput input = Input(parent, text, fontSize, textColor, command, align, charsLimit, mode, lineType);
        input.SetPosition(pos, offset);
        return input;
    }
    
    public UiInput Input(BaseLayout layout, string text, int fontSize, UiColor textColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        UiInput input = Input(layout.Reference, text, fontSize, textColor, command, align, charsLimit, mode, lineType);
        layout.AddElement(input);
        return input;
    }
    
    public UiTuple<UiPanel, UiInput> Input(in UiReference parent, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        UiPanel background = Panel(parent, backgroundColor);
        UiInput input = Input(background, UiPosition.Full, default, text, fontSize, textColor, command, align, charsLimit, mode, lineType);
        return new UiTuple<UiPanel, UiInput>(background, input);
    }
    
    public UiTuple<UiPanel, UiInput> Input(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null)
    {
        UiPanel panel = Panel(parent, pos, offset, backgroundColor);
        UiInput input = Input(panel, UiPosition.Full, textPadding?.ToOffset() ?? JsonDefaults.Common.TextPadding, text, fontSize, textColor, command, align, charsLimit, mode, lineType);
        return new UiTuple<UiPanel, UiInput>(panel, input);
    }
    
    public UiTuple<UiPanel, UiInput> Input(BaseLayout layout, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null)
    {
        UiPanel panel = Panel(layout, backgroundColor);
        UiInput input = Input(panel, UiPosition.Full, textPadding?.ToOffset() ?? JsonDefaults.Common.TextPadding, text, fontSize, textColor, command, align, charsLimit, mode, lineType);
        return new UiTuple<UiPanel, UiInput>(panel, input);
    }
    
    public UiInput Input(in UiReference parent, string text, int fontSize, UiColor textColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        return Input(parent, text, fontSize, textColor, command.Build(InputArg.Empty), align, charsLimit, mode, lineType);
    }
    
    public UiInput Input(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        return Input(parent, pos, offset, text, fontSize, textColor, command.Build(InputArg.Empty), align, charsLimit, mode, lineType);
    }
    
    public UiInput Input(BaseLayout layout, string text, int fontSize, UiColor textColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
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
    
    public UiTuple<UiPanel, UiInput> Input(BaseLayout layout, string text, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<InputArg> command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine, in UiPadding? textPadding = null)
    {
        return Input(layout, text, fontSize, textColor, backgroundColor, command.Build(InputArg.Empty), align, charsLimit, mode, lineType);
    }
    #endregion

    #region Anchor
    public UiSection Anchor(in UiReference parent)
    {
        UiSection section = UiSection.Create();
        AddAnchor(section, parent);
        return section;
    }
    
    public UiSection Anchor(in UiReference parent, in UiPosition pos, in UiOffset offset = default)
    {
        UiSection section = Anchor(parent);
        section.SetPosition(pos, offset);
        AddAnchor(section, parent);
        return section;
    }
    #endregion

    #region ScrollView
    public UiScrollView ScrollView(in UiReference parent, 
        ScrollRect.MovementType movementType = JsonDefaults.ScrollView.MovementType, 
        float elasticity = JsonDefaults.ScrollView.Elasticity,
        bool inertia = JsonDefaults.ScrollView.Inertia, 
        float decelerationRate = JsonDefaults.ScrollView.DecelerationRate, 
        float scrollSensitivity = JsonDefaults.ScrollView.ScrollSensitivity)
    {
        UiScrollView scroll = UiScrollView.Create(movementType, elasticity, inertia, decelerationRate, scrollSensitivity);
        AddComponent(scroll, parent);
        return scroll;
    }
    
    public UiScrollView ScrollView(in UiReference parent, in UiPosition pos, in UiOffset offset, 
        ScrollRect.MovementType movementType = JsonDefaults.ScrollView.MovementType, 
        float elasticity = JsonDefaults.ScrollView.Elasticity,
        bool inertia = JsonDefaults.ScrollView.Inertia, 
        float decelerationRate = JsonDefaults.ScrollView.DecelerationRate, 
        float scrollSensitivity = JsonDefaults.ScrollView.ScrollSensitivity)
    {
        UiScrollView scroll = ScrollView(parent, movementType, elasticity, inertia, decelerationRate, scrollSensitivity);
        scroll.SetPosition(pos, offset);
        return scroll;
    }
    
    public UiScrollView ScrollView(BaseLayout layout, 
        ScrollRect.MovementType movementType = JsonDefaults.ScrollView.MovementType, 
        float elasticity = JsonDefaults.ScrollView.Elasticity,
        bool inertia = JsonDefaults.ScrollView.Inertia, 
        float decelerationRate = JsonDefaults.ScrollView.DecelerationRate, 
        float scrollSensitivity = JsonDefaults.ScrollView.ScrollSensitivity)
    {
        UiScrollView scroll = ScrollView(layout.Reference, movementType, elasticity, inertia, decelerationRate, scrollSensitivity);
        layout.AddElement(scroll);
        return scroll;
    }
    #endregion
}