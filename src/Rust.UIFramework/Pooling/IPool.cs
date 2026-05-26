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
    /// Clears the pool of all items
    /// </summary>
    void ClearPool();
    bool HasPoolLeaked();
    void PrintLeaks();
    void LogDebug(DebugLogger logger);
}