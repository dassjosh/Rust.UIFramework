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
    
    public UiTuple<UiSection, UiHorizontalLayout> HorizontalLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, int numCols, float colSpacing = 0f, float rowPadding = 0f, in UiPadding? padding = null)
    {
        UiSection section = Section(reference, pos, offset);
        UiHorizontalLayout layout = UiHorizontalLayout.Create(section, numCols, colSpacing, rowPadding, padding);
        AddLayout(layout);
        return new UiTuple<UiSection, UiHorizontalLayout>(section, layout);
    }
    
    public UiTuple<UiSection, UiVerticalLayout> VerticalLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, int numRows, float rowSpacing = 0f, float colPadding = 0f, in UiPadding? padding = null)
    {
        UiSection section = Section(reference, pos, offset);
        UiVerticalLayout layout = UiVerticalLayout.Create(section, numRows, rowSpacing, colPadding, padding);
        AddLayout(layout);
        return new UiTuple<UiSection, UiVerticalLayout>(section, layout);
    }
    #endregion
}