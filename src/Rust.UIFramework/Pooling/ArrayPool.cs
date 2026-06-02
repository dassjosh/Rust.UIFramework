using System;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Helpers;

namespace Oxide.Ext.UiFramework.Pooling;

internal class ArrayPool<T> : BasePool
{
    private const int PoolSize = 16;
    private const int MaxArraySize = 1 << PoolSize;
    
    private readonly ArrayPoolInternal[] _pools = new ArrayPoolInternal[PoolSize];
    private readonly object _poolLock = new();

    public UiPooledArray<T> Get(int minSize)
    {
        if (minSize < 0) throw new ArgumentOutOfRangeException(nameof(minSize), $"{nameof(minSize)} cannot be less than 0");
        if (minSize == 0)
        {
            return UiPooledArray<T>.Empty;
        }

        if (minSize > MaxArraySize)
        {
            throw new ArgumentOutOfRangeException(nameof(minSize), $"{nameof(minSize)} cannot be greater than {MaxArraySize}");
        }

        ArrayPoolInternal pool = GetPool(minSize);
        return pool.Get();
    }

    private ArrayPoolInternal GetPool(int minSize)
    {
        uint size = BitOperations.RoundUpToPowerOf2((uint)minSize);
        uint index = BitOperations.Log2(size);
        ArrayPoolInternal pool = _pools[index];
        if (pool != null)
        {
            return pool;
        }
        lock (_poolLock)
        {
            pool = _pools[index];
            if (pool == null)
            {
                _pools[index] = pool = new ArrayPoolInternal();
                pool.InitArrayPool(PluginPool, size);
            }
            return pool;
        }
    }

    public void Free(UiPooledArray<T> item)
    {
        ArrayPoolInternal pool = GetPool(item.Count);
        pool.Free(item);
    }

    public override void ClearPool()
    {
        for (int index = 0; index < _pools.Length; index++)
        {
            ArrayPoolInternal pool = _pools[index];
            pool.ClearPool();
        }
    }

    public override bool HasPoolLeaked()
    {
        for (int index = 0; index < _pools.Length; index++)
        {
            ArrayPoolInternal pool = _pools[index];
            if (pool.HasPoolLeaked())
            {
                return true;
            }
        }

        return false;
    }

    public override void PrintLeaks()
    {
        for (int index = 0; index < _pools.Length; index++)
        {
            ArrayPoolInternal pool = _pools[index];
            pool.PrintLeaks();
        }
    }

    public override void LogDebug(DebugLogger logger)
    {
        logger.StartObject(GetType().GetRealTypeName());
        logger.StartArray("Pools");
        lock (_poolLock)
        {
            for (int i = 0; i < _pools.Length; i++)
            {
                ArrayPoolInternal pool = _pools[i];
                logger.AppendObject($"Size: {1 << i}", pool);
            }
        }
        logger.EndArray();
    }

    private sealed class ArrayPoolInternal : BaseObjectPool<UiPooledArray<T>>, IObjectPool<BasePoolable>
    {
        internal void InitArrayPool(UiPluginPool pool, uint size)
        {
            SetPolicy(new ArrayPoolInternalPolicy(size, this));
            InitPool(pool);
        }

        BasePoolable IObjectPool<BasePoolable>.Get() => base.Get();
        void IObjectPool<BasePoolable>.Free(BasePoolable item)
        {
            if (item is UiPooledArray<T> array)
            {
                base.Free(array);
                return;
            }

            throw new NotSupportedException($"Cannot Free item {item.GetType().GetRealTypeName()} to pool {GetType().GetRealTypeName()}");
        }

        private sealed class ArrayPoolInternalPolicy(uint size, ArrayPoolInternal pool) : IPooledObjectPolicy<UiPooledArray<T>>
        {
            public int GetPoolSize(PoolSettings settings) => settings.ArrayPoolSize;

            public UiPooledArray<T> Create()
            {
                UiPooledArray<T> obj = new(size);
                obj.OnInitInternal(pool);
                return obj;
            }

            public void Get(UiPooledArray<T> item) => item.LeavePoolInternal();
            public bool Return(UiPooledArray<T> item)
            {
                if (item.CanPool)
                {
                    item.EnterPoolInternal();
                    return true;
                }

                return false;
            }
        }
    }
}