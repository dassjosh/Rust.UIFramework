using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Cache;

#pragma warning disable S2743 // Static is not shared across generics
#pragma warning disable S3963 // Static constructor

internal static class Utf8EnumCache<T> where T : unmanaged, Enum
{
    private static readonly Utf8String[] Utf8Strings;

    static Utf8EnumCache()
    {
        if (Enum.GetUnderlyingType(typeof(T)) != typeof(int))
        {
            throw new Exception($"Cannot use enum {typeof(T).Name} with FastEnumCache<T> enum underlying type must be int.");
        }
        
        T[] values = Enum.GetValues(typeof(T)).Cast<T>().ToArray();
        Utf8Strings = new Utf8String[values.Length];
        
        foreach (T value in values)
        {
            Utf8Strings[GetIndex(value)] = value.ToString("D");
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Utf8String ToUtf8Number(T value) => Utf8Strings[GetIndex(value)];
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe int GetIndex(T value) => *(int*)&value;
}