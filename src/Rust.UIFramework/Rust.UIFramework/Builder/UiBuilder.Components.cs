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
        public UiPanel Panel(BaseUiComponent parent, UiColor color, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            UiPanel panel = UiPanel.Create(pos, offset, color);
            AddComponent(panel, parent);
            return panel;
        }
        #endregion

        #region Button
        public UiButton CommandButton(BaseUiComponent parent, UiColor color, string command, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            UiButton button = UiButton.CreateCommand(pos, offset, color, command);
            AddComponent(button, parent);
            return button;
        }

        public UiButton CloseButton(BaseUiComponent parent, UiColor color, string close, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            UiButton button = UiButton.CreateClose(pos, offset, color, close);
            AddComponent(button, parent);
            return button;
        }
        #endregion

        #region Image
        public UiImage ImageFileStorage(BaseUiComponent parent, string png, UiColor color, UiPosition pos, UiOffset offset = default(UiOffset))
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

        public UiImage ImageFileStorage(BaseUiComponent parent, string png, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            return ImageFileStorage(parent, png, UiColor.White, pos, offset);
        }
        
        public UiImage ImageSprite(BaseUiComponent parent, string sprite, UiColor color, UiPosition pos, UiOffset offset = default(UiOffset))
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

        public UiImage ImageSprite(BaseUiComponent parent, string sprite, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            return ImageSprite(parent, sprite, UiColor.White, pos, offset);
        }
        #endregion

        #region Item Icon
        public UiItemIcon ItemIcon(BaseUiComponent parent, int itemId, UiColor color, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            UiItemIcon image = UiItemIcon.Create(pos, offset, color, itemId);
            AddComponent(image, parent);
            return image;
        }

        public UiItemIcon ItemIcon(BaseUiComponent parent, int itemId, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            return ItemIcon(parent, itemId, UiColor.White, pos, offset);
        }

        public UiItemIcon ItemIcon(BaseUiComponent parent, int itemId, ulong skinId, UiColor color, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            UiItemIcon image = UiItemIcon.Create(pos, offset, color, itemId, skinId);
            AddComponent(image, parent);
            return image;
        }

        public UiItemIcon ItemIcon(BaseUiComponent parent, int itemId, ulong skinId, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            return ItemIcon(parent, itemId, skinId, UiColor.White, pos, offset);
        }
        #endregion

        #region Raw Image
        public UiRawImage WebImage(BaseUiComponent parent, string url, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            return WebImage(parent, url, UiColor.White, pos, offset);
        }
        
        public UiRawImage WebImage(BaseUiComponent parent, string url, UiColor color, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            if (!url.StartsWith("http"))
            {
                throw new UiFrameworkException($"WebImage Url '{url}' is not a valid url. If trying to use a png id please use {nameof(ImageFileStorage)} instead");
            }

            UiRawImage image = UiRawImage.CreateUrl(pos, offset, color, url);
            AddComponent(image, parent);
            return image;
        }
        
        public UiRawImage TextureImage(BaseUiComponent parent, string texture, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            return TextureImage(parent, texture, UiColor.White, pos, offset);
        }
        
        public UiRawImage TextureImage(BaseUiComponent parent, string texture, UiColor color, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            UiRawImage image = UiRawImage.CreateTexture(pos, offset, color, texture);
            AddComponent(image, parent);
            return image;
        }
        #endregion

        #region Label
        public UiLabel Label(BaseUiComponent parent, string text, int size, UiColor textColor, UiPosition pos, UiOffset offset = default(UiOffset), TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiLabel label = UiLabel.Create(pos, offset, textColor, text, size, _font, align);
            AddComponent(label, parent);
            return label;
        }

        public UiLabel LabelBackground(BaseUiComponent parent, string text, int size, UiColor textColor, UiColor backgroundColor, UiPosition pos, UiOffset offset = default(UiOffset), TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiPanel panel = Panel(parent, backgroundColor, pos);
            UiLabel label = UiLabel.Create(UiPosition.HorizontalPaddedFull, offset, textColor, text, size, _font, align);
            AddComponent(label, panel);
            return label;
        }
        #endregion
        
        #region Input
        public UiInput Input(BaseUiComponent parent, string text, int fontSize, UiColor textColor, UiPosition pos , UiOffset offset = default(UiOffset), string command = "", TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false, bool readOnly = false, InputField.LineType lineType = InputField.LineType.SingleLine)
        {
            UiInput input = UiInput.Create(pos, offset, textColor, text, fontSize, command, _font, align, charsLimit, isPassword, readOnly, lineType);
            AddComponent(input, parent);
            return input;
        }
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