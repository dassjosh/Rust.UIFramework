using System.Collections.Generic;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine.UI;


namespace Oxide.Ext.UiFramework.Controls;

public class UiScrollBar : BaseUiControl
{
    public UiPanel Background;
    public UiPanel ScrollBar;
    public List<UiButton> ScrollButtons;

    public static UiScrollBar Create(BaseUiBuilder builder, in UiReference parent, in UiPosition position, in UiOffset offset, int currentPage, int maxPage, UiColor barColor, UiColor backgroundColor, ICommandBuilder<int> command, ScrollbarDirection direction, string sprite)
    {
        UiScrollBar control = CreateControl<UiScrollBar>();
            
        control.Background = builder.Panel(parent, position, offset, backgroundColor).SetSpriteMaterialImage(sprite, null, Image.Type.Sliced);
        UiDirectionalLayout layout = builder.DirectionalLayout(parent, position, offset, maxPage + 1, direction == ScrollbarDirection.Horizontal ? LayoutDirection.Horizontal : LayoutDirection.Vertical);
        
        for (int i = 0; i <= maxPage; i++)
        {
            if (i != currentPage)
            {
                UiButton button = builder.CommandButton(layout, backgroundColor, command.Build(i)).SetSpriteMaterialImage(sprite, null, Image.Type.Sliced);
                control.ScrollButtons.Add(button);
            }
            else
            {
                control.ScrollBar = builder.Panel(layout, barColor).SetSpriteMaterialImage(sprite, null, Image.Type.Sliced);
            }
        }

        return control;
    }

    public void SetSpriteMaterialImage(string sprite = null, string material = null, Image.Type type = Image.Type.Simple)
    {
        Background.SetSpriteMaterialImage(sprite, material, type);
        ScrollBar.SetSpriteMaterialImage(sprite, material, type);
        for (int index = 0; index < ScrollButtons.Count; index++)
        {
            UiButton button = ScrollButtons[index];
            button.SetSpriteMaterialImage(sprite, material, type);
        }
    }
        
    protected override void LeavePool()
    {
        base.LeavePool();
        ScrollButtons = UiFrameworkPool.GetList<UiButton>();
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Background = null;
        ScrollBar = null;
        UiFrameworkPool.FreeList(ScrollButtons);
    }
}