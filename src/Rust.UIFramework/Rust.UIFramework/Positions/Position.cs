namespace UI.Framework.Rust.Positions
{
    public struct Position
    {
        public readonly string Min;
        public readonly string Max;

        private const string PosFormat = "0.####";

        public Position(float xMin, float yMin, float xMax, float yMax)
        {
            Min = string.Concat(xMin.ToString(PosFormat), " ", yMin.ToString(PosFormat));
            Max = string.Concat(xMax.ToString(PosFormat), " ", yMax.ToString(PosFormat));
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