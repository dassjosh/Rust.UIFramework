using System.Collections.Generic;
using System.Text;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Cache;

internal static class VectorCache
{
    private static readonly Dictionary<Vector2, Utf8String> PositionCache = new();

    public static void WriteVector(JsonUtf8Writer writer, Vector2 pos)
    {
        if (!PositionCache.TryGetValue(pos, out Utf8String value))
        {
            PositionCache[pos] = value = $"{pos.x} {pos.y}";
        }
        
        writer.Write(value);
    }
}