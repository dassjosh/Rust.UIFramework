using System;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Positions;

public class GridPosition : BasePosition
{
    public readonly float NumCols;
    public readonly float NumRows;

    private float _xScale = 1;
    private float _yScale = 1;
    private readonly float _xPadding;
    private readonly float _yPadding;

    public GridPosition(float xMin, float yMin, float xMax, float yMax, float xPad, float yPad, float numCols, float numRows) : base(xMin, yMin, xMax, yMax)
    {
        NumCols = numCols;
        NumRows = numRows;
        _xPadding = xPad;
        _yPadding = yPad;
        ApplyPadding();
    }

    public void MoveCols(int cols) => MoveCols((float)cols);
        
    public void MoveCols(float cols)
    {
        XMin += cols / NumCols * _xScale;
        XMax += cols / NumCols * _xScale;
            
        if (XMax > 1)
        {
            XMin -= 1;
            XMax -= 1;
            MoveRows(1f);
        }
    }

    public void MoveRows(int rows) => MoveRows((float)rows);
    
    public void MoveRows(float rows)
    {
        YMin -= rows / NumRows * _yScale;
        YMax -= rows / NumRows * _yScale;
    }

    [Obsolete("Use ApplyScrollViewVertical instead")]
    public float GetScrollViewYMin(int totalRows)
    {
        return Mathf.Min(1 - (totalRows / NumRows), 0);
    }
    
    [Obsolete("Use ApplyScrollViewVertical instead")]
    public float GetScrollViewYMin(int count, int countPerRow)
    {
        int totalRows = count / countPerRow;
        if (count % countPerRow != 0)
        {
            totalRows++;
        }

        return GetScrollViewYMin(totalRows);
    }
    
    [Obsolete("Use ApplyScrollViewHorizontal instead")]
    public float GetScrollViewXMax(int totalColumns)
    {
        return Mathf.Max(1 + (totalColumns / NumCols), 1);
    }
    
    [Obsolete("Use ApplyScrollViewHorizontal instead")]
    public float GetScrollViewXMax(int count, int countPerColumn)
    {
        int totalRows = count / countPerColumn;
        if (count % countPerColumn != 0)
        {
            totalRows++;
        }

        return GetScrollViewXMax(totalRows);
    }
    
    public void SetXScale(float scale)
    {
        _xScale = scale;
        Reset();
        XMax = XMin + (XMax - XMin) * scale;
    }

    public void SetYScale(float scale)
    {
        _yScale = scale;
        Reset();
        YMin = YMax - (YMax - YMin) * scale;
    }
    
    public void ApplyScrollViewContentVertical(int totalItems, UiScrollView scrollView)
    {
        float minY = GetScrollViewYMin(totalItems, Mathf.RoundToInt(NumCols));
        SetYScale(1 / (1 - minY));
        UiPosition position = scrollView.ScrollView.ContentTransform.Position;
        scrollView.UpdateContentTransform(position.WithYMin(minY));
    }

    public void ApplyScrollViewContentHorizontal(int totalItems, UiScrollView scrollView)
    {
        float maxX = GetScrollViewXMax(totalItems, Mathf.RoundToInt(NumRows));
        SetXScale(1 / maxX);
        UiPosition position = scrollView.ScrollView.ContentTransform.Position;
        scrollView.UpdateContentTransform(position.WithXMax(maxX));
    }

    private void ApplyPadding()
    {
        XMin += _xPadding * _xScale;
        XMax -= _xPadding * _xScale;
        YMin += _yPadding * _yScale;
        YMax -= _yPadding * _yScale;
    }

    public void ResetScale()
    {
        _xScale = 1;
        _yScale = 1;
    }
    
    public override void Reset()
    {
        base.Reset();
        ApplyPadding();
    }
}