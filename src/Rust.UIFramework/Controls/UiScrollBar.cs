using System.Collections.Generic;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Controls
{
    public class UiScrollBar : BaseUiControl
    {
        public UiPanel Background;
        public UiPanel ScrollBar;
        public List<UiButton> ScrollButtons;

        public static UiScrollBar Create(BaseUiBuilder builder, in UiReference parent, in UiPosition position, in UiOffset offset, int currentPage, int maxPage, UiColor barColor, UiColor backgroundColor, string command, ScrollbarDirection direction, string sprite)
        {
            UiScrollBar control = CreateControl<UiScrollBar>();
            
            control.Background = builder.Panel(parent, position, offset, backgroundColor);
            control.Background.SetSpriteMaterialImage(sprite, null, Image.Type.Sliced);
            float buttonSize = 1f / (maxPage + 1);
            for (int i = 0; i <= maxPage; i++)
            {
                float min = buttonSize * i;
                float max = buttonSize * (i + 1);
                UiPosition pagePosition = direction == ScrollbarDirection.Horizontal ? UiPosition.Full.SliceHorizontal(min, max) : new UiPosition(0, 1 - max, 1, 1 - min);
                
                if (i != currentPage)
                {
                    UiButton button = builder.CommandButton(control.Background, pagePosition, backgroundColor, $"{command} {StringCache<int>.ToString(i)}");
                    button.SetSpriteMaterialImage(sprite, null, Image.Type.Sliced);
                    control.ScrollButtons.Add(button);
                }
                else
                {
                    control.ScrollBar = builder.Panel(control.Background, pagePosition, barColor);
                    control.ScrollBar.SetSpriteMaterialImage(sprite, null, Image.Type.Sliced);
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
}