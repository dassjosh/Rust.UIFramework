using Oxide.Core;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Rust.UiFramework.Benchmarks;

internal static class BenchmarkHelpers
{
    public static readonly PluginId UnitTestPluginId = PluginId.CreateInternal("BenchmarkPlugin");
    public static UiPluginPool UnitTestPool => Singleton<UiPool>.Instance.GetOrCreate(UnitTestPluginId);
    public static readonly BenchmarkPlugin Plugin = new()
    {
        PluginPool = UnitTestPool
    };

    public static UiPluginPool CreatePoolForTest(string name)
    {
        PluginId uiBuilderTestsPlugin = PluginId.CreateInternal(name);
        return Singleton<UiPool>.Instance.GetOrCreate(uiBuilderTestsPlugin);
    }
    
    public class BenchmarkPlugin : IUiFrameworkPlugin
    {
        public string Name => nameof(BenchmarkPlugin);
        public string Title => "Unit Test Plugin";
        public string Description => "Unit Test Description";
        public string Author => "MJSU";
        public VersionNumber Version => new(1, 0, 0);
        public UiPluginPool PluginPool { get; set; }
    }
}