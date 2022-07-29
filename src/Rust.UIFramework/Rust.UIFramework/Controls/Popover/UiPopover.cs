using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Pooling;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Controls.Popover
{
    public class UiPopover : BasePopoverControl
    {
        public static UiPopover Create(string parentName, Vector2Int size, UiColor backgroundColor, PopoverPosition position = PopoverPosition.Bottom, string menuSprite = UiConstants.Sprites.RoundedBackground2)
        {
            UiPopover control = CreateControl<UiPopover>();
            CreateBuilder(control, parentName, size, backgroundColor, position, menuSprite);
            return control;
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
}