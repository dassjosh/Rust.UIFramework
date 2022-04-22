using System.Text;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Offsets
{
    public struct Offset
    {
        public readonly string Min;
        public readonly string Max;
        public readonly bool IsDefaultMin;
        public readonly bool IsDefaultMax;
        
        private const string PosFormat = "0.####";
        private const char Space = ' ';

        public Offset(int xMin, int yMin, int xMax, int yMax)
        {
            Min = null;
            if (xMin == 0 && yMin == 0)
            {
                IsDefaultMin = true;
            }
            else
            {
                Min = Build(xMin, yMin);
                IsDefaultMin = false;
            }
            
            Max = null;
            if (xMax == 0 && yMax == 0)
            {
                IsDefaultMax = true;
            }
            else
            {
                Max = Build(xMax, yMax);
                IsDefaultMax = false;
            }
        }
        
        private static string Build(float min, float max)
        {
            StringBuilder sb = UiFrameworkPool.GetStringBuilder();
            sb.Append(min.ToString(PosFormat));
            sb.Append(Space);
            sb.Append(max.ToString(PosFormat));
            string result = sb.ToString();
            UiFrameworkPool.FreeStringBuilder(ref sb);
            return result;
        }

        public override string ToString()
        {
            return string.Concat(Min, " ", Max);
        }
    }
}