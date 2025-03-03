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
    
    public void Remove(TKey key) => _cache.Remove(key);
    
    public bool TryGetExpiresIn(TKey key, out float remaining)
    {
        if (_cache.TryGetValue(key, out Expire value) && !value.IsExpired)
        {
            remaining = (float)(value.Expires - DateTimeOffset.UtcNow).TotalSeconds;
            return true;
        }

        remaining = default;
        return false;
    }

    public bool ContainsKey(TKey key) => TryGetExpiresIn(key, out _);
    
    public override void RemoveExpired() => _cache.RemoveAll(c => c.Value.IsExpired);

    private readonly record struct Expire(DateTimeOffset Expires)
    {
        public bool IsExpired => Expires < DateTimeOffset.UtcNow;
    }
}