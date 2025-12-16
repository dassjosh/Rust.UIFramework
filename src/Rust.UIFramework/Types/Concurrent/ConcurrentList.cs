using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Types;

public class ConcurrentList<T> : IList<T>, IReadOnlyList<T>
{
    private readonly List<T> _list = [];
    private readonly ReaderWriterLockSlim _lock = new();

    public int IndexOf(T item)
    {
        _lock.EnterReadLock();
        try
        {
            return _list.IndexOf(item);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public void Insert(int index, T item)
    {
        _lock.EnterWriteLock();
        try
        {
            _list.Insert(index, item);
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public void RemoveAt(int index)
    {
        _lock.EnterWriteLock();
        try
        {
            _list.RemoveAt(index);
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public T this[int index]
    {
        get
        {
            _lock.EnterReadLock();
            try
            {
                return _list[index];
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
        set
        {
            _lock.EnterWriteLock();
            try
            {
                _list[index] = value;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }

    public bool Remove(T item)
    {
        _lock.EnterWriteLock();
        try
        {
            return _list.Remove(item);
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public int Count
    {
        get
        {
            _lock.EnterReadLock();
            try
            {
                return _list.Count;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        _lock.EnterWriteLock();
        try
        {
            _list.Add(item);
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public bool TryAdd(T item)
    {
        _lock.EnterWriteLock();
        try
        {
            if (!_list.Contains(item))
            {
                _list.Add(item);
                return true;
            }

            return false;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }
    
    public void Clear()
    {
        _lock.EnterWriteLock();
        try
        {
            _list.Clear();
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public bool Contains(T item)
    {
        _lock.EnterReadLock();
        try
        {
            return _list.Contains(item);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _lock.EnterReadLock();
        try
        {
            _list.CopyTo(array, arrayIndex);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public IEnumerable<T> GetPooledEnumerator(IUiFrameworkPlugin plugin)
    {
        _lock.EnterReadLock();
        try
        {
            return PooledListEnumerator<T>.Create(plugin, this);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        _lock.EnterReadLock();
        try
        {
            return new PooledListEnumerator<T>(this);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}