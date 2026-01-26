using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class RegisteredImagesData
{
    private readonly ConcurrentDictionary<PluginId, ConcurrentList<RegisterImageRequest>> _pluginImages = new();

    public void OnPluginImageRegistered(PluginId plugin, RegisterImageRequest request)
    {
        ConcurrentList<RegisterImageRequest> requests = _pluginImages.GetOrAdd(plugin, _ => []);
        if(!requests.Contains(request))
        {
            requests.Add(request);
        }
    }

    // public void OnPluginImageRegistrationCompleted(RegisterImageRequest request)
    // {
    //     ConcurrentList<RegisterImageRequest> requests = _pluginImages.GetOrAdd(request.PluginId, _ => []);
    //     if(requests.All(r => r.))
    // }
}