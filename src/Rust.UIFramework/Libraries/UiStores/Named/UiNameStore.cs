using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public delegate INamedStore CreateNamedStore(string name);

public class UiNameStore : BaseUiFrameworkLibrary, ISingleton
{
    private readonly Dictionary<PluginNamedStore, INamedStore> _stores = new();
    private readonly Dictionary<PluginId, CreateNamedStore> _pluginData = [];
    private readonly IUiLogger<UiPlayerStore> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<UiPlayerStore>();

    private UiNameStore() { }

    public void RegisterStore(IUiFrameworkPlugin plugin, CreateNamedStore creator)
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        _pluginData[plugin.Id()] = creator ?? throw new ArgumentNullException(nameof(creator));
    }
    
    public void CreateStore<T>(IUiFrameworkPlugin plugin, T store) where T : class, INamedStore
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        if (store == null) throw new ArgumentNullException(nameof(store));
        InvalidStoreNameException.ThrowIfInvalidStoreName(store.Name);
        _stores[new PluginNamedStore(plugin.Id(), store.Name)] = store;
    }

    public T GetStore<T>(IUiFrameworkPlugin plugin, string name) where T : class, INamedStore
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        InvalidStoreNameException.ThrowIfInvalidStoreName(name);
        if(_stores.TryGetValue(new PluginNamedStore(plugin.Id(), name), out INamedStore store))
        {
            return (T)store;
        }

        return default;
    }
    
    public T GetOrCreateStore<T>(IUiFrameworkPlugin plugin, string name) where T : class, INamedStore
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        return (T)GetOrCreateStore(plugin.Id(), name);
    }

    public void RemoveStore(IUiFrameworkPlugin plugin, string name)
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        InvalidStoreNameException.ThrowIfInvalidStoreName(name);
        _stores.Remove(new PluginNamedStore(plugin.Id(), name));
    }

    internal INamedStore GetOrCreateStore(PluginId pluginId, string name)
    {
        InvalidStoreNameException.ThrowIfInvalidStoreName(name);
        PluginNamedStore key = new(pluginId, name);
        if (_stores.TryGetValue(key, out INamedStore value))
        {
            return value;
        }

        if (_pluginData.TryGetValue(pluginId, out CreateNamedStore creator))
        {
            value = creator(name);
            _stores[key] = value;
            return value;
        }

        _logger.Error("No named store found for name '{0}' and no creator found for plugin '{1}'", name, pluginId);
        return default;
    }

    protected override void OnPluginUnloaded(IUiFrameworkPlugin plugin)
    {
        PluginId pluginId = plugin.Id();
        _stores.RemoveAll(s => s.Key.PluginId == pluginId);
        _pluginData.Remove(pluginId);
    }

    private readonly record struct PluginNamedStore(PluginId PluginId, string Name);
}