using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Logging;

namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Represents a BasePool in UiFramework
/// </summary>
public abstract class BasePool : IPool, IDebugLoggable
{
    /// <summary>
    /// Owner Plugin Pool
    /// </summary>
    public UiPluginPool PluginPool { get; private set; }
    
    internal void InitPool(UiPluginPool pluginPool)
    {
        PluginPool = pluginPool;
        OnInit(pluginPool);
        UiFrameworkExtension.GlobalLogger.Debug("Creating Pool. Plugin ID: {0} Type: {1}", pluginPool.PluginName, GetType().GetRealTypeName());
    }

    protected virtual void OnInit(UiPluginPool pluginPool) {}

    /// <summary>
    /// Clears the pool of all pooled objects and resets state to when the pool was first created
    /// </summary>
    public abstract void ClearPool();
    public abstract bool HasPoolLeaked();
    public abstract void PrintLeaks();

    ///<inheritdoc/>
    public abstract void LogDebug(DebugLogger logger);
}