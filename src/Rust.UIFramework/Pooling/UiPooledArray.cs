using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Pooling;

public class UiPooledArray<T> : BasePoolable, IList<T>, IReadOnlyList<T>
{
    public readonly T[] Array;
    public int Count => _length < 0 ? Array.Length : _length;
    public bool IsReadOnly => Array.IsReadOnly;
    private int _length = -1;
    private static readonly bool ClearArray = !Type.IsUnmanaged<T>();
    
    internal static readonly UiPooledArray<T> Empty = new(0);

    internal UiPooledArray(uint size)
    {
        Array = size == 0 ? [] : new T[size];
    }

    public bool Contains(T item) => ((ICollection<T>)Array).Contains(item);
    public int IndexOf(T item) => ((IList<T>)Array).IndexOf(item);
    public void Add(T item) => ((ICollection<T>)Array).Add(item);
    public void Insert(int index, T item) => ((IList<T>)Array).Insert(index, item);
    public bool Remove(T item) => ((ICollection<T>)Array).Remove(item);
    public void RemoveAt(int index) => ((IList<T>)Array).RemoveAt(index);
    public void Clear() => Array.Clear();
    public void CopyTo(T[] array, int arrayIndex) => Array.CopyTo(array, arrayIndex);
    public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)Array).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public UiPooledArray<T> WithLength(int length)
    {
        _length = length;
        return this;
    }

    public T this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Array[index];
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => Array[index] = value;
    }
    
    public static implicit operator T[](UiPooledArray<T> pooledArray) => pooledArray.Array;
    public Span<T> AsSpan() => Array.AsSpan(0, Count);
    public Span<T> AsSpan(Range range) => Array.AsSpan(range);
    public ReadOnlySpan<T> AsReadOnlySpan() => AsSpan();
    public ReadOnlySpan<T> AsReadOnlySpan(Range range) => AsSpan(range);

    protected override void EnterPool()
    {
        _length = -1;
        if (ClearArray)
        {
            Clear();
        }
    }
}