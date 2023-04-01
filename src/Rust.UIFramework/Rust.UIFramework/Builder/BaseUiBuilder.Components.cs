using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Controls;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Builder
{
    public partial class BaseUiBuilder
    {
        #region Add Components
        public void AddComponent(BaseUiComponent component, UiReference parent)
        {
            component.Reference = new UiReference(parent.Name, UiNameCache.GetComponentName(RootName, Components.Count));
            Components.Add(component);
        }

        public void AddControl(BaseUiControl control)
        {
            Controls.Add(control);
        }
        
        private void AddAnchor(BaseUiComponent component, UiReference parent)
        {
            component.Reference = new UiReference(parent.Name, UiNameCache.GetAnchorName(RootName, Anchors.Count));
            Anchors.Add(component);
        }
        #endregion

        #region Section
        public UiSection Section(UiReference parent, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            UiSection section = UiSection.Create(pos, offset);
            AddComponent(section, parent);
            return section;
        }
        #endregion
        
        #region Panel
        public UiPanel Panel(UiReference parent, UiPosition pos, UiOffset offset, UiColor color)
        {
            UiPanel panel = UiPanel.Create(pos, offset, color);
            AddComponent(panel, parent);
            return panel;
        }
        
        public UiPanel Panel(UiReference parent, UiPosition pos, UiColor color) => Panel(parent, pos, default(UiOffset), color);
        #endregion

        #region Button
        public UiButton CommandButton(UiReference parent, UiPosition pos, UiOffset offset, UiColor color, string command)
        {
            UiButton button = UiButton.CreateCommand(pos, offset, color, command);
            AddComponent(button, parent);
            return button;
        }

        public UiButton CommandButton(UiReference parent, UiPosition pos, UiColor color, string command) => CommandButton(parent, pos, default(UiOffset), color, command);

        public UiButton CloseButton(UiReference parent, UiPosition pos, UiOffset offset, UiColor color, string close)
        {
            UiButton button = UiButton.CreateClose(pos, offset, color, close);
            AddComponent(button, parent);
            return button;
        }

        public UiButton CloseButton(UiReference parent, UiPosition pos, UiColor color, string close) => CloseButton(parent, pos, default(UiOffset), color, close);
        #endregion

        #region Image
        public UiImage ImageSprite(UiReference parent, UiPosition pos, UiOffset offset, string sprite, UiColor color)
        {
            UiImage image = UiImage.CreateSpriteImage(pos, offset, color, sprite);
            AddComponent(image, parent);
            return image;
        }

        public UiImage ImageSprite(UiReference parent, UiPosition pos, UiOffset offset, string sprite) => ImageSprite(parent, pos, offset, sprite, UiColor.White);
        public UiImage ImageSprite(UiReference parent, UiPosition pos, string sprite, UiColor color) => ImageSprite(parent, pos, default(UiOffset), sprite, color);
        public UiImage ImageSprite(UiReference parent, UiPosition pos, string sprite) => ImageSprite(parent, pos, sprite, UiColor.White);
        #endregion

        #region Item Icon
        public UiItemIcon ItemIcon(UiReference parent, UiPosition pos, UiOffset offset, int itemId, ulong skinId, UiColor color)
        {
            UiItemIcon image = UiItemIcon.Create(pos, offset, color, itemId, skinId);
            AddComponent(image, parent);
            return image;
        }
        
        public UiItemIcon ItemIcon(UiReference parent, UiPosition pos, UiOffset offset, int itemId, ulong skinId) => ItemIcon(parent, pos, offset, itemId, skinId, UiColor.White);
        public UiItemIcon ItemIcon(UiReference parent, UiPosition pos, UiOffset offset, int itemId, UiColor color) => ItemIcon(parent, pos, offset, itemId, 0, color);
        public UiItemIcon ItemIcon(UiReference parent, UiPosition pos, UiOffset offset, int itemId) => ItemIcon(parent, pos, offset, itemId, UiColor.White);
        public UiItemIcon ItemIcon(UiReference parent, UiPosition pos, int itemId, ulong skinId) => ItemIcon(parent, pos, default(UiOffset), itemId, skinId);
        public UiItemIcon ItemIcon(UiReference parent, UiPosition pos, int itemId, UiColor color) => ItemIcon(parent, pos, default(UiOffset), itemId, color);
        public UiItemIcon ItemIcon(UiReference parent, UiPosition pos, int itemId) => ItemIcon(parent, pos, default(UiOffset), itemId);
        #endregion

        #region Raw Image
        public UiRawImage WebImage(UiReference parent, UiPosition pos, UiOffset offset, string url, UiColor color)
        {
            if (!url.StartsWith("http"))
            {
                throw new UiFrameworkException($"WebImage Url '{url}' is not a valid url. If trying to use a png id please use {nameof(ImageFileStorage)} instead");
            }

            UiRawImage image = UiRawImage.CreateUrl(pos, offset, color, url);
            AddComponent(image, parent);
            return image;
        }

        public UiRawImage WebImage(UiReference parent, UiPosition pos, UiOffset offset, string url) => WebImage(parent, pos, offset, url, UiColor.White);
        public UiRawImage WebImage(UiReference parent, UiPosition pos, string url, UiColor color) => WebImage(parent, pos, default(UiOffset), url, color);
        public UiRawImage WebImage(UiReference parent, UiPosition pos, string url) => WebImage(parent, pos, url, UiColor.White);

        public UiRawImage TextureImage(UiReference parent, UiPosition pos, UiOffset offset, string texture, UiColor color)
        {
            UiRawImage image = UiRawImage.CreateTexture(pos, offset, color, texture);
            AddComponent(image, parent);
            return image;
        }

        public UiRawImage TextureImage(UiReference parent, UiPosition pos, UiOffset offset, string texture) => TextureImage(parent, pos, offset, texture, UiColor.White);
        public UiRawImage TextureImage(UiReference parent, UiPosition pos, string texture, UiColor color) => TextureImage(parent, pos, default(UiOffset), texture, color);
        public UiRawImage TextureImage(UiReference parent, UiPosition pos, string texture) => TextureImage(parent, pos, texture, UiColor.White);
        
        public UiRawImage ImageFileStorage(UiReference parent, UiPosition pos, UiOffset offset, string png, UiColor color)
        {
            uint _;
            if (!uint.TryParse(png, out _))
            {
                throw new UiFrameworkException($"Image PNG '{png}' is not a valid uint. If trying to use a url please use WebImage instead");
            }

            UiRawImage image = UiRawImage.CreateFileImage(pos, offset, color, png);
            AddComponent(image, parent);
            return image;
        }

        public UiRawImage ImageFileStorage(UiReference parent, UiPosition pos, string png, UiColor color) => ImageFileStorage(parent, pos, default(UiOffset), png, color);
        public UiRawImage ImageFileStorage(UiReference parent, UiPosition pos, UiOffset offset, string png) => ImageFileStorage(parent, pos, offset, png, UiColor.White);
        public UiRawImage ImageFileStorage(UiReference parent, UiPosition pos, string png) => ImageFileStorage(parent, pos, default(UiOffset), png, UiColor.White);
        #endregion

        #region Label
        public UiLabel Label(UiReference parent, UiPosition pos, UiOffset offset, string text, int size, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiLabel label = UiLabel.Create(pos, offset, textColor, text, size, Font, align);
            AddComponent(label, parent);
            return label;
        }

        public UiLabel Label(UiReference parent, UiPosition pos, string text, int fontSize, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter) => Label(parent, pos, default(UiOffset), text, fontSize, textColor, align);

        public UiLabelBackground LabelBackground(UiReference parent, UiPosition pos, UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiLabelBackground control = UiLabelBackground.Create(this, parent, pos, offset, text, fontSize, textColor, backgroundColor, align);
            AddControl(control);
            return control;
        }

        public UiLabelBackground LabelBackground(UiReference parent, UiPosition pos, string text, int fontSize, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter) => LabelBackground(parent, pos, default(UiOffset), text, fontSize, textColor, backgroundColor, align);
        #endregion
        
        #region Input
        public UiInput Input(UiReference parent, UiPosition pos, UiOffset offset, string text, int fontSize, UiColor textColor,  string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
        {
            UiInput input = UiInput.Create(pos, offset, textColor, text, fontSize, command, Font, align, charsLimit, mode, lineType);
            AddComponent(input, parent);
            return input;
        }

        public UiInput Input(UiReference parent, UiPosition pos, string text, int fontSize, UiColor textColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine) 
            => Input(parent, pos, default(UiOffset), text, fontSize, textColor, command, align, charsLimit, mode, lineType);
        #endregion

        #region Countdown
        public UiLabel Countdown(UiLabel label, int startTime, int endTime, int step, string command)
        {
            label.AddCountdown(startTime, endTime, step, command);
            return label;
        }
        #endregion

        #region Outline
        public T Outline<T>(T outline, UiColor color) where T : BaseUiOutline
        {
            outline.AddElementOutline(color);
            return outline;
        }

        public T Outline<T>(T outline, UiColor color, Vector2 distance, bool useGraphicAlpha = false) where T : BaseUiOutline
        {
            outline.AddElementOutline(color, distance, useGraphicAlpha);
            return outline;
        }
        #endregion

        #region Anchor
        public UiSection Anchor(UiReference parent, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            UiSection section = UiSection.Create(pos, offset);
            AddAnchor(section, parent);
            return section;
        }
        #endregion
    }
}