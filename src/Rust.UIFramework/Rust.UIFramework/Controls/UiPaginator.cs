using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Controls
{
    public class UiPaginator : BaseUiControl
    {
        public UiButton FirstPage;
        public UiButton PreviousPage;
        public List<UiButton> PageButtons;
        public UiButton NextPage;
        public UiButton LastPage;

        public static UiPaginator Create(UiBuilder builder, BaseUiComponent parent, GridPosition grid, int currentPage, int maxPage, int fontSize, UiColor textColor, UiColor buttonColor, UiColor activePageColor, string command)
        {
            UiPaginator control = CreateControl<UiPaginator>();
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

            control.FirstPage = builder.TextButton(parent, grid, "<<<", fontSize, textColor, buttonColor, $"{command} 0");
            grid.MoveCols(1);
            control.PreviousPage = builder.TextButton(parent, grid, "<", fontSize, textColor, buttonColor, $"{command} {StringCache<int>.ToString(Math.Max(0, currentPage - 1))}");
            grid.MoveCols(1);

            for (int i = startPage; i <= endPage; i++)
            {
                control.PageButtons.Add(builder.TextButton(parent, grid, (i + 1).ToString(), fontSize, textColor, i == currentPage ? activePageColor : buttonColor, $"{command} {StringCache<int>.ToString(i)}"));
                grid.MoveCols(1);
            }

            control.NextPage = builder.TextButton(parent, grid, ">", fontSize, textColor, buttonColor, $"{command} {StringCache<int>.ToString(Math.Min(maxPage, currentPage + 1))}");
            grid.MoveCols(1);
            control.LastPage = builder.TextButton(parent, grid, ">>>", fontSize, textColor, buttonColor, $"{command} {StringCache<int>.ToString(maxPage)}");

            return control;
        }

        protected override void LeavePool()
        {
            base.LeavePool();
            PageButtons = UiFrameworkPool.GetList<UiButton>();
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            FirstPage = null;
            PreviousPage = null;
            UiFrameworkPool.FreeList(ref PageButtons);
            NextPage = null;
            LastPage = null;
        }

        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
}