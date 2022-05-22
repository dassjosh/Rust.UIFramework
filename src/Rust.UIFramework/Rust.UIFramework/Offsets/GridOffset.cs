namespace Oxide.Ext.UiFramework.Offsets
{
    public class GridOffset : MovableOffset
    {
        public readonly int NumCols;
        public readonly int NumRows;
        public readonly UiOffset Area;

        public GridOffset(int xMin, int yMin, int xMax, int yMax, int numCols, int numRows, UiOffset area) : base(xMin, yMin, xMax, yMax)
        {
            NumCols = numCols;
            NumRows = numRows;
            Area = area;
        }

        public void MoveCols(int cols)
        {
            int distance = Area.Max.X - Area.Min.X;
            XMin += cols / NumCols * distance;
            XMax += cols / NumCols * distance;
            
            if (XMax > 1)
            {
                XMin -= 1;
                XMax -= 1;
                MoveRows(1);
            }
        }
        
        public void MoveCols(float cols)
        {
            int distance = Area.Max.X - Area.Min.X;
            XMin += (int)(cols / NumCols * distance);
            XMax += (int)(cols / NumCols * distance);
            
            if (XMax > 1)
            {
                XMin -= 1;
                XMax -= 1;
                MoveRows(1);
            }
        }

        public void MoveRows(int rows)
        {
            int distance = Area.Max.Y - Area.Min.Y;
            YMin -= rows / NumRows * distance;
            YMax -= rows / NumRows * distance;
        }
    }
}