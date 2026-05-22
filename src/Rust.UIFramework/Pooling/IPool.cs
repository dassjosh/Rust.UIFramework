using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Logging;

namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Represents a pool
/// </summary>
internal interface IPool
{
    UiPluginPool PluginPool { get; }
    
    /// <summary>
    /// Called on a pool when a plugin is unloaded
    /// </summary>
    /// <param name="pluginPool"></param>
    void OnPluginUnloaded(UiPluginPool pluginPool);

    /// <summary>
    /// Clears the pool of all items
    /// </summary>
    void ClearPoolEntities();

    /// <summary>
    /// Wipes all pools of the given type
    /// </summary>
    void RemoveAllPools();

    bool HasPoolLeaked();
    void PrintLeaks();
    
    void LogDebug(DebugLogger logger);
}