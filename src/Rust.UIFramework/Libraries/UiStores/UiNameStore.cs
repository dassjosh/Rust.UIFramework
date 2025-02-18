using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public delegate INamedStore CreateNameStore(string name);

public class UiNameStore : BaseUiFrameworkLibrary, ISingleton
{
    private readonly Dictionary<PluginNamedStore, INamedStore> _stores = new();
    private readonly Dictionary<PluginId, CreateNameStore> _storeCreators = new();

    private UiNameStore() { }
    
    public void RegisterStore(Plugin plugin, CreateNameStore creator)
    {
        _storeCreators[plugin.Id()] = creator;
    }

    public T GetOrCreateStore<T>(Plugin plugin, string name) where T : INamedStore => (T)GetOrCreateStore<T>(plugin.Id(), name);

    internal INamedStore GetOrCreateStore<T>(PluginId pluginId, string name)
    {
        PluginNamedStore key = new(pluginId, name);
        if (_stores.TryGetValue(key, out INamedStore value))
        {
            return value;
        }

        if (_storeCreators.TryGetValue(key.PluginId, out CreateNameStore creator))
        {
            _stores[key] = value = creator(name);
            return value;
        }

        value = (INamedStore)Activator.CreateInstance(typeof(T), name);
        _stores[key] = value;
        return value;
    }

    protected override void OnPluginUnloaded(Plugin plugin)
    {
        PluginId pluginId = plugin.Id();
        _stores.RemoveAll(s => s.Key.PluginId == pluginId);
        _storeCreators.Remove(pluginId);
    }

    private readonly record struct PluginNamedStore(PluginId PluginId, string Name);
}