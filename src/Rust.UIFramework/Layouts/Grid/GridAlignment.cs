namespace Oxide.Ext.UiFramework.Layouts;

public readonly struct GridAlignment(LayoutAlignment columnAlignment, LayoutAlignment rowAlignment)
{
    public readonly LayoutAlignment ColumnAlignment = columnAlignment;
    public readonly LayoutAlignment RowAlignment = rowAlignment;
}