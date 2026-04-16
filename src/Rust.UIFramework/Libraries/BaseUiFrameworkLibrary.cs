using System.Collections.Generic;
using Oxide.Core.Libraries;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries;

public abstract class BaseUiFrameworkLibrary : Library
{
    private static readonly List<BaseUiFrameworkLibrary> Libraries = [];
    
    protected BaseUiFrameworkLibrary()
    {
        Libraries.Add(this);
    }

    protected virtual void OnInit() {}
    protected virtual void OnCommunityEntitySpawned(CommunityEntity entity) {}
    protected virtual void OnServerInitialized() {}
    protected virtual void OnPluginLoaded(Plugin plugin){}
    protected virtual void OnPluginUnloaded(Plugin plugin){}
    protected virtual void OnPluginLoaded(IUiFrameworkPlugin plugin){}
    protected virtual void OnPluginUnloaded(IUiFrameworkPlugin plugin){}
    protected virtual void OnPlayerConnected(BasePlayer player){}
    protected virtual void OnPlayerDisconnected(BasePlayer player){}
    protected virtual void OnServerSave(){}
    protected virtual void OnServerShutdown(){}
    
    internal static void ProcessOnInit()
    {
        foreach (BaseUiFrameworkLibrary library in Libraries)
        {
            library.OnInit();
        }
    }
    
    internal static void ProcessOnCommunityEntitySpawned(CommunityEntity entity)
    {
        foreach (BaseUiFrameworkLibrary library in Libraries)
        {
            library.OnCommunityEntitySpawned(entity);
        }
    }
    
    internal static void ProcessOnServerInitialized()
    {
        foreach (BaseUiFrameworkLibrary library in Libraries)
        {
            library.OnServerInitialized();
        }
    }
    
    internal static void ProcessOnPluginLoaded(Plugin plugin)
    {
        foreach (BaseUiFrameworkLibrary library in Libraries)
        {
            library.OnPluginLoaded(plugin);
        }
    }
    
    internal static void ProcessOnPluginLoaded(IUiFrameworkPlugin plugin)
    {
        foreach (BaseUiFrameworkLibrary library in Libraries)
        {
            library.OnPluginLoaded(plugin);
        }
    }
    
    internal static void ProcessOnPluginUnloaded(Plugin plugin)
    {
        foreach (BaseUiFrameworkLibrary library in Libraries)
        {
            library.OnPluginUnloaded(plugin);
        }
    }
    
    internal static void ProcessOnPluginUnloaded(IUiFrameworkPlugin plugin)
    {
        foreach (BaseUiFrameworkLibrary library in Libraries)
        {
            library.OnPluginUnloaded(plugin);
        }
    }
    
    internal static void ProcessOnPlayerConnected(BasePlayer player)
    {
        foreach (BaseUiFrameworkLibrary library in Libraries)
        {
            library.OnPlayerConnected(player);
        }
    }
    
    internal static void ProcessOnPlayerDisconnected(BasePlayer player)
    {
        foreach (BaseUiFrameworkLibrary library in Libraries)
        {
            library.OnPlayerDisconnected(player);
        }
    }
    
    internal static void ProcessOnServerSave()
    {
        foreach (BaseUiFrameworkLibrary library in Libraries)
        {
            library.OnServerSave();
        }
    }
    
    internal static void ProcessOnServerShutdown()
    {
        foreach (BaseUiFrameworkLibrary library in Libraries)
        {
            library.OnServerShutdown();
        }
    }
}