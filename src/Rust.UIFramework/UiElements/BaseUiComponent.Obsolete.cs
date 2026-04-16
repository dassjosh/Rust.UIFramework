using System;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0618 // Type or member is obsolete

namespace Oxide.Ext.UiFramework.UiElements;

public static class BaseUiComponentObsolete
{
    extension(BaseUiComponent)
    {
        [Obsolete($"{nameof(BaseUiComponent)}.{nameof(CreateBase)} is obsolete. {nameof(BaseUiBuilder)}.{nameof(BaseUiBuilder.Component)} instead")]
        internal static T CreateBase<T>(in UiPosition pos, in UiOffset offset) where T : BaseUiComponent, new()
        {
            T component = UiFrameworkPool.Get<T>();
            component.Position = pos;
            component.Offset = offset;
            return component;
        }
    }

    extension(UiButton)
    {
        [Obsolete($"Use {nameof(BaseUiBuilder)}.{nameof(BaseUiBuilder.Button)} Instead")]
        public static UiButton CreateCommand(in UiPosition pos, in UiOffset offset, UiColor color, string command)
        {
            UiButton button = BaseUiComponent.CreateBase<UiButton>(pos, offset);
            button.Button.Color = color;
            button.Button.Command = command;
            button.Button.ButtonType = ButtonType.Command;
            return button;
        }

        [Obsolete($"Use {nameof(BaseUiBuilder)}.{nameof(BaseUiBuilder.Button)} Instead")]
        public static UiButton CreateClose(in UiPosition pos, in UiOffset offset, UiColor color, string close)
        {
            UiButton button = BaseUiComponent.CreateBase<UiButton>(pos, offset);
            button.Button.Color = color;
            button.Button.Command = close;
            button.Button.ButtonType = ButtonType.Close;
            return button;
        }
    }

    extension(UiImage)
    {
        [Obsolete($"Use {nameof(BaseUiBuilder)}.{nameof(BaseUiBuilder.ImageSprite)} Instead")]
        public static UiImage CreateSpriteImage(in UiPosition pos, in UiOffset offset, UiColor color, string sprite)
        {
            UiImage image = BaseUiComponent.CreateBase<UiImage>(pos, offset);
            image.Image.Color = color;
            image.Image.Sprite = sprite;
            return image;
        }
    }

    extension(UiInput)
    {
        [Obsolete($"Use {nameof(BaseUiBuilder)}.{nameof(BaseUiBuilder.Input)} Instead")]
        public static UiInput Create(in UiPosition pos, in UiOffset offset, UiColor textColor, string text, int size, string cmd, string font, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
        {
            UiInput input = BaseUiComponent.CreateBase<UiInput>(pos, offset);
            InputComponent comp = input.Input;
            comp.Text = text;
            comp.FontSize = size;
            comp.Color = textColor;
            comp.Align = align;
            comp.Font = font;
            comp.Command = cmd;
            comp.CharsLimit = charsLimit;
            comp.Mode = mode;
            comp.LineType = lineType;
            return input;
        }
    }

    extension(UiItemIcon)
    {
        [Obsolete($"Use {nameof(BaseUiBuilder)}.{nameof(BaseUiBuilder.ItemIcon)} Instead")]
        public static UiItemIcon Create(in UiPosition pos, in UiOffset offset, UiColor color, int itemId, ulong skinId = 0)
        {
            UiItemIcon icon = BaseUiComponent.CreateBase<UiItemIcon>(pos, offset);
            icon.Icon.Color = color;
            icon.Icon.ItemId = itemId;
            icon.Icon.SkinId = skinId;
            return icon;
        }
    }

    extension(UiLabel)
    {
        [Obsolete($"Use {nameof(BaseUiBuilder)}.{nameof(BaseUiBuilder.Label)} Instead")]
        public static UiLabel Create(in UiPosition pos, in UiOffset offset, UiColor color, string text, int size, string font, TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiLabel label = BaseUiComponent.CreateBase<UiLabel>(pos, offset);
            TextComponent textComp = label.Text;
            textComp.Text = text;
            textComp.FontSize = size;
            textComp.Color = color;
            textComp.Align = align;
            textComp.Font = font;
            return label;
        }
    }

