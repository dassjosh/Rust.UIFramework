using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Controls
{
    public class UiProgressBar : BaseUiControl
    {
        public UiPanel BackgroundPanel;
        public UiPanel BarPanel;

        public static UiProgressBar Create(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, float percentage, UiColor barColor, UiColor backgroundColor)
        {
            UiProgressBar control = CreateControl<UiProgressBar>();
            control.BackgroundPanel = builder.Panel(parent, pos, offset, backgroundColor);
            control.BarPanel = builder.Panel(control.BackgroundPanel, UiPosition.Full.SliceHorizontal(0, percentage), barColor);
            return control;
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            BackgroundPanel = null;
            BarPanel = null;
        }
    }
}