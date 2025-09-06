using System;
using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Represents a poolable object
/// </summary>
public abstract class BasePoolable : IPoolable
{
    private bool _disposed;
    internal bool CanPool => _pool != null && !_disposed;
    private IPool<BasePoolable> _pool;
    internal UiPluginPool PluginPool;
    UiPluginPool IPoolable.PluginPool => PluginPool;

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
        _disposed = true;
    }

    internal void LeavePoolInternal()
    {
        _disposed = false;
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
        if (CanPool && !_disposed)
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

        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }
            
        _pool.Free(this);
    }
}