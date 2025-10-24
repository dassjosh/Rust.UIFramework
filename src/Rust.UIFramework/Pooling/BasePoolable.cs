using System;
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

#if UNIT_TESTS
    private readonly string _createdStack = Environment.StackTrace;
    
    ~BasePoolable()
    {
        if (CanPool)
        {
            Console.WriteLine($"\n{new string('=', 30)}\nLeaked: {GetType().Name}\n{_createdStack}");
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
    
    internal void OverridePluginPool(UiPluginPool pluginPool)
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
            
        _pool.Free(this);
    }
}