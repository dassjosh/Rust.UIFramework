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
    public UiTuple<UiSection, TLayout> Layout<TLayout>(in UiReference reference) where TLayout : BaseUiLayout, new()
    {
        UiSection section = Section(reference);
        TLayout layout = BaseUiLayout.CreateBase<TLayout>(section);
        return new UiTuple<UiSection, TLayout>(section, layout);
    }
    
    public UiTuple<UiSection, TLayout> Layout<TLayout>(in UiReference reference, in UiPosition pos, in UiOffset offset = default) where TLayout : BaseUiLayout, new()
    {
        UiTuple<UiSection, TLayout> layout = Layout<TLayout>(reference);
        UiSection section = layout;
        section.SetPosition(pos, offset);
        return layout;
    }
    #endregion
    
    #region Grid
    public UiTuple<UiSection, UiGridLayout> GridLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, int numCols, int numRows, GridAlignment alignment = default, LayoutPadding layoutPadding = default, in UiPadding padding = default)
    {
        UiSection section = Section(reference, pos, offset);
        UiGridLayout layout = UiGridLayout.Create(section, numCols, numRows, alignment, layoutPadding, padding);
        AddLayout(layout);
        return new UiTuple<UiSection, UiGridLayout>(section, layout);
    }
    
    public UiTuple<UiSection, UiGridLayout> GridLayout(BaseUiLayout parentLayout, int numCols, int numRows, GridAlignment alignment = default, LayoutPadding layoutPadding = default, in UiPadding padding = default)
    {
        UiSection section = Section(parentLayout.Reference);
        UiGridLayout layout = UiGridLayout.Create(section, numCols, numRows, alignment, layoutPadding, padding);
        AddLayout(layout);
        return new UiTuple<UiSection, UiGridLayout>(section, layout);
    }
    #endregion

    #region Grid Position Layout
    public UiTuple<UiSection, UiGridPositionLayout> GridPositionLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, GridPosition grid)
    {
        UiSection section = Section(reference, pos, offset);
        UiGridPositionLayout layout = UiGridPositionLayout.Create(section, grid);
        AddLayout(layout);
        return new UiTuple<UiSection, UiGridPositionLayout>(section, layout);
    }
    
    public UiTuple<UiSection, UiGridPositionLayout> GridPositionLayout(BaseUiLayout parentLayout, GridPosition grid)
    {
        UiSection section = Section(parentLayout.Reference);
        UiGridPositionLayout layout = UiGridPositionLayout.Create(section, grid);
        AddLayout(layout);
        return new UiTuple<UiSection, UiGridPositionLayout>(section, layout);
    }
    #endregion

    #region Directional
    public UiTuple<UiSection, UiDirectionalLayout> DirectionalLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, int numElements, LayoutDirection direction = default, LayoutAlignment alignment = default, LayoutPadding layoutPadding = default, in UiPadding padding = default)
    {
        UiSection section = Section(reference, pos, offset);
        UiDirectionalLayout layout = UiDirectionalLayout.Create(section, numElements, direction, alignment, layoutPadding, padding);
        AddLayout(layout);
        return new UiTuple<UiSection, UiDirectionalLayout>(section, layout);
    }
    
    public UiTuple<UiSection, UiDirectionalLayout> DirectionalLayout(BaseUiLayout parentLayout, int numElements, LayoutDirection direction = default, LayoutAlignment alignment = default, LayoutPadding layoutPadding = default, in UiPadding padding = default)
    {
        UiSection section = Section(parentLayout.Reference);
        UiDirectionalLayout layout = UiDirectionalLayout.Create(section, numElements, direction, alignment, layoutPadding, padding);
        AddLayout(layout);
        return new UiTuple<UiSection, UiDirectionalLayout>(section, layout);
    }
    #endregion

    #region Flex
    public UiTuple<UiSection, UiFlexBoxLayout> FlexLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, FlexDirection direction, FlexWrap wrap, FlexAlignItems alignItems, FlexJustifyContent defaultJustifyContent, in UiPadding padding = default, float gap = 0f)
    {
        UiSection section = Section(reference, pos, offset);
        UiFlexBoxLayout layout = UiFlexBoxLayout.Create(section, direction, wrap, alignItems, defaultJustifyContent, padding, gap);
        AddLayout(layout);
        return new UiTuple<UiSection, UiFlexBoxLayout>(section, layout);
    }
    
    public UiTuple<UiSection, UiFlexBoxLayout> FlexLayout(BaseUiLayout parentLayout, FlexDirection direction, FlexWrap wrap, FlexAlignItems alignItems, FlexJustifyContent defaultJustifyContent, in UiPadding padding = default, float gap = 0f)
    {
        UiSection section = Section(parentLayout.Reference);
        UiFlexBoxLayout layout = UiFlexBoxLayout.Create(section, direction, wrap, alignItems, defaultJustifyContent, padding, gap);
        AddLayout(layout);
        return new UiTuple<UiSection, UiFlexBoxLayout>(section, layout);
    }
    #endregion

    #region Dock
    public UiTuple<UiSection, UiDockLayout> DockLayout(in UiReference reference, in UiPosition pos, in UiOffset offset)
    {
        UiSection section = Section(reference, pos, offset);
        UiDockLayout layout = UiDockLayout.Create(section);
        AddLayout(layout);
        return new UiTuple<UiSection, UiDockLayout>(section, layout);
    }
    
    public UiTuple<UiSection, UiDockLayout> DockLayout(BaseUiLayout parentLayout)
    {
        UiSection section = Section(parentLayout.Reference);
        UiDockLayout layout = UiDockLayout.Create(section);
        AddLayout(layout);
        return new UiTuple<UiSection, UiDockLayout>(section, layout);
    }
    #endregion
}