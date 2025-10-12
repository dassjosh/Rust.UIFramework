using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Cache;

[Obsolete(ObsoleteMessage)]
public static class UiNameCache
{
    private const string ObsoleteMessage = $"Create a new class that inherits from {nameof(INamingCache)} and register your cache in an instance of {nameof(UiBuilder)} calling builder.{nameof(UiBuilder.SetNamingCache)}";
    
    private static readonly Dictionary<string, List<string>> ComponentNameCache = new();
    private static readonly Dictionary<string, List<string>> AnchorNameCache = new();

    [Obsolete(ObsoleteMessage)]
    public static string GetComponentName(string baseName, int index) => GetName(ComponentNameCache, baseName, "_", index);
    
    [Obsolete(ObsoleteMessage)]
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