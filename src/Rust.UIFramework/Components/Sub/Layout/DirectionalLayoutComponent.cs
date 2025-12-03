using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class DirectionalLayoutComponent : BaseLayoutComponent
{
    [TrackedDefaults(typeof(JsonDefaults.DirectionalLayout), nameof(JsonDefaults.DirectionalLayout.Spacing))]
    public partial float Spacing { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.DirectionalLayout), nameof(JsonDefaults.DirectionalLayout.ChildForceExpandWidth))]
    public partial bool ChildForceExpandWidth { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.DirectionalLayout), nameof(JsonDefaults.DirectionalLayout.ChildForceExpandHeight))]
    public partial bool ChildForceExpandHeight { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.DirectionalLayout), nameof(JsonDefaults.DirectionalLayout.ChildControlWidth))]
    public partial bool ChildControlWidth { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.DirectionalLayout), nameof(JsonDefaults.DirectionalLayout.ChildControlHeight))]
    public partial bool ChildControlHeight { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.DirectionalLayout), nameof(JsonDefaults.DirectionalLayout.ChildScaleWidth))]
    public partial bool ChildScaleWidth { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.DirectionalLayout), nameof(JsonDefaults.DirectionalLayout.ChildScaleHeight))]
    public partial bool ChildScaleHeight { get; set; }
    
    public partial LayoutDirection Direction { get; set; }
    
    public override Utf8String Type => Direction == LayoutDirection.Horizontal ? JsonDefaults.DirectionalLayout.HorizontalType : JsonDefaults.DirectionalLayout.VerticalType;
    public override ComponentType ComponentType => ComponentType.DirectionalLayout;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        base.WriteComponentFields(writer, mode);
        writer.AddField(JsonDefaults.DirectionalLayout.SpacingName, _spacing, mode);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildForceExpandWidthName, _childForceExpandWidth, mode);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildForceExpandHeightName, _childForceExpandHeight, mode);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildControlWidthName, _childControlWidth, mode);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildControlHeightName, _childControlHeight, mode);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildScaleWidthName, _childScaleWidth, mode);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildScaleHeightName, _childScaleHeight, mode);
    }
}