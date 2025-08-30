using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Layouts.GridPositions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Padding;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder;

public abstract partial class BaseUiBuilder
{
    #region Generic
    public TLayout Layout<TLayout>(in UiReference reference) where TLayout : BaseUiLayout, new()
    {
        UiSection section = Section(reference);
        TLayout layout = BaseUiLayout.CreateBase<TLayout>(PluginPool, section);
        AddLayout(layout);
        return layout;
    }
    
    public TLayout Layout<TLayout>(in UiReference reference, in UiPosition pos, in UiOffset offset = default) where TLayout : BaseUiLayout, new()
    {
        TLayout layout = Layout<TLayout>(reference);
        layout.Section.SetPosition(pos, offset);
        return layout;
    }
    
    public TLayout Layout<TLayout>(BaseUiLayout parentLayout) where TLayout : BaseUiLayout, new()
    {
        UiSection parentSection = Section(parentLayout);
        TLayout layout = BaseUiLayout.CreateBase<TLayout>(PluginPool, parentSection);
        AddLayout(layout);
        return layout;
    }
    #endregion
    
    #region Grid
    public UiGridLayout GridLayout(in UiReference reference) => Layout<UiGridLayout>(reference);
    public UiGridLayout GridLayout(in UiReference reference, in UiPosition pos, in UiOffset offset) => Layout<UiGridLayout>(reference, pos, offset);
    public UiGridLayout GridLayout(BaseUiLayout parentLayout) => Layout<UiGridLayout>(parentLayout);

    public UiGridLayout GridLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, int numCols, int numRows, GridAlignment alignment = default, LayoutPadding layoutPadding = default, in UiPadding padding = default)
    {
        UiGridLayout layout = GridLayout(reference, pos, offset);
        layout.Init(numCols, numRows, alignment, layoutPadding, padding);
        return layout;
    }
    
    public UiGridLayout GridLayout(BaseUiLayout parentLayout, int numCols, int numRows, GridAlignment alignment = default, LayoutPadding layoutPadding = default, in UiPadding padding = default)
    {
        UiGridLayout layout = GridLayout(parentLayout);
        layout.Init(numCols, numRows, alignment, layoutPadding, padding);
        return layout;
    }
    #endregion

    #region Grid Position Layout
    public UiGridPositionLayout GridPositionLayout(in UiReference reference) => Layout<UiGridPositionLayout>(reference);
    public UiGridPositionLayout GridPositionLayout(in UiReference reference, in UiPosition pos, in UiOffset offset) => Layout<UiGridPositionLayout>(reference, pos, offset);
    public UiGridPositionLayout GridPositionLayout(BaseUiLayout parentLayout) => Layout<UiGridPositionLayout>(parentLayout);
    
    public UiGridPositionLayout GridPositionLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, GridPosition grid)
    {
        UiGridPositionLayout layout = GridPositionLayout(reference, pos, offset);
        layout.Init(grid);
        return layout;
    }
    
    public UiGridPositionLayout GridPositionLayout(BaseUiLayout parentLayout, GridPosition grid)
    {
        UiGridPositionLayout layout = GridPositionLayout(parentLayout);
        layout.Init(grid);
        return layout;
    }
    #endregion

    #region Directional
    public UiDirectionalLayout DirectionalLayout(in UiReference reference) => Layout<UiDirectionalLayout>(reference);
    public UiDirectionalLayout DirectionalLayout(in UiReference reference, in UiPosition pos, in UiOffset offset) => Layout<UiDirectionalLayout>(reference, pos, offset);
    public UiDirectionalLayout DirectionalLayout(BaseUiLayout parentLayout) => Layout<UiDirectionalLayout>(parentLayout);
    
    public UiDirectionalLayout DirectionalLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, int numElements, LayoutDirection direction = default, LayoutAlignment alignment = default, LayoutPadding layoutPadding = default, in UiPadding padding = default)
    {
        UiDirectionalLayout layout = DirectionalLayout(reference, pos, offset);
        layout.Init(numElements, direction, alignment, layoutPadding, padding);
        return layout;
    }
    
    public UiDirectionalLayout DirectionalLayout(BaseUiLayout parentLayout, int numElements, LayoutDirection direction = default, LayoutAlignment alignment = default, LayoutPadding layoutPadding = default, in UiPadding padding = default)
    {
        UiDirectionalLayout layout = DirectionalLayout(parentLayout);
        layout.Init(numElements, direction, alignment, layoutPadding, padding);
        return layout;
    }
    #endregion

    #region Flex
    public UiFlexBoxLayout FlexLayout(in UiReference reference) => Layout<UiFlexBoxLayout>(reference);
    public UiFlexBoxLayout FlexLayout(in UiReference reference, in UiPosition pos, in UiOffset offset) => Layout<UiFlexBoxLayout>(reference, pos, offset);
    public UiFlexBoxLayout FlexLayout(BaseUiLayout parentLayout) => Layout<UiFlexBoxLayout>(parentLayout);
    
    public UiFlexBoxLayout FlexLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, FlexDirection direction, FlexWrap wrap, FlexAlignItems alignItems, FlexJustifyContent defaultJustifyContent, in UiPadding padding = default, float gap = 0f)
    {
        UiFlexBoxLayout layout = FlexLayout(reference, pos, offset);
        layout.Init(direction, wrap, alignItems, defaultJustifyContent, padding, gap);
        return layout;
    }
    
    public UiFlexBoxLayout FlexLayout(BaseUiLayout parentLayout, FlexDirection direction, FlexWrap wrap, FlexAlignItems alignItems, FlexJustifyContent defaultJustifyContent, in UiPadding padding = default, float gap = 0f)
    {
        UiFlexBoxLayout layout = FlexLayout(parentLayout);
        layout.Init(direction, wrap, alignItems, defaultJustifyContent, padding, gap);
        return layout;
    }
    #endregion

    #region Dock
    public UiDockLayout DockLayout(in UiReference reference) => Layout<UiDockLayout>(reference);
    public UiDockLayout DockLayout(in UiReference reference, in UiPosition pos, in UiOffset offset) => Layout<UiDockLayout>(reference, pos, offset);
    public UiDockLayout DockLayout(BaseUiLayout parentLayout) => Layout<UiDockLayout>(parentLayout);
    
    public UiDockLayout DockLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, LayoutPadding layoutPadding, in UiPadding padding)
    {
        UiDockLayout layout = DockLayout(reference, pos, offset);
        layout.Init(layoutPadding, padding);
        return layout;
    }
    
    public UiDockLayout DockLayout(BaseUiLayout parentLayout, LayoutPadding layoutPadding, in UiPadding padding)
    {
        UiDockLayout layout = DockLayout(parentLayout);
        layout.Init(layoutPadding, padding);
        return layout;
    }
    #endregion
}