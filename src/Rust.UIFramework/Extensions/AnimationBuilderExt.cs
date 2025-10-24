using System;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Extensions;

public static class AnimationBuilderExt
{
    public static IAnimationGroup AnimateGroup(this IAnimationBuilder builder)
    {
        AnimationGroup group = AnimationGroup.Create(builder.Plugin);
        builder.AddAnimation(group);
        return group;
    }
    
    public static IElementAnimation<T> Animate<T>(this IAnimationBuilder builder, T element) where T : BaseUiComponent, new()
    {
        ElementAnimation<T> animation = ElementAnimation<T>.Create(builder.Plugin, element);
        builder.AddAnimation(animation);
        return animation;
    }
    
    public static IElementAnimation<T> Animate<T>(this IAnimationBuilder builder, string name) where T : BaseUiComponent, new()
    {
        ElementAnimation<T> animation = ElementAnimation<T>.Create(builder.Plugin, name);
        builder.AddAnimation(animation);
        return animation;
    }
    
    public static IElementAnimation<UiRawImage> AnimateDownload(this IAnimationBuilder builder, UiRawImage image)
    {
        if (image.Image.StartsWith("http", StringComparison.OrdinalIgnoreCase) && Singleton<UiImageStorage>.Instance.IsDownloading(image.Image))
        {
            IElementAnimation<UiRawImage> animation = builder.Animate(image);
            Singleton<ImageUpdateAnimations>.Instance.QueueUpdate(image.Image, animation, null);
            return animation;
        }

        return null;
    }
    
    public static IElementAnimation<UiRawImage> AnimateDownload(this IAnimationBuilder builder, UiRawImage image, ImageAnimationOptions options)
    {
        if (image.Image.StartsWith("http", StringComparison.OrdinalIgnoreCase) && Singleton<UiImageStorage>.Instance.IsDownloading(image.Image))
        {
            if (!string.IsNullOrEmpty(options.DownloadingImageNameOrUrl))
            {
                image.Image = Singleton<UiImageStorage>.Instance.Get(builder.Plugin, options.DownloadingImageNameOrUrl);
            }

            float timeout = (float)options.Timeout.TotalSeconds;
            
            string timeoutImage = !string.IsNullOrEmpty(options.TimeoutImageNameOrUrl) ? options.TimeoutImageNameOrUrl : options.FailedImageNameOrUrl;

            IElementAnimation<UiRawImage> animation = builder.Animate(image)
                .Timeout(timeout)
                .Delay(timeout)
                .OnTimeout(a => a.Element.Image = timeoutImage);
            
            Singleton<ImageUpdateAnimations>.Instance.QueueUpdate(image.Image, animation, options);

            return animation;
        }

        return null;
    }
}