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
    
    public static IElementAnimation<T> Animate<T>(this IAnimationBuilder builder, in UiReference reference) where T : BaseUiComponent, new() => builder.Animate<T>(reference.Name);
    
    public static IElementAnimation<T> Animate<T>(this IAnimationBuilder builder, string name) where T : BaseUiComponent, new()
    {
        ElementAnimation<T> animation = ElementAnimation<T>.Create(builder.Plugin, name);
        builder.AddAnimation(animation);
        return animation;
    }
    
    public static IElementAnimation<UiRawImage> AnimateDownload(this IAnimationBuilder builder, UiRawImage image)
    {
        string url = image.Image;
        if (url.IsValidUrl() && Singleton<UiImageStorage>.Instance.IsDownloading(url))
        {
            IElementAnimation<UiRawImage> animation = builder.Animate(image)
                .OnQueued(a =>
                {
                    Singleton<ImageDownloadAnimationHandler>.Instance.QueueUpdate(url, a, null);
                });
            return animation;
        }

        return null;
    }
    
    public static IElementAnimation<UiRawImage> AnimateDownload(this IAnimationBuilder builder, UiRawImage image, ImageAnimationOptions options)
    {
        string url = image.Image;
        if (url.IsValidUrl() && Singleton<UiImageStorage>.Instance.IsDownloading(url))
        {
            if (!string.IsNullOrEmpty(options.DownloadingImageNameOrUrl))
            {
                image.Image = Singleton<UiImageStorage>.Instance.Get(builder.Plugin, options.DownloadingImageNameOrUrl);
            }

            float timeout = (float)options.Timeout.TotalSeconds;
            
            string timeoutImage = !string.IsNullOrEmpty(options.TimeoutImageNameOrUrl) ? options.TimeoutImageNameOrUrl : options.FailedImageNameOrUrl;

            IElementAnimation<UiRawImage> animation = builder.Animate(image)
                .OnQueued(a => Singleton<ImageDownloadAnimationHandler>.Instance.QueueUpdate(url, a, options))
                .OnTimeout(a => a.Element.Image = timeoutImage)
                .TimeoutDelay(timeout);

            return animation;
        }

        return null;
    }
}