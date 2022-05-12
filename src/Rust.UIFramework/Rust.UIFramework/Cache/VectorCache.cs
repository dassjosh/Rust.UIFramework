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
        
        public static void WritePos(JsonBinaryWriter sb, Vector2 pos)
        {
            if (pos.x >= 0f && pos.x <= 1f)
            {
                sb.Write(PositionCache[(ushort)(pos.x * PositionRounder)]);
            }
            else
            {
                string value;
                if(!PositionCache.TryGetValue((ushort)(pos.x * PositionRounder), out value))
                {
                    value = pos.x.ToString(Format);
                    PositionCache[(ushort)(pos.x * PositionRounder)] = value;
                }
                
                sb.Write(value);
            }
            
            sb.Write(Space);
            
            if (pos.y >= 0f && pos.y <= 1f)
            {
                sb.Write(PositionCache[(ushort)(pos.y * PositionRounder)]);
            }
            else
            {
                string value;
                if(!PositionCache.TryGetValue((ushort)(pos.y * PositionRounder), out value))
                {
                    value = pos.y.ToString(Format);
                    PositionCache[(ushort)(pos.y * PositionRounder)] = value;
                }
                
                sb.Write(value);
            }
        }
        
        public static void WriteVector2(JsonBinaryWriter sb, Vector2 pos)
        {
            string formattedPos;
            if (!PositionCache.TryGetValue((ushort)(pos.x * PositionRounder), out formattedPos))
            {
                formattedPos = pos.x.ToString(Format);
                PositionCache[(ushort)(pos.x * PositionRounder)] = formattedPos;
            }
                
            sb.Write(formattedPos);
            sb.Write(Space);
                
            if (!PositionCache.TryGetValue((ushort)(pos.y * PositionRounder), out formattedPos))
            {
                formattedPos = pos.y.ToString(Format);
                PositionCache[(ushort)(pos.y * PositionRounder)] = formattedPos;
            }
                
            sb.Write(formattedPos);
        }
        
        public static void WritePos(JsonBinaryWriter sb, Vector2Short pos)
        {
            string formattedPos;
            if (!OffsetCache.TryGetValue(pos.X, out formattedPos))
            {
                formattedPos = pos.X.ToString();
                OffsetCache[pos.X] = formattedPos;
            }
            
            sb.Write(formattedPos);
            sb.Write(Space);
            
            if (!OffsetCache.TryGetValue(pos.Y, out formattedPos))
            {
                formattedPos = pos.Y.ToString();
                OffsetCache[pos.Y] = formattedPos;
            }
            
            sb.Write(formattedPos);
        }
    }
}