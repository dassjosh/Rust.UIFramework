using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder.UI;

public partial class UiBuilder
{
    public ColorAnimation AnimateColor(BaseUiComponent component, UiColor endColor, float duration, float delay = 0f)
    {
        if (component is IUiColor color)
        {
            return this.AnimateColor(component, color.GetColor(), endColor, duration, delay);
        }

        return null;
    }
    
    public PositionAnimation AnimatePosition(BaseUiComponent component, in UiPosition endPosition, float duration, float delay = 0f) => this.AnimatePosition(component, component.Position, endPosition, duration, delay);

    public OffsetAnimation AnimateOffset(BaseUiComponent component, in UiOffset endOffset, float duration, float delay = 0f) => this.AnimateOffset(component, component.Offset, endOffset, duration, delay);
}