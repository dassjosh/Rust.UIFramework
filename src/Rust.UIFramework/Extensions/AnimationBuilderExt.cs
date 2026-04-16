using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Extensions;

public static class AnimationBuilderExt
{
    extension(IAnimationBuilder builder)
    {
        public AnimationRef<IAnimationGroup> AnimateGroup()
        {
            AnimationGroup group = AnimationGroup.Create(builder.Plugin);
            builder.AddAnimation(group);
            return new AnimationRef<IAnimationGroup>(group);
        }

        public AnimationRef<IElementAnimation<T>> Animate<T>(T element) where T : BaseUiComponent, new()
        {
            ElementAnimation<T> animation = ElementAnimation<T>.Create(builder.Plugin, element);
            builder.AddAnimation(animation);
            return new AnimationRef<IElementAnimation<T>>(animation);
        }

        public AnimationRef<IElementAnimation<T>> Animate<T>(in UiReference reference) where T : BaseUiComponent, new() => builder.Animate<T>(reference.Name);

        public AnimationRef<IElementAnimation<T>> Animate<T>(string name) where T : BaseUiComponent, new()
        {
            ElementAnimation<T> animation = ElementAnimation<T>.Create(builder.Plugin, name);
            builder.AddAnimation(animation);
            return new AnimationRef<IElementAnimation<T>>(animation);
        }

        public AnimationRef<IElementAnimation<UiRawImage>> AnimateDownload(UiRawImage image)
        {
            string url = image.Image;
            if (url.IsValidUrl() && Singleton<UiImageStorage>.Instance.IsDownloading(url))
            {
                AnimationRef<IElementAnimation<UiRawImage>> animation = builder.Animate(image)
                    .OnQueued(a =>
                    {
                        Singleton<ImageDownloadAnimationHandler>.Instance.QueueUpdate(url, a, null);
                    });
                return animation;
            }

            return default;
        }

        public AnimationRef<IElementAnimation<UiRawImage>> AnimateDownload(UiRawImage image, ImageAnimationOptions options)
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

                AnimationRef<IElementAnimation<UiRawImage>> animation = builder.Animate(image)
                    .OnQueued(a => Singleton<ImageDownloadAnimationHandler>.Instance.QueueUpdate(url, a, options))
                    .OnTimeout(a => a.Animation.Element.Image = timeoutImage)
                    .TimeoutDelay(timeout);

                return animation;
            }

            return default;
        }
        
        public void CancelAnimation(string name)
        {
            builder.CancelAnimation(new CancelAnimationRequest(name));
        }
        
        public void CancelAnimation(in UiReference reference)
        {
            if (reference.IsValidName())
            {
                builder.CancelAnimation(reference.Name);
            }
        }
        
        public T CancelAnimation<T>(T element) where T : BaseUiComponent, new()
        {
            builder.CancelAnimation(element.Reference);
            return element;
        }
    }
}