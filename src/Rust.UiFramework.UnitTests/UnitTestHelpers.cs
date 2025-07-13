using Oxide.Core.Plugins;
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
}