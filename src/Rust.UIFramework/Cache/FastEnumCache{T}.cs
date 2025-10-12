using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;

// ReSharper disable StaticMemberInGenericType
#pragma warning disable S2743 // Static is not shared across generics
#pragma warning disable S3963 // Static constructor

namespace Oxide.Ext.UiFramework.Cache;

public static class FastEnumCache<T> where T : struct, Enum
{
    private static readonly string[] CachedStrings;
    private static readonly string[] LowerStrings;
    private static readonly string[] NumberStrings;
    private static readonly ReadOnlyCollection<T> EnumValues = new(Enum.GetValues(typeof(T)).Cast<T>().ToArray());

    static FastEnumCache()
    {
        CachedStrings = new string[EnumValues.Count];
        LowerStrings = new string[EnumValues.Count];
        NumberStrings = new string[EnumValues.Count];
        
        foreach (T value in EnumValues)
        {
            string enumString = value.ToString();
            CachedStrings[UnsafeUtility.EnumToInt(value)] = enumString;
            LowerStrings[UnsafeUtility.EnumToInt(value)] = enumString.ToLower();
            NumberStrings[UnsafeUtility.EnumToInt(value)] = value.ToString("D");
        }
    }
        
    public static string ToString(T value) => CachedStrings[UnsafeUtility.EnumToInt(value)];

    public static string ToLower(T value) => LowerStrings[UnsafeUtility.EnumToInt(value)];
    public static string ToNumber(T value) => NumberStrings[UnsafeUtility.EnumToInt(value)];
    public static IReadOnlyCollection<T> GetValues() => EnumValues;
}