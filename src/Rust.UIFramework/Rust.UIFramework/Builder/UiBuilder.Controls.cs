using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Builder
{
    public partial class UiBuilder
    {
        public UiButton Checkbox(BaseUiComponent parent, bool isChecked, int textSize, UiColor textColor, UiColor backgroundColor, UiPosition pos, string cmd)
        {
            return TextButton(parent, isChecked ? "<b>✓</b>" : string.Empty, textSize, textColor, backgroundColor, pos, cmd);
        }

        public UiPanel ProgressBar(BaseUiComponent parent, float percentage, UiColor barColor, UiColor backgroundColor, UiPosition pos)
        {
            UiPanel background = Panel(parent, backgroundColor, pos);
            Panel(parent, barColor, pos.SliceHorizontal(0, Mathf.Clamp01(percentage)));
            return background;
        }
        
        public void SimpleNumberPicker(BaseUiComponent parent, int value, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiPosition pos, string cmd, float buttonWidth = 0.1f, bool readOnly = false)
        {
            UiPosition subtractSlice = pos.SliceHorizontal(0, buttonWidth);
            UiPosition addSlice = pos.SliceHorizontal(1 - buttonWidth, 1);
            
            TextButton(parent, "-", fontSize, textColor, buttonColor, subtractSlice, $"{cmd} {(value - 1).ToString()}");
            TextButton(parent, "+", fontSize, textColor, buttonColor, addSlice, $"{cmd} {(value + 1).ToString()}");
            
            UiInput input = Input(parent, value.ToString(), fontSize, textColor, backgroundColor, pos.SliceHorizontal(buttonWidth, 1 - buttonWidth), cmd, readOnly: readOnly);
            input.SetRequiresKeyboard();
        }
        
        public void IncrementalNumberPicker(BaseUiComponent parent, int value, int[] increments, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiPosition pos, string cmd, float buttonWidth = 0.3f, bool readOnly = false)
        {
            int incrementCount = increments.Length;
            float buttonSize = buttonWidth / incrementCount;
            for (int i = 0; i < incrementCount; i++)
            {
                int increment = increments[i];
                UiPosition subtractSlice = pos.SliceHorizontal(i * buttonSize, (i + 1) * buttonSize);
                UiPosition addSlice = pos.SliceHorizontal(1 - buttonWidth + i * buttonSize, 1 - buttonWidth + (i + 1) * buttonSize);

                string incrementDisplay = increment.ToString();
                TextButton(parent, string.Concat("-", incrementDisplay), fontSize, textColor, buttonColor, subtractSlice, $"{cmd} {(value - increment).ToString()}");
                TextButton(parent, string.Concat("+", incrementDisplay), fontSize, textColor, buttonColor, addSlice, $"{cmd} {(value + increment).ToString()}");
            }
            
            UiInput input = Input(parent, value.ToString(), fontSize, textColor, backgroundColor, pos.SliceHorizontal(0.3f, 0.7f), cmd, readOnly: readOnly);
            input.SetRequiresKeyboard();
        }
        
        public void IncrementalNumberPicker(BaseUiComponent parent, float value, float[] increments, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiPosition pos, string cmd, float buttonWidth = 0.3f, bool readOnly = false, string incrementFormat = "0.##")
        {
            int incrementCount = increments.Length;
            float buttonSize = buttonWidth / incrementCount;
            for (int i = 0; i < incrementCount; i++)
            {
                float increment = increments[i];
                UiPosition subtractSlice = pos.SliceHorizontal(i * buttonSize, (i + 1) * buttonSize);
                UiPosition addSlice = pos.SliceHorizontal(1 - buttonWidth + i * buttonSize, 1 - buttonWidth + (i + 1) * buttonSize);

                string incrementDisplay = increment.ToString(incrementFormat);
                TextButton(parent, string.Concat("-", incrementDisplay), fontSize, textColor, buttonColor, subtractSlice, $"{cmd} {(value - increment).ToString()}");
                TextButton(parent, incrementDisplay, fontSize, textColor, buttonColor, addSlice, $"{cmd} {(value + increment).ToString()}");
            }
            
            UiInput input = Input(parent, value.ToString(), fontSize, textColor, backgroundColor, pos.SliceHorizontal(0.3f, 0.7f), cmd, readOnly: readOnly);
            input.SetRequiresKeyboard();
        }

        public void Paginator(BaseUiComponent parent, int currentPage, int maxPage, int fontSize, UiColor textColor, UiColor buttonColor, UiColor activePageColor, GridPosition grid, string cmd)
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

            TextButton(parent, "<<<", fontSize, textColor, buttonColor, grid, $"{cmd} 0");
            grid.MoveCols(1);
            TextButton(parent, "<", fontSize, textColor, buttonColor, grid, $"{cmd} {Math.Max(0, currentPage - 1).ToString()}");
            grid.MoveCols(1);

            for (int i = startPage; i <= endPage; i++)
            {
                TextButton(parent, (i + 1).ToString(), fontSize, textColor, i == currentPage ? activePageColor : buttonColor, grid, $"{cmd} {i.ToString()}");
                grid.MoveCols(1);
            }

            TextButton(parent, ">", fontSize, textColor, buttonColor, grid, $"{cmd} {Math.Min(maxPage, currentPage + 1).ToString()}");
            grid.MoveCols(1);
            TextButton(parent, ">>>", fontSize, textColor, buttonColor, grid, $"{cmd} {maxPage.ToString()}");
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