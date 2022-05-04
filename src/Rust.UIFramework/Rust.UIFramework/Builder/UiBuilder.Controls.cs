using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder
{
    public partial class UiBuilder
    {
        public UiButton Checkbox(BaseUiComponent parent, bool isChecked, int textSize, UiColor textColor, UiColor backgroundColor, UiPosition pos, string cmd)
        {
            return TextButton(parent, isChecked ? "<b>✓</b>" : string.Empty, textSize, textColor, backgroundColor, pos, cmd);
        }

        public void SimpleNumberPicker(BaseUiComponent parent, int value, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiPosition pos, string cmd, float buttonWidth = 0.1f, bool readOnly = false)
        {
            UiPosition subtractSlice = pos.SliceHorizontal(0, .1f * buttonWidth);
            UiPosition addSlice = pos.SliceHorizontal(1 - buttonWidth, 1);
            
            TextButton(parent, "-", fontSize, textColor, buttonColor, subtractSlice, $"{cmd} {(value - 1).ToString()}");
            TextButton(parent, "+", fontSize, textColor, buttonColor, addSlice, $"{cmd} {(value + 1).ToString()}");
            
            UiInput input = Input(parent, value.ToString(), fontSize, textColor, backgroundColor, pos.SliceHorizontal(0.3f, 0.7f), cmd, readOnly: readOnly);
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
    }
}