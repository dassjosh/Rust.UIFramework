using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Network;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Guards;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Builder;

internal static class SendInfoBuilder
{
    private const sbyte UiChannel = 3;
    private const sbyte AnimationsChannel = 4;
    private const sbyte PreCache = 5;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static SendInfo Get(BasePlayer player)
    {
        Guard.IsEntityNotNull(player);
        return Get(player.Connection);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static SendInfo GetForPrecache(BasePlayer player)
    {
        Guard.IsEntityNotNull(player);
        return Get(player.Connection, PreCache);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static SendInfo Get(Connection connection) => Get(connection, UiChannel);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static SendInfo Get(Connection connection, sbyte channel)
    {
        Guard.IsNotNull(connection);
        return new SendInfo(connection)
        {
            channel = channel
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static SendInfo Get(IEnumerable<Connection> connections)
    {
        Guard.IsNotNull(connections);
        List<Connection> pooledConnection = UiPool.Internal.GetList<Connection>();
        if (connections is IList<Connection> list)
        {
            //Fast Path
            for (int index = 0; index < list.Count; index++)
            {
                Connection connection = list[index];
                if(connection is { connected: true })
                {
                    pooledConnection.Add(connection);
                }
            }
        }
        else
        {
            foreach (Connection connection in connections)
            {
                if (connection is { connected: true })
                {
                    pooledConnection.Add(connection);
                }
            }
        }
        
        return new SendInfo(pooledConnection)
        {
            channel = UiChannel
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static SendInfo Get(IEnumerable<BasePlayer> players)
    {
        Guard.IsNotNull(players);
        List<Connection> pooledConnection = UiPool.Internal.GetList<Connection>();
        if (players is IList<BasePlayer> list)
        {
            //Fast Path
            for (int index = 0; index < list.Count; index++)
            {
                BasePlayer player = list[index];
                if(player && player.Connection is { connected: true })
                {
                    pooledConnection.Add(player.Connection);
                }
            }
        }
        else
        {
            foreach (BasePlayer player in players)
            {
                if (player && player.Connection is { connected: true })
                {
                    pooledConnection.Add(player.Connection);
                }
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
        for (int index = 0; index < info.connections.Count; index++)
        {
            Connection connection = info.connections[index];
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