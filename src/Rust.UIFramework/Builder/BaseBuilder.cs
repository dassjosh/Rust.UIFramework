using System;
using System.Collections.Generic;
using System.Text;
using Network;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Threading;

namespace Oxide.Ext.UiFramework.Builder;

public abstract class BaseBuilder : BasePoolable
{
    protected string RootName;

    public string GetRootName() => RootName;
    #region Add UI
    public void AddUi(BasePlayer player)
    {
        if (!player) throw new ArgumentNullException(nameof(player));
        AddUi(player.Connection);
    }

    public void AddUi(Connection connection)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        AddUi(new SendInfo(connection));
    }

    public void AddUi(IEnumerable<Connection> connections)
    {
        if (connections == null) throw new ArgumentNullException(nameof(connections));
        List<Connection> pooledConnection = ListPool<Connection>.Instance.Get();
        pooledConnection.AddRange(connections);
        AddUi(new SendInfo(pooledConnection));
    }

    public void AddUi()
    {
        AddUi(Net.sv.connections);
    }

    protected void AddUi(SendInfo send)
    {
        SendHandler.Enqueue(UiSendRequest.Create(this, send));
    }

    internal abstract void SendUi(SendInfo send);

    internal void AddUi(SendInfo send, JsonFrameworkWriter writer)
    {
        NetWrite write = ClientRPCStart(UiConstants.RpcFunctions.AddUiFunc);
        if (write != null)
        {
            writer.WriteToNetwork(write);
            write.Send(send);
        }
    }
        
    protected void AddUi(SendInfo send, byte[] bytes)
    {
        NetWrite write = ClientRPCStart(UiConstants.RpcFunctions.AddUiFunc);
        if (write != null)
        {
            write.BytesWithSize(bytes);
            write.Send(send);
        }
    }

    private static NetWrite ClientRPCStart(string funcName)
    {
        if (!Net.sv.IsConnected() || CommunityEntity.ServerInstance.net == null)
        {
            return null;
        }

        NetWrite write = Net.sv.StartWrite();
        write.PacketID(Message.Type.RPCMessage);
        write.EntityID(CommunityEntity.ServerInstance.net.ID);
        write.UInt32(StringPool.Get(funcName));
        return write;
    }
    #endregion

    #region Destroy UI
    public void DestroyUi(BasePlayer player)
    {
        if (!player) throw new ArgumentNullException(nameof(player));
        DestroyUi(player, RootName);
    }

    public void DestroyUi(Connection connection)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        DestroyUi(new SendInfo(connection), RootName);
    }

    public void DestroyUi(List<Connection> connections)
    {
        if (connections == null) throw new ArgumentNullException(nameof(connections));
        DestroyUi(new SendInfo(connections), RootName);
    }

    public void DestroyUi()
    {
        DestroyUi(RootName);
    }
        
    public static void DestroyUi(BasePlayer player, string name)
    {
        if (!player) throw new ArgumentNullException(nameof(player));
        DestroyUi(new SendInfo(player.Connection), name);
    }

    public static void DestroyUi(string name)
    {
        DestroyUi(new SendInfo(Net.sv.connections), name);
    }

    public static void DestroyUi(SendInfo send, string name)
    {
        CommunityEntity.ServerInstance.ClientRPC(new RpcTarget
        {
            Function = UiConstants.RpcFunctions.DestroyUiFunc,
            Connections = send
        }, name);
    }
    #endregion

    #region JSON
                        
    public abstract byte[] GetBytes();
        
    /// <summary>
    /// Warning this is only recommend to use for debugging purposes
    /// </summary>
    /// <returns></returns>
    public string GetJsonString() => Encoding.UTF8.GetString(GetBytes());
    #endregion

    #region Pooling
    protected override void EnterPool()
    {
        RootName = null;
    }
    #endregion
}