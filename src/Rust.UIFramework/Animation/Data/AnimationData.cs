using System.Collections.Concurrent;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

internal class AnimationData : ISingleton
{
    private readonly ConcurrentDictionary<AnimationId, IAnimation> _allAnimations = new();
    private readonly ConcurrentDictionary<AnimationId, ISendableAnimation> _groupAnimations = new();
    private readonly ConcurrentDictionary<ulong, PlayerAnimationData> _playerAnimations = new();

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
        if (animation is ISendableAnimation sendable)
        {
            AddAnimation(sendable);
        }   
    }
    
    private void AddAnimation(ISendableAnimation animation)
    {
        if (animation.TryGetSinglePlayer(out ulong playerId))
        {
            if (!_playerAnimations.TryGetValue(playerId, out PlayerAnimationData animations))
            {
                _playerAnimations[playerId] = animations = PlayerAnimationData.Create(animation);
            }

            animations.AddAnimation(animation);
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
            return;
        }

        if (animation is ISendableAnimation sendable)
        {
            if (sendable.IsSinglePlayer())
            {
                RemoveSinglePlayerAnimation(sendable);
            }
            else
            {
                _groupAnimations.TryRemove(id, out ISendableAnimation _);
            }
        }
        
        if (id == animation.Id)
        {
            animation.TryDispose();
        }
    }

    private void RemoveSinglePlayerAnimation(ISendableAnimation animation)
    {
        if (!animation.TryGetSinglePlayer(out ulong playerId))
        {
            return;
        }
        
        if (!_playerAnimations.TryGetValue(playerId, out PlayerAnimationData animations))
        {
            return;
        }
        
        animations.RemoveAnimation(animation);
        if (!animations.IsEmpty)
        {
            return;
        }
        
        _playerAnimations.TryRemove(playerId, out PlayerAnimationData _);
        animations.Dispose();
    }

    public void CleanupCompletedAnimations()
    {
        foreach ((AnimationId id, IAnimation animation) in _allAnimations.GetEnumeratorPooled(UiFrameworkPlugin.Instance))
        {
            if (animation is IPoolable { IsPooled: true } || id != animation.Id)
            {
                //Somehow the animation was disposed and we didn't remove it.
                RemoveAnimation(animation.Id);
                continue;
            }

            switch (animation.State)
            {
                case AnimationState.Cancelled:
                case AnimationState.Completed:
                case AnimationState.Timeout:
                    RemoveAnimation(animation.Id);
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