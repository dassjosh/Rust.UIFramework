using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Pooling;

namespace Rust.UiFramework.UnitTests;

public static class UnitTestHelpers
{
    public static UiFrameworkPluginPool GetPool(string name) => UiFrameworkPoolLib.CreateUnitTest(name);

    public static void EnterPool<T>(T poolable) where T : BasePoolable => poolable.TestEnterPool();
    public static void LeavePool<T>(T poolable) where T : BasePoolable => poolable.TestLeavePool();
}