using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Pooling;

// ReSharper disable once UnusedTypeParameter
internal static class PoolId<T> where T : BasePool
{
    public static readonly int Id = IdGen<BasePool>.GetNextId();
}

// ReSharper disable once UnusedTypeParameter
internal static class CustomPoolId<T> where T : ICustomPool
{
    public static readonly int Id = IdGen<ICustomPool>.GetNextId();
}