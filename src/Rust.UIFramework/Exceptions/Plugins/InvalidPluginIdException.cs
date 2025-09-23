using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Exceptions;

public class InvalidPluginIdException(string message) : BaseUiFrameworkException(message)
{
    internal static void ThrowIfInvalidPluginId(PluginId pluginId)
    {
        if (!pluginId.IsValid)
        {
            throw new InvalidPluginIdException($"Invalid plugin id: {pluginId}");
        }
    }
}