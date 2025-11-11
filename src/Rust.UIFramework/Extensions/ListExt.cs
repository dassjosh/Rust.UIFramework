using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Pooling;
using Unity.Collections.LowLevel.Unsafe;

namespace Oxide.Ext.UiFramework.Extensions;

public static class ListExt
{
    public static void FreeValues<T>(this List<T> list) where T : IPoolable
    {
        if (list == null)
        {
            return;
        }
        
        int count = list.Count;
        T[] array = list.GetInternalArray();
        for (int index = 0; index < count; index++)
        {
            array[index].TryDispose();
        }
        
        list.Clear();
    }

    extension<T>(List<T> list)
    {
        public void TryFreeValues()
        {
            if (list == null)
            {
                return;
            }
        
            int count = list.Count;
            T[] array = list.GetInternalArray();
            for (int index = 0; index < count; index++)
            {
                array[index].TryReturnToPool();
            }
        
            list.Clear();
        }

        public void RemoveSequentialDuplicates()
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
        internal T[] GetInternalArray()
        {
            return list.GetPrivateFieldsUnsafe()._items;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private PrivateList<T> GetPrivateFieldsUnsafe()
        {
            return list == null ? throw new ArgumentNullException(nameof(list)) : UnsafeUtility.As<List<T>, PrivateList<T>>(ref list);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<T> GetAsReadOnlySpan()
        {
            return list.GetInternalArray().AsSpan(0, list.Count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Span<T> GetAsSpan()
        {
            return list.GetInternalArray().AsSpan(0, list.Count);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    // ReSharper disable once ClassNeverInstantiated.Local
    private sealed class PrivateList<T>
    {
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
        internal T[] _items;
        internal int _size;
        internal int _version;
#pragma warning restore CS0649 // Field is never assigned to, and will always have its default value
    }
}