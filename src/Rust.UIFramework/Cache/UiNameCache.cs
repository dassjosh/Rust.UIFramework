using System;
using System.Collections.Generic;

namespace Oxide.Ext.UiFramework.Cache;

[Obsolete]
public static class UiNameCache
{
    private static readonly Dictionary<string, List<string>> ComponentNameCache = new();
    private static readonly Dictionary<string, List<string>> AnchorNameCache = new();

    [Obsolete]
    public static string GetComponentName(string baseName, int index) => GetName(ComponentNameCache, baseName, "_", index);
    
    [Obsolete]
    public static string GetAnchorName(string baseName, int index) => GetName(AnchorNameCache, baseName, "_anchor_", index);
        
    private static string GetName(Dictionary<string, List<string>> cache, string baseName, string splitter, int index)
    {
        if (!cache.TryGetValue(baseName, out List<string> names))
        {
            cache[baseName] = names = [];
        }

        if (index >= names.Count)
        {
            for (int i = names.Count; i <= index; i++)
            {
                names.Add(string.Concat(baseName, splitter, i.ToString()));
            }
        }

        return names[index];
    }
}