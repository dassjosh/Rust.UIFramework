using System.Collections.Concurrent;
using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

internal class AnimationTracker : ISingleton
{
    private readonly ConcurrentDictionary<PlayerPanel, AnimationId> _playerPanels = new();
    private readonly ConcurrentDictionary<AnimationId, List<PlayerPanel>> _animatedPlayers = new();
    
    private AnimationTracker() { }
    
    public void OnAnimationQueued(IAnimation animation, SendInfo send, string rootName)
    {
        if (animation is IElementAnimation { Tracked: false })
        {
            return;
        }
        
        if (send.connections == null)
        {
            OnAnimationQueued(animation, send.connection.userid, rootName);
            return;
        }

        List<Connection> connections = send.connections;
        for (int index = 0; index < connections.Count; index++)
        {
            Connection connection = connections[index];
            OnAnimationQueued(animation, connection.userid, rootName);
        }
    }
    
    private void OnAnimationQueued(IAnimation animation, ulong playerId, string rootName)
    {
        List<PlayerPanel> animationPanels = GetAnimationPanels(animation.Id);
        List<IElementAnimation> elements = UiPool.Internal.GetList<IElementAnimation>();
        animation.AllOfType(elements);
        for (int index = 0; index < elements.Count; index++)
        {
            IElementAnimation element = elements[index];
            PlayerPanel playerPanel = new(playerId, element.Reference);
            AddPlayerPanel(animation.Id, playerPanel, animationPanels);
        }
        UiPool.Internal.FreeList(elements);

        if (!string.IsNullOrEmpty(rootName))
        {
            PlayerPanel rootPlayerPanel = new(playerId, rootName);
            AddPlayerPanel(animation.Id, rootPlayerPanel, animationPanels);
        }
    }

    private void AddPlayerPanel(AnimationId id, in PlayerPanel rootPlayerPanel, List<PlayerPanel> animationPanels)
    {
        _playerPanels.TryAdd(rootPlayerPanel, id);
        animationPanels.Add(rootPlayerPanel);
    }

    private List<PlayerPanel> GetAnimationPanels(AnimationId id)
    {
        if (!_animatedPlayers.TryGetValue(id, out List<PlayerPanel> playerPanels))
        {
            _animatedPlayers[id] = playerPanels = UiPool.Internal.GetList<PlayerPanel>();
        }

        return playerPanels;
    }

    public void OnAnimationFinalized(AnimationId id)
    {
        if (_animatedPlayers.TryGetValue(id, out List<PlayerPanel> playerPanels))
        {
            for (int index = 0; index < playerPanels.Count; index++)
            {
                PlayerPanel playerPanel = playerPanels[index];
                _playerPanels.TryRemove(playerPanel, out AnimationId _);
            }

            UiPool.Internal.FreeList(playerPanels);
        }
    }
    
    public void RemoveUiForSend(SendInfo send, string name)
    {
        if (send.connections == null)
        {
            RemoveUiForSend(send.connection.userid, name);
            return;
        }

        List<Connection> connections = send.connections;
        for (int index = 0; index < connections.Count; index++)
        {
            Connection connection = connections[index];
            RemoveUiForSend(connection.userid, name);
        }
    }

    private void RemoveUiForSend(ulong playerId, string name)
    {
        PlayerPanel playerPanel = new(playerId, name);
        if (_playerPanels.Remove(playerPanel, out AnimationId id))
        {
            Singleton<AnimationHandler>.Instance.GetAnimation(id)?.RemovePlayer(playerId);
        }
    }

    private readonly record struct PlayerPanel(ulong PlayerId, string PanelName);
}