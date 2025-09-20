using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Builder;

public class NamingCache(string baseName) : INamingCache
{
    private readonly List<string> _names = [];
    private readonly List<string> _anchors = [];

    private readonly string _baseName = $"{baseName}_";
    private readonly string _anchorBaseName = $"{baseName}_anchor_";
    
    private readonly object _lock = new();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetComponentName(int index) => GetName(_names, _baseName, index);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetAnchorName(int index) => GetName(_anchors, _anchorBaseName, index);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string GetName(List<string> names, string rootName, int index)
    {
        if (index >= names.Count)
        {
            lock (_lock)
            {
                for (int i = names.Count; i <= index; i++)
                {
                    names.Add(string.Concat(rootName, i.ToString()));
                }
            }
        }

        return names[index];
    }
}