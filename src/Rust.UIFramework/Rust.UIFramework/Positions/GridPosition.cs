namespace UI.Framework.Rust.Positions
{
    public class GridPosition : MovablePosition
    {
        public float NumCols => _numCols;
        public float NumRows => _numRows;

        private readonly float _numCols;
        private readonly float _numRows;

        public GridPosition(float xMin, float yMin, float xMax, float yMax, float numCols, float numRows) : base(xMin, yMin, xMax, yMax)
        {
            _numCols = numCols;
            _numRows = numRows;
        }

        public void MoveCols(int cols)
        {
            XMin += cols / _numCols;
            XMax += cols / _numCols;
            
            if (XMax > 1)
            {
                XMin -= 1;
                XMax -= 1;
                MoveRows(-1);
            }

#if UiDebug
            ValidatePositions();
#endif
        }

        public void MoveRows(int rows)
        {
            YMin += rows / _numRows;
            YMax += rows / _numRows;

#if UiDebug
            ValidatePositions();
#endif
        }
    }
}