using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Oxide.Ext.UiFramework.Cache;

public static class EnumCache<T> where T : Enum
{
    private static readonly ConcurrentDictionary<T, string> CachedStrings = new();
    private static readonly ConcurrentDictionary<T, string> LowerStrings = new();
    private static readonly ConcurrentDictionary<T, string> NumberStrings = new();

    static EnumCache()
    {
        foreach (T value in Enum.GetValues(typeof(T)).Cast<T>())
        {
            string enumString = value.ToString();
            CachedStrings[value] = enumString;
            LowerStrings[value] = enumString.ToLower();
            NumberStrings[value] = value.ToString("D");
        }
    }
        
    public static string ToString(T value) => CachedStrings[value];

    public static string ToLower(T value) => LowerStrings[value];
    public static string ToNumber(T value) => NumberStrings[value];
}