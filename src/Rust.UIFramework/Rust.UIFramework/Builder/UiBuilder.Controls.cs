using System;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Builder
{
    public partial class UiBuilder
    {
        public UiButton TextButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, string text, int textSize, UiColor textColor, UiColor buttonColor, string command, TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
            Label(button, UiPosition.HorizontalPaddedFull, default(UiOffset), text, textSize, textColor , align);
            return button;
        }

        public UiButton TextButton(BaseUiComponent parent, UiPosition pos, string text, int textSize, UiColor textColor, UiColor buttonColor, string command, TextAnchor align = TextAnchor.MiddleCenter) 
            => TextButton(parent, pos, default(UiOffset), text, textSize, textColor, buttonColor, command, align);
        
        public UiButton ImageFileStorageButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor buttonColor, string png, string command)
        {
            UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
            ImageFileStorage(button, UiPosition.Full, png);
            return button;
        }

        public UiButton ImageFileStorageButton(BaseUiComponent parent, UiPosition pos, UiColor buttonColor, string png, string command) => ImageFileStorageButton(parent, pos, default(UiOffset), buttonColor, png, command);
        
        public UiButton ImageSpriteButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor buttonColor, string sprite, string command)
        {
            UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
            ImageSprite(button, UiPosition.Full, sprite);
            return button;
        }

        public UiButton ImageSpriteButton(BaseUiComponent parent, UiPosition pos, UiColor buttonColor, string sprite, string command) => ImageSpriteButton(parent, pos, default(UiOffset), buttonColor, sprite, command);
        
        public UiButton WebImageButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor buttonColor, string url, string command)
        {
            UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
            WebImage(button, UiPosition.Full, url);
            return button;
        }

        public UiButton WebImageButton(BaseUiComponent parent, UiPosition pos, UiColor buttonColor, string url, string command) => WebImageButton(parent, pos, default(UiOffset), buttonColor, url, command);
        
        public UiButton ItemIconButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor buttonColor, int itemId, string command)
        {
            UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
            ItemIcon(button, UiPosition.Full, itemId);
            return button;
        }

        public UiButton ItemIconButton(BaseUiComponent parent, UiPosition pos, UiColor buttonColor, int itemId, string command) => ItemIconButton(parent, pos, default(UiOffset), buttonColor, itemId, command);
        
        public UiButton ItemIconButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor buttonColor, int itemId, ulong skinId, string command)
        {
            UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
            ItemIcon(button, UiPosition.Full, itemId, skinId);
            return button;
        }

        public UiButton ItemIconButton(BaseUiComponent parent, UiPosition pos, UiColor buttonColor, int itemId, ulong skinId, string command) => ItemIconButton(parent, pos, default(UiOffset), buttonColor, itemId, skinId, command);

        public UiInput InputBackground(BaseUiComponent parent, UiPosition pos, UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false, bool readOnly = false, InputField.LineType lineType = InputField.LineType.SingleLine)
        {
            parent = Panel(parent,  pos, offset, backgroundColor);
            UiInput input = Input(parent, UiPosition.HorizontalPaddedFull, text, fontSize, textColor, command, align, charsLimit, isPassword, readOnly, lineType);
            return input;
        }

        public UiInput InputBackground(BaseUiComponent parent, UiPosition pos, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false, bool readOnly = false,
            InputField.LineType lineType = InputField.LineType.SingleLine) =>
            InputBackground(parent, pos, default(UiOffset), text, fontSize, textColor, backgroundColor, command, align, charsLimit, isPassword, readOnly, lineType);
        
        public UiButton Checkbox(BaseUiComponent parent, UiPosition pos, UiOffset offset, bool isChecked, int textSize, UiColor textColor, UiColor backgroundColor, string command)
        {
            return TextButton(parent, pos, offset, isChecked ? "<b>✓</b>" : string.Empty, textSize, textColor, backgroundColor, command);
        }

        public UiButton Checkbox(BaseUiComponent parent, UiPosition pos, bool isChecked, int textSize, UiColor textColor, UiColor backgroundColor, string command) => Checkbox(parent, pos, default(UiOffset), isChecked, textSize, textColor, backgroundColor, command);

        public UiPanel ProgressBar(BaseUiComponent parent, UiPosition pos, float percentage, UiColor barColor, UiColor backgroundColor)
        {
            UiPanel background = Panel(parent, pos, backgroundColor);
            Panel(parent, UiPosition.SliceHorizontal(pos,0, Mathf.Clamp01(percentage)), barColor);
            return background;
        }
        
        public void ButtonNumberPicker(BaseUiComponent parent, UiPosition pos, int currentValue, int minValue, int maxValue, int textSize, UiColor textColor, UiColor buttonColor, UiColor currentButtonColor, string command)
        {
            float size = 1f / (maxValue - minValue + 1);
            UiSection section = Section(parent, pos);
            for (int i = minValue; i <= maxValue; i++)
            {
                UiPosition buttonPos = UiPosition.SliceHorizontal(UiPosition.Full, size * (i - minValue), size * (i + 1 - minValue));
                if (i == currentValue)
                {
                    TextButton(section, buttonPos, NumberCache<int>.ToString(i),textSize, textColor, currentButtonColor, $"{command} {i}");
                }
                else
                {
                    TextButton(section, buttonPos, NumberCache<int>.ToString(i), textSize, textColor, buttonColor, $"{command} {i}");
                }
            }
        }
        
        public void SimpleNumberPicker(BaseUiComponent parent, UiPosition pos, int value, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, string command, int minValue = int.MinValue, int maxValue = int.MaxValue, float buttonWidth = 0.1f)
        {
            if (value > minValue)
            {
                UiPosition subtractSlice = UiPosition.SliceHorizontal(pos,0, buttonWidth);
                TextButton(parent, subtractSlice, "-", fontSize, textColor, buttonColor, $"{command} {NumberCache<int>.ToString(value - 1)}");
            }

            if (value < maxValue)
            {
                UiPosition addSlice = UiPosition.SliceHorizontal(pos, 1 - buttonWidth, 1);
                TextButton(parent, addSlice, "+", fontSize, textColor, buttonColor, $"{command} {NumberCache<int>.ToString(value + 1)}");
            }

            LabelBackground(parent, UiPosition.SliceHorizontal(pos,buttonWidth, 1 - buttonWidth), NumberCache<int>.ToString(value), fontSize, textColor, backgroundColor);
        }
        
        public void IncrementalNumberPicker(BaseUiComponent parent, UiPosition pos, int value, int[] increments, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, string command, int minValue = int.MinValue, int maxValue = int.MaxValue, float buttonWidth = 0.3f, bool readOnly = false)
        {
            int incrementCount = increments.Length;
            float buttonSize = buttonWidth / incrementCount;
            for (int i = 0; i < incrementCount; i++)
            {
                int increment = increments[i];
                UiPosition subtractSlice = UiPosition.SliceHorizontal(pos, i * buttonSize, (i + 1) * buttonSize);
                UiPosition addSlice = UiPosition.SliceHorizontal(pos, 1 - buttonWidth + i * buttonSize, 1 - buttonWidth + (i + 1) * buttonSize);

                string incrementDisplay = increment.ToString();
                if (value - increment > minValue)
                {
                    TextButton(parent, subtractSlice, string.Concat("-", incrementDisplay), fontSize, textColor, buttonColor, $"{command} {NumberCache<float>.ToString(value - increment)}");
                }

                if (value + increment < maxValue)
                {
                    TextButton(parent, addSlice, string.Concat("+", incrementDisplay), fontSize, textColor, buttonColor, $"{command} {NumberCache<float>.ToString(value + increment)}");
                }
            }
            
            UiInput input = InputBackground(parent, UiPosition.SliceHorizontal(pos, buttonWidth, 1f - buttonWidth), value.ToString(), fontSize, textColor, backgroundColor, command, readOnly: readOnly);
            input.SetRequiresKeyboard();
        }
        
        public void IncrementalNumberPicker(BaseUiComponent parent, UiPosition pos, float value, float[] increments, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, string command, float minValue = float.MinValue, float maxValue = float.MaxValue, float buttonWidth = 0.3f, bool readOnly = false, string incrementFormat = "0.##")
        {
            int incrementCount = increments.Length;
            float buttonSize = buttonWidth / incrementCount;
            for (int i = 0; i < incrementCount; i++)
            {
                float increment = increments[i];
                UiPosition subtractSlice = UiPosition.SliceHorizontal(pos, i * buttonSize, (i + 1) * buttonSize);
                UiPosition addSlice = UiPosition.SliceHorizontal(pos,1 - buttonWidth + i * buttonSize, 1 - buttonWidth + (i + 1) * buttonSize);

                string incrementDisplay = increment.ToString(incrementFormat);
                if (value - increment > minValue)
                {
                    TextButton(parent, subtractSlice, string.Concat("-", incrementDisplay), fontSize, textColor, buttonColor, $"{command} {NumberCache<float>.ToString(value - increment)}");
                }

                if (value + increment < maxValue)
                {
                    TextButton(parent, addSlice, incrementDisplay, fontSize, textColor, buttonColor, $"{command} {NumberCache<float>.ToString(value + increment)}");
                }
            }
            
            UiInput input = InputBackground(parent, UiPosition.SliceHorizontal(pos, buttonWidth, 1f - buttonWidth), value.ToString(), fontSize, textColor, backgroundColor, command, readOnly: readOnly);
            input.SetRequiresKeyboard();
        }

        public void Paginator(BaseUiComponent parent, GridPosition grid, int currentPage, int maxPage, int fontSize, UiColor textColor, UiColor buttonColor, UiColor activePageColor, string command)
        {
            grid.Reset();

            int totalButtons = (int)Math.Round(grid.NumCols, 0);
            int pageButtons = totalButtons - 5;

            int startPage = Math.Max(currentPage - pageButtons / 2, 0);
            int endPage = Math.Min(maxPage, startPage + pageButtons);
            if (endPage - startPage != pageButtons)
            {
                startPage = Math.Max(endPage - pageButtons, 0);
                if (endPage - startPage != pageButtons)
                {
                    grid.MoveCols((pageButtons - endPage - startPage) / 2f);
                }
            }

            TextButton(parent, grid, "<<<", fontSize, textColor, buttonColor, $"{command} 0");
            grid.MoveCols(1);
            TextButton(parent, grid, "<", fontSize, textColor, buttonColor, $"{command} {NumberCache<int>.ToString(Math.Max(0, currentPage - 1))}");
            grid.MoveCols(1);

            for (int i = startPage; i <= endPage; i++)
            {
                TextButton(parent, grid, (i + 1).ToString(), fontSize, textColor, i == currentPage ? activePageColor : buttonColor, $"{command} {NumberCache<int>.ToString(i)}");
                grid.MoveCols(1);
            }

            TextButton(parent, grid, ">", fontSize, textColor, buttonColor, $"{command} {NumberCache<int>.ToString(Math.Min(maxPage, currentPage + 1))}");
            grid.MoveCols(1);
            TextButton(parent, grid, ">>>", fontSize, textColor, buttonColor, $"{command} {NumberCache<int>.ToString(maxPage)}");
        }

        public void ScrollBar(BaseUiComponent parent, UiPosition position, int currentPage, int maxPage, UiColor barColor, UiColor backgroundColor, string command, ScrollbarDirection direction = ScrollbarDirection.Vertical, string sprite = UiConstants.Sprites.RoundedBackground2)
        {
            UiPanel background = Panel(parent, position, backgroundColor);
            background.SetSpriteMaterialImage(sprite, null, Image.Type.Sliced);
            float buttonSize = 1f / maxPage;
            for (int i = 0; i < maxPage; i++)
            {
                float min = buttonSize * i;
                float max = buttonSize * (i + 1);
                UiPosition pagePosition = direction == ScrollbarDirection.Vertical ? UiPosition.SliceVertical(UiPosition.Full, min, max) : UiPosition.SliceHorizontal(UiPosition.Full, min, max);
                if (i != currentPage)
                {
                    UiButton button = CommandButton(background, pagePosition, backgroundColor, $"{command} {NumberCache<int>.ToString(i)}");
                    button.SetSpriteMaterialImage(sprite, null, Image.Type.Sliced);
                }
                else
                {
                    UiPanel panel = Panel(background, pagePosition, barColor);
                    panel.SetSpriteMaterialImage(sprite, null, Image.Type.Sliced);
                }
            }
        }

        public void Border(BaseUiComponent parent, UiColor color, int width = 1, BorderMode border = BorderMode.All)
        {
            //If width is 0 nothing is displayed so don't try to render
            if (width == 0)
            {
                return;
            }
            
            bool top = HasBorderFlag(border, BorderMode.Top);
            bool left = HasBorderFlag(border, BorderMode.Left);
            bool bottom = HasBorderFlag(border, BorderMode.Bottom);
            bool right = HasBorderFlag(border, BorderMode.Right);

            if (width > 0)
            {
                int tbMin = left ? -width : 0;
                int tbMax = right ? width : 0;
                int lrMin = top ? -width : 0;
                int lrMax = bottom ? width : 0;
            
                if (top)
                {
                    Panel(parent, UiPosition.Top, new UiOffset(tbMin, 0, tbMax, width), color);
                }
            
                if (left)
                {
                    Panel(parent, UiPosition.Left, new UiOffset(-width, lrMin, 0, lrMax), color);
                }
            
                if (bottom)
                {
                    Panel(parent, UiPosition.Bottom, new UiOffset(tbMin, -width, tbMax, 0), color);
                }
            
                if (right)
                {
                    Panel(parent, UiPosition.Right, new UiOffset(0, lrMin, width, lrMax), color);
                }
            }
            else
            {
                int tbMin = left ? width : 0;
                int tbMax = right ? -width : 0;
                int lrMin = top ? width : 0;
                int lrMax = bottom ? -width : 0;
                
                if (top)
                {
                    Panel(parent, UiPosition.Top, new UiOffset(tbMin, width, tbMax, 0), color);
                }
            
                if (left)
                {
                    Panel(parent, UiPosition.Left, new UiOffset(0, lrMin, -width, lrMax), color);
                }
            
                if (bottom)
                {
                    Panel(parent, UiPosition.Bottom, new UiOffset(tbMin, 0, tbMax, -width), color);
                }
            
                if (right)
                {
                    Panel(parent, UiPosition.Right, new UiOffset(width, lrMin, 0, lrMax), color);
                }
            }
        }

        private bool HasBorderFlag(BorderMode mode, BorderMode flag)
        {
            return (mode & flag) != 0;
        }
    }
}