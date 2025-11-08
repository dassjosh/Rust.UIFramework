using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(IDirectionalLayoutComponent))]
[GenerateBuilderMethods]
public partial class DirectionalLayoutComponent : BaseLayoutComponent, IDirectionalLayoutComponent
{
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