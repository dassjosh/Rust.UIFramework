using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Pooling;

public class UiPooledArray<T> : BasePoolable, IList<T>, IReadOnlyList<T>
{
    private readonly T[] _array;
    public int Count { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => _array.Length; }
    public bool IsReadOnly => _array.IsReadOnly;
    
    internal static readonly UiPooledArray<T> Empty = new(0);
    
    internal UiPooledArray(uint size)
    {
        _array = size == 0 ? [] : new T[size];
    }

    public bool Contains(T item) => ((ICollection<T>)_array).Contains(item);
    public int IndexOf(T item) => ((IList<T>)_array).IndexOf(item);
    public void Add(T item) => ((ICollection<T>)_array).Add(item);
    public void Insert(int index, T item) => ((IList<T>)_array).Insert(index, item);
    public bool Remove(T item) => ((ICollection<T>)_array).Remove(item);
    public void RemoveAt(int index) => ((IList<T>)_array).RemoveAt(index);
    public void Clear() => _array.Clear();
    public void CopyTo(T[] array, int arrayIndex) => _array.CopyTo(array, arrayIndex);
    public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)_array).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public T this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _array[index];
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _array[index] = value;
    }
    
    public static implicit operator T[](UiPooledArray<T> pooledArray) => pooledArray._array;
    public Span<T> AsSpan() => _array.AsSpan();
    public Span<T> AsSpan(int start) => _array.AsSpan(start);

    protected override void EnterPool()
    {
        _array.Clear();
    }
}