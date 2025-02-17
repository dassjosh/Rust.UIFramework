using System.Linq;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Libraries.Covalence;
using Oxide.Plugins;

namespace Oxide.Ext.UiFramework.Cache;

internal static class OxideLibrary 
{
    internal static readonly Covalence Covalence = Interface.Oxide.GetLibrary<Covalence>();
    internal static readonly Permission Permission = Interface.Oxide.GetLibrary<Permission>();
    internal static readonly Lang Lang = Interface.Oxide.GetLibrary<Lang>();
    internal static readonly Core.Libraries.Plugins Plugins = Interface.Oxide.GetLibrary<Core.Libraries.Plugins>();
    internal static readonly CSharpPluginLoader PluginLoader = Interface.Oxide.GetPluginLoaders().OfType<CSharpPluginLoader>().FirstOrDefault();
}