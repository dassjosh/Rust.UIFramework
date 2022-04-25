using Oxide.Ext.UiFramework.Offsets;
using Oxide.Plugins;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Extensions
{
    public static class VectorExt
    {
        private const string Format = "0.####";

        private const short PositionRounder = 10000;
        private static readonly Hash<short, string> PositionCache = new Hash<short, string>();
        private static readonly Hash<short, string> OffsetCache = new Hash<short, string>();

        static VectorExt()
        {
            for (short i = 0; i <= 10000; i++)
            {
                PositionCache[i] = (i / 10000f).ToString(Format);
            }
        }
        
        public static string ToString(Vector2 pos)
        {
            return string.Concat(PositionCache[(short)(pos.x * PositionRounder)], " ", PositionCache[(short)(pos.y * PositionRounder)]);
        }
        
        public static string ToString(Vector2Short pos)
        {
            string x;
            string y;
            if (!OffsetCache.TryGetValue(pos.X, out x))
            {
                x = pos.X.ToString();
                OffsetCache[pos.X] = x;
            }
            
            if (!OffsetCache.TryGetValue(pos.Y, out y))
            {
                y = pos.Y.ToString();
                OffsetCache[pos.Y] = y;
            }
            
            return string.Concat(x, " ", y);
        }
    }
}