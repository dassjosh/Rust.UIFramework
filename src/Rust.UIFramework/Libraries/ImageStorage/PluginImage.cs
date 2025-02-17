using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries;

internal readonly record struct PluginImage(PluginId PluginId, string Name)
{
    public bool IsValid => PluginId.IsValid && !string.IsNullOrEmpty(Name);

    public PluginImage(Plugin plugin, string name) : this(plugin.Id(), name) {}
}