using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Pooling;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Extensions;

public static class ListExt
{
    public static void FreeValues<T>(this List<T> list) where T : BasePoolable
    {
        if (list == null)
        {
            return;
        }
        
        int count = list.Count;
        Span<T> span = list.ListAsSpan();
        for (int index = 0; index < count; index++)
        {
            span[index].TryDispose();
        }
        
        list.Clear();
    }

    public static void TryFreeValues<T>(this List<T> list)
    {
        if (list == null)
        {
            return;
        }
        
        int count = list.Count;
        Span<T> span = list.ListAsSpan();
        for (int index = 0; index < count; index++)
        {
            span[index].TryReturnToPool();
        }
        
        list.Clear();
    }

    public static void RemoveSequentialDuplicates<T>(this List<T> list)
    {
        for (int i = list.Count - 1; i > 0;)
        {
            int prevIndex = i - 1;
            if (list[i].Equals(list[prevIndex]))
            {
                list.RemoveAt(i);
            }
            else
            {
                i = prevIndex;
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static T[] GetInternalArrayUnsafe<T>(this List<T> list)
    {
        return list.GetPrivateFieldsUnsafe()._items;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static PrivateList<T> GetPrivateFieldsUnsafe<T>(this List<T> list)
    {
        return list == null ? throw new ArgumentNullException(nameof(list)) : UnsafeUtility.As<List<T>, PrivateList<T>>(ref list);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<T> ListAsReadOnlySpan<T>(this List<T> list)
    {
        return list.GetInternalArrayUnsafe().AsSpan(0, list.Count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> ListAsSpan<T>(this List<T> list)
    {
        return list.GetInternalArrayUnsafe().AsSpan(0, list.Count);
    }
    
    private class PrivateList<T>
    {
        internal T[] _items;
        internal int _size;
        internal int _version;
    }
}