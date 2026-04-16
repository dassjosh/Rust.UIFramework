using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Represents a pool for <see cref="ConcurrentHashSet{T}"/>
/// </summary>
/// <typeparam name="T">Type that will be in the HashSet</typeparam>
internal class ConcurrentHashSetPool<T> : BaseObjectPool<ConcurrentHashSet<T>, ConcurrentHashSetPool<T>>
{
    protected override PoolSize GetPoolSize(PoolSettings settings) => settings.HashSetPoolSize;
    protected override ConcurrentHashSet<T> CreateNew() => [];

    ///<inheritdoc/>
    protected override bool OnFreeItem(ConcurrentHashSet<T> item)
    {
        item.Clear();
        return true;
    }
}