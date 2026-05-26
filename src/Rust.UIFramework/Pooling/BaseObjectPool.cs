using System;
using System.Collections.Concurrent;
using System.Threading;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Logging;

#if TRACE_LEAKS
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Cache;
#endif

namespace Oxide.Ext.UiFramework.Pooling;

public abstract class BaseObjectPool<TPooled> : BasePool, IObjectPool<TPooled>
    where TPooled : class
{
    private Func<TPooled> _createFunc;
    private Action<TPooled> _getFunc;
    private Func<TPooled, bool> _returnFunc;

    private int _numItems;

    private PoolSize _size;

    private readonly ConcurrentQueue<TPooled> _pool = new();

#if TRACE_LEAKS
    private readonly ConditionalWeakTable<TPooled, InstanceTracker> _instanceTracker = new();
    private readonly ConditionalWeakTable<TPooled, LifetimeTracker> _lifetimeTracker = new();
    private int _index;
#endif

    protected BaseObjectPool() { }

    protected BaseObjectPool(IPooledObjectPolicy<TPooled> policy)
    {
        SetPolicy(policy);
    }

    protected void SetPolicy(IPooledObjectPolicy<TPooled> policy)
    {
        _createFunc = policy.Create;
        _getFunc = policy.Get;
        _returnFunc = policy.Return;
    }

    protected override void OnInit(UiPluginPool pluginPool)
    {
        _size = GetPoolSize(pluginPool.Settings);
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
        if (_pool.TryDequeue(out TPooled item))
        {
            Interlocked.Decrement(ref _numItems);
        }
        else
        {
            item = _createFunc();
        }

#if TRACE_LEAKS
        _instanceTracker.Add(item, new InstanceTracker());
        LifetimeTracker tracker = _lifetimeTracker.GetOrCreateValue(item);
        tracker.OnGet();
        Interlocked.Increment(ref _index);
#endif

        _getFunc(item);
        return item;
    }

    /// <summary>
    /// Frees an item back to the pool
    /// </summary>
    /// <param name="item">Item being freed</param>
    public void Free(TPooled item)
    {
        if (item == null)
        {
            return;
        }

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

        Interlocked.Decrement(ref _index);
#endif

        if (!_returnFunc(item))
        {
            return;
        }

        if (Interlocked.Increment(ref _numItems) <= _size.MaxSize)
        {
            _pool.Enqueue(item);
        }
        else
        {
            Interlocked.Decrement(ref _numItems);
        }
    }

    public override void ClearPool()
    {
        _pool.Clear();
        _numItems = 0;
#if TRACE_LEAKS
        _index = 0;
        _lifetimeTracker.Clear();
         foreach (KeyValuePair<TPooled, InstanceTracker> tracker in _instanceTracker)
        {
            tracker.Value.Dispose();
        }
        _instanceTracker.Clear();
#endif
    }

    public override bool HasPoolLeaked()
    {
#if TRACE_LEAKS
        return _index != 0;
#else
        return false;
#endif
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
#if TRACE_LEAKS
        logger.AppendLine($"{GetType().GetRealTypeName()}: Pool: {_pool.Count - _index}/{_pool.Count}");
#else
        logger.AppendLine($"{GetType().GetRealTypeName()}: Pool: {_pool.Count}");
#endif
    }

#if TRACE_LEAKS
    private sealed class InstanceTracker : IDisposable
    {
        private readonly string _stack = Environment.StackTrace;
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
                OxideLibrary.LogWarning($"{typeof(TPooled).Name} was leaked. Created at: {Environment.NewLine}{_stack}");
            }
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
                OxideLibrary.LogWarning($"{typeof(TPooled).Name}{Environment.NewLine}" +
                                        $"Created at: ({_createdTime}){_createdStack}{Environment.NewLine}" +
                                        $"Last Get: ({_lastGetTime}){_lastGetStack}{Environment.NewLine}" +
                                        $"Last Pooled: ({_lastPooledTime}){_lastPooledStack}");
            }
        }
    }
#endif
}