using Oxide.Core.Libraries;
using Oxide.Core.Libraries.Covalence;
using Oxide.Plugins;

namespace Oxide.Ext.UiFramework.Cache;

internal static class OxideLibrary 
{
#if !UNIT_TESTS && !BENCHMARKS
    internal static readonly Covalence Covalence = Interface.Oxide.GetLibrary<Covalence>();
    internal static readonly Permission Permission = Interface.Oxide.GetLibrary<Permission>();
    internal static readonly Lang Lang = Interface.Oxide.GetLibrary<Lang>();
    internal static readonly Core.Libraries.Plugins Plugins = Interface.Oxide.GetLibrary<Core.Libraries.Plugins>();
    internal static readonly CSharpPluginLoader PluginLoader = Interface.Oxide.GetPluginLoaders().OfType<CSharpPluginLoader>().FirstOrDefault();
    
    internal static readonly string ConfigFolder = Interface.Oxide.ConfigDirectory;
    internal static readonly string DataFolder = Interface.Oxide.DataDirectory;
    internal static readonly string LogFolder = Interface.Oxide.LogDirectory;
#else

    internal static readonly Covalence Covalence = null;
    internal static readonly Permission Permission = null;
    internal static readonly Lang Lang = null;
    internal static readonly Core.Libraries.Plugins Plugins = null;
    internal static readonly CSharpPluginLoader PluginLoader = null;

    internal const string ConfigFolder = "oxide/config";
    internal const string DataFolder = "oxide/data";
    internal const string LogFolder = "oxide/log";
#endif

}