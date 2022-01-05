namespace UI.Framework.Rust.Positions
{
    public struct Offset
    {
        public readonly string Min;
        public readonly string Max;

        public Offset(int xMin, int yMin, int xMax, int yMax)
        {
            Min = string.Concat(xMin.ToString(), " ", yMin.ToString());
            Max = string.Concat(xMax.ToString(), " ", yMax.ToString());
        }

        public Offset(string min, string max)
        {
            Min = min;
            Max = max;
        }

        public override string ToString()
        {
            return string.Concat(Min, " ", Max);
            ;
        }
    }
}