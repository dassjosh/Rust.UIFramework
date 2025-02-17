using System.Reflection;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Exceptions.UiCommands;

public class DuplicateUiCommandRegistration : BaseUiFrameworkException
{
    internal DuplicateUiCommandRegistration(PluginId pluginId, MethodInfo info) : base($"Failed to register command {info.Name} for {pluginId.FullName()}. Method being registered multiple times") { }
}