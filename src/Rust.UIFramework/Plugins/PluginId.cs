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

    [ProtoIgnore] internal bool IsExtensionPlugin => IsValid && this == UiFrameworkExtension.Instance.PluginId;
        
    internal PluginId(Plugin plugin) => Id = plugin?.Name ?? throw new ArgumentNullException(nameof(plugin));
    internal PluginId(string id) => Id = id ?? throw new ArgumentNullException(nameof(id));

    internal static PluginId CreateInternal(Plugin plugin)
    {
        PluginId pluginId = new(plugin);
        PluginIdExt.RegisterInternalPluginId(pluginId);
        return pluginId;
    }
    
    internal static PluginId CreateInternal(string id)
    {
        PluginId pluginId = new(id);
        PluginIdExt.RegisterInternalPluginId(pluginId);
        return pluginId;
    }

    /// <summary>
    /// Returns the PluginName
    /// </summary>
    /// <returns></returns>
    public override string ToString() => this.PluginName();
}