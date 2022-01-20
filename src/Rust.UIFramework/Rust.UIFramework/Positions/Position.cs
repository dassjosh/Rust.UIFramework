namespace UI.Framework.Rust.Positions
{
    public struct Position
    {
        public readonly string Min;
        public readonly string Max;

        //private static readonly string PosFormat = "0.####";

        public Position(float xMin, float yMin, float xMax, float yMax)
        {
            Min = string.Concat(xMin.ToString(), " ", yMin.ToString());
            Max = string.Concat(xMax.ToString(), " ", yMax.ToString());
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