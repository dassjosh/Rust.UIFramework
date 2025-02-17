using System.Collections.Concurrent;

namespace Oxide.Ext.UiFramework.Cache;

public static class StringCache<T>
{
    private static readonly ConcurrentDictionary<T, string> Cache = new();
    
    public static string ToString(T value)
    {
        if (!Cache.TryGetValue(value, out string text))
        {
            text = value.ToString();
            Cache[value] = text;
        }

        return text;
    }
}