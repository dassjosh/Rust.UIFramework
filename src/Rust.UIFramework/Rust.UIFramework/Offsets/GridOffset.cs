using System;

namespace Oxide.Ext.UiFramework.Offsets
{
    public class GridOffset : MovableOffset
    {
        public readonly int NumCols;
        public readonly int NumRows;
        public readonly int Width;
        public readonly int Height;

        public GridOffset(int xMin, int yMin, int xMax, int yMax, int numCols, int numRows, int width, int height) : base(xMin, yMin, xMax, yMax)
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
            int distance = (int)Math.Floor(cols / NumCols * Width);
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
            int distance = (int)Math.Floor(rows / (float)NumRows * Height);
            YMin -= distance;
            YMax -= distance;
        }
    }
}