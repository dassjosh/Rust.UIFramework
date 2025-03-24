using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Colors;
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
            ColorAnimation animation = ColorAnimation.Create(color.GetColor(), endColor, component, delay, duration);
            AddAnimation(animation);
            return animation;
        }

        return null;
    }
    
    public PositionAnimation AnimatePosition(BaseUiComponent component, in UiPosition endPosition, float duration, float delay = 0f)
    {
        PositionAnimation animation = PositionAnimation.Create(component.Position, endPosition, component, delay, duration);
        AddAnimation(animation);
        return animation;
    }
    
    public OffsetAnimation AnimateOffset(BaseUiComponent component, in UiOffset endOffset, float duration, float delay = 0f)
    {
        OffsetAnimation animation = OffsetAnimation.Create(component.Offset, endOffset, component, delay, duration);
        AddAnimation(animation);
        return animation;
    }
}