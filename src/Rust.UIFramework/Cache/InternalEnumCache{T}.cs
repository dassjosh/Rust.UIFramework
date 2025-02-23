using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Oxide.Ext.UiFramework.Cache;

internal static class InternalEnumCache<T>
{
    private static readonly ConcurrentDictionary<T, string> NumberStrings = new();

    static InternalEnumCache()
    {
        foreach (T value in Enum.GetValues(typeof(T)).Cast<T>())
        {
            NumberStrings[value] = ((IFormattable)value).ToString("D", null);
        }
    }
    
    public static string ToNumber(T value) => NumberStrings[value];
}