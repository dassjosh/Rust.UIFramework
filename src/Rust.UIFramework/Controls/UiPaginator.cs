using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Libraries.UiCommands;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UI;

namespace Oxide.Ext.UiFramework.Controls;

public class UiPaginator : BaseUiControl
{
    public UiButton FirstPage;
    public UiButton PreviousPage;
    public List<UiButton> PageButtons;
    public UiButton NextPage;
    public UiButton LastPage;

    public static UiPaginator Create(BaseUiBuilder builder, in UiReference parent, GridPosition grid, int currentPage, int maxPage, int fontSize, UiColor textColor, UiColor buttonColor, UiColor activePageColor, string command, UiColor? disabledColorMultiplier)
    {
        return Create(builder, parent, grid, currentPage, maxPage, fontSize, textColor, buttonColor, activePageColor, PartialCommand.Create<int>(command), disabledColorMultiplier);
    }
    
    public static UiPaginator Create(BaseUiBuilder builder, in UiReference parent, GridPosition grid, int currentPage, int maxPage, int fontSize, UiColor textColor, UiColor buttonColor, UiColor activePageColor, ICommandBuilder<int> command, UiColor? disabledColorMultiplier)
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

        bool isFirstPage = currentPage == 0;
        bool isLastPage = currentPage == maxPage;
        UiColor disabledButtonColor = buttonColor * (disabledColorMultiplier ?? UiColors.DisabledButtonMultiplier);
        UiColor disabledTextColor = textColor * (disabledColorMultiplier ?? UiColors.DisabledButtonMultiplier);
        var padding = new UiPadding(2f).ToOffset();
        
        control.FirstPage = builder.IconButton(parent, grid, padding, isFirstPage ? disabledButtonColor : buttonColor, Icons.StepBackward, command.Build(0), isFirstPage ? disabledTextColor : textColor);
        grid.MoveCols(1);
        control.PreviousPage = builder.IconButton(parent, grid, padding, isFirstPage ? disabledButtonColor : buttonColor, Icons.Backward,  command.Build(Math.Max(0, currentPage - 1)), isFirstPage ? disabledTextColor : textColor);
        grid.MoveCols(1);

        for (int i = startPage; i <= endPage; i++)
        {
            control.PageButtons.Add(builder.TextButton(parent, grid, padding, StringCache<int>.ToString(i + 1), fontSize, textColor, i == currentPage ? activePageColor : buttonColor, command.Build(i)));
            grid.MoveCols(1);
        }

        control.NextPage = builder.IconButton(parent, grid, padding, isLastPage ? disabledButtonColor : buttonColor, Icons.Forward, command.Build(Math.Min(maxPage, currentPage + 1)), isLastPage ? disabledTextColor : textColor);
        grid.MoveCols(1);
        control.LastPage = builder.IconButton(parent, grid, padding, isLastPage ? disabledButtonColor : buttonColor, Icons.StepForward, command.Build(maxPage), isLastPage ? disabledTextColor : textColor);

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
        UiFrameworkPool.FreeList(PageButtons);
        NextPage = null;
        LastPage = null;
    }
}