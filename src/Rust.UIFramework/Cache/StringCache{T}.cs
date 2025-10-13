using System;
using System.Collections.Concurrent;

namespace Oxide.Ext.UiFramework.Cache;

public static class StringCache<T>
{
    private static readonly ConcurrentDictionary<T, string> Cache = new();
    
    public static string ToString(T value)
    {
        return Cache.GetOrAdd(value, ValueFactory);
        static string ValueFactory(T v) => v.ToString();
    }
    
    [Obsolete("Use FormatCache<T>.ToString() instead.")]
    public static string ToString<TFormat>(TFormat value, string format) where TFormat : IFormattable => FormatCache<TFormat>.ToString(value, format);
}