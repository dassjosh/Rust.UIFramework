using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Pooling;

internal class ConcurrentListPool<T>() : BaseObjectPool<ConcurrentList<T>, ConcurrentListPool<T>>(ConcurrentListPoolPolicy.Instance)
{
    protected override PoolSize GetPoolSize(PoolSettings settings) => settings.ListPoolSize;
    
    private sealed class ConcurrentListPoolPolicy : IPooledObjectPolicy<ConcurrentList<T>>
    {
        public static readonly ConcurrentListPoolPolicy Instance = new();
        
        public ConcurrentList<T> Create() => [];
        public void Get(ConcurrentList<T> item) { }
        public bool Return(ConcurrentList<T> item)
        {
            item.Clear();
            return true;
        }
    }
}