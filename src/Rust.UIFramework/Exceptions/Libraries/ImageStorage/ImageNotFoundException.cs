using Oxide.Core.Plugins;

namespace Oxide.Ext.UiFramework.Exceptions;

public class ImageNotFoundException(Plugin plugin, string name) : BaseUiFrameworkException($"Image {name} not found for plugin {plugin.Name}")
{
    public readonly Plugin Plugin = plugin;
    public readonly string Name = name;
}