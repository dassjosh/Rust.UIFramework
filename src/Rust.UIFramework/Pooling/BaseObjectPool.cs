using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Logging;

namespace Oxide.Ext.UiFramework.Pooling;

public abstract class BaseObjectPool<TPooled, TPool> : BasePool<TPooled, TPool>, IObjectPool<TPooled>
    where TPooled : class
    where TPool : BasePool<TPooled, TPool>, new()
{
    private PoolSize _size;
    private TPooled[] _pool;
    private int _index;
    private LeakHandler _leakHandler;
#if TRACE_LEAKS
    private readonly ConditionalWeakTable<TPooled, InstanceTracker> _instanceTracker = new();
    private readonly ConditionalWeakTable<TPooled, LifetimeTracker> _lifetimeTracker = new();
#endif

    protected override void OnInit(UiPluginPool pluginPool)
    {
        _size = GetPoolSize(pluginPool.Settings);
        InvalidPoolException.ThrowIfInvalidPoolSize(_size);
        _pool = new TPooled[_size.StartingSize];
    }

    /// <summary>
    /// Returns the pool size from the pool settings for the pool
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    protected abstract PoolSize GetPoolSize(PoolSettings settings);

    /// <summary>
    /// Returns an element from the pool if it exists else it creates a new one
    /// </summary>
    /// <returns></returns>
    public TPooled Get()
    {
        TPooled item = null;
        int index = Interlocked.Increment(ref _index) - 1; //We want the previous index before the increment here
        if (index >= _pool.Length && _size.CanResize(_pool.Length))
        {
            lock (PoolLock)
            {
                if (index >= _pool.Length && _size.CanResize(_pool.Length))
                {
                    int nextSize = PoolSize.GetNextSize(_pool.Length);
                    UiFrameworkExtension.GlobalLogger.Debug("{0} Resizing Pool {1} Current Size: {2} Next Size: {3}", PluginPool.PluginName, GetType(), _pool.Length, nextSize);
                    Array.Resize(ref _pool, nextSize);
                }
            }
        }

        if (index < _pool.Length)
        {
            item = _pool[index];
            _pool[index] = null;
        }
        else
        {
            HandleLeak(index);
        }

        item ??= CreateNew();

#if TRACE_LEAKS
        _instanceTracker.Add(item, new InstanceTracker());
        LifetimeTracker tracker = _lifetimeTracker.GetOrCreateValue(item);
        tracker.OnGet();
#endif

        OnGetItem(item);
        return item;
    }

    /// <summary>
    /// Frees an item back to the pool
    /// </summary>
    /// <param name="item">Item being freed</param>
    public override void Free(TPooled item)
    {
        if (item == null)
        {
            return;
        }

        if (!OnFreeItem(item))
        {
            return;
        }

#if TRACE_LEAKS
        for (int poolIndex = 0; poolIndex < _pool.Length; poolIndex++)
        {
            TPooled pooled = _pool[poolIndex];
            if (pooled == item)
            {
                throw new Exception("Returned item is already in the pool!");
            }
        }
#endif

        int index = Interlocked.Decrement(ref _index);
        if (index >= 0)
        {
            _pool[index] = item;
#if TRACE_LEAKS
            if (_instanceTracker.TryGetValue(item, out InstanceTracker tracker))
            {
                _instanceTracker.Remove(item);
                tracker.Dispose();
            }

            if (_lifetimeTracker.TryGetValue(item, out LifetimeTracker lifetime))
            {
                lifetime.OnFree();
            }
#endif
        }
        else
        {
            Interlocked.Exchange(ref _index, 0);
#if TRACE_LEAKS
            if (_instanceTracker.TryGetValue(item, out InstanceTracker tracker))
            {
                tracker.AddAdditionalData("Index < 0");
            }
#endif
        }
    }

    /// <summary>
    /// Creates new type of T
    /// </summary>
    /// <returns>Newly created type of T</returns>
    protected abstract TPooled CreateNew();

    public override void ClearPoolEntities()
    {
        lock (PoolLock)
        {
            _pool.Clear();
        }
    }

    private void HandleLeak(int index)
    {
        LeakHandler leak = _leakHandler ??= new LeakHandler(PluginPool.PluginId, GetType().ToString());
        leak.OnLeak(index, _pool.Length);
    }

    public override bool HasPoolLeaked()
    {
        if (_index != 0)
        {
            UiFrameworkExtension.GlobalLogger.Error("Plugin: {0} Pool: {1} Has Leaked {2}/{3} Entities", PluginPool.PluginName, GetType().GetRealTypeName(), _index, _pool.Length);
            return true;
        }

        return false;
    }

    public override void PrintLeaks()
    {
#if TRACE_LEAKS
        foreach (KeyValuePair<TPooled, InstanceTracker> tracker in _instanceTracker)
        {
            tracker.Value.PrintIfLeaked();
        }

        foreach (KeyValuePair<TPooled, LifetimeTracker> tracker in _lifetimeTracker)
        {
            tracker.Value.PrintIfNotFreed();
        }
#endif
    }

    ///<inheritdoc/>
    public override void LogDebug(DebugLogger logger)
    {
        logger.AppendLine($"{GetType().GetRealTypeName()}: Pool: {_pool.Length - _index}/{_pool.Length}");
    }

#if TRACE_LEAKS
    private sealed class InstanceTracker : IDisposable
    {
        private readonly string _stack = Environment.StackTrace;
        private string _additionalData = string.Empty;
        private bool _disposed;

        public void Dispose()
        {
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        ~InstanceTracker()
        {
            PrintIfLeaked();
        }

        public void PrintIfLeaked()
        {
            if (!_disposed && !Environment.HasShutdownStarted)
            {
                Console.WriteLine($"{typeof(TPooled).Name} was leaked.{(!string.IsNullOrEmpty(_additionalData) ? $"{Environment.NewLine}Info{_additionalData}" : "")} Created at: {Environment.NewLine}{_stack}");
            }
        }

        public void AddAdditionalData(string data)
        {
            _additionalData += data;
        }
    }

    private sealed class LifetimeTracker
    {
        private readonly string _createdStack = Environment.StackTrace;
        private readonly DateTime _createdTime = DateTime.Now;
        private string _lastGetStack;
        private DateTime _lastGetTime;
        private string _lastPooledStack;
        private DateTime _lastPooledTime;
        private bool _isFreed;

        public void OnGet()
        {
            _lastGetStack = Environment.StackTrace;
            _lastGetTime = DateTime.Now;
            _isFreed = false;
        }

        public void OnFree()
        {
            _lastPooledStack = Environment.StackTrace;
            _lastPooledTime = DateTime.Now;
            _isFreed = true;
        }

        public void PrintIfNotFreed()
        {
            if(!_isFreed)
            {
                Console.WriteLine($"{typeof(TPooled).Name}{Environment.NewLine}" +
                                  $"Created at: ({_createdTime}){_createdStack}{Environment.NewLine}" +
                                  $"Last Get: ({_lastGetTime}){_lastGetStack}{Environment.NewLine}" +
                                  $"Last Pooled: ({_lastPooledTime}){_lastPooledStack}");
            }
        }
    }
#endif
}