using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

internal class AnimationData : ISingleton
{
    private readonly ConcurrentDictionary<AnimationId, IAnimation> _allAnimations = new();
    private readonly ConcurrentDictionary<AnimationId, ISendableAnimation> _groupAnimations = new();
    private readonly ConcurrentDictionary<AnimationId, PlayerAnimationData> _animationPlayer = new();
    private readonly ConcurrentDictionary<ulong, PlayerAnimationData> _playerAnimations = new();
    private readonly IUiLogger _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<AnimationData>();

    internal ConcurrentDictionary<AnimationId, ISendableAnimation> GroupAnimations => _groupAnimations;
    internal ConcurrentDictionary<ulong, PlayerAnimationData> PlayerAnimations => _playerAnimations;
    
    public int Count => _allAnimations.Count;

    private AnimationData() { }
    
    public IAnimation GetAnimation(AnimationId id) => _allAnimations.GetValueOrDefault(id);
    public bool TryGetAnimation(AnimationId id, out IAnimation animation) => _allAnimations.TryGetValue(id, out animation);
    
    public ISendableAnimation GetSendableAnimation(AnimationId id) => _allAnimations.GetValueOrDefault(id) as ISendableAnimation;
    public bool TryGetSendableAnimation(AnimationId id, out ISendableAnimation sendable)
    {
        if (_allAnimations.TryGetValue(id, out IAnimation animation) && animation is ISendableAnimation send)
        {
            sendable = send;
            return true;
        }

        sendable = default;
        return false;
    }

    public void AddAnimation(IAnimation animation)
    {
        _allAnimations[animation.Id] = animation;
    }
    
    public void OnAnimationQueued(ISendableAnimation animation)
    {
        if (animation == null) throw new ArgumentNullException(nameof(animation));
        AnimationException.ThrowIfMissingSend(animation);
        if (animation.TryGetSinglePlayer(out ulong playerId))
        {
            if (!_playerAnimations.TryGetValue(playerId, out PlayerAnimationData animations))
            {
                _playerAnimations[playerId] = animations = PlayerAnimationData.Create(animation, playerId);
            }

            animations.AddAnimation(animation);
            _animationPlayer[animation.Id] = animations;
            return;
        }

        _groupAnimations[animation.Id] = animation;
    }
    
    public void RemoveAnimation(AnimationId id)
    {
        if (!id.IsValid)
        {
            return;
        }

        if (!_allAnimations.TryRemove(id, out IAnimation animation))
        {
            _logger.Debug($"{nameof(RemoveAnimation)} Tried to remove animation id {{0}} which is not in the all animations collection", id);
            return;
        }

        if (_animationPlayer.TryRemove(id, out PlayerAnimationData playerData))
        {
            RemoveSinglePlayerAnimation(id, playerData, (ISendableAnimation)animation);
        }
        
        _groupAnimations.TryRemove(id, out ISendableAnimation _);
        
        if (animation.Id != id)
        {
            _logger.Debug($"{nameof(RemoveAnimation)} Animation ID does not match ID: {{0}} Animation.Id: {{1}}", id, animation.Id);
            return;
        }

        animation.Parent?.RemoveChild(animation);
        animation.TryDispose();
    }

    private void RemoveSinglePlayerAnimation(AnimationId id, PlayerAnimationData playerAnimations, ISendableAnimation sendable)
    {
        _animationPlayer.TryRemove(id, out _);
        playerAnimations.RemoveAnimation(sendable);
        if (!playerAnimations.IsEmpty)
        {
            return;
        }
        
        _playerAnimations.TryRemove(playerAnimations.PlayerId, out _);
        playerAnimations.Dispose();
    }

    public void CleanupCompletedAnimations()
    {
        foreach ((AnimationId id, IAnimation animation) in _allAnimations.GetEnumeratorPooled(UiFrameworkPlugin.Instance))
        {
            if (animation is IPoolable { IsPooled: true } || id != animation.Id)
            {
                //Somehow the animation was disposed and we didn't remove it.
                RemoveAnimation(id);
                _logger.Debug("Animation ID: {0} is already pooled but still in the All Animations collection", id);
                continue;
            }

            switch (animation.State)
            {
                case AnimationState.Cancelled:
                case AnimationState.Completed:
                case AnimationState.Timeout:
                    RemoveAnimation(id);
                    break;
                case AnimationState.Queued:
                case AnimationState.Delayed:
                case AnimationState.Running:
                    animation.TickCleanup();
                    break;
            }
        }
    }
    
    public void OnPlayerDisconnected(ulong playerId)
    {
        if(_playerAnimations.TryGetValue(playerId, out PlayerAnimationData animations))
        {
            foreach (KeyValuePair<AnimationId, ISendableAnimation> pair in animations.Animations)
            {
                RemoveAnimation(pair.Key);
            }
        }

        foreach (ISendableAnimation animation in _groupAnimations.Values)
        {
            animation.RemovePlayer(playerId);
        }
    }

    public void CancelPluginAnimations(IUiFrameworkPlugin plugin)
    {
        foreach (IAnimation animation in _allAnimations.GetEnumeratorPooled(UiFrameworkPlugin.Instance).Values)
        {
            if (animation.Plugin == plugin)
            {
                RemoveAnimation(animation.Id);
            }
        }
    }
    
    public void CancelPlayerAnimations(IUiFrameworkPlugin plugin, ulong playerId)
    {
        foreach (IAnimation animation in _allAnimations.GetEnumeratorPooled(UiFrameworkPlugin.Instance).Values)
        {
            if (animation.Plugin == plugin && animation is ISendableAnimation sendable)
            {
                sendable.RemovePlayer(playerId);
            }
        }
    }

    internal void OnPluginUnloaded(IUiFrameworkPlugin plugin) => CancelPluginAnimations(plugin);
}