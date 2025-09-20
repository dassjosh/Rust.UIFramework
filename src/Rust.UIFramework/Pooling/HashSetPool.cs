using System.Collections.Generic;

namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Represents a pool for <see cref="HashSet{T}"/>
/// </summary>
/// <typeparam name="T">Type that will be in the HashSet</typeparam>
internal class HashSetPool<T> : BasePool<HashSet<T>, HashSetPool<T>>
{
    protected override PoolSize GetPoolSize(PoolSettings settings) => settings.HashSetPoolSize;
    protected override HashSet<T> CreateNew() => [];

    ///<inheritdoc/>
    protected override bool OnFreeItem(HashSet<T> item)
    {
        item.Clear();
        return true;
    }
}