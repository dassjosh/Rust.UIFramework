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
    public float GetScrollViewYMin(float totalRows)
    {
        return Mathf.Min(1 - (totalRows / NumRows), 0);
    }
    
    [Obsolete("Use ApplyScrollViewVertical instead")]
    public float GetScrollViewYMin(int count, int countPerRow)
    {
        float totalRows = count / countPerRow;
        if (count % Mathf.RoundToInt(countPerRow) != 0)
        {
            totalRows++;
        }

        return GetScrollViewYMin(totalRows);
    }
    
    [Obsolete("Use ApplyScrollViewHorizontal instead")]
    public float GetScrollViewXMax(float totalColumns)
    {
        return Mathf.Max(totalColumns / NumCols, 1);
    }
    
    [Obsolete("Use ApplyScrollViewHorizontal instead")]
    public float GetScrollViewXMax(int totalItems, int countPerRow)
    {
        float totalRows = totalItems / countPerRow;
        if (totalItems % Mathf.RoundToInt(countPerRow) != 0)
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
#pragma warning disable CS0618 // Type or member is obsolete
        float minY = GetScrollViewYMin(totalItems, Mathf.RoundToInt(NumCols));
#pragma warning restore CS0618 // Type or member is obsolete
        SetYScale(1 / (1 - minY));
        UiPosition position = scrollView.ScrollView.ContentTransform.Position;
        scrollView.UpdateContentTransform(position.WithYMin(minY));
    }

    public void ApplyScrollViewContentHorizontal(int totalItems, UiScrollView scrollView)
    {
#pragma warning disable CS0618 // Type or member is obsolete
        float maxX = GetScrollViewXMax(totalItems, Mathf.RoundToInt(NumRows));
#pragma warning restore CS0618 // Type or member is obsolete
        SetXScale(1 / maxX);
        UiPosition position = scrollView.ScrollView.ContentTransform.Position;
        scrollView.UpdateContentTransform(position.WithXMax(maxX));
    }

    private void ApplyPadding()
    {
        float xPadding = _xPadding * _xScale;
        float yPadding = _yPadding * _yScale;
        XMin += xPadding;
        XMax -= xPadding;
        YMin += yPadding;
        YMax -= yPadding;
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