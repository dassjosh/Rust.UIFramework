using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries;

internal class BorderRadiusRequestHandler(PluginId id, string name, BorderRadiusData data): RegisterImageRequestHandler(id), IBorderRadiusRequestHandler
{
    public string Name { get; } = name;
    public BorderRadiusData Data { get; } = data;
}