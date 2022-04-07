using System.Text;

namespace Oxide.Ext.UiFramework.Positions
{
    public struct Position
    {
        public readonly string Min;
        public readonly string Max;
        
        private const string PosFormat = "0.####";
        private const char Space = ' ';
        private static readonly StringBuilder _builder = new StringBuilder();

        public Position(float xMin, float yMin, float xMax, float yMax)
        {
            Min = null;
            Max = null;
            Min = Build(xMin, yMin);
            Max = Build(xMax, yMax);
        }

        private static string Build(float min, float max)
        {
            _builder.Clear();
            _builder.Append(min.ToString(PosFormat));
            _builder.Append(Space);
            _builder.Append(max.ToString(PosFormat));
            return _builder.ToString();
        }

        public Position(string min, string max)
        {
            Min = min;
            Max = max;
        }

        public override string ToString()
        {
            return string.Concat(Min, " ", Max);
        }
    }
}