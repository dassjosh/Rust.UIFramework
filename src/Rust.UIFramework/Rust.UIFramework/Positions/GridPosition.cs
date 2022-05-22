namespace Oxide.Ext.UiFramework.Positions
{
    public class GridPosition : MovablePosition
    {
        public readonly float NumCols;
        public readonly float NumRows;

        public GridPosition(float xMin, float yMin, float xMax, float yMax, float numCols, float numRows) : base(xMin, yMin, xMax, yMax)
        {
            NumCols = numCols;
            NumRows = numRows;
        }

        public void MoveCols(int cols)
        {
            XMin += cols / NumCols;
            XMax += cols / NumCols;
            
            if (XMax > 1)
            {
                XMin -= 1;
                XMax -= 1;
                MoveRows(1);
            }
        }
        
        public void MoveCols(float cols)
        {
            XMin += cols / NumCols;
            XMax += cols / NumCols;
            
            if (XMax > 1)
            {
                XMin -= 1;
                XMax -= 1;
                MoveRows(1);
            }
        }

        public void MoveRows(int rows)
        {
            YMin -= rows / NumRows;
            YMax -= rows / NumRows;
        }
    }
}