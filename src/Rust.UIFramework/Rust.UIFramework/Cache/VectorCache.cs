using System.Collections.Generic;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Cache
{
    public static class VectorCache
    {
        private const string Format = "0.####";
        private const char Space = ' ';
        private const short PositionRounder = 10000;
        
        private static readonly Dictionary<ushort, string> PositionCache = new Dictionary<ushort, string>();
        private static readonly Dictionary<short, string> OffsetCache = new Dictionary<short, string>();

        static VectorCache()
        {
            for (ushort i = 0; i <= PositionRounder; i++)
            {
                PositionCache[i] = (i / (float)PositionRounder).ToString(Format);
            }
        }
        
        public static void WritePos(JsonBinaryWriter writer, Vector2 pos)
        {
            WriteFromCache(writer, pos.x);
            writer.Write(Space);
            WriteFromCache(writer, pos.y);
        }

        private static void WriteFromCache(JsonBinaryWriter writer, float pos)
        {
            if (pos >= 0f && pos <= 1f)
            {
                writer.Write(PositionCache[(ushort)(pos * PositionRounder)]);
            }
            else
            {
                string value;
                if (!PositionCache.TryGetValue((ushort)(pos * PositionRounder), out value))
                {
                    value = pos.ToString(Format);
                    PositionCache[(ushort)(pos * PositionRounder)] = value;
                }

                writer.Write(value);
            }
        }

        public static void WriteVector2(JsonBinaryWriter writer, Vector2 pos)
        {
            string formattedPos;
            if (!PositionCache.TryGetValue((ushort)(pos.x * PositionRounder), out formattedPos))
            {
                formattedPos = pos.x.ToString(Format);
                PositionCache[(ushort)(pos.x * PositionRounder)] = formattedPos;
            }
                
            writer.Write(formattedPos);
            writer.Write(Space);
                
            if (!PositionCache.TryGetValue((ushort)(pos.y * PositionRounder), out formattedPos))
            {
                formattedPos = pos.y.ToString(Format);
                PositionCache[(ushort)(pos.y * PositionRounder)] = formattedPos;
            }
                
            writer.Write(formattedPos);
        }
        
        public static void WritePos(JsonBinaryWriter writer, Vector2Short pos)
        {
            string formattedPos;
            if (!OffsetCache.TryGetValue(pos.X, out formattedPos))
            {
                formattedPos = pos.X.ToString();
                OffsetCache[pos.X] = formattedPos;
            }
            
            writer.Write(formattedPos);
            writer.Write(Space);
            
            if (!OffsetCache.TryGetValue(pos.Y, out formattedPos))
            {
                formattedPos = pos.Y.ToString();
                OffsetCache[pos.Y] = formattedPos;
            }
            
            writer.Write(formattedPos);
        }
    }
}