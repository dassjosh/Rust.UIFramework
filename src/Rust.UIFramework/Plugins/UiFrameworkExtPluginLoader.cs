using System;
using Oxide.Core.Plugins;

namespace Oxide.Ext.UiFramework.Plugins;

internal class UiFrameworkExtPluginLoader : PluginLoader
{
    public override Type[] CorePlugins => [typeof(UiFrameworkPlugin)];
}