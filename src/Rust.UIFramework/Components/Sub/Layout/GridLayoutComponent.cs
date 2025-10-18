using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(IGridLayoutComponent))]
[GenerateBuilderMethods]
public partial class GridLayoutComponent : BaseLayoutComponent, IGridLayoutComponent
{
    public override Utf8String Type => JsonDefaults.GridLayout.Type;
    public override ComponentType ComponentType => ComponentType.GridLayout;
    
    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.GridLayout.CellSizeName, _cellSize, mode);
        writer.AddField(JsonDefaults.GridLayout.SpacingName, _spacing, mode);
        writer.AddField(JsonDefaults.GridLayout.StartCornerName, _startCorner, mode);
        writer.AddField(JsonDefaults.GridLayout.StartAxisName, _startAxis, mode);
        writer.AddField(JsonDefaults.GridLayout.ConstraintName, _constraint, mode);
        writer.AddField(JsonDefaults.GridLayout.ConstraintCountName, _constraintCount, mode);
    }
}