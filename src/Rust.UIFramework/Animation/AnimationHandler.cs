using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Network;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

internal class AnimationHandler : ISingleton
{
    private readonly ConcurrentDictionary<AnimationId, BaseAnimation> _animations = new();
    private readonly ConcurrentDictionary<ulong, PlayerAnimations> _playerAnimations = new();
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
        
        animation.OnQueued(send);
        
        if (!UiFrameworkConfig.Instance.Animations.Enabled)
        {
            JsonFrameworkWriter writer = Create();
            animation.WriteCompletedComponent(writer);
            SendAnimations(writer, animation.Send);
            animation.OnRemoved();
            return;
        }
        
        _animations[animation.Id] = animation;
        if (animation.IsSinglePlayer)
        {
            AddSinglePlayerAnimation(animation);
        }
        _reset.Set();
        _logger.Debug("Adding animation {0}", animation.Id);
    }

    public void AddSinglePlayerAnimation(BaseAnimation animation)
    {
        if (!_playerAnimations.TryGetValue(animation.PlayerId, out PlayerAnimations animations))
        {
            _playerAnimations[animation.PlayerId] = animations = PlayerAnimations.Create(animation.Send);
        }

        animations.AddAnimation(animation);
    }
    
    public void RemoveAnimation(AnimationId id)
    {
        if (id.IsValid && _animations.TryRemove(id, out BaseAnimation animation))
        {
            _logger.Debug("Removing animation {0}", id);
            RemoveSinglePlayerAnimation(animation);
            animation.OnRemoved();
        }
    }

    public void RemoveSinglePlayerAnimation(BaseAnimation animation)
    {
        if (animation.IsSinglePlayer && _playerAnimations.TryGetValue(animation.PlayerId, out PlayerAnimations animations))
        {
            animations.RemoveAnimation(animation);
            if (animations.IsEmpty)
            {
                _playerAnimations.TryRemove(animation.PlayerId, out PlayerAnimations _);
                animations.Dispose();
            }
        }
    }
    
    private void AnimationLoop(object tokenObj)
    {
        CancellationToken token = (CancellationToken)tokenObj;
        
        while (!token.IsCancellationRequested)
        {
            try
            {
                float startTime = Time.realtimeSinceStartup;
                _logger.Debug("Processing {0} animations", _animations.Count);
                ProcessAnimations(startTime);
                _logger.Debug("Processed animations. {0} remaining", _animations.Count);
                float endTime = Time.realtimeSinceStartup;
                
                if (_animations.Count == 0)
                {
                    _sendCount++;
                    int timeout = GetSleepDuration();
                    _reset.WaitOne(timeout);
                }
                else
                {
                    _sendCount = 0;
                    int processDuration = Mathf.RoundToInt((endTime - startTime) * 1000);
                    int sleepDuration = Mathf.Max(UiFrameworkConfig.Instance.Animations.UpdateRate - processDuration, 1);
                    Thread.Sleep(sleepDuration);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception("An error occurred while processing animations", ex);
            }
        }
    }
    
    private void ProcessAnimations(float currentTime)
    {
        foreach (PlayerAnimations playerAnimations in _playerAnimations.Values)
        {
            JsonFrameworkWriter writer = Create();
            foreach ((AnimationId id, BaseAnimation animation) in playerAnimations.Animations)
            {
                ProcessAnimation(id, animation, currentTime, writer);
            }
            SendAnimations(writer, playerAnimations.Send);
        }
        
        foreach ((AnimationId id, BaseAnimation animation) in _animations)
        {
            if (!animation.IsSinglePlayer)
            {
                JsonFrameworkWriter writer = Create();
                ProcessAnimation(id, animation, currentTime, writer);
                SendAnimations(writer, animation.Send);
            }
        }
    }

    private void ProcessAnimation(AnimationId id, BaseAnimation animation, float currentTime, JsonFrameworkWriter writer)
    {
        _logger.Debug("Processing Animation {0}", id);

        if (animation.Cancelled)
        {
            RemoveAnimation(id);
            return;
        }
            
        animation.OnTick(currentTime);
                
        // Check if we need to wait for the delay
        if (animation.Delay > 0 && animation.Elapsed < animation.Delay)
        {
            return;
        }
            
        float effectiveElapsed = animation.Elapsed - animation.Delay;
        if (effectiveElapsed <= animation.Duration)
        {
            animation.WriteAnimationComponent(writer, animation.ElapsedPercentage);
            return;
        }

        animation.WriteCompletedComponent(writer);
        if (animation.OnAnimationEnded(currentTime))
        {
            RemoveAnimation(id);
        }
    }
    
    public void OnPlayerDisconnected(ulong playerId)
    {
        if(_playerAnimations.TryGetValue(playerId, out PlayerAnimations animations))
        {
            foreach (KeyValuePair<AnimationId, BaseAnimation> pair in animations.Animations)
            {
                RemoveAnimation(pair.Key);
            }
        }

        foreach (BaseAnimation animation in _animations.Values)
        {
            animation.RemoveForPlayer(playerId);
        }
    }

    private static JsonFrameworkWriter Create()
    {
        JsonFrameworkWriter writer = JsonFrameworkWriter.Create();
        writer.WriteStartArray();
        return writer;
    }

    private static void SendAnimations(JsonFrameworkWriter writer, SendInfo send)
    {
        writer.WriteEndArray();
        BaseBuilder.AddUi(send, writer);
        writer.Dispose();
    }

    private int GetSleepDuration()
    {
        if (_sendCount > 10)
        {
            return -1;
        }
        
        return 25 + (1 << _sendCount);
    }
    
    internal void OnServerShutdown() => _cancellationTokenSource.Cancel();
}