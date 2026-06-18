using System.Collections.Concurrent;

namespace Oxide.Ext.UiFramework.Libraries;

internal static class BorderRadiusKeyCache
{
    private static readonly ConcurrentDictionary<BorderRadiusData, string> Cache = [];

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
}