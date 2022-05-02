using System.Collections.Generic;
using System.Text;
using Oxide.Ext.UiFramework.Offsets;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Extensions
{
    public static class VectorExt
    {
        private const string Format = "0.####";
        private const char Space = ' ';
        private const short PositionRounder = 10000;
        
        private static readonly Dictionary<ushort, string> PositionCache = new Dictionary<ushort, string>();
        private static readonly Dictionary<short, string> OffsetCache = new Dictionary<short, string>();

        static VectorExt()
        {
            for (ushort i = 0; i <= PositionRounder; i++)
            {
                PositionCache[i] = (i / (float)PositionRounder).ToString(Format);
            }
        }
        
        public static void WritePos(StringBuilder sb, Vector2 pos)
        {
            sb.Append(PositionCache[(ushort)(pos.x * PositionRounder)]);
            sb.Append(Space);
            sb.Append(PositionCache[(ushort)(pos.y * PositionRounder)]);
        }
        
        public static void WritePos(StringBuilder sb, Vector2Short pos)
        {
            string formattedPos;
            if (!OffsetCache.TryGetValue(pos.X, out formattedPos))
            {
                formattedPos = pos.X.ToString();
                OffsetCache[pos.X] = formattedPos;
            }
            
            sb.Append(formattedPos);
            sb.Append(Space);
            
            if (!OffsetCache.TryGetValue(pos.Y, out formattedPos))
            {
                formattedPos = pos.Y.ToString();
                OffsetCache[pos.Y] = formattedPos;
            }
            
            sb.Append(formattedPos);
        }
    }
}