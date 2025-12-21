using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Network;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Builder;

internal static class SendInfoBuilder
{
    private const sbyte UiChannel = 3;
    private const sbyte AnimationsChannel = 4;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static SendInfo Get(BasePlayer player)
    {
        if (!player) throw new ArgumentNullException(nameof(player));
        return Get(player.Connection);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static SendInfo Get(Connection connection)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        return new SendInfo(connection)
        {
            channel = UiChannel
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static SendInfo Get(IEnumerable<Connection> connections)
    {
        if (connections == null) throw new ArgumentNullException(nameof(connections));
        List<Connection> pooledConnection = UiPool.Internal.GetList<Connection>();
        foreach (Connection connection in connections)
        {
            if (connection is { connected: true })
            {
                pooledConnection.Add(connection);
            }
        }
        
        return new SendInfo(pooledConnection)
        {
            channel = UiChannel
        };
    }

    internal static SendInfo GetForAnimations(SendInfo info)
    {
        sbyte channel = Singleton<AnimationTime>.Instance.AnimationsEnabled ? AnimationsChannel : UiChannel;
        return GetForChannel(info, channel);
    }
    
    internal static SendInfo GetForUi(SendInfo info)
    {
        return GetForChannel(info, UiChannel);
    }

    private static SendInfo GetForChannel(SendInfo info, sbyte channel)
    {
        if (info.connection != null)
        {
            return new SendInfo(info.connection)
            {
                channel = channel
            };
        }

        List<Connection> connections = UiPool.Internal.GetList<Connection>();
        foreach (Connection connection in info.connections)
        {
            if (connection is { connected: true })
            {
                connections.Add(connection);
            }
        }
        
        return new SendInfo(connections)
        {
            channel = channel
        };
    }
}