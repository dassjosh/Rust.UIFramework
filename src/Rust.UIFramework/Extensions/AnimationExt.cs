using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Interfaces.Builders;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Extensions;

public static class AnimationExt
{
    public static ColorAnimation AnimateColor(this IAnimationBuilder builder, in AnimationReference reference, IAnimator<UiColor> animator, IAnimationDuration duration)
    {
        ColorAnimation animation = ColorAnimation.Create(reference, animator, duration);
        builder.AddAnimation(animation);
        return animation;
    }

    public static ColorAnimation AnimateColor(this IAnimationBuilder builder, in AnimationReference reference, UiColor startColor, UiColor endColor, float duration, float delay = 0f)
        => AnimateColor(builder, in reference, UiColorLerpAnimator.Create(startColor, endColor), DefaultDuration.Create(duration, delay));
    
    public static PositionAnimation AnimatePosition(this IAnimationBuilder builder, in UiReference reference, IAnimator<UiPosition> animator, IAnimationDuration duration)
    {
        PositionAnimation animation = PositionAnimation.Create(reference, animator, duration);
        builder.AddAnimation(animation);
        return animation;
    }
    
    public static PositionAnimation AnimatePosition(this IAnimationBuilder builder, in UiReference reference, in UiPosition startPosition, in UiPosition endPosition, float duration, float delay = 0f)
        => AnimatePosition(builder, in reference, UiPositionLerpAnimator.Create(startPosition, endPosition), DefaultDuration.Create(duration, delay));
    
    public static OffsetAnimation AnimateOffset(this IAnimationBuilder builder, in UiReference reference, IAnimator<UiOffset> animator, IAnimationDuration duration)
    {
        OffsetAnimation animation = OffsetAnimation.Create(reference, animator, duration);
        builder.AddAnimation(animation);
        return animation;
    }
    
    public static OffsetAnimation AnimateOffset(this IAnimationBuilder builder, in UiReference reference, in UiOffset startOffset, in UiOffset endOffset, float duration, float delay = 0f)
        => AnimateOffset(builder, in reference, UiOffsetLerpAnimator.Create(startOffset, endOffset), DefaultDuration.Create(duration, delay));
}