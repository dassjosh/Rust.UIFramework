using System.Collections.Generic;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Layouts.GridPositions;

public class UiGridPositionLayout : BaseLayout, IFixedElementsLayout
{
    public int NumElements => (int)(Grid.NumCols * Grid.NumRows);
    public GridPosition Grid;
    public readonly List<BaseUiComponent> Elements = [];
    
    public static UiGridPositionLayout Create(in UiReference reference, GridPosition grid)
    {
        UiGridPositionLayout layout = CreateBase<UiGridPositionLayout>(reference);
        layout.Grid = grid;
        return layout;
    }
    
    public override void AddElement(BaseUiComponent element) => Elements.Add(element);

    public override void CalculateElementPositions()
    {
        Grid.Reset();
        for (int index = 0; index < Elements.Count; index++)
        {
            BaseUiComponent element = Elements[index];
            element.SetPosition(Grid.ToPosition(), default);
            Grid.MoveCols(1);
        }
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Grid = null;
        Elements.Clear();
    }
}