using System;
using System.Collections.Concurrent;
using System.Reflection;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Cache;

public static class UiLayerCache
{
    private static readonly ConcurrentDictionary<UiLayer, string> Layers = new();
    static UiLayerCache()
    {
        Type stringType = typeof(string);
        foreach (FieldInfo field in typeof(UiLayers).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
        {
            if (field.IsLiteral && !field.IsInitOnly && field.FieldType == stringType)
            {
                string enumName = field.Name;
                string value = (string)field.GetRawConstantValue();
                if (Enum.TryParse(enumName, out UiLayer layer))
                {
                    Layers[layer] = value;
                }
                else
                {
                    OxideLibrary.LogWarning($"Failed to parse UiLayer enum: {enumName} Value: {value}");
                }
            }
        }
    }

    public static string GetLayer(UiLayer layer)
    {
        return Layers[layer];
    }
}