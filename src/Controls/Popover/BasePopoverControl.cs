using System;
using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Controls.Popover
{
    public abstract class BasePopoverControl : BaseUiControl
    {
        public UiBuilder Builder;
        public UiButton OutsideClose;
        public UiPanel PopoverBackground;

        public static BasePopoverControl CreateBuilder(BasePopoverControl control, string parentName, Vector2Int size, UiColor backgroundColor, PopoverPosition position = PopoverPosition.Bottom, string menuSprite = UiConstants.Sprites.RoundedBackground2)
        {
            string name = $"{parentName}_Popover";
            
            UiBuilder builder = UiBuilder.Create(UiSection.Create(UiPosition.Full, default(UiOffset)), name, parentName);
            
            UiPosition anchor = GetPopoverPosition(position);
            UiOffset offset = GetPopoverOffset(position, size);

            control.Builder = builder;
            control.OutsideClose = builder.CloseButton(builder.Root, UiPosition.Full, new UiOffset(-1000, -1000, 1000, 1000), UiColor.Clear, name);
            control.PopoverBackground = builder.Panel(builder.Root, anchor, offset, backgroundColor);
            control.PopoverBackground.SetSpriteMaterialImage(menuSprite, null, Image.Type.Sliced);
            control.PopoverBackground.AddElementOutline(UiColor.Black.WithAlpha(0.75f));
            builder.OverrideRoot(control.PopoverBackground);

            return control;
        }

        public static UiPosition GetPopoverPosition(PopoverPosition position)
        {
            switch (position)
            {
                case PopoverPosition.Top:
                case PopoverPosition.Left:
                    return UiPosition.TopLeft;
                
                case PopoverPosition.Right:
                    return UiPosition.TopRight;
                
                case PopoverPosition.Bottom:
                    return UiPosition.BottomLeft;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(position), position, null);
            }
        }

        public static UiOffset GetPopoverOffset(PopoverPosition position, Vector2Int size)
        {
            switch (position)
            {
                case PopoverPosition.Top:
                    return new UiOffset(0, 1, 1 + size.x, size.y);

                case PopoverPosition.Left:
                    return new UiOffset(-size.x, -size.y - 1, 0, -1);

                case PopoverPosition.Right:
                    return new UiOffset(0, -size.y - 1, size.x, -1);

                case PopoverPosition.Bottom:
                    return new UiOffset(1, -size.y, 1 + size.x, 0);

                default:
                    throw new ArgumentOutOfRangeException(nameof(position), position, null);
            }
        }
        
        protected override void EnterPool()
        {
            Builder = null;
            OutsideClose = null;
            PopoverBackground = null;
        }
    }
}