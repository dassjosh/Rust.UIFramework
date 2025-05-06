using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Builders;
using Oxide.Ext.UiFramework.Threading;
using Oxide.Game.Rust.Cui;

namespace Oxide.Ext.UiFramework.Extensions;

public static class OxideCuiExt
{
    public static void AddUiAsync(this CuiElementContainer container, BasePlayer player, string destroyUiName = null)
    {
        SendInfo send = SendInfoBuilder.Get(player);
        AddUiAsync(container, send, destroyUiName);
    }
    
    public static void AddUiAsync(this CuiElementContainer container, Connection connection, string destroyUiName = null)
    {
        SendInfo send = SendInfoBuilder.Get(connection);
        AddUiAsync(container, send, destroyUiName);
    }
    
    public static void AddUiAsync(this CuiElementContainer container, IEnumerable<Connection> connections, string destroyUiName = null)
    {
        SendInfo send = SendInfoBuilder.Get(connections);
        AddUiAsync(container, send, destroyUiName);
    }
    
    public static void AddUiAsync(this CuiElementContainer container, string destroyUiName = null)
    {
        SendInfo send = SendInfoBuilder.Get(Net.sv.connections);
        AddUiAsync(container, send, destroyUiName);
    }
    
    public static void AddUiAsync(this CuiElementContainer container, SendInfo send, string destroyUiName = null)
    {
        OxideCuiRequest request = OxideCuiRequest.Create(container, send, destroyUiName);
        SendHandler.Enqueue(request);
    }
    
    public static void DestroyUi(Connection connection, string destroyUiName = null)
    {
        SendInfo send = SendInfoBuilder.Get(connection);
        DestroyUi(send, destroyUiName);
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
    
    public static void DestroyUi(SendInfo send, string destroyUiName)
    {
        BaseBuilder.DestroyUi(send, destroyUiName);
    }
}