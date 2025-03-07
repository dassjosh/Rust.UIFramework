using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder;

public abstract partial class BaseUiBuilder
{
    #region Layouts
    public UiTuple<UiSection, GridLayout> AddGridLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, int numRows, int numCols, float rowSpacing = 0f, float colSpacing = 0f, in UiPadding? padding = null)
    {
        UiSection section = Section(reference, pos, offset);
        GridLayout layout = GridLayout.Create(section, numRows, numCols, rowSpacing, colSpacing, padding);
        AddLayout(layout);
        return new UiTuple<UiSection, GridLayout>(section, layout);
    }
    
    public UiTuple<UiSection, HorizontalLayout> AddHorizontalLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, int numCols, float colSpacing = 0f, float rowPadding = 0f, in UiPadding? padding = null)
    {
        UiSection section = Section(reference, pos, offset);
        HorizontalLayout layout = HorizontalLayout.Create(section, numCols, colSpacing, rowPadding, padding);
        AddLayout(layout);
        return new UiTuple<UiSection, HorizontalLayout>(section, layout);
    }
    
    public UiTuple<UiSection, VerticalLayout> AddVerticalLayout(in UiReference reference, in UiPosition pos, in UiOffset offset, int numRows, float rowSpacing = 0f, float colPadding = 0f, in UiPadding? padding = null)
    {
        UiSection section = Section(reference, pos, offset);
        VerticalLayout layout = VerticalLayout.Create(section, numRows, rowSpacing, colPadding, padding);
        AddLayout(layout);
        return new UiTuple<UiSection, VerticalLayout>(section, layout);
    }
    #endregion
}