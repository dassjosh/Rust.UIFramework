using System;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Plugins;

namespace Oxide.Ext.UiFramework.Extensions;

/// <summary>
/// Extension methods for plugins
/// </summary>
internal static class PluginExt
{
    private static readonly Hash<PluginId, string> FullNameCache = new();

    internal static PluginId Id(this Plugin plugin) => new(plugin.Name);

    internal static string PluginName(this Plugin plugin) => plugin?.Name ?? throw new ArgumentNullException(nameof(plugin));
    internal static string PluginName(this PluginId pluginId) => pluginId.Id;

    internal static string FullName(this Plugin plugin)
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        PluginId pluginId = plugin.Id();
        string name = FullNameCache[pluginId];
        if (name == null)
        {
            FullNameCache[pluginId] = name = CreatePluginFullName(plugin);
        }

        return name;
    }
        
    internal static string FullName(this PluginId pluginId)
    {
        if (!pluginId.IsValid)
        {
            return $"Invalid Plugin ID: {pluginId.Id}";
        }
        
        if (FullNameCache.TryGetValue(pluginId, out string fullName))
        {
            return fullName;
        }

        Plugin plugin = OxideLibrary.Plugins.Find(pluginId.Id);
        if (plugin != null)
        {
            return plugin.FullName();
        }
            
        return $"Invalid Plugin ID: {pluginId.Id}";
    }

    internal static void OnPluginUnloaded(Plugin plugin)
    {
        PluginId id = plugin.Id();
        FullNameCache.Remove(id);
    }

    private static string CreatePluginFullName(Plugin plugin) => $"{plugin.Name} by {plugin.Author} v{plugin.Version}";
}