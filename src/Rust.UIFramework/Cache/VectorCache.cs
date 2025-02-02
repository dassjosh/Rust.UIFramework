using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Oxide.Ext.UiFramework.Json;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Cache;

internal static class VectorCache
{
    private static readonly Dictionary<Vector2, byte[]> PositionCache = new();

    public static void WriteVector(JsonUtf8Writer writer, Vector2 pos)
    {
        if (!PositionCache.TryGetValue(pos, out byte[] value))
        {
            value = Encoding.UTF8.GetBytes($"{pos.x} {pos.y}");
            PositionCache[pos] = value;
        }
        
        writer.Write(value);
    }
}