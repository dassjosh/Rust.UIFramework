using System;
using System.Collections.Generic;
using System.Text;
using Network;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Helpers;
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
    public void AddUi(BasePlayer player, in UiDebugOptions? options = default)
    {
        if (player && player.IsConnected)
        {
            AddUi(SendInfoBuilder.Get(player), options);
        }
        else
        {
            TryDispose();
        }
    }

    public void AddUi(Connection connection, in UiDebugOptions? options = default)
    {
        if (connection is { connected: true })
        {
            AddUi(SendInfoBuilder.Get(connection), options);
        }
        else
        {
            TryDispose();
        }
    }

    public void AddUi(IEnumerable<Connection> connections, in UiDebugOptions? options = default) => AddUi(SendInfoBuilder.Get(connections), options);
    public void AddUi(IEnumerable<BasePlayer> players, in UiDebugOptions? options = default) => AddUi(SendInfoBuilder.Get(players), options);
    public void AddUi(in UiDebugOptions? options = default) => AddUi(SendInfoBuilder.Get(Net.sv.connections), options);
    public void AddUi(SendInfo send, in UiDebugOptions? options = default)
    {
        UiSendRequest.Create(this, send, options).Enqueue();
    }

    internal abstract void SendUi(SendInfo send, in UiDebugOptions? options);
    internal abstract void SendAnimations(SendInfo send);

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

    public void DestroyUi(IEnumerable<Connection> connections) => DestroyUi(SendInfoBuilder.Get(connections), RootName);
    public void DestroyUi(List<Connection> connections) => DestroyUi(SendInfoBuilder.Get(connections), RootName);
    public void DestroyUi(IEnumerable<BasePlayer> players) => DestroyUi(SendInfoBuilder.Get(players), RootName);
    public void DestroyUi(List<BasePlayer> players) => DestroyUi(SendInfoBuilder.Get(players), RootName);
    public void DestroyUi() => DestroyUi(RootName);

    public static void DestroyUi(string name) => DestroyUi(Net.sv.connections, name);
    public static void DestroyUi(IEnumerable<Connection> connections, string name) => DestroyUi(SendInfoBuilder.Get(connections), name);
    public static void DestroyUi(List<Connection> connections, string name) => DestroyUi(SendInfoBuilder.Get(connections), name);
    public static void DestroyUi(IEnumerable<BasePlayer> players, string name) => DestroyUi(SendInfoBuilder.Get(players), name);
    public static void DestroyUi(List<BasePlayer> players, string name) => DestroyUi(SendInfoBuilder.Get(players), name);

    public static void DestroyUi(BasePlayer player, string name)
    {
        if (player)
        {
            DestroyUi(player.Connection, name);
        }
    }

    public static void DestroyUi(Connection connection, string name)
    {
        if (connection is { connected: true })
        {
            DestroyUi(SendInfoBuilder.Get(connection), name);
        }
    }

    public static void DestroyUi(SendInfo send, string name)
    {
        if (Net.sv.IsConnected() && CommunityEntity.ServerInstance.net != null)
        {
            Singleton<AnimationTracker>.Instance.RemoveUiForSend(send, name);
            UiDestroyRequest.Create(name, send).Enqueue();
        }
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

    #region Combined
    public abstract void Combine(SendInfo send, JsonFrameworkWriter writer);
    #endregion

    #region Pooling
    protected static void ClearAnimationList(List<ISendableAnimation> animations)
    {
        int count = animations.Count;
        for (int index = 0; index < count; index++)
        {
            ISendableAnimation animation = animations[index];
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
        Plugin = null;
    }
    #endregion
}