using System;
using System.Diagnostics;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Positions;

[DebuggerDisplay("Cols: {NumCols}, Rows: {NumRows} XMin: {XMin}, YMin: {YMin}, XMax: {XMax}, YMax: {YMax} Padding: {Padding}")]
public class GridPosition : BasePosition
{
    public readonly float NumCols;
    public readonly float NumRows;
    
    public int NumElements => Mathf.FloorToInt(NumCols) * Mathf.FloorToInt(NumRows);

    private float _xScale = 1;
    private float _yScale = 1;
    
    public UiPadding Padding;

    public GridPosition(UiPosition initialState, UiPadding padding, float numCols, float numRows) : base(initialState.XMin, initialState.YMin, initialState.XMax, initialState.YMax)
    {
        Padding = padding;
        NumCols = numCols;
        NumRows = numRows;
        ApplyPadding();
    }
    
    [Obsolete("Use GridPosition(UiPosition initialState, UiPadding padding, float numCols, float numRows) Instead")]
    public GridPosition(float xMin, float yMin, float xMax, float yMax, float horizontalPadding, float verticalPadding, float numCols, float numRows) : base(xMin, yMin, xMax, yMax)
    {
        NumCols = numCols;
        NumRows = numRows;
        Padding = new UiPadding(horizontalPadding, verticalPadding);
        ApplyPadding();
    }

    public void MoveCols(int cols) => MoveCols((float)cols);
        
    public void MoveCols(float cols)
    {
        float amount = cols / NumCols * _xScale;
        XMin += amount;
        XMax += amount;
            
        if (XMax > 1 && !Mathf.Approximately(XMax, 1))
        {
            ResetX();
            MoveRows(1f);
        }
    }

    public void MoveRows(int rows) => MoveRows((float)rows);
    
    public void MoveRows(float rows)
    {
        float amount = rows / NumRows * _yScale;
        YMin -= amount;
        YMax -= amount;
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
        UiPosition position = scrollView.GetOrCreateContentTransform().Position;
        scrollView.UpdateContentTransform(position.WithYMin(minY));
    }

    public void ApplyScrollViewContentHorizontal(int totalItems, UiScrollView scrollView)
    {
#pragma warning disable CS0618 // Type or member is obsolete
        float maxX = GetScrollViewXMax(totalItems, Mathf.RoundToInt(NumRows));
#pragma warning restore CS0618 // Type or member is obsolete
        SetXScale(1 / maxX);
        UiPosition position = scrollView.GetOrCreateContentTransform().Position;
        scrollView.UpdateContentTransform(position.WithXMax(maxX));
    }

    private void ApplyPadding()
    {
        ApplyHorizontalPadding();
        ApplyVerticalPadding();
    }

    private void ApplyHorizontalPadding()
    {
        UiPadding padding = Padding;
        XMin += padding.Left * _xScale;
        XMax -= padding.Right * _xScale;
    }

    private void ApplyVerticalPadding()
    {
        UiPadding padding = Padding;
        YMin += padding.Bottom * _yScale;
        YMax -= padding.Top * _yScale;
    }

    public void ResetScale()
    {
        _xScale = 1;
        _yScale = 1;
    }

    public override void ResetX()
    {
        base.ResetX();
        ApplyHorizontalPadding();
    }
    
    public override void ResetY()
    {
        base.ResetY();
        ApplyVerticalPadding();
    }

    public override void Reset()
    {
        base.Reset();
        ApplyPadding();
    }
}