using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder;

public abstract partial class BaseUiBuilder
{
    #region Grid
    public UiTuple<UiSection, UiGridLayout> GridLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, int numCols, int numRows, GridAlignment alignment = default, LayoutPadding layoutPadding = default, in UiPadding padding = default)
    {
        UiSection section = Section(reference, pos, offset);
        UiGridLayout layout = UiGridLayout.Create(section, numCols, numRows, alignment, layoutPadding, padding);
        AddLayout(layout);
        return new UiTuple<UiSection, UiGridLayout>(section, layout);
    }
    
    public UiTuple<UiSection, UiGridLayout> GridLayout(BaseLayout parentLayout, int numCols, int numRows, GridAlignment alignment = default, LayoutPadding layoutPadding = default, in UiPadding padding = default)
    {
        UiSection section = Section(parentLayout.Reference);
        UiGridLayout layout = UiGridLayout.Create(section, numCols, numRows, alignment, layoutPadding, padding);
        AddLayout(layout);
        return new UiTuple<UiSection, UiGridLayout>(section, layout);
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
    
    public UiTuple<UiSection, UiDirectionalLayout> DirectionalLayout(BaseLayout parentLayout, int numElements, LayoutDirection direction = default, LayoutAlignment alignment = default, LayoutPadding layoutPadding = default, in UiPadding padding = default)
    {
        UiSection section = Section(parentLayout.Reference);
        UiDirectionalLayout layout = UiDirectionalLayout.Create(section, numElements, direction, alignment, layoutPadding, padding);
        AddLayout(layout);
        return new UiTuple<UiSection, UiDirectionalLayout>(section, layout);
    }
    #endregion
}