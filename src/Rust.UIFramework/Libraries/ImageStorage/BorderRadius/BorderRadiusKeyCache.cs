using System.Collections.Concurrent;

namespace Oxide.Ext.UiFramework.Libraries;

internal static class BorderRadiusKeyCache
{
    private static readonly ConcurrentDictionary<BorderRadiusData, string> Cache = [];
    private static readonly ConcurrentDictionary<BorderRadiusImageData, string> ImageCache = [];

    public static string GetKey(BorderRadiusData data)
    {
        if (Cache.TryGetValue(data, out string name))
        {
            return name;
        }

        name = data.ToName();
        Cache.TryAdd(data.New(), name);
        return name;
    }

    public static string GetKey(BorderRadiusImageData key)
    {
        if (ImageCache.TryGetValue(key, out string name))
        {
            return name;
        }

        name = key.ToName();
        ImageCache.TryAdd(key.New(), name);
        return name;
    }
}