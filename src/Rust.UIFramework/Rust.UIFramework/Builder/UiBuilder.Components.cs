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
            component.Name = GetComponentName();
            //_componentLookup[component.Name] = component;
            _components.Add(component);
        }

        public string GetComponentName()
        {
            return string.Concat(_componentBaseName, _components.Count.ToString());
        }
        #endregion

        #region Section
        public UiSection Section(BaseUiComponent parent, UiPosition pos, UiOffset? offset = null)
        {
            UiSection section = UiSection.Create(pos, offset);
            AddComponent(section, parent);
            return section;
        }
        #endregion
        
        #region Panel
        public UiPanel Panel(BaseUiComponent parent, UiColor color, UiPosition pos, UiOffset? offset = null)
        {
            UiPanel panel = UiPanel.Create(pos, offset, color);
            AddComponent(panel, parent);
            return panel;
        }
        #endregion

        #region Button
        public UiButton EmptyCommandButton(BaseUiComponent parent, UiColor color, UiPosition pos, string cmd)
        {
            UiButton button = UiButton.CreateCommand(pos, null, color, cmd);
            AddComponent(button, parent);
            return button;
        }

        public UiButton EmptyCloseButton(BaseUiComponent parent, UiColor color, UiPosition pos, string close)
        {
            UiButton button = UiButton.CreateClose(pos, null, color, close);
            AddComponent(button, parent);
            return button;
        }

        public UiButton TextButton(BaseUiComponent parent, string text, int textSize, UiColor textColor, UiColor buttonColor, UiPosition pos, string cmd, TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiButton button = EmptyCommandButton(parent, buttonColor, pos, cmd);
            Label(button, text, textSize, textColor, UiPosition.Full, align);
            return button;
        }

        public UiButton ImageButton(BaseUiComponent parent, UiColor buttonColor, string png, UiPosition pos, string cmd)
        {
            UiButton button = EmptyCommandButton(parent, buttonColor, pos, cmd);
            Image(button, png, UiPosition.Full);
            return button;
        }

        public UiButton WebImageButton(BaseUiComponent parent, UiColor buttonColor, string url, UiPosition pos, string cmd)
        {
            UiButton button = EmptyCommandButton(parent, buttonColor, pos, cmd);
            WebImage(button, url, UiPosition.Full);
            return button;
        }

        public UiButton ItemIconButton(BaseUiComponent parent, UiColor buttonColor, int itemId, UiPosition pos, string cmd)
        {
            UiButton button = EmptyCommandButton(parent, buttonColor, pos, cmd);
            ItemIcon(button, itemId, UiPosition.Full);
            return button;
        }

        public UiButton ItemIconButton(BaseUiComponent parent, UiColor buttonColor, int itemId, ulong skinId, UiPosition pos, string cmd)
        {
            UiButton button = EmptyCommandButton(parent, buttonColor, pos, cmd);
            ItemIcon(button, itemId, skinId, UiPosition.Full);
            return button;
        }

        public UiButton TextCloseButton(BaseUiComponent parent, string text, int textSize, UiColor textColor, UiColor buttonColor, UiPosition pos, string close, TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiButton button = EmptyCloseButton(parent, buttonColor, pos, close);
            Label(button, text, textSize, textColor, UiPosition.Full, align);
            return button;
        }

        public UiButton ImageCloseButton(BaseUiComponent parent, UiColor buttonColor, string png, UiPosition pos, string close)
        {
            UiButton button = EmptyCloseButton(parent, buttonColor, pos, close);
            Image(button, png, UiPosition.Full);
            return button;
        }

        public UiButton WebImageCloseButton(BaseUiComponent parent, UiColor buttonColor, string url, UiPosition pos, string close)
        {
            UiButton button = EmptyCloseButton(parent, buttonColor, pos, close);
            WebImage(button, url, UiPosition.Full);
            return button;
        }
        #endregion

        #region Image
        public UiImage Image(BaseUiComponent parent, string png, UiPosition pos, UiColor color)
        {
            uint _;
            if (!uint.TryParse(png, out _))
            {
                throw new UiFrameworkException($"Image PNG '{png}' is not a valid uint. If trying to use a url please use WebImage instead");
            }

            UiImage image = UiImage.Create(pos, null, color, png);
            AddComponent(image, parent);
            return image;
        }

        public UiImage Image(BaseUiComponent parent, string png, UiPosition pos)
        {
            return Image(parent, png, pos, UiColors.StandardColors.White);
        }
        #endregion

        #region Item Icon
        public UiItemIcon ItemIcon(BaseUiComponent parent, int itemId, UiPosition pos, UiColor color)
        {
            UiItemIcon image = UiItemIcon.Create(pos, null, color, itemId);
            AddComponent(image, parent);
            return image;
        }

        public UiItemIcon ItemIcon(BaseUiComponent parent, int itemId, UiPosition pos)
        {
            return ItemIcon(parent, itemId, pos, UiColors.StandardColors.White);
        }

        public UiItemIcon ItemIcon(BaseUiComponent parent, int itemId, ulong skinId, UiPosition pos, UiColor color)
        {
            UiItemIcon image = UiItemIcon.Create(pos, null, color, itemId, skinId);
            AddComponent(image, parent);
            return image;
        }

        public UiItemIcon ItemIcon(BaseUiComponent parent, int itemId, ulong skinId, UiPosition pos)
        {
            return ItemIcon(parent, itemId, skinId, pos, UiColors.StandardColors.White);
        }
        #endregion

        #region Raw Image
        public UiRawImage WebImage(BaseUiComponent parent, string url, UiPosition pos)
        {
            return WebImage(parent, url, pos, UiColors.StandardColors.White);
        }
        
        public UiRawImage WebImage(BaseUiComponent parent, string url, UiPosition pos, UiColor color)
        {
            if (!url.StartsWith("http"))
            {
                throw new UiFrameworkException($"WebImage Url '{url}' is not a valid url. If trying to use a png id please use Image instead");
            }

            UiRawImage image = UiRawImage.CreateUrl(pos, null, color, url);
            AddComponent(image, parent);
            return image;
        }
        
        public UiRawImage TextureImage(BaseUiComponent parent, string texture, UiPosition pos)
        {
            return TextureImage(parent, texture, pos, UiColors.StandardColors.White);
        }
        
        public UiRawImage TextureImage(BaseUiComponent parent, string texture, UiPosition pos, UiColor color)
        {
            UiRawImage image = UiRawImage.CreateTexture(pos, null, color, texture);
            AddComponent(image, parent);
            return image;
        }
        #endregion

        #region Label
        public UiLabel Label(BaseUiComponent parent, string text, int size, UiColor textColor, UiPosition pos, TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiLabel label = UiLabel.Create(pos, null, textColor, text, size, _font, align);
            AddComponent(label, parent);
            return label;
        }

        public UiLabel LabelBackground(BaseUiComponent parent, string text, int size, UiColor textColor, UiColor backgroundColor, UiPosition pos, TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiPanel panel = Panel(parent, backgroundColor, pos);
            UiLabel label = UiLabel.Create(UiPosition.Full, null, textColor, text, size, _font, align);
            AddComponent(label, panel);
            return label;
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

        public T TextOutline<T>(T outline, UiColor color, Vector2 distance) where T : BaseUiTextOutline
        {
            outline.AddTextOutline(color, distance);
            return outline;
        }

        public T TextOutline<T>(T outline, UiColor color, Vector2 distance, bool useGraphicAlpha) where T : BaseUiTextOutline
        {
            outline.AddTextOutline(color, distance, useGraphicAlpha);
            return outline;
        }
        #endregion

        #region Input
        public UiInput Input(BaseUiComponent parent, string text, int fontSize, UiColor textColor, UiColor backgroundColor, UiPosition pos, string cmd, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false, bool readOnly = false, InputField.LineType lineType = InputField.LineType.SingleLine)
        {
            parent = Panel(parent, backgroundColor, pos);
            UiInput input = UiInput.Create(UiPosition.Full, null, textColor, text, fontSize, cmd, _font, align, charsLimit, isPassword, readOnly, lineType);
            AddComponent(input, parent);
            return input;
        }
        #endregion
    }
}