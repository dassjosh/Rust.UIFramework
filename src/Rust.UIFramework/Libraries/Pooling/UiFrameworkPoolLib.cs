using System;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;
using Oxide.Plugins;

namespace Oxide.Ext.UiFramework.Libraries;

public class UiFrameworkPoolLib : BaseUiFrameworkLibrary, ISingleton
{
    private readonly Hash<PluginId, UiFrameworkPluginPool> _pluginPools = new();
    internal UiFrameworkPluginPool Internal;
    internal UiFrameworkPluginPool Obsolete;
    private readonly IUiLogger<UiFrameworkPoolLib> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<UiFrameworkPoolLib>();

    private UiFrameworkPoolLib()
    {
        Internal = CreatePoolInternal(new PluginId(UiFrameworkExtension.Instance.Name));
        Internal.SetSettings(PoolSettings.CreateInternal());        
        
        Obsolete = CreatePoolInternal(new PluginId($"{UiFrameworkExtension.Instance.Name}.Obsolete"));
        Obsolete.SetSettings(PoolSettings.CreateInternal());
    }
    
    /// <summary>
    /// Returns an existing <see cref="UiFrameworkPluginPool"/> for the given plugin or returns a new one
    /// </summary>
    /// <param name="plugin">The pool the plugin is for</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Thrown if the plugin is null</exception>
    public UiFrameworkPluginPool GetOrCreate(Plugin plugin)
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        return CreatePoolInternal(plugin);
    }

#if UNIT_TESTS
    internal static UiFrameworkPluginPool CreateUnitTest(string name)
    {
        return new UiFrameworkPluginPool(new PluginId(name));
    }
#endif
    
    private UiFrameworkPluginPool CreatePoolInternal(Plugin plugin) => CreatePoolInternal(plugin.Id());
    
    private UiFrameworkPluginPool CreatePoolInternal(PluginId id)
    {
        UiFrameworkPluginPool pool = _pluginPools[id];
        if (pool == null)
        {
            _pluginPools[id] = pool = new UiFrameworkPluginPool(id);
        }

        return pool;
    }

    ///<inheritdoc/>
    protected override void OnPluginLoaded(Plugin plugin)
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        if (plugin is IUiFrameworkPool pool)
        {
            pool.Pool = GetOrCreate(plugin);
        }
    }

    ///<inheritdoc/>
    protected override void OnPluginUnloaded(Plugin plugin)
    {
        PluginId id = plugin.Id();
        UiFrameworkPluginPool pool = _pluginPools[id];
        if (pool != null)
        {
            pool.OnPluginUnloaded();
            _pluginPools.Remove(id);
        }
    }

    internal void Clear()
    {
        foreach (UiFrameworkPluginPool pool in _pluginPools.Values)
        {
            pool.Clear();
        }
    }
        
    internal void Wipe()
    {
        foreach (UiFrameworkPluginPool pool in _pluginPools.Values)
        {
            pool.Wipe();
        }
    }
    
    internal void CheckForLeaks()
    {
        foreach (UiFrameworkPluginPool pool in _pluginPools.Values)
        {
            pool.CheckForLeaks();
        }
    }

    ///<inheritdoc/>
    public void LogDebug(DebugLogger logger)
    {
        logger.AppendObject("Internal", Internal);
        foreach (UiFrameworkPluginPool pool in _pluginPools.Values)
        {
            if (pool != Internal)
            {
                logger.AppendObject(pool.PluginName, pool);
            }
        }
    }
}