using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Padding;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Controls.Popover;

public class UiDropdownMenu : BaseUiControl
{
    public UiSection ScrollBarSection;
    public UiScrollBar ScrollBar;
    public List<UiDropdownMenuItem> Items = [];
        
    public static UiDropdownMenu Create(BaseUiBuilder builder, in UiReference reference, in UiPosition position, in UiOffset offset, List<DropdownMenuData> items, int fontSize, UiColor textColor, UiColor backgroundColor, DropdownMenuScrollMode scrollMode, ICommandBuilder<int> pageCommand = null, int page = 0, int maxValuesPerPage = 100, in UiPadding? padding = null)
    {
        UiDropdownMenu control = CreateControl<UiDropdownMenu>(builder.PluginPool);
        
        int itemCount = Math.Min(items.Count, maxValuesPerPage);
        int maxPage = UiHelpers.CalculateMaxPage(items.Count, maxValuesPerPage);
            
        UiDirectionalLayout layout = builder.DirectionalLayout(reference, position, offset, itemCount, LayoutDirection.Vertical, padding: padding ?? new UiPadding(5, 4));
        
        if (scrollMode == DropdownMenuScrollMode.ScrollBar)
        {
            control.ScrollBarSection = builder.Section(layout.Section, UiPosition.Right, new UiOffset(-10, 5, -3, -5));
            control.ScrollBar = builder.ScrollBar(control.ScrollBarSection, UiPosition.Full, default, page, maxPage, UiColors.ButtonPrimary, UiColors.PanelSecondary, pageCommand);
            control.ScrollBar.SetSpriteMaterialImage(UiSprites.Content.Ui.UiRounded, null, Image.Type.Sliced);
        }
        
        for (int i = page * maxValuesPerPage; i < page * maxValuesPerPage + itemCount; i++)
        {
            control.Items.Add(new UiDropdownMenuItem(builder, layout, items[i], fontSize, textColor, backgroundColor));
        }

        return control;
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Items.Clear();
    }
}