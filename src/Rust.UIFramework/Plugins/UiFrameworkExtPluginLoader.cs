using System;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Plugins.Core;

namespace Oxide.Ext.UiFramework.Plugins;

internal class UiFrameworkExtPluginLoader : PluginLoader
{
    public override Type[] CorePlugins => [typeof(UiFrameworkPlugin)];
}