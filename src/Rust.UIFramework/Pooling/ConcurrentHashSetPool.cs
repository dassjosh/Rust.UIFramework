using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Represents a pool for <see cref="ConcurrentHashSet{T}"/>
/// </summary>
/// <typeparam name="T">Type that will be in the HashSet</typeparam>
internal class ConcurrentHashSetPool<T>() : BaseObjectPool<ConcurrentHashSet<T>>(ConcurrentHashSetPoolPolicy.Instance)
{
    protected override PoolSize GetPoolSize(PoolSettings settings) => settings.HashSetPoolSize;
    
    private sealed class ConcurrentHashSetPoolPolicy : IPooledObjectPolicy<ConcurrentHashSet<T>>
    {
        public static readonly ConcurrentHashSetPoolPolicy Instance = new();
        
        public ConcurrentHashSet<T> Create() => [];
        public void Get(ConcurrentHashSet<T> item) { }
        public bool Return(ConcurrentHashSet<T> item)
        {
            item.Clear();
            return true;
        }
    }
}