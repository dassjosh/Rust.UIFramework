using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Interfaces.Builders;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Extensions;

public static class AnimationExt
{
    public static ColorAnimation AnimateColor(this IAnimationBuilder builder, in AnimationReference reference, UiColor startColor, UiColor endColor, float duration, float delay = 0f)
    {
        ColorAnimation animation = ColorAnimation.Create(reference, startColor, endColor, delay, duration);
        builder.AddAnimation(animation);
        return animation;
    }
    
    public static PositionAnimation AnimatePosition(this IAnimationBuilder builder, in UiReference reference, in UiPosition startPosition, in UiPosition endPosition, float duration, float delay = 0f)
    {
        PositionAnimation animation = PositionAnimation.Create(reference, startPosition, endPosition, delay, duration);
        builder.AddAnimation(animation);
        return animation;
    }
    
    public static OffsetAnimation AnimateOffset(this IAnimationBuilder builder, in UiReference reference, in UiOffset startOffset, in UiOffset endOffset, float duration, float delay = 0f)
    {
        OffsetAnimation animation = OffsetAnimation.Create(reference, startOffset, endOffset, delay, duration);
        builder.AddAnimation(animation);
        return animation;
    }
}