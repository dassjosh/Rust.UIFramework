using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public class GridLayoutComponentSerializer : BaseLayoutComponentSerializer<GridLayoutComponent>
{
    protected override void SerializeComponent(JsonFrameworkWriter writer, GridLayoutComponent component, GridLayoutComponent defaults, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.GridLayout.CellSizeName, component.CellSize, defaults.CellSize);
        writer.AddField(JsonDefaults.GridLayout.SpacingName, component.Spacing, defaults.Spacing);
        writer.AddField(JsonDefaults.GridLayout.StartCornerName, component.StartCorner, defaults.StartCorner);
        writer.AddField(JsonDefaults.GridLayout.StartAxisName, component.StartAxis, defaults.StartAxis);
        writer.AddField(JsonDefaults.GridLayout.ConstraintName, component.Constraint, defaults.Constraint);
        writer.AddField(JsonDefaults.GridLayout.ConstraintCountName, component.ConstraintCount, defaults.ConstraintCount);
    }
}