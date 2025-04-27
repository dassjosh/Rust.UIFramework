namespace Oxide.Ext.UiFramework.Offsets;

public class GridOffset(float xMin, float yMin, float xMax, float yMax, int numCols, int numRows, float width, float height) : BaseOffset(xMin, yMin, xMax, yMax)
{
    public readonly int NumCols = numCols;
    public readonly int NumRows = numRows;
    public readonly float Width = width;
    public readonly float Height = height;

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