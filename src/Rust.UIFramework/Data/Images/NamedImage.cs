using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Data;

internal class NamedImage(PluginId pluginId, string name, ImageId imageId)
{
    public PluginId PluginId { get; set; } = pluginId;
    public string Name { get; set; } = name;
    public ImageId ImageId { get; set; } = imageId;
}