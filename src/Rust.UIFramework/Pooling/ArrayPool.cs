using System;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Helpers;

namespace Oxide.Ext.UiFramework.Pooling;

internal class ArrayPool<T> : BasePool<UiPooledArray<T>, ArrayPool<T>>
{
    private const int PoolSize = 16;
    private const int MaxArraySize = 1 << PoolSize;
    
    private readonly ArrayPoolInternal[] _pools = new ArrayPoolInternal[PoolSize];
    private readonly object _poolLock = new();
    
    protected override void OnInit(UiPluginPool pluginPool) { }

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
        if (pool == null)
        {
            lock (_poolLock)
            {
                pool = _pools[index];
                if (pool == null)
                {
                    _pools[index] = pool = new ArrayPoolInternal();
                    pool.InitArrayPool(PluginPool, size);
                }
            }
        }

        return pool;
    }

    public override void Free(UiPooledArray<T> item)
    {
        ArrayPoolInternal pool = GetPool(item.Count);
        pool.Free(item);
    }

    public override void ClearPoolEntities()
    {
        for (int index = 0; index < _pools.Length; index++)
        {
            ArrayPoolInternal pool = _pools[index];
            pool.ClearPoolEntities();
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

    private sealed class ArrayPoolInternal : BaseObjectPool<UiPooledArray<T>, ArrayPoolInternal>, IObjectPool<BasePoolable>
    {
        private uint _size;
        
        protected override PoolSize GetPoolSize(PoolSettings settings) => settings.ArrayPoolSize;

        internal void InitArrayPool(UiPluginPool pool, uint size)
        {
            _size = size;
            InitPool(pool);
        }

        protected override UiPooledArray<T> CreateNew()
        {
            UiPooledArray<T> obj = new(_size);
            obj.OnInitInternal(this);
            return obj;
        }
        
        protected override void OnGetItem(UiPooledArray<T> item)
        {
            item.LeavePoolInternal();
        }
        
        protected override bool OnFreeItem(UiPooledArray<T> item)
        {
            if (item.CanPool)
            {
                item.EnterPoolInternal();
                return true;
            }

            return false;
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
    }
}