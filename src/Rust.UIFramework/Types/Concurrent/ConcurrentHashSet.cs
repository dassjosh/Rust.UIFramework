using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Types;

public class ConcurrentHashSet<T> : ISet<T>, IReadOnlyCollection<T>
{
    private readonly ConcurrentDictionary<T, byte> _hashSet;
    private readonly IEqualityComparer<T> _comparer;
    
    public int Count => _hashSet.Count;
    public bool IsReadOnly => false;

    #region Constructors
    public ConcurrentHashSet() : this(EqualityComparer<T>.Default) { }

    public ConcurrentHashSet(IEqualityComparer<T> comparer)
    {
        _comparer = comparer ?? EqualityComparer<T>.Default;
        _hashSet = new ConcurrentDictionary<T, byte>(_comparer);
    }

    public ConcurrentHashSet(IEnumerable<T> collection) : this(collection, EqualityComparer<T>.Default) { }

    public ConcurrentHashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
    {
        _comparer = comparer ?? EqualityComparer<T>.Default;
        _hashSet = new ConcurrentDictionary<T, byte>(_comparer);
        if (collection != null)
        {
            foreach (T item in collection)
            {
                Add(item);
            }
        }
    }
    #endregion

    public bool Contains(T item) => item is null ? throw new ArgumentNullException(nameof(item)) : _hashSet.ContainsKey(item);
    public bool Add(T item) => _hashSet.TryAdd(item, 0);
    public bool Remove(T item) => item is null ? throw new ArgumentNullException(nameof(item)) : _hashSet.TryRemove(item, out _);
    public void Clear() => _hashSet.Clear();

    void ICollection<T>.Add(T item)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        Add(item);
    }

    public void UnionWith(IEnumerable<T> other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        foreach (T obj in other)
        {
            Add(obj);
        }
    }

    public void ExceptWith(IEnumerable<T> other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        if (Count == 0) return;
        if (ReferenceEquals(other, this))
        {
            Clear();
            return;
        }

        foreach (T obj in other)
        {
            Remove(obj);
        }
    }

    public void IntersectWith(IEnumerable<T> other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        if (ReferenceEquals(other, this)) return;
        if (Count == 0) return;
        
        List<T> otherItems = other.ToListPooled(UiFrameworkPlugin.Instance);
        foreach (T key in _hashSet.Keys)
        {
            if (!otherItems.Contains(key, _comparer))
            {
                _hashSet.TryRemove(key, out _);
            }
        }
        UiFrameworkPlugin.Instance.PluginPool.FreeList(otherItems);
    }

    public void SymmetricExceptWith(IEnumerable<T> other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        if (ReferenceEquals(this, other))
        {
            Clear();
            return;
        }

        foreach (T item in other)
        {
            if (!_hashSet.TryAdd(item, 0))
            {
                _hashSet.TryRemove(item, out _);
            }
        }
    }

    public bool IsSubsetOf(IEnumerable<T> other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        if (Count == 0) return true;

        List<T> otherItems = other.ToList();
        foreach (T item in _hashSet.Keys)
        {
            if (!otherItems.Contains(item, _comparer))
            {
                return false;
            }
        }
        return true;
    }

    public bool IsSupersetOf(IEnumerable<T> other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        foreach (T item in other)
        {
            if (!Contains(item))
            {
                return false;
            }
        }
        return true;
    }

    public bool IsProperSupersetOf(IEnumerable<T> other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        
        int otherCount = 0;
        HashSet<T> tempSet = new(_comparer);

        foreach(T item in other)
        {
            if (tempSet.Add(item))
            {
                otherCount++;
            }
        }

        if (otherCount == 0)
        {
            return Count > 0;
        }
        
        return Count > otherCount && IsSupersetOf(tempSet);
    }

    public bool IsProperSubsetOf(IEnumerable<T> other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        
        int otherCount = 0;
        HashSet<T> tempSet = new(_comparer);
        foreach(T item in other)
        {
            if (tempSet.Add(item))
            {
                otherCount++;
            }
        }

        return Count < otherCount && IsSubsetOf(tempSet);
    }

    public bool Overlaps(IEnumerable<T> other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        if (Count == 0) return false;

        foreach (T obj in other)
        {
            if (Contains(obj))
            {
                return true;
            }
        }
        return false;
    }

    public bool SetEquals(IEnumerable<T> other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        if (ReferenceEquals(this, other)) return true;

        HashSet<T> otherSet = new(other, _comparer);
        return Count == otherSet.Count && IsSubsetOf(otherSet);
    }

    public void CopyTo(T[] array, int arrayIndex) => _hashSet.Keys.CopyTo(array, arrayIndex);

    public IEnumerator<T> GetEnumerator() => _hashSet.Keys.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}