using Oxide.Ext.UiFramework.Plugins;
using ProtoBuf;

namespace Oxide.Ext.UiFramework.Libraries;

[ProtoContract]
internal readonly record struct PluginImage([property: ProtoMember(1)] PluginId PluginId, [property: ProtoMember(2)] string Name)
{
    public bool IsValid => PluginId.IsValid && !string.IsNullOrEmpty(Name);
}