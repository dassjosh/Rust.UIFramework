using System.Collections.Concurrent;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Builder;

public class UiNamingCache : ISingleton
{
    private readonly ConcurrentDictionary<string, INamingCache> _namingCache = [];
    public readonly INamingCache Default;
    
    private UiNamingCache()
    {
        Default = GetNamingCache(string.Empty);
    }

    public void SetNamingCache(string name, INamingCache cache)
    {
        _namingCache[name] = cache;
    }

    public INamingCache GetNamingCache(string name)
    {
        name ??= string.Empty;
        if (!_namingCache.TryGetValue(name, out INamingCache cache))
        {
            _namingCache[name] = cache = new NamingCache(name);
        }
        return cache;
    }
}