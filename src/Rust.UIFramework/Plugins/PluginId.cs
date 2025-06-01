using System;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Extensions;
using ProtoBuf;

namespace Oxide.Ext.UiFramework.Plugins;

/// <summary>
/// Represents a Plugin ID
/// </summary>
[ProtoContract]
internal readonly record struct PluginId
{
    /// <summary>
    /// ID of the plugin
    /// </summary>
    [ProtoMember(1)]
    public readonly string Id;

    /// <summary>
    /// Returns if the PluginId is valid
    /// </summary>
    [ProtoIgnore]
    public bool IsValid => !string.IsNullOrEmpty(Id);
    
    [ProtoIgnore]
    internal bool IsExtensionPlugin => IsValid && this == UiFrameworkPlugin.Instance.PluginId;
        
    internal PluginId(Plugin plugin) => Id = plugin?.Name ?? throw new ArgumentNullException(nameof(plugin));
    internal PluginId(string id) => Id = id ?? throw new ArgumentNullException(nameof(id));

    /// <summary>
    /// Returns the PluginName
    /// </summary>
    /// <returns></returns>
    public override string ToString() => this.PluginName();
}