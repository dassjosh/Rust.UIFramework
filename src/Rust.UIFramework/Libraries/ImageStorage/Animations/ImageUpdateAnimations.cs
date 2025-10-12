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

    internal void QueueUpdate(string url, ImageDownloadAnimation animation)
    {
        CancelPreviousUpdates(animation.PlayerId, animation.Reference);
        if (!_queuedUpdates.TryGetValue(url, out ImageDownloadAnimations updates))
        {
            _queuedUpdates[url] = updates = UiPool.Internal.Get<ImageDownloadAnimations>();
        }
        
        updates.Add(animation);
    }

    private void CancelPreviousUpdates(ulong playerId, in UiReference reference)
    {
        foreach (ImageDownloadAnimations animation in _queuedUpdates.Values)
        {
            foreach (ImageAnimationData data in animation.QueuedAnimations)
            {
                if (data.Animation.PlayerId == playerId && data.Animation.Reference == reference)
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

        public void Add(ImageDownloadAnimation animation)
        {
            QueuedAnimations.Add(new ImageAnimationData(animation));
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

    private readonly record struct ImageAnimationData(ImageDownloadAnimation Animation)
    {
        public readonly ImageDownloadAnimation Animation = Animation;
        public readonly AnimationId Id = Animation.Id;
        public bool IsAnimationValid => Animation.Id.IsValid && Id.IsValid && Animation.Id == Id && Animation.IsActive;

        public void OnImageDownloadedSuccessfully(ImageId id)
        {
            if (IsAnimationValid)
            {
                Animation.OnImageDownloadedSuccessfully(id);
            }
        }

        public void OnImageDownloadFailed()
        {
            if (IsAnimationValid)
            {
                Animation.OnImageDownloadFailed();
            }
        }

        public void Cancel()
        {
            if (IsAnimationValid)
            {
                Animation.Cancel();
            }
        }
    }
}