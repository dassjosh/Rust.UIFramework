using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;


public class UiNameStore : BaseUiFrameworkLibrary, ISingleton
{
    private readonly Dictionary<PluginNamedStore, INamedStore> _stores = new();

    private UiNameStore() { }

    public void CreateStore<T>(Plugin plugin, T store) where T : class, INamedStore
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        if (store == null) throw new ArgumentNullException(nameof(store));
        InvalidStoreNameException.ThrowIfInvalidStoreName(store.Name);
        _stores[new PluginNamedStore(plugin.Id(), store.Name)] = store;
    }

    public T GetStore<T>(Plugin plugin, string name) where T : class, INamedStore
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        InvalidStoreNameException.ThrowIfInvalidStoreName(name);
        return (T)_stores[new PluginNamedStore(plugin.Id(), name)];
    }

    public void RemoveStore(Plugin plugin, string name)
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        InvalidStoreNameException.ThrowIfInvalidStoreName(name);
        _stores.Remove(new PluginNamedStore(plugin.Id(), name));
    }

    public T GetOrCreateStore<T>(Plugin plugin, string name) where T : class, INamedStore
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        return (T)GetOrCreateStore<T>(plugin.Id(), name);
    }

    internal INamedStore GetOrCreateStore<T>(PluginId pluginId, string name)
    {
        InvalidStoreNameException.ThrowIfInvalidStoreName(name);
        PluginNamedStore key = new(pluginId, name);
        if (_stores.TryGetValue(key, out INamedStore value))
        {
            return value;
        }

        value = (INamedStore)Activator.CreateInstance(typeof(T));
        value!.Name = name;
        _stores[key] = value;
        return value;
    }

    protected override void OnPluginUnloaded(Plugin plugin)
    {
        PluginId pluginId = plugin.Id();
        _stores.RemoveAll(s => s.Key.PluginId == pluginId);
    }

    private readonly record struct PluginNamedStore(PluginId PluginId, string Name);
}