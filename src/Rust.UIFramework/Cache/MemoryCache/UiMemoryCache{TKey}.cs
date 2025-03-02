using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Cache;

internal class UiMemoryCache<TKey> : BaseMemoryCache
{
    private readonly Dictionary<TKey, Expire> _cache = new();

    internal UiMemoryCache(TimeSpan cacheDuration) : base(cacheDuration) {}

    public bool TryAdd(TKey key)
    {
        if (ContainsKey(key))
        {
            return false;
        }
        
        _cache[key] = new Expire(DateTimeOffset.UtcNow.Add(CacheDuration));
        return true;
    }
    
    public bool TryRemove(TKey key) => _cache.Remove(key, out Expire value) && !value.IsExpired;

    private bool ContainsKey(TKey key) => _cache.TryGetValue(key, out Expire value) && !value.IsExpired;
    
    public override void RemoveExpired() => _cache.RemoveAll(c => c.Value.IsExpired);

    private readonly record struct Expire(DateTimeOffset Expires)
    {
        public bool IsExpired => Expires < DateTimeOffset.UtcNow;
    }
}