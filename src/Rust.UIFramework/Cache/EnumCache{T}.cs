using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Oxide.Ext.UiFramework.Cache;

public static class EnumCache<T> where T : Enum
{
    private static readonly ConcurrentDictionary<T, string> CachedStrings = new();
    private static readonly ConcurrentDictionary<T, string> CachedNumbers = new();

    static EnumCache()
    {
        foreach (T value in Enum.GetValues(typeof(T)).Cast<T>())
        {
            CachedStrings[value] = value.ToString();
            CachedNumbers[value] = value.ToString("D");
        }
    }
        
    public static string ToString(T value)
    {
        return CachedStrings[value];
    }

    public static string ToNumber(T value)
    {
        return CachedNumbers[value];
    }
}