using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Builder
{
    public partial class UiBuilder
    {
        #region Add Components
        public void AddComponent(BaseUiComponent component, BaseUiComponent parent)
        {
            component.Parent = parent.Name;
            component.Name = UiNameCache.GetName(_rootName, _components.Count);
            //_componentLookup[component.Name] = component;
            _components.Add(component);
        }
        #endregion

        #region Section
        public UiSection Section(BaseUiComponent parent, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            UiSection section = UiSection.Create(pos, offset);
            AddComponent(section, parent);
            return section;
        }
        #endregion
        
        #region Panel
        public UiPanel Panel(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor color)
        {
            UiPanel panel = UiPanel.Create(pos, offset, color);
            AddComponent(panel, parent);
            return panel;
        }
        
        public UiPanel Panel(BaseUiComponent parent, UiPosition pos, UiColor color) => Panel(parent, pos, default(UiOffset), color);
        #endregion

        #region Button
        public UiButton CommandButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor color, string command)
        {
            UiButton button = UiButton.CreateCommand(pos, offset, color, command);
            AddComponent(button, parent);
            return button;
        }

        public UiButton CommandButton(BaseUiComponent parent, UiPosition pos, UiColor color, string command) => CommandButton(parent, pos, default(UiOffset), color, command);

        public UiButton CloseButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor color, string close)
        {
            UiButton button = UiButton.CreateClose(pos, offset, color, close);
            AddComponent(button, parent);
            return button;
        }

        public UiButton CloseButton(BaseUiComponent parent, UiPosition pos, UiColor color, string close) => CloseButton(parent, pos, default(UiOffset), color, close);
        #endregion

        #region Image
        public UiImage ImageFileStorage(BaseUiComponent parent, UiPosition pos, UiOffset offset, string png, UiColor color)
        {
            uint _;
            if (!uint.TryParse(png, out _))
            {
                throw new UiFrameworkException($"Image PNG '{png}' is not a valid uint. If trying to use a url please use WebImage instead");
            }

            UiImage image = UiImage.CreateFileImage(pos, offset, color, png);
            AddComponent(image, parent);
            return image;
        }

        public UiImage ImageFileStorage(BaseUiComponent parent, UiPosition pos, string png, UiColor color) => ImageFileStorage(parent, pos, default(UiOffset), png, color);
        public UiImage ImageFileStorage(BaseUiComponent parent, UiPosition pos, UiOffset offset, string png) => ImageFileStorage(parent, pos, offset, png, UiColor.White);
        public UiImage ImageFileStorage(BaseUiComponent parent, UiPosition pos, string png) => ImageFileStorage(parent, pos, default(UiOffset), png, UiColor.White);

        public UiImage ImageSprite(BaseUiComponent parent, UiPosition pos, UiOffset offset, string sprite, UiColor color)
        {
            uint _;
            if (!uint.TryParse(sprite, out _))
            {
                throw new UiFrameworkException($"Image PNG '{sprite}' is not a valid uint. If trying to use a url please use WebImage instead");
            }

            UiImage image = UiImage.CreateSpriteImage(pos, offset, color, sprite);
            AddComponent(image, parent);
            return image;
        }

        public UiImage ImageSprite(BaseUiComponent parent, UiPosition pos, UiOffset offset, string sprite) => ImageSprite(parent, pos, offset, sprite, UiColor.White);
        public UiImage ImageSprite(BaseUiComponent parent, UiPosition pos, string sprite, UiColor color) => ImageSprite(parent, pos, default(UiOffset), sprite, color);
        public UiImage ImageSprite(BaseUiComponent parent, UiPosition pos, string sprite) => ImageSprite(parent, pos, sprite, UiColor.White);
        #endregion

        #region Item Icon
        public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, UiOffset offset, int itemId, ulong skinId, UiColor color)
        {
            UiItemIcon image = UiItemIcon.Create(pos, offset, color, itemId, skinId);
            AddComponent(image, parent);
            return image;
        }
        
        public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, UiOffset offset, int itemId, ulong skinId) => ItemIcon(parent, pos, offset, itemId, skinId, UiColor.White);
        public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, UiOffset offset, int itemId, UiColor color) => ItemIcon(parent, pos, offset, itemId, 0, color);
        public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, UiOffset offset, int itemId) => ItemIcon(parent, pos, offset, itemId, UiColor.White);
        public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, int itemId, ulong skinId) => ItemIcon(parent, pos, default(UiOffset), itemId, skinId);
        public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, int itemId, UiColor color) => ItemIcon(parent, pos, default(UiOffset), itemId, color);
        public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, int itemId) => ItemIcon(parent, pos, default(UiOffset), itemId);
        #endregion

        #region Raw Image
        public UiRawImage WebImage(BaseUiComponent parent, UiPosition pos, UiOffset offset, string url, UiColor color)
        {
            if (!url.StartsWith("http"))
            {
                throw new UiFrameworkException($"WebImage Url '{url}' is not a valid url. If trying to use a png id please use {nameof(ImageFileStorage)} instead");
            }

            UiRawImage image = UiRawImage.CreateUrl(pos, offset, color, url);
            AddComponent(image, parent);
            return image;
        }

        public UiRawImage WebImage(BaseUiComponent parent, UiPosition pos, UiOffset offset, string url) => WebImage(parent, pos, offset, url, UiColor.White);
        public UiRawImage WebImage(BaseUiComponent parent, UiPosition pos, string url, UiColor color) => WebImage(parent, pos, default(UiOffset), url, color);
        public UiRawImage WebImage(BaseUiComponent parent, UiPosition pos, string url) => WebImage(parent, pos, url, UiColor.White);

        public UiRawImage TextureImage(BaseUiComponent parent, UiPosition pos, UiOffset offset, string texture, UiColor color)
        {
            UiRawImage image = UiRawImage.CreateTexture(pos, offset, color, texture);
            AddComponent(image, parent);
            return image;
        }

        public UiRawImage TextureImage(BaseUiComponent parent, UiPosition pos, UiOffset offset, string texture) => TextureImage(parent, pos, offset, texture, UiColor.White);
        public UiRawImage TextureImage(BaseUiComponent parent, UiPosition pos, string texture, UiColor color) => TextureImage(parent, pos, default(UiOffset), texture, color);
        public UiRawImage TextureImage(BaseUiComponent parent, UiPosition pos, string texture) => TextureImage(parent, pos, texture, UiColor.White);
        #endregion

        #region Label
        public UiLabel Label(BaseUiComponent parent, UiPosition pos, UiOffset offset, string text, int size, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiLabel label = UiLabel.Create(pos, offset, textColor, text, size, _font, align);
            AddComponent(label, parent);
            return label;
        }

        public UiLabel Label(BaseUiComponent parent, UiPosition pos, string text, int fontSize, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter) => Label(parent, pos, default(UiOffset), text, fontSize, textColor, align);

        public UiLabel LabelBackground(BaseUiComponent parent, UiPosition pos, UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiPanel panel = Panel(parent, pos, backgroundColor);
            UiLabel label = UiLabel.Create(UiPosition.HorizontalPaddedFull, offset, textColor, text, fontSize, _font, align);
            AddComponent(label, panel);
            return label;
        }

        public UiLabel LabelBackground(BaseUiComponent parent, UiPosition pos, string text, int fontSize, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter) => LabelBackground(parent, pos, default(UiOffset), text, fontSize, textColor, backgroundColor, align);
        #endregion
        
        #region Input
        public UiInput Input(BaseUiComponent parent, UiPosition pos, UiOffset offset, string text, int fontSize, UiColor textColor,  string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false, bool readOnly = false, InputField.LineType lineType = InputField.LineType.SingleLine)
        {
            UiInput input = UiInput.Create(pos, offset, textColor, text, fontSize, command, _font, align, charsLimit, isPassword, readOnly, lineType);
            AddComponent(input, parent);
            return input;
        }

        public UiInput Input(BaseUiComponent parent, UiPosition pos, string text, int fontSize, UiColor textColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false, bool readOnly = false, InputField.LineType lineType = InputField.LineType.SingleLine) 
            => Input(parent, pos, default(UiOffset), text, fontSize, textColor, command, align, charsLimit, isPassword, readOnly, lineType);
        #endregion

        #region Countdown
        public UiLabel Countdown(UiLabel label, int startTime, int endTime, int step, string command)
        {
            label.AddCountdown(startTime, endTime, step, command);
            return label;
        }
        #endregion

        #region Outline
        public T TextOutline<T>(T outline, UiColor color) where T : BaseUiTextOutline
        {
            outline.AddTextOutline(color);
            return outline;
        }

        public T TextOutline<T>(T outline, UiColor color, Vector2 distance, bool useGraphicAlpha = false) where T : BaseUiTextOutline
        {
            outline.AddTextOutline(color, distance, useGraphicAlpha);
            return outline;
        }
        #endregion
    }
}