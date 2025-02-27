using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries;

internal readonly record struct PluginImage(PluginId PluginId, string Name)
{
    public bool IsValid => PluginId.IsValid && !string.IsNullOrEmpty(Name);

    public PluginImage(Plugin plugin, string name) : this(plugin.Id(), name) {}
    
    public static implicit operator string(PluginImage snowflake) => $"{snowflake.PluginId.Id}:{snowflake.Name}";
    
    public static explicit operator PluginImage(string id)
    {
        int index = id.IndexOf(':');
        if (index == -1) return default;
        
        string pluginId = id[..index];
        string name = id[(index + 1)..];
        
        return new PluginImage(new PluginId(pluginId), name);
    }
}