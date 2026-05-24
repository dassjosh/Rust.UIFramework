using System.Collections.Generic;

namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Represents a pool for <see cref="HashSet{T}"/>
/// </summary>
/// <typeparam name="T">Type that will be in the HashSet</typeparam>
internal class HashSetPool<T>() : BaseObjectPool<HashSet<T>, HashSetPool<T>>(HashSetPoolPolicy.Instance)
{
    protected override PoolSize GetPoolSize(PoolSettings settings) => settings.HashSetPoolSize;
    
    private sealed class HashSetPoolPolicy : IPooledObjectPolicy<HashSet<T>>
    {
        public static readonly HashSetPoolPolicy Instance = new();
        
        public HashSet<T> Create() => [];
        public void Get(HashSet<T> item) { }
        public bool Return(HashSet<T> item)
        {
            item.Clear();
            return true;
        }
    }
}