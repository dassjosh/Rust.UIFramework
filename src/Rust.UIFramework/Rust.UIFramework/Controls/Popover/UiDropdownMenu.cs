using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Controls.Data;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Controls.Popover
{
    public class UiDropdownMenu : BasePopoverControl
    {
        public UiSection ScrollBarSection;
        public UiScrollBar ScrollBar;
        public List<UiDropdownMenuItem> Items;
        
        public static UiDropdownMenu Create(UiReference reference, List<DropdownMenuData> items, int fontSize, UiColor textColor, UiColor backgroundColor, string selectedCommand, string pageCommand = null, int page = 0, int maxValuesPerPage = 100, int minWidth = 100,
            PopoverPosition position = PopoverPosition.Bottom, string menuSprite = UiConstants.Sprites.RoundedBackground2)
        {
            const int itemPadding = 4;
            const int menuPadding = 5;
            
            UiDropdownMenu control = CreateControl<UiDropdownMenu>();

            int itemCount = Math.Min(items.Count, maxValuesPerPage);
            int width = Math.Max(minWidth, control.GetWidth(items, fontSize));
            int itemHeight = UiHelpers.TextOffsetHeight(fontSize);
            int height = itemCount * (itemHeight + itemPadding) + menuPadding * 2;
            int maxPage = UiHelpers.CalculateMaxPage(items.Count, maxValuesPerPage);
            
            Vector2Int size = new Vector2Int(width, height);
            CreateBuilder(control, reference.Parent, size, backgroundColor, position, menuSprite);
            
            UiBuilder builder = control.Builder;
            
            UiOffset buttonPos = new UiOffset(menuPadding, -itemHeight - menuPadding, width + menuPadding, -menuPadding);
            if (maxPage > 0)
            {
                buttonPos = buttonPos.SliceHorizontal(0, 10);
                control.ScrollBarSection = builder.Section(builder.Root, UiPosition.Right, new UiOffset(-10, 5, -3, -5));
                control.ScrollBar = builder.ScrollBar(control.ScrollBarSection, UiPosition.Full, default(UiOffset), page, maxPage, UiColors.ButtonPrimary, UiColors.PanelSecondary, pageCommand);
                control.ScrollBar.SetSpriteMaterialImage(UiConstants.Sprites.RoundedBackground1, null, Image.Type.Sliced);
            }
            
            for (int i = page * maxValuesPerPage; i < page * maxValuesPerPage + itemCount; i++)
            {
                control.Items.Add(UiDropdownMenuItem.Create(builder, buttonPos, items[i], fontSize, textColor, backgroundColor, selectedCommand));
                buttonPos = buttonPos.MoveY(-itemHeight - itemPadding);
            }

            return control;
        }

        public virtual int GetWidth(List<DropdownMenuData> items, int fontSize)
        {
            int width = 0;
            int count = items.Count;
            for (int i = 0; i < count; i++)
            {
                DropdownMenuData item = items[i];
                int valueWidth = UiHelpers.TextOffsetWidth(item.DisplayName.Length, fontSize);
                if (valueWidth > width)
                {
                    width = valueWidth;
                }
            }

            return width;
        }
    }
}