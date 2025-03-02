using System;
using System.Collections.Generic;

namespace Oxide.Ext.UiFramework.Cache;

internal abstract class BaseMemoryCache
{
    protected readonly TimeSpan CacheDuration;
    private static readonly List<WeakReference<BaseMemoryCache>> Caches = [];
    private DateTimeOffset _nextExpireCheck;

    protected BaseMemoryCache(TimeSpan cacheDuration)
    {
        CacheDuration = cacheDuration;
        Caches.Add(new WeakReference<BaseMemoryCache>(this));
        SetNextExpiresCheck();
    }
    
    //TODO: Find a better way and/or place to call this
    internal static void ExpireCaches()
    {
        for (int index = Caches.Count - 1; index >= 0; index--)
        {
            WeakReference<BaseMemoryCache> cacheRef = Caches[index];
            if (!cacheRef.TryGetTarget(out BaseMemoryCache cache))
            {
                Caches.RemoveAt(index);
                continue;
            }

            if (cache.ShouldExpire())
            {
                cache.RemoveExpired();
                cache.SetNextExpiresCheck();
            }
        }
    }

    public bool ShouldExpire() => _nextExpireCheck < DateTimeOffset.UtcNow;

    public abstract void RemoveExpired();

    private void SetNextExpiresCheck()
    {
        _nextExpireCheck = DateTimeOffset.UtcNow + CacheDuration;
    }
}