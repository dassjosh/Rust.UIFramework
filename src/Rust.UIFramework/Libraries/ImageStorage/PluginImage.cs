using Oxide.Ext.UiFramework.Plugins;
using ProtoBuf;

namespace Oxide.Ext.UiFramework.Libraries;

[ProtoContract]
internal readonly record struct PluginImage([property: ProtoMember(1)] PluginId PluginId, [property: ProtoMember(2)] string Name)
{
    public bool IsValid => PluginId.IsValid && !string.IsNullOrEmpty(Name);
    
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