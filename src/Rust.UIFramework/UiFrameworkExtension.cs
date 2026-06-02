using System.Collections.Generic;
using System.Reflection;
using Cysharp.Threading.Tasks;
using Oxide.Core;
using Oxide.Core.Extensions;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Data;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Libraries.ImagePrecache;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Threading;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework;

public class UiFrameworkExtension : Extension
{
    public override string Name => "UiFramework";
    public override string Author => "MJSU";
    public override VersionNumber Version { get; }

    internal readonly PluginId PluginId;

    internal static UiFrameworkExtension Instance;
    internal static IUiLogger GlobalLogger;

    public UiFrameworkExtension(ExtensionManager manager) : base(manager)
    {
        Instance = this;
        AssemblyName assembly = Assembly.GetExecutingAssembly().GetName();
        Version = new VersionNumber(assembly.Version.Major, assembly.Version.Minor, assembly.Version.Build);
        PluginId = PluginId.CreateInternal(Name);
    }

    public override void OnModLoad()
    {
        Singleton<DataHandler>.Instance.LoadAll();
        GlobalLogger = Singleton<UiLoggerFactory>.Instance.CreateGlobalLogger();
        GlobalLogger.Info($"Using UiFramework v{Version}");
        OxideLibrary.ExtensionManager = Manager;
        Manager.RegisterPluginLoader(new UiFrameworkExtPluginLoader());
        if (UiFrameworkConfig.Instance.ImageDatabase.Enabled)
        {
            GlobalLogger.Debug("Using {0} Image DB", nameof(UiImageDatabase));
            Manager.RegisterLibrary(nameof(IImageDatabase), Singleton<UiImageDatabase>.Instance);
        }
        else
        {
            GlobalLogger.Debug("Using {0} Image DB", nameof(UiFileStorageDatabase));
            Manager.RegisterLibrary(nameof(IImageDatabase), Singleton<UiFileStorageDatabase>.Instance);
        }
        Manager.RegisterLibrary(nameof(UiImageStorage), Singleton<UiImageStorage>.Instance);
        Manager.RegisterLibrary(nameof(UiImagePrecache), Singleton<UiImagePrecache>.Instance);
        Manager.RegisterLibrary(nameof(UiCommands), Singleton<UiCommands>.Instance);
        Manager.RegisterLibrary(nameof(UiNameStore), Singleton<UiNameStore>.Instance);
        Manager.RegisterLibrary(nameof(UiPlayerStore), Singleton<UiPlayerStore>.Instance);
        Manager.RegisterLibrary(nameof(ThemeManager), Singleton<ThemeManager>.Instance);
        Manager.RegisterLibrary(nameof(UiPlayerAvatars), Singleton<UiPlayerAvatars>.Instance);
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
    /// Called when the server is shutdown
    /// </summary>
    public override void OnShutdown()
    {
        Singleton<DataHandler>.Instance.Shutdown();
        Singleton<SendHandler>.Instance.OnServerShutdown();
        Singleton<AnimationHandler>.Instance.OnServerShutdown();
        Singleton<AnimationTrackerChannel>.Instance.OnServerShutdown();
        Singleton<UiLoggerFactory>.Instance.OnServerShutdown();
    }
}