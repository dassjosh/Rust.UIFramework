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
        public UiButton TextButton(BaseUiComponent parent, string text, int textSize, UiColor textColor, UiColor buttonColor, string command, UiPosition pos, UiOffset offset = default(UiOffset), TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiButton button = CommandButton(parent, buttonColor, command, pos, offset);
            Label(button, text, textSize, textColor, UiPosition.HorizontalPaddedFull, offset, align);
            return button;
        }
        
        public UiButton ImageFileStorageButton(BaseUiComponent parent, UiColor buttonColor, string png, string command, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            UiButton button = CommandButton(parent, buttonColor, command, pos, offset);
            ImageFileStorage(button, png, UiPosition.Full);
            return button;
        }
        
        public UiButton ImageSpriteButton(BaseUiComponent parent, UiColor buttonColor, string sprite, string command, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            UiButton button = CommandButton(parent, buttonColor, command, pos, offset);
            ImageSprite(button, sprite, UiPosition.Full);
            return button;
        }
        
        public UiButton WebImageButton(BaseUiComponent parent, UiColor buttonColor, string url,  string command, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            UiButton button = CommandButton(parent, buttonColor, command, pos, offset);
            WebImage(button, url, UiPosition.Full);
            return button;
        }
        
        public UiButton ItemIconButton(BaseUiComponent parent, UiColor buttonColor, int itemId, string command, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            UiButton button = CommandButton(parent, buttonColor, command, pos, offset);
            ItemIcon(button, itemId, UiPosition.Full);
            return button;
        }
        
        public UiButton ItemIconButton(BaseUiComponent parent, UiColor buttonColor, int itemId, ulong skinId, string command, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            UiButton button = CommandButton(parent, buttonColor, command, pos, offset);
            ItemIcon(button, itemId, skinId, UiPosition.Full);
            return button;
        }

        public UiInput InputBackground(BaseUiComponent parent, string text, int fontSize, UiColor textColor, UiColor backgroundColor, UiPosition pos , UiOffset offset = default(UiOffset), string command = "", TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false, bool readOnly = false, InputField.LineType lineType = InputField.LineType.SingleLine)
        {
            parent = Panel(parent, backgroundColor, pos);
            UiInput input = Input(parent, text, fontSize, textColor,UiPosition.HorizontalPaddedFull, offset, command, align, charsLimit, isPassword, readOnly, lineType);
            return input;
        }
        
        public UiButton Checkbox(BaseUiComponent parent, bool isChecked, int textSize, UiColor textColor, UiColor backgroundColor, UiPosition pos, string command)
        {
            return TextButton(parent, isChecked ? "<b>✓</b>" : string.Empty, textSize, textColor, backgroundColor, command, pos);
        }

        public UiPanel ProgressBar(BaseUiComponent parent, float percentage, UiColor barColor, UiColor backgroundColor, UiPosition pos)
        {
            UiPanel background = Panel(parent, backgroundColor, pos);
            Panel(parent, barColor, UiPosition.SliceHorizontal(pos,0, Mathf.Clamp01(percentage)));
            return background;
        }
        
        public void SimpleNumberPicker(BaseUiComponent parent, int value, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiPosition pos, string command, int minValue = int.MinValue, int maxValue = int.MaxValue, float buttonWidth = 0.1f)
        {
            if (value > minValue)
            {
                UiPosition subtractSlice = UiPosition.SliceHorizontal(pos,0, buttonWidth);
                TextButton(parent, "-", fontSize, textColor, buttonColor, $"{command} {NumberCache<int>.ToString(value - 1)}", subtractSlice);
            }

            if (value < maxValue)
            {
                UiPosition addSlice = UiPosition.SliceHorizontal(pos, 1 - buttonWidth, 1);
                TextButton(parent, "+", fontSize, textColor, buttonColor, $"{command} {NumberCache<int>.ToString(value + 1)}", addSlice);
            }

            LabelBackground(parent, NumberCache<int>.ToString(value), fontSize, textColor, backgroundColor, UiPosition.SliceHorizontal(pos,buttonWidth, 1 - buttonWidth));
        }
        
        public void IncrementalNumberPicker(BaseUiComponent parent, int value, int[] increments, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiPosition pos, string command, int minValue = int.MinValue, int maxValue = int.MaxValue, float buttonWidth = 0.3f, bool readOnly = false)
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
                    TextButton(parent, string.Concat("-", incrementDisplay), fontSize, textColor, buttonColor, $"{command} {NumberCache<float>.ToString(value - increment)}", subtractSlice);
                }

                if (value + increment < maxValue)
                {
                    TextButton(parent, string.Concat("+", incrementDisplay), fontSize, textColor, buttonColor, $"{command} {NumberCache<float>.ToString(value + increment)}", addSlice);
                }
            }
            
            UiInput input = InputBackground(parent, value.ToString(), fontSize, textColor, backgroundColor, UiPosition.SliceHorizontal(pos, buttonWidth, 1f - buttonWidth), command: command, readOnly: readOnly);
            input.SetRequiresKeyboard();
        }
        
        public void IncrementalNumberPicker(BaseUiComponent parent, float value, float[] increments, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiPosition pos, string command, float minValue = float.MinValue, float maxValue = float.MaxValue, float buttonWidth = 0.3f, bool readOnly = false, string incrementFormat = "0.##")
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
                    TextButton(parent, string.Concat("-", incrementDisplay), fontSize, textColor, buttonColor, $"{command} {NumberCache<float>.ToString(value - increment)}", subtractSlice);
                }

                if (value + increment < maxValue)
                {
                    TextButton(parent, incrementDisplay, fontSize, textColor, buttonColor, $"{command} {NumberCache<float>.ToString(value + increment)}", addSlice);
                }
            }
            
            UiInput input = InputBackground(parent, value.ToString(), fontSize, textColor, backgroundColor, UiPosition.SliceHorizontal(pos, buttonWidth, 1f - buttonWidth), command: command, readOnly: readOnly);
            input.SetRequiresKeyboard();
        }

        public void Paginator(BaseUiComponent parent, int currentPage, int maxPage, int fontSize, UiColor textColor, UiColor buttonColor, UiColor activePageColor, GridPosition grid, string command)
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

            TextButton(parent, "<<<", fontSize, textColor, buttonColor, $"{command} 0", grid);
            grid.MoveCols(1);
            TextButton(parent, "<", fontSize, textColor, buttonColor, $"{command} {NumberCache<int>.ToString(Math.Max(0, currentPage - 1))}", grid);
            grid.MoveCols(1);

            for (int i = startPage; i <= endPage; i++)
            {
                TextButton(parent, (i + 1).ToString(), fontSize, textColor, i == currentPage ? activePageColor : buttonColor, $"{command} {NumberCache<int>.ToString(i)}", grid);
                grid.MoveCols(1);
            }

            TextButton(parent, ">", fontSize, textColor, buttonColor, $"{command} {NumberCache<int>.ToString(Math.Min(maxPage, currentPage + 1))}", grid);
            grid.MoveCols(1);
            TextButton(parent, ">>>", fontSize, textColor, buttonColor, $"{command} {NumberCache<int>.ToString(maxPage)}", grid);
        }

        public void ScrollBar(BaseUiComponent parent, int currentPage, int maxPage, UiColor barColor, UiColor backgroundColor, UiPosition position, string command, ScrollbarDirection direction = ScrollbarDirection.Vertical, string sprite = UiConstants.Sprites.RoundedBackground2)
        {
            UiPanel background = Panel(parent, backgroundColor, position);
            background.SetSpriteMaterialImage(sprite, null, Image.Type.Sliced);
            float buttonSize = 1f / maxPage;
            for (int i = 0; i < maxPage; i++)
            {
                float min = buttonSize * i;
                float max = buttonSize * (i + 1);
                UiPosition pagePosition = direction == ScrollbarDirection.Vertical ? UiPosition.SliceVertical(UiPosition.Full, min, max) : UiPosition.SliceHorizontal(UiPosition.Full, min, max);
                if (i != currentPage)
                {
                    UiButton button = CommandButton(background, backgroundColor, $"{command} {NumberCache<int>.ToString(i)}", pagePosition);
                    button.SetSpriteMaterialImage(sprite, null, Image.Type.Sliced);
                }
                else
                {
                    UiPanel panel = Panel(background, barColor, pagePosition);
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
                    Panel(parent, color, UiPosition.Top, new UiOffset(tbMin, 0, tbMax, width));
                }
            
                if (left)
                {
                    Panel(parent, color, UiPosition.Left, new UiOffset(-width, lrMin, 0, lrMax));
                }
            
                if (bottom)
                {
                    Panel(parent, color, UiPosition.Bottom, new UiOffset(tbMin, -width, tbMax, 0));
                }
            
                if (right)
                {
                    Panel(parent, color, UiPosition.Right, new UiOffset(0, lrMin, width, lrMax));
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
                    Panel(parent, color, UiPosition.Top, new UiOffset(tbMin, width, tbMax, 0));
                }
            
                if (left)
                {
                    Panel(parent, color, UiPosition.Left, new UiOffset(0, lrMin, -width, lrMax));
                }
            
                if (bottom)
                {
                    Panel(parent, color, UiPosition.Bottom, new UiOffset(tbMin, 0, tbMax, -width));
                }
            
                if (right)
                {
                    Panel(parent, color, UiPosition.Right, new UiOffset(width, lrMin, 0, lrMax));
                }
            }
        }

        private bool HasBorderFlag(BorderMode mode, BorderMode flag)
        {
            return (mode & flag) != 0;
        }
    }
}