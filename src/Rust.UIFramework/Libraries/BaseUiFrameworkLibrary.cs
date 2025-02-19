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
    protected virtual void OnPluginUnloaded(Plugin plugin){}
    protected virtual void OnPlayerDisconnected(BasePlayer player){}
    
    internal static void ProcessOnServerInitialized()
    {
        foreach (BaseUiFrameworkLibrary library in Libraries)
        {
            library.OnServerInitialized();
        }
    }
    
    internal static void ProcessOnPluginUnloaded(Plugin plugin)
    {
        foreach (BaseUiFrameworkLibrary library in Libraries)
        {
            library.OnPluginUnloaded(plugin);
        }
    }
    
    internal static void ProcessOnPlayerDisconnected(BasePlayer player)
    {
        foreach (BaseUiFrameworkLibrary library in Libraries)
        {
            library.OnPlayerDisconnected(player);
        }
    }
}