using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class GridLayoutComponent : BaseLayoutComponent
{
    [TrackedDefaults(typeof(JsonDefaults.GridLayout), nameof(JsonDefaults.GridLayout.CellSize))]
    public partial Vector2 CellSize { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.GridLayout), nameof(JsonDefaults.GridLayout.Spacing))]
    public partial Vector2 Spacing { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.GridLayout), nameof(JsonDefaults.GridLayout.StartCorner))]
    public partial GridLayoutGroup.Corner StartCorner { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.GridLayout), nameof(JsonDefaults.GridLayout.StartAxis))]
    public partial GridLayoutGroup.Axis StartAxis { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.GridLayout), nameof(JsonDefaults.GridLayout.Constraint))]
    public partial GridLayoutGroup.Constraint Constraint { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.GridLayout), nameof(JsonDefaults.GridLayout.ConstraintCount))]
    public partial int ConstraintCount { get; set; }
    
    public override Utf8String Type => JsonDefaults.GridLayout.Type;
    public override ComponentType ComponentType => ComponentType.GridLayout;
    
    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        base.WriteComponentFields(writer, mode);
        writer.AddField(JsonDefaults.GridLayout.CellSizeName, _cellSize, mode);
        writer.AddField(JsonDefaults.GridLayout.SpacingName, _spacing, mode);
        writer.AddField(JsonDefaults.GridLayout.StartCornerName, _startCorner, mode);
        writer.AddField(JsonDefaults.GridLayout.StartAxisName, _startAxis, mode);
        writer.AddField(JsonDefaults.GridLayout.ConstraintName, _constraint, mode);
        writer.AddField(JsonDefaults.GridLayout.ConstraintCountName, _constraintCount, mode);
    }
}