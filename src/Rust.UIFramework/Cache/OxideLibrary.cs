using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Libraries.Covalence;
using Oxide.Plugins;

namespace Oxide.Ext.UiFramework.Cache;

internal static class OxideLibrary 
{
#if SERVER
    internal static readonly Covalence Covalence = Interface.Oxide.GetLibrary<Covalence>();
    internal static readonly Permission Permission = Interface.Oxide.GetLibrary<Permission>();
    internal static readonly Lang Lang = Interface.Oxide.GetLibrary<Lang>();
    internal static readonly Core.Libraries.Plugins Plugins = Interface.Oxide.GetLibrary<Core.Libraries.Plugins>();
    internal static readonly CSharpPluginLoader PluginLoader = Interface.Oxide.GetPluginLoaders().OfType<CSharpPluginLoader>().FirstOrDefault();
    
    internal static readonly string ConfigFolder = Interface.Oxide.ConfigDirectory;
    internal static readonly string DataFolder = Interface.Oxide.DataDirectory;
    internal static readonly string LogFolder = Interface.Oxide.LogDirectory;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void LogInfo(string message) => Interface.Oxide.LogInfo(message); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void LogWarning(string message) => Interface.Oxide.LogWarning(message); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void LogError(string message) => Interface.Oxide.LogError(message); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void LogException(string message, Exception exception) => Interface.Oxide.LogException(message, exception); 
#else

    internal static readonly Covalence Covalence = null;
    internal static readonly Permission Permission = null;
    internal static readonly Lang Lang = null;
    internal static readonly Core.Libraries.Plugins Plugins = null;
    internal static readonly CSharpPluginLoader PluginLoader = null;

    internal const string ConfigFolder = "oxide/config";
    internal const string DataFolder = "oxide/data";
    internal const string LogFolder = "oxide/log";
    internal static void LogInfo(string message) => Console.WriteLine(message);
    internal static void LogWarning(string message) => Console.WriteLine(message);
    internal static void LogError(string message) => Console.WriteLine(message);
    internal static void LogException(string message, Exception exception) => Console.WriteLine($"{message}\n{exception}");
#endif

}