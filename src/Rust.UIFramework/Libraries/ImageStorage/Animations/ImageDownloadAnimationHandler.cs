using System;
using System.Collections.Concurrent;
using Network;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Libraries;

internal class ImageDownloadAnimationHandler : ISingleton
{
    private readonly ConcurrentDictionary<string, ImageDownloadAnimations> _queuedUpdates = new();
    
    private ImageDownloadAnimationHandler() { }

    internal void QueueUpdate(string url, in AnimationRef<IElementAnimation<UiRawImage>> animation, ImageAnimationOptions options)
    {
        UiFrameworkExtension.GlobalLogger.Debug($"{nameof(QueueUpdate)}: Url: {{0}} Animation: {{1}}", url, animation.Id);
        if (animation.Animation.TryGetSinglePlayer(out ulong playerId))
        {
            CancelPreviousUpdates(playerId, animation.Animation.Element.Reference);
        }
        else
        {
            foreach (Connection connection in animation.Animation.Send.connections)
            {
                CancelPreviousUpdates(connection.userid, animation.Animation.Element.Reference);
            }
        }
       
        if (!_queuedUpdates.TryGetValue(url, out ImageDownloadAnimations updates))
        {
            _queuedUpdates[url] = updates = UiPool.Internal.Get<ImageDownloadAnimations>().Init(url);
        }
        
        updates.Add(animation, options);
    }

    private void CancelPreviousUpdates(ulong playerId, in UiReference reference)
    {
        foreach (ImageDownloadAnimations animation in _queuedUpdates.Values)
        {
            foreach (ImageAnimationData data in animation.QueuedAnimations)
            {
                if (!data.Animation.IsValid)
                {
                    animation.QueuedAnimations.Remove(data);
                    continue;
                }
                
                if (data.Animation.Animation.TryGetSinglePlayer(out ulong animationPlayerId) && animationPlayerId == playerId && data.Animation.Animation.Element.Reference.Name == reference.Name)
                {
                    UiFrameworkExtension.GlobalLogger.Debug($"{nameof(CancelPreviousUpdates)}: Cancel Download Animation Url: {{0}} Animation: {{1}} Player: {{2}}", animation.Url, data.Animation.Id, playerId);
                    data.Cancel();
                    animation.QueuedAnimations.Remove(data);
                }
            }
        }
    }

    internal void OnDownloadCompleted(string url, bool success, ImageId id)
    {
        UiFrameworkExtension.GlobalLogger.Debug($"{nameof(OnDownloadCompleted)}: Url: {{0}} Success: {{1}} ID: {{2}}", url, success, id.Id);
        
        if (!_queuedUpdates.TryRemove(url, out ImageDownloadAnimations updates))
        {
            UiFrameworkExtension.GlobalLogger.Debug($"{nameof(OnDownloadCompleted)}. No Animation for Url: {{0}}", url);
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
        public string Url;

        internal ImageDownloadAnimations Init(string url)
        {
            Url = url;
            return this;
        }

        public void Add(in AnimationRef<IElementAnimation<UiRawImage>> animation, ImageAnimationOptions options)
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
            Url = null;
        }
    }

    private readonly record struct ImageAnimationData(AnimationRef<IElementAnimation<UiRawImage>> Animation, ImageAnimationOptions Options)
    {
        public void OnImageDownloadedSuccessfully(ImageId id)
        {
            UiFrameworkExtension.GlobalLogger.Debug("Image downloaded successfully: {0} Animation: {1} IsValid: {2}", id.Id, Animation.Id, Animation.IsValid);
            if (Animation.IsValid)
            {
                Animation.Animation.Element.Image = id.ToString();
                Animation.Animation.CompleteAnimation();
            }
        }

        public void OnImageDownloadFailed()
        {
            UiFrameworkExtension.GlobalLogger.Debug("Image downloaded failed. Animation: {0} IsValid: {1}", Animation.Id, Animation.IsValid);
            if (Animation.IsValid)
            {
                if (Options != null)
                {
                    Animation.Animation.Element.Image = Singleton<UiImageStorage>.Instance.Get(Animation.Plugin, Options.FailedImage);
                }
                
                Animation.Animation.CompleteAnimation();
            }
        }

        public void Cancel()
        {
            UiFrameworkExtension.GlobalLogger.Debug("Image downloaded animation cancelled Animation: {0} IsValid: {1}", Animation.Id, Animation.IsValid);
            Animation.CancelAnimation();
        }
    }
}