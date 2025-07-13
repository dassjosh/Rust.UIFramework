using System;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;
using Oxide.Plugins;

namespace Oxide.Ext.UiFramework.Libraries;

public class UiPool : BaseUiFrameworkLibrary, ISingleton
{
    public static readonly UiPluginPool Internal = Singleton<UiPool>.Instance._internal;
    
    private readonly Hash<PluginId, UiPluginPool> _pluginPools = new();
    private readonly UiPluginPool _internal;
    private readonly IUiLogger<UiPool> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<UiPool>();

    private UiPool()
    {
        _internal = CreatePoolInternal(new PluginId(UiFrameworkExtension.Instance.Name));
        _internal.SetSettings(PoolSettings.CreateInternal());        
    }
    
    internal UiPluginPool CreateObsoletePool()
    {
        UiPluginPool obsolete = CreatePoolInternal(PluginId.CreateInternal($"{UiFrameworkExtension.Instance.Name}.Obsolete"));
        obsolete.SetSettings(PoolSettings.CreateInternal());
        return obsolete; 
    }
    
    /// <summary>
    /// Returns an existing <see cref="UiPluginPool"/> for the given plugin or returns a new one
    /// </summary>
    /// <param name="plugin">The pool the plugin is for</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Thrown if the plugin is null</exception>
    public UiPluginPool GetOrCreate(Plugin plugin)
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        return GetOrCreate(plugin.Id());
    }
    
    internal UiPluginPool GetOrCreate(PluginId plugin)
    {
        return CreatePoolInternal(plugin);
    }
    
    private UiPluginPool CreatePoolInternal(Plugin plugin) => CreatePoolInternal(plugin.Id());
    
    private UiPluginPool CreatePoolInternal(PluginId id)
    {
        UiPluginPool pool = _pluginPools[id];
        if (pool == null)
        {
            _pluginPools[id] = pool = new UiPluginPool(id);
        }

        return pool;
    }

    ///<inheritdoc/>
    protected override void OnPluginLoaded(Plugin plugin)
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        if (plugin is IUiFrameworkPlugin uiPlugin)
        {
            uiPlugin.Pool = GetOrCreate(plugin);
        }
    }

    ///<inheritdoc/>
    protected override void OnPluginUnloaded(Plugin plugin)
    {
        PluginId id = plugin.Id();
        UiPluginPool pool = _pluginPools[id];
        if (pool != null)
        {
            pool.OnPluginUnloaded();
            _pluginPools.Remove(id);
        }
    }

    internal void Clear()
    {
        foreach (UiPluginPool pool in _pluginPools.Values)
        {
            pool.Clear();
        }
    }
        
    internal void Wipe()
    {
        foreach (UiPluginPool pool in _pluginPools.Values)
        {
            pool.Wipe();
        }
    }
    
    internal void CheckForLeaks()
    {
        foreach (UiPluginPool pool in _pluginPools.Values)
        {
            pool.CheckForLeaks();
        }
    }

    ///<inheritdoc/>
    public void LogDebug(DebugLogger logger)
    {
        logger.AppendObject("Internal", _internal);
        foreach (UiPluginPool pool in _pluginPools.Values)
        {
            if (pool != _internal)
            {
                logger.AppendObject(pool.PluginName, pool);
            }
        }
    }
}