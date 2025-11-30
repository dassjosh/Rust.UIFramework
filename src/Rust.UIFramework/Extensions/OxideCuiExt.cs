using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Threading;
using Oxide.Ext.UiFramework.Threading.UiChannel;
using Oxide.Game.Rust.Cui;

namespace Oxide.Ext.UiFramework.Extensions;

public static class OxideCuiExt
{
    extension(CuiElementContainer container)
    {
        public void AddUiAsync(BasePlayer player, string destroyUiName = null)
        {
            if (player && player.IsConnected)
            {
                SendInfo send = SendInfoBuilder.Get(player);
                AddUiAsync(container, send, destroyUiName);
            }
        }

        public void AddUiAsync(Connection connection, string destroyUiName = null)
        {
            if (connection is { connected: true })
            {
                SendInfo send = SendInfoBuilder.Get(connection);
                AddUiAsync(container, send, destroyUiName);
            }
        }

        public void AddUiAsync(IEnumerable<Connection> connections, string destroyUiName = null)
        {
            SendInfo send = SendInfoBuilder.Get(connections);
            AddUiAsync(container, send, destroyUiName);
        }

        public void AddUiAsync(string destroyUiName = null)
        {
            SendInfo send = SendInfoBuilder.Get(Net.sv.connections);
            AddUiAsync(container, send, destroyUiName);
        }

        public void AddUiAsync(SendInfo send, string destroyUiName = null)
        {
            OxideCuiContainerRequest.Create(container, send, destroyUiName).Enqueue();
        }
    }

    public static void DestroyUi(Connection connection, string destroyUiName = null)
    {
        if (connection is { connected: true })
        {
            SendInfo send = SendInfoBuilder.Get(connection);
            DestroyUi(send, destroyUiName);
        }
    }
    
    public static void DestroyUi(IEnumerable<Connection> connections, string destroyUiName = null)
    {
        SendInfo send = SendInfoBuilder.Get(connections);
        DestroyUi(send, destroyUiName);
    }
    
    public static void DestroyUi(string destroyUiName = null)
    {
        SendInfo send = SendInfoBuilder.Get(Net.sv.connections);
        DestroyUi(send, destroyUiName);
    }
    
    public static void DestroyUi(SendInfo send, string destroyUiName) => BaseBuilder.DestroyUi(send, destroyUiName);
}