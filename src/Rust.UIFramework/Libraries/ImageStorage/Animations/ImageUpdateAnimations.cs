using System;
using System.Collections.Concurrent;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Libraries;

internal class ImageUpdateAnimations : ISingleton
{
    private readonly ConcurrentDictionary<string, ImageDownloadAnimations> _queuedUpdates = new();
    
    private ImageUpdateAnimations() { }

    internal void QueueUpdate(string url, IElementAnimation<UiRawImage> animation, ImageAnimationOptions options)
    {
        CancelPreviousUpdates(animation.SinglePlayerId(), animation.Element.Reference);
        if (!_queuedUpdates.TryGetValue(url, out ImageDownloadAnimations updates))
        {
            _queuedUpdates[url] = updates = UiPool.Internal.Get<ImageDownloadAnimations>();
        }
        
        updates.Add(animation, options);
    }

    private void CancelPreviousUpdates(ulong playerId, in UiReference reference)
    {
        foreach (ImageDownloadAnimations animation in _queuedUpdates.Values)
        {
            foreach (ImageAnimationData data in animation.QueuedAnimations)
            {
                if (data.Animation.Id.IsValid && data.Animation.SinglePlayerId() == playerId && data.Animation.Element.Reference.Name == reference.Name)
                {
                    data.Cancel();
                    animation.QueuedAnimations.Remove(data);
                }
            }
        }
    }

    internal void OnDownloadCompleted(string url, bool success, ImageId id)
    {
        if (!_queuedUpdates.TryRemove(url, out ImageDownloadAnimations updates))
        {
            return;
        }

        foreach (ImageAnimationData data in updates.QueuedAnimations)
        {
            if (success)
            {
                data.OnImageDownloadedSuccessfully(id);
            }
            else
            {
                data.OnImageDownloadFailed();
            }
        }
    }

    internal void CleanupOldUpdates()
    {
        foreach ((string url, ImageDownloadAnimations updates) in _queuedUpdates)
        {
            if (DateTime.UtcNow - updates.CreatedAt >= TimeSpan.FromMinutes(1))
            {
                _queuedUpdates.TryRemove(url, out _);
                foreach (ImageAnimationData data in updates.QueuedAnimations)
                {
                    data.Cancel();
                }
            }
        }
    }

    private sealed class ImageDownloadAnimations : BasePoolable
    {
        public readonly ConcurrentList<ImageAnimationData> QueuedAnimations = [];
        public DateTime CreatedAt;

        public void Add(IElementAnimation<UiRawImage> animation, ImageAnimationOptions options)
        {
            QueuedAnimations.Add(new ImageAnimationData(animation, options));
        }

        protected override void LeavePool()
        {
            CreatedAt = DateTime.UtcNow;
        }

        protected override void EnterPool()
        {
            QueuedAnimations.Clear();
        }
    }

    private readonly record struct ImageAnimationData(IElementAnimation<UiRawImage> Animation, ImageAnimationOptions Options)
    {
        public readonly AnimationId Id = Animation.Id;
        public bool IsAnimationValid => Animation.Id.IsValid && Id.IsValid && Animation.Id == Id;

        public void OnImageDownloadedSuccessfully(ImageId id)
        {
            if (IsAnimationValid)
            {
                Animation.Element.Image = id.ToString();
                Animation.CompleteAnimation();
            }
        }

        public void OnImageDownloadFailed()
        {
            if (IsAnimationValid)
            {
                Animation.Element.Image = Singleton<UiImageStorage>.Instance.Get(Animation.Plugin, Options.FailedImageNameOrUrl);
                Animation.CompleteAnimation();
            }
        }

        public void Cancel()
        {
            if (IsAnimationValid)
            {
                Animation.CancelAnimation();
            }
        }
    }
}