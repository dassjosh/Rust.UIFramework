using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Network;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public class AnimationHandler : ISingleton
{
    private readonly ConcurrentDictionary<AnimationId, BaseAnimation> _animations = new();
    private readonly Thread _thread;
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly AutoResetEvent _reset = new(false);
    private int _sendCount;
    private readonly IUiLogger _logger = UiFrameworkExtension.GlobalLogger;

    private AnimationHandler()
    {
        _thread = new Thread(AnimationLoop)
        {
            IsBackground = true
        };
        _thread.Start(_cancellationTokenSource.Token);
    }

    public BaseAnimation GetAnimation(AnimationId id) => _animations.GetValueOrDefault(id);
    
    public void EnqueueAnimation(BaseAnimation animation, SendInfo send)
    {
        if (animation == null) throw new ArgumentNullException(nameof(animation));
        _animations[animation.Id] = animation;
        animation.OnQueued(send);
        _reset.Set();
        _logger.Debug("Adding animation {0}", animation.Id);
    }
    
    public void RemoveAnimation(AnimationId id)
    {
        if (id.IsValid && _animations.TryRemove(id, out BaseAnimation animation))
        {
            _logger.Debug("Removing animation {0}", id);
            animation.OnRemoved();
        }
    }
    
    private void AnimationLoop(object tokenObj)
    {
        CancellationToken token = (CancellationToken)tokenObj;
        
        while (!token.IsCancellationRequested)
        {
            try
            {
                _logger.Debug("Processing {0} animations", _animations.Count);
                ProcessAnimations();
                _logger.Debug("Processed animations. {0} remaining", _animations.Count);
                
                if (_animations.Count == 0)
                {
                    _sendCount++;
                    int timeout = GetSleepDuration();
                    _reset.WaitOne(timeout);
                }
                else
                {
                    _sendCount = 0;
                    Thread.Sleep(25);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception("An error occured", ex);
            }
        }
    }
    
    private void ProcessAnimations()
    {
        // Get the current time
        float currentTime = Time.realtimeSinceStartup;
        
        // Create a copy of animation keys to prevent collection modification errors
        foreach ((AnimationId id, BaseAnimation animation) in _animations)
        {
            _logger.Debug("Processing Animation {0}", id);

            if (animation.Cancelled)
            {
                RemoveAnimation(id);
                continue;
            }
            
            animation.OnTick(currentTime);
                
            // Check if we need to wait for the delay
            if (animation.Delay > 0 && animation.Elapsed < animation.Delay)
            {
                continue;
            }
            
            float effectiveElapsed = animation.Elapsed - animation.Delay;
            if (effectiveElapsed >= 0 && effectiveElapsed <= animation.Duration)
            {
                animation.SendAnimation(animation.ElapsedPercentage);
                continue;
            }

            if (effectiveElapsed > animation.Duration)
            {
                animation.SendAnimation(1f);
                if (animation.OnAnimationEnded(currentTime))
                {
                    RemoveAnimation(id);
                }
            }
        }
    }

    private int GetSleepDuration()
    {
        if (_sendCount > 10)
        {
            return -1;
        }
        
        return 25 + 1 << _sendCount;
    }
    
    internal void OnServerShutdown() => _cancellationTokenSource.Cancel();
}