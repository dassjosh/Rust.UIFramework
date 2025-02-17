using System;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Plugins;

/// <summary>
/// Represents a Plugin ID
/// </summary>
internal readonly record struct PluginId
{
    /// <summary>
    /// ID of the plugin
    /// </summary>
    public readonly string Id;

    /// <summary>
    /// Returns if the PluginId is valid
    /// </summary>
    public bool IsValid => !string.IsNullOrEmpty(Id);
        
    internal PluginId(Plugin plugin) => Id = plugin?.Name ?? throw new ArgumentNullException(nameof(plugin));
    internal PluginId(string id) => Id = id ?? throw new ArgumentNullException(nameof(id));

    /// <summary>
    /// Returns the PluginName
    /// </summary>
    /// <returns></returns>
    public override string ToString() => this.PluginName();
}