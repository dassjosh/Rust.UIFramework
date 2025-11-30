using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Represents a poolable object
/// </summary>
public abstract class BasePoolable : IPoolable
{
    public bool IsPooled { get; private set; }

    internal bool CanPool => _pool != null && !IsPooled;
    private IPool<BasePoolable> _pool;
    internal UiPluginPool PluginPool;
    UiPluginPool IPoolable.PluginPool => PluginPool;

#if DEBUG
    private readonly string _createdStack = Environment.StackTrace;
    private string _lastGetStack;
    private DateTime _lastGetTime;
    private string _lastPooledStack;
    private DateTime _lastPooledTime;
    
    ~BasePoolable()
    {
        if (CanPool)
        {
            OxideLibrary.LogWarning($"\n{new string('=', 30)}\nLeaked: {GetType().Name}\n{_createdStack}\n\n{_lastGetStack}\n\n{_lastPooledStack}");
        }
    }
#endif

    internal void OnInitInternal(IPool<BasePoolable> pool)
    {
        _pool = pool;
        PluginPool = pool.PluginPool;
        OnInit();
    }
    
    internal virtual void OnInit() {}
    
    internal virtual void OverridePluginPool(UiPluginPool pluginPool)
    {
        PluginPool = pluginPool;
    }

    internal void EnterPoolInternal()
    {
        EnterPool();
        IsPooled = true;
    }

    internal void LeavePoolInternal()
    {
        IsPooled = false;
        LeavePool();
#if DEBUG
        _lastGetStack = Environment.StackTrace;
        _lastGetTime = DateTime.Now;
#endif
    }

    /// <summary>
    /// Called when the object is returned to the pool.
    /// Can be overriden in child classes to clean up used data
    /// </summary>
    protected virtual void EnterPool() { }
        
    /// <summary>
    /// Called when the object leaves the pool.
    /// Can be overriden in child classes to set the initial object state
    /// </summary>
    protected virtual void LeavePool() { }
    
#if UNIT_TESTS
    internal void TestEnterPool() => EnterPool();
    internal void TestLeavePool() => LeavePool();
#endif

    public void TryDispose()
    {
        if (CanPool && !IsPooled)
        {
            Dispose();
        }
    }
    
    public virtual void Dispose()
    {
        if (_pool == null)
        {
            return;
        }
        
        if (IsPooled)
        {
            throw new ObjectDisposedException(GetType().Name);
        }
#if DEBUG
        _lastPooledStack = Environment.StackTrace;
        _lastPooledTime = DateTime.Now;
#endif
        
        _pool.Free(this);
    }
}