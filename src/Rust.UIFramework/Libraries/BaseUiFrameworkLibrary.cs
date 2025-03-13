using System.Collections.Generic;
using Oxide.Core.Libraries;
using Oxide.Core.Plugins;

namespace Oxide.Ext.UiFramework.Libraries;

public abstract class BaseUiFrameworkLibrary : Library
{
    private static readonly List<BaseUiFrameworkLibrary> Libraries = [];
    
    protected BaseUiFrameworkLibrary()
    {
        Libraries.Add(this);
    }

    protected virtual void OnServerInitialized() {}
    protected virtual void OnPluginLoaded(Plugin plugin){}
    protected virtual void OnPluginUnloaded(Plugin plugin){}
    protected virtual void OnPlayerConnected(BasePlayer player){}
    protected virtual void OnPlayerDisconnected(BasePlayer player){}
    protected virtual void OnServerShutdown(){}
    
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
    
    internal static void ProcessOnPluginUnloaded(Plugin plugin)
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
    
    internal static void ProcessOnServerShutdown()
    {
        foreach (BaseUiFrameworkLibrary library in Libraries)
        {
            library.OnServerShutdown();
        }
    }
}