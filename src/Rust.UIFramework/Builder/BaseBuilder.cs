using System;
using System.Collections.Generic;
using System.Text;
using Network;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Threading;
using Oxide.Ext.UiFramework.Types;

#if BENCHMARKS
using Facepunch;
using Oxide.Ext.UiFramework.Benchmarks;
#endif

namespace Oxide.Ext.UiFramework.Builder;

public abstract class BaseBuilder : BasePoolable
{
    protected string RootName;
    
    public string GetRootName() => RootName;
    
    public IUiFrameworkPlugin Plugin { get; protected set; }

    protected void Init(IUiFrameworkPlugin plugin)
    {
        Plugin = plugin;
    }
    
    #region Add UI
    public void AddUi(BasePlayer player)
    {
        if (player && player.IsConnected)
        {
            AddUi(SendInfoBuilder.Get(player));
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
            AddUi(SendInfoBuilder.Get(connection));
        }
        else
        {
            TryDispose();
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
        Singleton<SendHandler>.Instance.Enqueue(UiSendRequest.Create(this, send));
    }

    public void AddUiDebug(BasePlayer player, in UiDebugOptions options)
    {
        if (player && player.IsConnected)
        {
            AddUiDebug(SendInfoBuilder.Get(player), options);
        }
        else
        {
            TryDispose();
        }
    }

    public void AddUiDebug(Connection connection, in UiDebugOptions options)
    {
        if (connection is { connected: true })
        {
            AddUiDebug(SendInfoBuilder.Get(connection), options);
        }
        else
        {
            TryDispose();
        }
    }

    public void AddUiDebug(IEnumerable<Connection> connections, in UiDebugOptions options)
    {
        AddUiDebug(SendInfoBuilder.Get(connections), options);
    }

    public void AddUiDebug(in UiDebugOptions options)
    {
        AddUiDebug(SendInfoBuilder.Get(Net.sv.connections), options);
    }
    
    public void AddUiDebug(SendInfo send, in UiDebugOptions options)
    {
        Singleton<SendHandler>.Instance.Enqueue(UiDebugSendRequest.Create(this, send, options));
    }

    internal abstract void SendUi(SendInfo send, in UiDebugOptions? options);

    internal void AddUi(SendInfo send, JsonFrameworkWriter writer, in UiDebugOptions? options)
    {
        RpcFunctions.SendAddUi(send, writer);
        
        if (options.HasValue)
        {
            UiDebugHandler.HandleDebug(Plugin, writer, options.Value);
        }
    }
        
    protected void AddUi(SendInfo send, byte[] bytes, in UiDebugOptions? options)
    {
        RpcFunctions.SendAddUi(send, bytes);
        
        if (options.HasValue)
        {
            UiDebugHandler.HandleDebug(Plugin, bytes, options.Value);
        }
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
        if (!Net.sv.IsConnected() || CommunityEntity.ServerInstance.net == null)
        {
            return;
        }
        
        Singleton<AnimationTracker>.Instance.RemoveUiForSend(send, name);
        Singleton<SendHandler>.Instance.Enqueue(UiDestroyRequest.Create(name, send));
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
    protected static void ClearAnimationList(List<BaseAnimation> animations)
    {
        int count = animations.Count;
        for (int index = 0; index < count; index++)
        {
            BaseAnimation animation = animations[index];
            if (animation.State == AnimationState.Init)
            {
                animation.Dispose();
            }
        }
        
        animations.Clear();
    }
    
    protected override void EnterPool()
    {
        RootName = null;
    }
    #endregion
}