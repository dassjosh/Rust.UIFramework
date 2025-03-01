using System.Collections.Generic;
using System.Reflection;
using Oxide.Core;
using Oxide.Core.Extensions;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Data;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Libraries.Themes;
using Oxide.Ext.UiFramework.Libraries.UiCommands;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Threading;
using Oxide.Ext.UiFramework.Types;

// ReSharper disable once CheckNamespace
namespace Oxide.Ext.UiFramework;

public class UiFrameworkExtension : Extension
{
    public override string Name => "UiFramework";
    public override string Author => "MJSU";
    public override VersionNumber Version { get; }

    internal static UiFrameworkExtension Instance;
    internal static IUiLogger GlobalLogger;

    public UiFrameworkExtension(ExtensionManager manager) : base(manager)
    {
        Instance = this;
        AssemblyName assembly = Assembly.GetExecutingAssembly().GetName();
        Version = new VersionNumber(assembly.Version.Major, assembly.Version.Minor, assembly.Version.Build);
    }

    public override void OnModLoad()
    {
        UiFrameworkConfig.LoadConfig();
        Singleton<DataHandler>.Instance.LoadAll();
        Manager.RegisterPluginLoader(new UiFrameworkExtPluginLoader());
        Manager.RegisterLibrary(nameof(UiImageStorage), Singleton<UiImageStorage>.Instance);
        Manager.RegisterLibrary(nameof(UiCommands), Singleton<UiCommands>.Instance);
        Manager.RegisterLibrary(nameof(UiNameStore), Singleton<UiNameStore>.Instance);
        Manager.RegisterLibrary(nameof(UiPlayerStore), Singleton<UiPlayerStore>.Instance);
        Manager.RegisterLibrary(nameof(ThemeManager), Singleton<ThemeManager>.Instance);
    }

    public override IEnumerable<string> GetPreprocessorDirectives()
    {
        string name = Name.ToUpper();
        yield return $"{name}_EXT";
        for (int i = 0; i <= Version.Minor; i++)
        {
            yield return $"{name}_EXT_{Version.Major}_{i}";
        }
    }
    
    /// <summary>
    /// Called when server is shutdown
    /// </summary>
    public override void OnShutdown()
    {
        Singleton<DataHandler>.Instance.Shutdown();
        Singleton<SendHandler>.Instance.OnServerShutdown();
    }
}