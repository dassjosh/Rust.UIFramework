using HarmonyLib;
using Oxide.Core;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Rust.UiFramework.UnitTests.Mocks.Plugins;

public class UiFrameworkCorePluginMock : IUiFrameworkCorePlugin
{
    public string Name => nameof(UiFrameworkCorePluginMock);
    public string Title => "UI Framework Core Mock";
    public string Description => "UI Framework Core Mock Description";
    public string Author => "MJSU";
    public VersionNumber Version => new(1, 0, 0);
    public bool IsLoaded => true;
    public object CallHook(string hook, params object[] args) { return null; }
    public UiPluginPool PluginPool { get; set; }
    public Harmony Harmony => new(nameof(UiFrameworkCorePluginMock));

    public UiFrameworkCorePluginMock()
    {
        PluginPool = Singleton<UiPool>.Instance.GetOrCreate(this);
    }
}