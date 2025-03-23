using System;
using System.Collections.Concurrent;
using System.Threading;
using Network;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;

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
        _logger.Debug("Initialized");
    }

    public void EnqueueAnimation(BaseAnimation animation, SendInfo send)
    {
        if (animation == null) throw new ArgumentNullException(nameof(animation));
        _animations[animation.Id] = animation;
        animation.WasQueued = true;
        animation.Send = send;
        _reset.Set();
        _logger.Debug("Adding animation {0}", animation.Id);
    }
    
    public void RemoveAnimation(AnimationId id)
    {
        if (id.IsValid)
        {
            _animations.TryRemove(id, out _);
            _logger.Debug("Removing animation {0}", id);
        }
    }
    
    public void ClearAllAnimations()
    {
        _animations.Clear();
    }
    
    private void AnimationLoop(object tokenObj)
    {
        CancellationToken token = (CancellationToken)tokenObj;
        
        while (!token.IsCancellationRequested)
        {
            try
            {
                _logger.Debug("Processing {0} animations", _animations.Count);
                ProcessAnimations(token);
                _logger.Debug("Processed animations. {0} remaining", _animations.Count);
                
                if (_animations.Count == 0)
                {
                    _sendCount++;
                    int timeout = GetSleepDuration();
                    _logger.Debug("Waiting for reset with timeout {0}ms", timeout);
                    _reset.WaitOne(timeout);
                }
                else
                {
                    _sendCount = 0;
                    _logger.Debug("Sleeping for 25ms");
                    Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception("An error occured", ex);
            }
        }
    }
    
    private void ProcessAnimations(CancellationToken token)
    {
        // Get the current time
        DateTime currentTime = DateTime.UtcNow;
        
        // Create a copy of animation keys to prevent collection modification errors
        foreach ((AnimationId id, BaseAnimation animation) in _animations)
        {
            if (token.IsCancellationRequested)
            {
                break;
            }
            
            _logger.Debug("Processing Animation {0}", id);
                
            // Initialize start time if not already set
            if (animation.StartTime == default)
            {
                animation.StartTime = currentTime;
            }
                
            // Calculate elapsed time
            TimeSpan elapsed = currentTime - animation.StartTime;
            animation.Elapsed = (float)elapsed.TotalSeconds;
                
            // Check if we need to wait for the delay
            if (animation.Delay > 0 && animation.Elapsed < animation.Delay)
            {
                _logger.Debug("Animation {0} is on delay for {1} more seconds", id, animation.Delay - animation.Elapsed);
                continue;
            }
                
            // Check if the animation has reached its duration
            float effectiveElapsed = animation.Elapsed - animation.Delay;
            if (effectiveElapsed >= 0 && (animation.Duration <= 0 || effectiveElapsed <= animation.Duration))
            {
                animation.SendAnimation(animation.ElapsedPercentage);
                _logger.Debug("Sending Animation {0} {1}%", id, animation.ElapsedPercentage);
            }
            else if (animation.Duration > 0 && effectiveElapsed > animation.Duration)
            {
                // Animation duration is complete
                if (animation.Repeats > 1)
                {
                    // Decrement repeats count
                    animation.Repeats--;
                        
                    // Reset start time accounting for repeat delay
                    animation.StartTime = currentTime.AddSeconds(animation.RepeatDelay);
                    _logger.Debug("Animation {0} Will repeat {1} more times", id, animation.Repeats);
                }
                else
                {
                    animation.SendAnimation(1f);
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
}