using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;

// ReSharper disable StaticMemberInGenericType
#pragma warning disable S2743 // Static is not shared across generics
#pragma warning disable S3963 // Static constructor

namespace Oxide.Ext.UiFramework.Cache;

public static class FastEnumCache<T> where T : unmanaged, Enum
{
    private static readonly string[] CachedStrings;
    private static readonly string[] LowerStrings;
    private static readonly string[] NumberStrings;
    private static readonly ReadOnlyCollection<T> EnumValues;

    static FastEnumCache()
    {
        if (Enum.GetUnderlyingType(typeof(T)) != typeof(byte))
        {
            throw new Exception($"Cannot use enum {typeof(T).Name} with FastEnumCache<T> enum underlying type must be byte.");
        }
        
        T[] values = Enum.GetValues(typeof(T)).Cast<T>().ToArray();
        EnumValues = new ReadOnlyCollection<T>(values);
        CachedStrings = new string[values.Length];
        LowerStrings = new string[values.Length];
        NumberStrings = new string[values.Length];
        
        foreach (T value in values)
        {
            string enumString = value.ToString();
            int index = GetIndex(value);
            if (index < 0 || index >= values.Length)
            {
                throw new Exception($"Cannot use enum {typeof(T).Name} with FastEnumCache<T> because enum values are not sequential. Value: {value} Index: {index}. Please use EnumCache<T> instead.");
            }
            
            CachedStrings[index] = enumString;
            LowerStrings[index] = enumString.ToLower();
            NumberStrings[index] = value.ToString("D");
        }
    }
        
    public static string ToString(T value) => CachedStrings[GetIndex(value)];

    public static string ToLower(T value) => LowerStrings[GetIndex(value)];
    public static string ToNumber(T value) => NumberStrings[GetIndex(value)];
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe int GetIndex(T value) => *(byte*)&value;
    
    public static IReadOnlyCollection<T> GetValues() => EnumValues;
}