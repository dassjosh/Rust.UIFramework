using System.Collections.Concurrent;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Cache;

public class ScrollViewContentCache : ISingleton
{
    private readonly ConcurrentDictionary<string, string> _contentCache = new();

    private ScrollViewContentCache() { }
    
    public string GetContentName(string name) => _contentCache.GetOrAdd(name, content => $"{content}___Content");
}