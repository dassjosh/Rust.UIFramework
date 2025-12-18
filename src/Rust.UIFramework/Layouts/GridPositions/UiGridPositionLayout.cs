using System.Collections.Generic;
using System.Diagnostics;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Layouts.GridPositions;

[DebuggerDisplay("Grid: {Grid} Elements: {Elements.Count} NumElements: {NumElements}")]
public class UiGridPositionLayout : BaseUiLayout, IFixedElementsLayout
{
    public int NumElements => Grid.NumElements;
    public GridPosition Grid;
    public readonly List<BaseUiComponent> Elements = [];
    public GridMoveMode MoveMode = GridMoveMode.Column;
    public UiPadding Padding;

    public UiGridPositionLayout Init(GridPosition grid, in UiPadding padding)
    {
        Grid = grid;
        Padding = padding;
        return this;
    }

    public UiGridPositionLayout SetMoveMode(GridMoveMode mode)
    {
        MoveMode = mode;
        return this;
    }
    
    public UiGridPositionLayout SetPadding(in UiPadding padding)
    {
        Padding = padding;
        return this;
    }

    public override void AddElement(BaseUiComponent element) => Elements.Add(element);

    public override void CalculateElementPositions()
    {
        Grid.Reset();
        for (int index = 0; index < Elements.Count; index++)
        {
            BaseUiComponent element = Elements[index];
            element.SetPosition(Grid.ToPosition()).SetOffsetPadding(Padding);
            switch (MoveMode)
            {
                case GridMoveMode.Column:
                    Grid.MoveCols(1);
                    break;
                case GridMoveMode.Row:
                    Grid.MoveRows(1);
                    break;
            }
        }
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Grid = null;
        Elements.Clear();
        MoveMode = GridMoveMode.Column;
        Padding = default;
    }
}