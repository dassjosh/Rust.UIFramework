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
    
    public void OnAnimatedPanelCreated(SendInfo send, string rootName, string name, AnimationId id)
    {
        if (send.connections == null)
        {
            OnAnimatedPanelCreated(send.connection.userid, rootName, name, id);
            return;
        }

        List<Connection> connections = send.connections;
        for (int index = 0; index < connections.Count; index++)
        {
            Connection connection = connections[index];
            OnAnimatedPanelCreated(connection.userid, rootName, name, id);
        }
    }
    
    private void OnAnimatedPanelCreated(ulong playerId, string rootName, string name, AnimationId id)
    {
        PlayerPanel rootPlayerPanel = new(playerId, rootName);
        PlayerPanel playerPanel = new(playerId, name);
        List<PlayerPanel> animationPanels = GetAnimationPanels(id);
        AddPlayerPanel(id, playerPanel, animationPanels);
        if (!string.IsNullOrEmpty(rootName))
        {
            AddPlayerPanel(id, rootPlayerPanel, animationPanels);
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

    public void OnAnimationCompleted(AnimationId id)
    {
        if (_animatedPlayers.TryGetValue(id, out List<PlayerPanel> playerPanels))
        {
            foreach (PlayerPanel playerPanel in playerPanels)
            {
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
            Singleton<AnimationHandler>.Instance.GetAnimation(id)?.RemoveForPlayer(playerId);
        }
    }

    private readonly record struct PlayerPanel(ulong PlayerId, string PanelName);
}