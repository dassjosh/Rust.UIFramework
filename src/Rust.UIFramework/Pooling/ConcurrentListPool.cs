using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Pooling;

internal class ConcurrentListPool<T>() : BaseObjectPool<ConcurrentList<T>>(ConcurrentListPoolPolicy.Instance)
{
    private sealed class ConcurrentListPoolPolicy : IPooledObjectPolicy<ConcurrentList<T>>
    {
        public static readonly ConcurrentListPoolPolicy Instance = new();

        public int GetPoolSize(PoolSettings settings) => settings.ListPoolSize;
        public ConcurrentList<T> Create() => [];
        public void Get(ConcurrentList<T> item) { }
        public bool Return(ConcurrentList<T> item)
        {
            item.Clear();
            return true;
        }
    }
}