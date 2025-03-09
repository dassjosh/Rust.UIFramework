using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder;

public abstract partial class BaseUiBuilder
{
    #region Layouts
    public UiTuple<UiSection, UiGridLayout> GridLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, int numCols, int numRows, float rowSpacing = 0f, float colSpacing = 0f, in UiPadding? padding = null)
    {
        UiSection section = Section(reference, pos, offset);
        UiGridLayout layout = UiGridLayout.Create(section, numCols, numRows, rowSpacing, colSpacing, padding);
        AddLayout(layout);
        return new UiTuple<UiSection, UiGridLayout>(section, layout);
    }
    
    public UiTuple<UiSection, UiGridLayout> GridLayout(BaseLayout parentLayout, int numCols, int numRows, float rowSpacing = 0f, float colSpacing = 0f, in UiPadding? padding = null)
    {
        UiSection section = Section(parentLayout);
        UiGridLayout layout = UiGridLayout.Create(section, numCols, numRows, rowSpacing, colSpacing, padding);
        AddLayout(layout);
        return new UiTuple<UiSection, UiGridLayout>(section, layout);
    }
    
    public UiTuple<UiSection, UiDirectionalLayout> DirectionalLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, int numElements, LayoutDirection direction = LayoutDirection.LeftToRight, float directionalSpacing = 0f, float nonDirectionalSpacing = 0f, in UiPadding? padding = null)
    {
        UiSection section = Section(reference, pos, offset);
        UiDirectionalLayout layout = UiDirectionalLayout.Create(section, numElements, direction, directionalSpacing, nonDirectionalSpacing, padding);
        AddLayout(layout);
        return new UiTuple<UiSection, UiDirectionalLayout>(section, layout);
    }
    
    public UiTuple<UiSection, UiDirectionalLayout> DirectionalLayout(BaseLayout parentLayout, int numElements, LayoutDirection direction = LayoutDirection.LeftToRight, float directionalSpacing = 0f, float nonDirectionalSpacing = 0f, in UiPadding? padding = null)
    {
        UiSection section = Section(parentLayout);
        UiDirectionalLayout layout = UiDirectionalLayout.Create(section, numElements, direction, directionalSpacing, nonDirectionalSpacing, padding);
        AddLayout(layout);
        return new UiTuple<UiSection, UiDirectionalLayout>(section, layout);
    }
    #endregion
}