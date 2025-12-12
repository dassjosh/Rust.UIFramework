using System.Collections;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Pooling;

public abstract class BasePooledEnumerator<T> : BasePoolable, IEnumerator<T>, IEnumerable<T>
{
    public abstract T Current { get; protected set; }

    object IEnumerator.Current => Current;
    
    public abstract bool MoveNext();

    public virtual void Reset() { }
    public IEnumerator<T> GetEnumerator() => this;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public abstract class BaseListPooledEnumerator<T> : BasePooledEnumerator<T>
{
    protected IList<T> _list;
    protected int _currentIndex = -1;
    
    public override T Current { get; protected set; }
    
    public override bool MoveNext()
    {
        int nextIndex = _currentIndex + 1;
        if (nextIndex < _list.Count)
        {
            Current = _list[nextIndex];
            _currentIndex = nextIndex;
            return true;
        }

        Current = default;
        return false;
    }
    
    public override void Reset()
    {
        _currentIndex = -1;
        Current = default;
    }

    protected override void EnterPool()
    {
        _currentIndex = -1;
    }
}

public sealed class PooledListEnumerator<T> : BaseListPooledEnumerator<T>
{
    public static PooledListEnumerator<T> Create(IUiFrameworkPlugin plugin, IList<T> list) => plugin.PluginPool.Get<PooledListEnumerator<T>>().Init(list);

    private PooledListEnumerator<T> Init(IList<T> list)
    {
        _list = list;
        return this;
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        if (_list is BasePoolable poolable)
        {
            poolable.TryDispose();
        }
    }
}

public sealed class PooledCopyListEnumerator<T> : BaseListPooledEnumerator<T>
{
    public PooledCopyListEnumerator()
    {
        _list = [];
    }
        
    internal PooledCopyListEnumerator(ICollection<T> list)
    {
        _list = new List<T>(list);
    }
        
    public static PooledCopyListEnumerator<T> Create(IUiFrameworkPlugin plugin, IList<T> list) => plugin.PluginPool.Get<PooledCopyListEnumerator<T>>().Init(list);

    private PooledCopyListEnumerator<T> Init(IList<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            _list.Add(list[i]);
        }
        
        return this;
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        _list.Clear();
    }
}