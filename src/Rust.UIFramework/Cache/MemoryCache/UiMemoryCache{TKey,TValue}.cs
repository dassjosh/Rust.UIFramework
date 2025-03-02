using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Cache;

internal class UiMemoryCache<TKey, TValue> : BaseMemoryCache
{
    private readonly Dictionary<TKey, CachedValue> _cache = new();

    internal UiMemoryCache(TimeSpan cacheDuration) : base(cacheDuration) { }
    
    public bool TryAdd(TKey key, TValue value)
    {
        if (ContainsKey(key))
        {
            return false;
        }
        
        _cache[key] = new CachedValue(value, DateTimeOffset.UtcNow.Add(CacheDuration));
        return true;
    }
    
    public bool TryRemove(TKey key, out TValue value)
    {
        if (!_cache.Remove(key, out CachedValue cachedValue) || cachedValue.IsExpired)
        {
            value = default;
            return false;
        }
        
        value = cachedValue.Value;
        return true;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        if (_cache.TryGetValue(key, out CachedValue cachedValue) && !cachedValue.IsExpired)
        {
            value = cachedValue.Value;
            return true;
        }

        value = default;
        return false;
    }
    
    public bool ContainsKey(TKey key) => TryGetValue(key, out TValue _);
    public TValue this[TKey key]
    {
        get => TryGetValue(key, out TValue value) ? value : throw new KeyNotFoundException();
        set => _cache[key] = new CachedValue(value, DateTimeOffset.UtcNow + CacheDuration);
    }
    
    public override void RemoveExpired() => _cache.RemoveAll(c => c.Value.IsExpired);

    private readonly record struct CachedValue(TValue Value, DateTimeOffset Expires)
    {
        public bool IsExpired => Expires < DateTimeOffset.UtcNow;
    }
}