using Oxide.Core;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Controls;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UI;
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
    #endregion

    #region Section
    public UiSection Section(in UiReference parent, in UiPosition pos, in UiOffset offset = default)
    {
        UiSection section = UiSection.Create(pos, offset);
        AddComponent(section, parent);
        return section;
    }
    #endregion
        
    #region Panel
    public UiPanel Panel(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor color)
    {
        UiPanel panel = UiPanel.Create(pos, offset, color);
        AddComponent(panel, parent);
        return panel;
    }
    #endregion

    #region Button
    public UiButton CommandButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor color, string command)
    {
        UiButton button = UiButton.CreateCommand(pos, offset, color, command);
        AddComponent(button, parent);
        return button;
    }

    public UiButton CloseButton(in UiReference parent, in UiPosition pos, in UiOffset offset, UiColor color, string close)
    {
        UiButton button = UiButton.CreateClose(pos, offset, color, close);
        AddComponent(button, parent);
        return button;
    }
    #endregion

    #region Image
    public UiImage ImageSprite(in UiReference parent, in UiPosition pos, in UiOffset offset, string sprite, UiColor? color = default)
    {
        UiImage image = UiImage.CreateSpriteImage(pos, offset, color ?? UiColor.White, sprite);
        AddComponent(image, parent);
        return image;
    }
    #endregion

    #region Item Icon
    public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, int itemId, ulong skinId = 0, UiColor? color = default)
    {
        UiItemIcon image = UiItemIcon.Create(pos, offset, color ?? UiColor.White, itemId, skinId);
        AddComponent(image, parent);
        return image;
    }
    #endregion
    
    #region Player Avatar
    public UiPlayerAvatar PlayerAvatar(in UiReference parent, in UiPosition pos, in UiOffset offset, ulong steamId, UiColor? color = default)
    {
        UiPlayerAvatar image = UiPlayerAvatar.Create(pos, offset, color ?? UiColor.White, steamId);
        AddComponent(image, parent);
        return image;
    }
    #endregion

    #region Raw Image
    public UiRawImage WebImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string url, UiColor? color = default)
    {
        if (!url.StartsWith("http"))
        {
            Interface.Oxide.LogWarning($"[UiFramework] WebImage Url '{url}' is not a valid url. If trying to use a png id please use {nameof(ImageFileStorage)} instead.");
            return UiRawImage.CreateDefault(pos, offset);
        }

        UiRawImage image = UiRawImage.CreateUrl(pos, offset, color ?? UiColor.White, url);
        AddComponent(image, parent);
        return image;
    }

    public UiRawImage TextureImage(in UiReference parent, in UiPosition pos, in UiOffset offset, string texture, UiColor? color = default)
    {
        UiRawImage image = UiRawImage.CreateTexture(pos, offset, color ?? UiColor.White, texture);
        AddComponent(image, parent);
        return image;
    }
        
    public UiRawImage ImageFileStorage(in UiReference parent, in UiPosition pos, in UiOffset offset, string png, UiColor? color = null)
    {
        if (!uint.TryParse(png, out uint _))
        {
            Interface.Oxide.LogWarning($"[UiFramework] Image PNG '{png}' is not a valid uint. If trying to use a url please use WebImage instead.");
            return UiRawImage.CreateDefault(pos, offset);
        }

        UiRawImage image = UiRawImage.CreateFileImage(pos, offset, color ?? UiColor.White, png);
        AddComponent(image, parent);
        return image;
    }
    #endregion
    
    #region Rust Icon

    public UiRawImage RustIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, Icons icon, UiColor? color = default)
    {
        string img = RustIconCache.GetIcon(icon);
        UiRawImage image = img.StartsWith("http") ? WebImage(parent, pos, offset, img, color ?? UiColor.White) : ImageFileStorage(parent, pos, offset, img, color ?? UiColor.White);
        
        image.SetMaterial(UiMaterials.Assets.Icons.Iconmaterial);
        AddComponent(image, parent);
        return image;
    }
    #endregion

    #region Label
    public UiLabel Label(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int size, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter)
    {
        UiLabel label = UiLabel.Create(pos, offset, textColor, text, size, Font, align);
        AddComponent(label, parent);
        return label;
    }
    #endregion
        
    #region Input
    public UiInput Input(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        UiInput input = UiInput.Create(pos, offset, textColor, text, fontSize, command, Font, align, charsLimit, mode, lineType);
        AddComponent(input, parent);
        return input;
    }
    #endregion

    #region Anchor
    public UiSection Anchor(in UiReference parent, in UiPosition pos, in UiOffset offset = default)
    {
        UiSection section = UiSection.Create(pos, offset);
        AddAnchor(section, parent);
        return section;
    }
    #endregion

    #region ScrollView
    public UiScrollView ScrollView(in UiReference parent, in UiPosition pos, in UiOffset offset, 
        ScrollRect.MovementType movementType = JsonDefaults.ScrollView.MovementType, 
        float elasticity = JsonDefaults.ScrollView.Elasticity,
        bool inertia = JsonDefaults.ScrollView.Inertia, 
        float decelerationRate = JsonDefaults.ScrollView.DecelerationRate, 
        float scrollSensitivity = JsonDefaults.ScrollView.ScrollSensitivity)
    {
        UiScrollView scroll = UiScrollView.Create(pos, offset, movementType, elasticity, inertia, decelerationRate, scrollSensitivity);
        AddComponent(scroll, parent);
        return scroll;
    }
    #endregion
}