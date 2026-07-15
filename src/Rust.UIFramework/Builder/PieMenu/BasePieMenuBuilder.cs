using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Builder;

public abstract class BasePieMenuBuilder : BasePoolable
{
    public void AddUi(BasePlayer player)
    {
        if (player && player.IsConnected)
        {
            SendUi(SendInfoBuilder.Get(player));
        }
        else
        {
            TryDispose();
        }
    }

    public void AddUi(Connection connection)
    {
        if (connection is { connected: true })
        {
            SendUi(SendInfoBuilder.Get(connection));
        }
        else
        {
            TryDispose();
        }
    }

    public void AddUi(IEnumerable<Connection> connections) => SendUi(SendInfoBuilder.Get(connections));
    public void AddUi(IEnumerable<BasePlayer> players) => SendUi(SendInfoBuilder.Get(players));
    public void AddUi() => SendUi(SendInfoBuilder.Get(Net.sv.connections));

    public abstract void SendUi(SendInfo send);
}