using System.Collections.Generic;

namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Represents a pool for <see cref="List{T}"/>
/// </summary>
/// <typeparam name="T">Type that will be in the list</typeparam>
internal class ListPool<T> : BasePool<List<T>, ListPool<T>>
{
    protected override PoolSize GetPoolSize(PoolSettings settings) => settings.ListPoolSize;
    protected override List<T> CreateNew() => [];

    ///<inheritdoc/>
    protected override bool OnFreeItem(List<T> item)
    {
        item.Clear();
        return true;
    }
}