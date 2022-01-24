using System.Text;

namespace UI.Framework.Rust.Positions
{
    public struct Offset
    {
        public readonly string Min;
        public readonly string Max;
        
        private const string PosFormat = "0.####";
        private const char Space = ' ';
        private static readonly StringBuilder _builder = new StringBuilder();

        public Offset(int xMin, int yMin, int xMax, int yMax)
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

        public Offset(string min, string max)
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