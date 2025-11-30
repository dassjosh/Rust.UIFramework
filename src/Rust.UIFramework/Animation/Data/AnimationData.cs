using System.Collections.Concurrent;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

internal class AnimationData
{
    private readonly ConcurrentDictionary<AnimationId, ISendableAnimation> _animations = new();
    private readonly ConcurrentDictionary<AnimationId, ISendableAnimation> _groupAnimations = new();
    private readonly ConcurrentDictionary<ulong, PlayerAnimationData> _playerAnimations = new();

    public IEnumerable<PlayerAnimationData> PlayerAnimations => _playerAnimations.Values;

    public IEnumerable<KeyValuePair<AnimationId, ISendableAnimation>> GroupAnimations => _groupAnimations;

    public int Count => _animations.Count;
    
    public ISendableAnimation GetAnimation(AnimationId id) => _animations.GetValueOrDefault(id);
    
    public void EnqueueAnimation(ISendableAnimation animation)
    {
        _animations[animation.Id] = animation;
        if (!animation.TryGetSinglePlayer(out ulong playerId))
        {
            _groupAnimations[animation.Id] = animation;
            return;
        }
        
        if (!_playerAnimations.TryGetValue(playerId, out PlayerAnimationData animations))
        {
            _playerAnimations[playerId] = animations = PlayerAnimationData.Create(animation);
        }

        animations.AddAnimation(animation);
    }
    
    public void RemoveAnimation(AnimationId id)
    {
        if (!id.IsValid)
        {
            return;
        }

        if (!_animations.TryRemove(id, out ISendableAnimation animation))
        {
            return;
        }
        
        if (animation.IsSinglePlayer())
        {
            RemoveSinglePlayerAnimation(animation);
        }
        else
        {
            _groupAnimations.TryRemove(id, out ISendableAnimation _);
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
        foreach ((AnimationId id, ISendableAnimation animation) in _animations)
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
    
    internal void OnPluginUnloaded(IUiFrameworkPlugin plugin)
    {
        foreach (ISendableAnimation animation in _animations.Values)
        {
            if (animation.Plugin == plugin)
            {
                RemoveAnimation(animation.Id);
            }
        }
    }
}