using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Extensions;

public static class AnimationExt
{
    public static ColorAnimation AnimateColor(this IAnimationBuilder builder, in AnimationReference reference, ISimpleAnimator<UiColor> animator, IAnimationDuration duration)
    {
        UiReferenceException.ThrowIfInvalidReference(reference);
        ColorAnimation animation = ColorAnimation.Create(builder, reference, animator, duration);
        builder.AddAnimation(animation);
        return animation;
    }

    public static ColorAnimation AnimateColor(this IAnimationBuilder builder, in AnimationReference reference, UiColor startColor, UiColor endColor, float duration, float delay = 0f)
        => AnimateColor(builder, in reference, UiColorLerpAnimator.Create(builder.PluginPool, startColor, endColor), builder.DefaultDuration(duration, delay));
    
    public static PositionAnimation AnimatePosition(this IAnimationBuilder builder, in UiReference reference, ISimpleAnimator<UiPosition> animator, IAnimationDuration duration)
    {
        UiReferenceException.ThrowIfInvalidReference(reference);
        PositionAnimation animation = PositionAnimation.Create(builder, reference, animator, duration);
        builder.AddAnimation(animation);
        return animation;
    }
    
    public static PositionAnimation AnimatePosition(this IAnimationBuilder builder, in UiReference reference, in UiPosition startPosition, in UiPosition endPosition, float duration, float delay = 0f)
        => AnimatePosition(builder, in reference, UiPositionLerpAnimator.Create(builder.PluginPool, startPosition, endPosition), builder.DefaultDuration(duration, delay));
    
    public static OffsetAnimation AnimateOffset(this IAnimationBuilder builder, in UiReference reference, ISimpleAnimator<UiOffset> animator, IAnimationDuration duration)
    {
        UiReferenceException.ThrowIfInvalidReference(reference);
        OffsetAnimation animation = OffsetAnimation.Create(builder, reference, animator, duration);
        builder.AddAnimation(animation);
        return animation;
    }
    
    public static OffsetAnimation AnimateOffset(this IAnimationBuilder builder, in UiReference reference, in UiOffset startOffset, in UiOffset endOffset, float duration, float delay = 0f)
        => AnimateOffset(builder, in reference, UiOffsetLerpAnimator.Create(builder.PluginPool, startOffset, endOffset), builder.DefaultDuration(duration, delay));
    
    public static RotationAnimation AnimateRotation(this IAnimationBuilder builder, in UiReference reference, ISimpleAnimator<UiRotation> animator, IAnimationDuration duration)
    {
        UiReferenceException.ThrowIfInvalidReference(reference);
        RotationAnimation animation = RotationAnimation.Create(builder, reference, animator, duration);
        builder.AddAnimation(animation);
        return animation;
    }
    
    public static RotationAnimation AnimateRotation(this IAnimationBuilder builder, in UiReference reference, in UiRotation start, in UiRotation end, float duration, float delay = 0f)
        => AnimateRotation(builder, in reference, UiRotationLerpAnimator.Create(builder.PluginPool, start, end), builder.DefaultDuration(duration, delay));
    
    public static TextAnimation AnimateText(this IAnimationBuilder builder, in AnimationReference reference, ISimpleAnimator<string> animator, IAnimationDuration duration, TextFormatter formatter = null)
    {
        UiReferenceException.ThrowIfInvalidReference(reference);
        TextAnimation animation = TextAnimation.Create(builder, reference, animator, duration, formatter);
        builder.AddAnimation(animation);
        return animation;
    }
    
    public static TextAnimation AnimateText(this IAnimationBuilder builder, in AnimationReference reference, string start, string end, float duration, float delay = 0f, TextFormatter formatter = null)
        => AnimateText(builder, in reference, StringLerpAnimator.Create(builder.PluginPool, start, end), builder.DefaultDuration(duration, delay), formatter);

    internal static ImageDownloadAnimation AnimateImageDownload(this IAnimationBuilder builder, in UiReference reference, string url, ImageDownloadOptions options)
    {
        UiReferenceException.ThrowIfInvalidReference(reference);
        
        ImageDownloadAnimation animation = ImageDownloadAnimation.Create(builder, reference, options);
        builder.AddAnimation(animation);
        Singleton<ImageUpdateAnimations>.Instance.QueueUpdate(url, animation);
        return animation;
    }
    
    private static DefaultDuration DefaultDuration(this IAnimationBuilder builder, float duration, float delay = 0f) => Animation.DefaultDuration.Create(builder.PluginPool, duration, delay);
}