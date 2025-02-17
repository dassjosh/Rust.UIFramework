using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Oxide.Ext.UiFramework.Cache;

public static class EnumCache<T>
{
    private static readonly ConcurrentDictionary<T, string> CachedStrings = new();
    private static readonly ConcurrentDictionary<T, string> LoweredStrings = new();

    static EnumCache()
    {
        foreach (T value in Enum.GetValues(typeof(T)).Cast<T>())
        {
            CachedStrings[value] = value.ToString();
            LoweredStrings[value] = value.ToString().ToLower();
        }
    }
        
    public static string ToString(T value)
    {
        return CachedStrings[value];
    }
    
    public static string ToLower(T value)
    {
        return LoweredStrings[value];
    }
}