    extension(UiPanel)
    {
        [Obsolete($"Use {nameof(BaseUiBuilder)}.{nameof(BaseUiBuilder.Panel)} Instead")]
        public static UiPanel Create(in UiPosition pos, in UiOffset offset, UiColor color)
        {
            UiPanel panel = BaseUiComponent.CreateBase<UiPanel>(pos, offset);
            panel.Image.Color = color;
            return panel;
        }
    }

    extension(UiPlayerAvatar)
    {
        [Obsolete($"Use {nameof(BaseUiBuilder)}.{nameof(BaseUiBuilder.PlayerAvatar)} Instead")]
        public static UiPlayerAvatar Create(in UiPosition pos, in UiOffset offset, UiColor color, string steamId)
        {
            UiPlayerAvatar icon = BaseUiComponent.CreateBase<UiPlayerAvatar>(pos, offset);
            icon.Avatar.Color = color;
            icon.Avatar.SteamId = ulong.Parse(steamId);
            return icon;
        }
    }

    extension(UiRawImage)
    {
        [Obsolete($"Use {nameof(BaseUiBuilder)}.{nameof(BaseUiBuilder.RawImage)} Instead")]
        public static UiRawImage CreateDefault(in UiPosition pos, in UiOffset offset)
        {
            UiRawImage image = BaseUiComponent.CreateBase<UiRawImage>(pos, offset);
            return image;
        } 
        
        [Obsolete($"Use {nameof(BaseUiBuilder)}.{nameof(BaseUiBuilder.WebImage)} Instead")]
        public static UiRawImage CreateUrl(in UiPosition pos, in UiOffset offset, UiColor color, string url)
        {
            UiRawImage image = BaseUiComponent.CreateBase<UiRawImage>(pos, offset);
            image.RawImage.Color = color;
            image.RawImage.Url = url;
            return image;
        }
        
        [Obsolete($"Use {nameof(BaseUiBuilder)}.{nameof(BaseUiBuilder.TextureImage)} Instead")]
        public static UiRawImage CreateTexture(in UiPosition pos, in UiOffset offset, UiColor color, string icon)
        {
            UiRawImage image = BaseUiComponent.CreateBase<UiRawImage>(pos, offset);
            image.RawImage.Color = color;
            image.RawImage.Texture = icon;
            return image;
        }
        
        [Obsolete($"Use {nameof(BaseUiBuilder)}.{nameof(BaseUiBuilder.FileStorageImage)} Instead")]
        public static UiRawImage CreateFileImage(in UiPosition pos, in UiOffset offset, UiColor color, string png)
        {
            UiRawImage image = BaseUiComponent.CreateBase<UiRawImage>(pos, offset);
            image.RawImage.Color = color;
            image.RawImage.Png = png;
            return image;
        }
    }

    extension(UiScrollView)
    {
        [Obsolete($"Use {nameof(BaseUiBuilder)}.{nameof(BaseUiBuilder.ScrollView)} Instead")]
        public static UiScrollView Create(in UiPosition pos, in UiOffset offset, bool horizontal, bool vertical, ScrollRect.MovementType movementType, float elasticity, bool inertia, float decelerationRate, float scrollSensitivity)
        {
            UiScrollView scroll = BaseUiComponent.CreateBase<UiScrollView>(pos, offset);
            ScrollViewComponent comp = scroll.ScrollView;
            comp.MovementType = movementType;
            comp.Elasticity = elasticity;
            comp.Inertia = inertia;
            comp.DecelerationRate = decelerationRate;
            comp.ScrollSensitivity = scrollSensitivity;
            return scroll;
        }
    }

    extension(UiSection)
    {
        [Obsolete($"Use {nameof(BaseUiBuilder)}.{nameof(BaseUiBuilder.Section)} Instead")]
        public static UiSection Create(in UiPosition pos, in UiOffset offset)
        {
            UiSection panel = BaseUiComponent.CreateBase<UiSection>(pos, offset);
            return panel;
        }
    }
}