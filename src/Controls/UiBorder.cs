using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Controls.Data;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Controls
{
    public class UiBorder : BaseUiControl
    {
        public UiPanel Left;
        public UiPanel Right;
        public UiPanel Top;
        public UiPanel Bottom;

        public static UiBorder Create(BaseUiBuilder builder, UiReference parent, UiColor color, UiBorderWidth width, BorderMode border = BorderMode.All)
        {
            UiBorder control = CreateControl<UiBorder>();
            if (width.IsEmpty())
            {
                return control;
            }
            
            bool top = HasBorderFlag(border, BorderMode.Top);
            bool left = HasBorderFlag(border, BorderMode.Left);
            bool bottom = HasBorderFlag(border, BorderMode.Bottom);
            bool right = HasBorderFlag(border, BorderMode.Right);

            if (top)
            {
                control.Top = builder.Panel(parent, UiPosition.Top, new UiOffset(left ? -width.Left : 0, 0, right ? width.Right : 0, width.Top), color);
            }
            
            if (left)
            {
                control.Left = builder.Panel(parent, UiPosition.Left, new UiOffset(-width.Left, bottom ? -width.Bottom : 0, 0, top ? width.Top : 0), color);
            }
            
            if (bottom)
            {
                control.Bottom = builder.Panel(parent, UiPosition.Bottom, new UiOffset(left ? -width.Left : 0, -width.Bottom, right ? width.Right : 0, 0), color);
            }
            
            if (right)
            {
                control.Right = builder.Panel(parent, UiPosition.Right, new UiOffset(0, bottom ? -width.Bottom : 0, width.Right, top ? width.Top : 0), color);
            }

            return control;
        }
        
        private static bool HasBorderFlag(BorderMode mode, BorderMode flag)
        {
            return (mode & flag) != 0;
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            Left = null;
            Top = null;
            Right = null;
            Bottom = null;
        }
    }
}