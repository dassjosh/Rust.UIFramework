using System.Reflection;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Exceptions.UiCommands;

public class MissingUiCommandAttributeException : BaseUiFrameworkException
{
    internal MissingUiCommandAttributeException(PluginId pluginId, MethodInfo info) : base($"Failed to register command for {pluginId.FullName()}. Method: {info.Name} has no UiCommandAttribute")
    {
        
    }
}