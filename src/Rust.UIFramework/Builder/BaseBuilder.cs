using System;
using System.Collections.Generic;
using System.Text;
using Facepunch;
using Network;
using Oxide.Ext.UiFramework.Benchmarks;
using Oxide.Ext.UiFramework.Builders;
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
        if (player && player.IsConnected)
        {
            AddUi(SendInfoBuilder.Get(player));
        }
    }

    public void AddUi(Connection connection)
    {
        if (connection is { connected: true })
        {
            AddUi(SendInfoBuilder.Get(connection));
        }
    }

    public void AddUi(IEnumerable<Connection> connections)
    {
        AddUi(SendInfoBuilder.Get(connections));
    }

    public void AddUi()
    {
        AddUi(SendInfoBuilder.Get(Net.sv.connections));
    }

    public void AddUi(SendInfo send)
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
        if (player && player.IsConnected)
        {
            DestroyUi(player, RootName);
        }
    }

    public void DestroyUi(Connection connection)
    {
        if (connection is { connected: true })
        {
            DestroyUi(SendInfoBuilder.Get(connection), RootName);
        }
    }

    public void DestroyUi(List<Connection> connections)
    {
        if (connections == null) throw new ArgumentNullException(nameof(connections));
        DestroyUi(SendInfoBuilder.Get(connections), RootName);
    }
    
    public void DestroyUi(IEnumerable<Connection> connections)
    {
        if (connections == null) throw new ArgumentNullException(nameof(connections));
        SendInfo send = SendInfoBuilder.Get(connections);
        DestroyUi(send, RootName);
        ListPool<Connection>.Instance.Free(send.connections);
    }

    public void DestroyUi()
    {
        DestroyUi(RootName);
    }
        
    public static void DestroyUi(BasePlayer player, string name)
    {
        if (player && player.IsConnected)
        {
            DestroyUi(SendInfoBuilder.Get(player.Connection), name);
        }
    }

    public static void DestroyUi(string name)
    {
        DestroyUi(SendInfoBuilder.Get(Net.sv.connections), name);
    }

    public static void DestroyUi(SendInfo send, string name)
    {
        CommunityEntity.ServerInstance.ClientRPC(new RpcTarget
        {
            Function = UiConstants.RpcFunctions.DestroyUiFunc,
            Connections = send
        }, name);
    }

    public static void DestroyUi(IEnumerable<Connection> connections, string name)
    {
        DestroyUi(SendInfoBuilder.Get(connections), name);
    }
    #endregion

#if BENCHMARKS
    #region Benchmark UI

    internal void AddUiBenchmark(JsonFrameworkWriter writer)
    {
        BenchmarkNetWrite write = Pool.Get<BenchmarkNetWrite>();
        writer.WriteToNetwork(write);
        Pool.Free(ref write);
    }
    #endregion
#endif

    #region JSON
                        
    public abstract byte[] GetBytes();
        
    /// <summary>
    /// Warning this is only recommended to use for debugging purposes
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