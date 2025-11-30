using Oxide.Core;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Rust.UiFramework.UnitTests;

internal static class UnitTestHelpers
{
    public static void EnterPool<T>(T poolable) where T : BasePoolable => poolable.TestEnterPool();
    public static void LeavePool<T>(T poolable) where T : BasePoolable => poolable.TestLeavePool();
    public static readonly PluginId UnitTestPluginId = PluginId.CreateInternal("UnitTestPlugin");
    
    public static UiPluginPool UnitTestPool => Singleton<UiPool>.Instance.GetOrCreate(UnitTestPluginId);
    public static readonly UnitTestPlugin Plugin = new()
    {
        PluginPool = UnitTestPool
    };

    public static UiPluginPool CreatePoolForTest(string name)
    {
        PluginId uiBuilderTestsPlugin = PluginId.CreateInternal(name);
        return Singleton<UiPool>.Instance.GetOrCreate(uiBuilderTestsPlugin);
    }
    
    public class UnitTestPlugin : IUiFrameworkPlugin
    {
        public string Name => nameof(UnitTestPlugin);
        public string Title => "Unit Test Plugin";
        public string Description => "Unit Test Description";
        public string Author => "MJSU";
        public VersionNumber Version => new(1, 0, 0);
        public bool IsLoaded => true;
        public UiPluginPool PluginPool { get; set; }
    }
}