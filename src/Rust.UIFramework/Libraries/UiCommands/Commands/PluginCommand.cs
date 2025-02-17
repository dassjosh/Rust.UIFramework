using System;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal readonly record struct PluginCommand(PluginId Plugin, uint CommandId);