using System;
using System.Collections.Concurrent;
using Oxide.Ext.UiFramework.Json;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Cache
{
    public static class VectorCache
    {
        private const string Format = "0.####";
        private const char Space = ' ';
        private const short PositionRounder = 10000;
        
        private static readonly ConcurrentDictionary<short, string> PositionCache = new();

        public static void WritePosition(JsonBinaryWriter writer, Vector2 pos)
        {
            WriteFromCache(writer, pos.x);
            writer.Write(Space);
            WriteFromCache(writer, pos.y);
        }

        private static void WriteFromCache(JsonBinaryWriter writer, float pos)
        {
            string value;
            if (!PositionCache.TryGetValue((short)(pos * PositionRounder), out value))
            {
                value = pos.ToString(Format);
                PositionCache[(short)(pos * PositionRounder)] = value;
            }

            writer.Write(value);
        }

        public static void WriteVector(JsonBinaryWriter writer, Vector2 pos)
        {
            string formattedPos;
            if (!PositionCache.TryGetValue((short)(pos.x * PositionRounder), out formattedPos))
            {
                formattedPos = pos.x.ToString(Format);
                PositionCache[(short)(pos.x * PositionRounder)] = formattedPos;
            }
                
            writer.Write(formattedPos);
            writer.Write(Space);
                
            if (!PositionCache.TryGetValue((short)(pos.y * PositionRounder), out formattedPos))
            {
                formattedPos = pos.y.ToString(Format);
                PositionCache[(short)(pos.y * PositionRounder)] = formattedPos;
            }
                
            writer.Write(formattedPos);
        }
        
        public static void WriteOffset(JsonBinaryWriter writer, Vector2 pos)
        {
            writer.Write(StringCache<short>.ToString((short)Math.Round(pos.x)));
            writer.Write(Space);
            writer.Write(StringCache<short>.ToString((short)Math.Round(pos.y)));
        }
    }
}