using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Network;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Builders;

internal static class SendInfoBuilder
{
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
        return new SendInfo(connection);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static SendInfo Get(IEnumerable<Connection> connections)
    {
        if (connections == null) throw new ArgumentNullException(nameof(connections));
        List<Connection> pooledConnection = ListPool<Connection>.Instance.Get();
        pooledConnection.AddRange(connections);
        return new SendInfo(pooledConnection);
    }
}