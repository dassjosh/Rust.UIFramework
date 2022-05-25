namespace Oxide.Ext.UiFramework.Offsets
{
    public class GridOffset : MovableOffset
    {
        public readonly int NumCols;
        public readonly int NumRows;
        public readonly float Width;
        public readonly float Height;
        
        public GridOffset(float xMin, float yMin, float xMax, float yMax, int numCols, int numRows, float width, float height) : base(xMin, yMin, xMax, yMax)
        {
            NumCols = numCols;
            NumRows = numRows;
            Width = width;
            Height = height;
        }
        
        public void MoveCols(int cols)
        {
            MoveCols((float)cols);
        }
        
        public void MoveCols(float cols)
        {
            float distance = cols / NumCols * Width;
            XMin += distance;
            XMax += distance;
            
            if (XMax > Width)
            {
                XMin -= Width;
                XMax -= Width;
                MoveRows(1);
            }
        }
        
        public void MoveRows(int rows)
        {
            float distance = rows / (float)NumRows * Height;
            YMin -= distance;
            YMax -= distance;
        }
    }
}