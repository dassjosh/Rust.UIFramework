using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
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

        public static UiBorder Create(BaseUiBuilder builder, UiReference parent, UiColor color, int width = 1, BorderMode border = BorderMode.All)
        {
            UiBorder control = CreateControl<UiBorder>();
            //If width is 0 nothing is displayed so don't try to render
            if (width == 0)
            {
                return control;
            }
            
            bool top = HasBorderFlag(border, BorderMode.Top);
            bool left = HasBorderFlag(border, BorderMode.Left);
            bool bottom = HasBorderFlag(border, BorderMode.Bottom);
            bool right = HasBorderFlag(border, BorderMode.Right);

            if (width > 0)
            {
                int tbMin = left ? -width : 0;
                int tbMax = right ? width : 0;
                int lrMin = top ? -width : 0;
                int lrMax = bottom ? width : 0;
            
                if (top)
                {
                    control.Top = builder.Panel(parent, UiPosition.Top, new UiOffset(tbMin, 0, tbMax, width), color);
                }
            
                if (left)
                {
                    control.Left = builder.Panel(parent, UiPosition.Left, new UiOffset(-width, lrMin, 0, lrMax), color);
                }
            
                if (bottom)
                {
                    control.Bottom = builder.Panel(parent, UiPosition.Bottom, new UiOffset(tbMin, -width, tbMax, 0), color);
                }
            
                if (right)
                {
                    control.Right = builder.Panel(parent, UiPosition.Right, new UiOffset(0, lrMin, width, lrMax), color);
                }
            }
            else
            {
                int tbMin = left ? width : 0;
                int tbMax = right ? -width : 0;
                int lrMin = top ? width : 0;
                int lrMax = bottom ? -width : 0;
                
                if (top)
                {
                    control.Top = builder.Panel(parent, UiPosition.Top, new UiOffset(tbMin, width, tbMax, 0), color);
                }
            
                if (left)
                {
                    control.Left = builder.Panel(parent, UiPosition.Left, new UiOffset(0, lrMin, -width, lrMax), color);
                }
            
                if (bottom)
                {
                    control.Bottom = builder.Panel(parent, UiPosition.Bottom, new UiOffset(tbMin, 0, tbMax, -width), color);
                }
            
                if (right)
                {
                    control.Right = builder.Panel(parent, UiPosition.Right, new UiOffset(width, lrMin, 0, lrMax), color);
                }
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