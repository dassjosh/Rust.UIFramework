using System.Collections.Concurrent;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public class UiChannels : BaseUiFrameworkLibrary, ISingleton
{
    private readonly ConcurrentDictionary<PluginId, ConcurrentBag<IUiChannel>> _channels = new();

    private UiChannels() { }

    public UiChannel<T> Create<T>(IUiFrameworkPlugin plugin, UiChannelOptions options, IUiChannelHandler<T> handler = null) where T : IBaseUiChannelObject => Create(plugin.Id(), options, handler);

    private UiChannel<T> Create<T>(PluginId pluginId, UiChannelOptions options, IUiChannelHandler<T> handler = null) where T : IBaseUiChannelObject
    {
        ConcurrentBag<IUiChannel> channels = _channels.GetOrAdd(pluginId, []);
        UiChannel<T> channel = new(options, handler);
        channels.Add(channel);
        return channel;
    }

    protected override void OnPluginUnloaded(IUiFrameworkPlugin plugin)
    {
        _channels.TryRemove(plugin.Id(), out ConcurrentBag<IUiChannel> channels);
        foreach (IUiChannel channel in channels)
        {
            channel.Stop();
        }
    }

    protected override void OnServerShutdown()
    {
        foreach (KeyValuePair<PluginId, ConcurrentBag<IUiChannel>> pluginChannels in _channels)
        {
            foreach (IUiChannel channel in pluginChannels.Value)
            {
                channel.Stop();
            }
        }

        _channels.Clear();
    }
}