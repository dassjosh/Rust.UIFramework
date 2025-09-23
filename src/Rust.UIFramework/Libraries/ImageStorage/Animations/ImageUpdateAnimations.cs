using System;
using System.Collections.Concurrent;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class ImageUpdateAnimations : ISingleton
{
    private readonly ConcurrentDictionary<string, ImageDownloadData> _queuedUpdates = new();
    
    private ImageUpdateAnimations() { }

    internal void QueueUpdate(string url, ImageDownloadAnimation animation)
    {
        if (!_queuedUpdates.TryGetValue(url, out ImageDownloadData updates))
        {
            _queuedUpdates[url] = updates = UiPool.Internal.Get<ImageDownloadData>();
        }
        
        updates.Add(animation);
    }

    internal void OnDownloadCompleted(string url, bool success, ImageId id)
    {
        if (!_queuedUpdates.TryRemove(url, out ImageDownloadData updates))
        {
            return;
        }

        foreach (ImageDownloadAnimation update in updates.QueuedAnimations)
        {
            if (update.State == AnimationState.Running)
            {
                if (success)
                {
                    update.OnImageDownloadedSuccessfully(id);
                }
                else
                {
                    update.OnImageDownloadFailed();
                }
            }
        }
    }

    internal void CleanupOldUpdates()
    {
        foreach ((string url, ImageDownloadData updates) in _queuedUpdates)
        {
            if (DateTime.UtcNow - updates.CreatedAt >= TimeSpan.FromMinutes(1))
            {
                _queuedUpdates.TryRemove(url, out _);
                foreach (ImageDownloadAnimation animation in updates.QueuedAnimations)
                {
                    animation.Cancel();
                }
            }
        }
    }

    private sealed class ImageDownloadData : BasePoolable
    {
        public readonly ConcurrentList<ImageDownloadAnimation> QueuedAnimations = [];
        public DateTime CreatedAt;

        public void Add(ImageDownloadAnimation animation)
        {
            QueuedAnimations.Add(animation);
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
}