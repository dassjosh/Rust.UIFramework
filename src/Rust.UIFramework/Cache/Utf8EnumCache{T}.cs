using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Types;
using Unity.Collections.LowLevel.Unsafe;

namespace Oxide.Ext.UiFramework.Cache;

#pragma warning disable S2743 // Static is not shared across generics
#pragma warning disable S3963 // Static constructor

internal static class Utf8EnumCache<T> where T : struct, Enum
{
    private static readonly Utf8String[] Utf8Strings;

    static Utf8EnumCache()
    {
        T[] values = Enum.GetValues(typeof(T)).Cast<T>().ToArray();
        Utf8Strings = new Utf8String[values.Length];
        
        foreach (T value in values)
        {
            Utf8Strings[UnsafeUtility.EnumToInt(value)] = value.ToString("D");
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Utf8String ToUtf8Number(T value) => Utf8Strings[UnsafeUtility.EnumToInt(value)];
}