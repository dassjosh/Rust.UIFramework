using System.Collections.Generic;

namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Represents a pool for <see cref="List{T}"/>
/// </summary>
/// <typeparam name="T">Type that will be in the list</typeparam>
internal class ListPool<T>() : BaseObjectPool<List<T>, ListPool<T>>(ListPoolPolicy.Instance)
{
    protected override PoolSize GetPoolSize(PoolSettings settings) => settings.ListPoolSize;

    private sealed class ListPoolPolicy : IPooledObjectPolicy<List<T>>
    {
        public static readonly ListPoolPolicy Instance = new();

        public List<T> Create() => [];
        public void Get(List<T> item) { }
        public bool Return(List<T> item)
        {
            item.Clear();
            return true;
        }
    }
}