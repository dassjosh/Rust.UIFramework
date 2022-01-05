namespace UI.Framework.Rust.Positions
{
    public class StaticUiPosition : UiPosition
    {
        private readonly Position _pos;

        public StaticUiPosition(float xMin, float yMin, float xMax, float yMax)
        {
            _pos = new Position(xMin, yMin, xMax, yMax);
        }

        public override Position ToPosition()
        {
            return _pos;
        }
    }
}