using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Layouts.GridPositions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
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

    public UiGridLayout GridLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, int numCols, int numRows, GridAlignment alignment = default, in UiPadding padding = default)
        => GridLayout(reference, pos, offset).Init(numCols, numRows, alignment, padding);

    public UiGridLayout GridLayout(BaseUiLayout parentLayout, int numCols, int numRows, GridAlignment alignment = default, in UiPadding padding = default) 
        => GridLayout(parentLayout).Init(numCols, numRows, alignment, padding);

    #endregion

    #region Grid Position Layout
    public UiGridPositionLayout GridPositionLayout(in UiReference reference) => Layout<UiGridPositionLayout>(reference);
    public UiGridPositionLayout GridPositionLayout(in UiReference reference, in UiPosition pos, in UiOffset offset) => Layout<UiGridPositionLayout>(reference, pos, offset);
    public UiGridPositionLayout GridPositionLayout(BaseUiLayout parentLayout) => Layout<UiGridPositionLayout>(parentLayout);
    
    public UiGridPositionLayout GridPositionLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, GridPosition grid, in UiPadding padding) => GridPositionLayout(reference, pos, offset).Init(grid, padding);

    public UiGridPositionLayout GridPositionLayout(BaseUiLayout parentLayout, GridPosition grid, in UiPadding padding) => GridPositionLayout(parentLayout).Init(grid, padding);

    #endregion

    #region Directional
    public UiDirectionalLayout DirectionalLayout(in UiReference reference) => Layout<UiDirectionalLayout>(reference);
    public UiDirectionalLayout DirectionalLayout(in UiReference reference, in UiPosition pos, in UiOffset offset) => Layout<UiDirectionalLayout>(reference, pos, offset);
    public UiDirectionalLayout DirectionalLayout(BaseUiLayout parentLayout) => Layout<UiDirectionalLayout>(parentLayout);
    
    public UiDirectionalLayout DirectionalLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, int numElements, LayoutDirection direction = default, LayoutAlignment alignment = default, in UiPadding padding = default)
        => DirectionalLayout(reference, pos, offset).Init(numElements, direction, alignment, padding);

    public UiDirectionalLayout DirectionalLayout(BaseUiLayout parentLayout, int numElements, LayoutDirection direction = default, LayoutAlignment alignment = default, in UiPadding padding = default)
        => DirectionalLayout(parentLayout).Init(numElements, direction, alignment, padding);

    #endregion

    #region Flex
    public UiFlexBoxLayout FlexLayout(in UiReference reference) => Layout<UiFlexBoxLayout>(reference);
    public UiFlexBoxLayout FlexLayout(in UiReference reference, in UiPosition pos, in UiOffset offset) => Layout<UiFlexBoxLayout>(reference, pos, offset);
    public UiFlexBoxLayout FlexLayout(BaseUiLayout parentLayout) => Layout<UiFlexBoxLayout>(parentLayout);
    
    public UiFlexBoxLayout FlexLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, FlexDirection direction, FlexWrap wrap, FlexAlignItems alignItems, FlexJustifyContent defaultJustifyContent, in UiPadding padding = default, float gap = 0f)
        => FlexLayout(reference, pos, offset).Init(direction, wrap, alignItems, defaultJustifyContent, padding, gap);

    public UiFlexBoxLayout FlexLayout(BaseUiLayout parentLayout, FlexDirection direction, FlexWrap wrap, FlexAlignItems alignItems, FlexJustifyContent defaultJustifyContent, in UiPadding padding = default, float gap = 0f)
        => FlexLayout(parentLayout).Init(direction, wrap, alignItems, defaultJustifyContent, padding, gap);

    #endregion

    #region Dock
    public UiDockLayout DockLayout(in UiReference reference) => Layout<UiDockLayout>(reference);
    public UiDockLayout DockLayout(in UiReference reference, in UiPosition pos, in UiOffset offset) => Layout<UiDockLayout>(reference, pos, offset);
    public UiDockLayout DockLayout(BaseUiLayout parentLayout) => Layout<UiDockLayout>(parentLayout);
    
    public UiDockLayout DockLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, in UiPadding padding) => DockLayout(reference, pos, offset).SetPadding(padding);

    public UiDockLayout DockLayout(BaseUiLayout parentLayout, in UiPadding padding) => DockLayout(parentLayout).SetPadding(padding);

    #endregion
}