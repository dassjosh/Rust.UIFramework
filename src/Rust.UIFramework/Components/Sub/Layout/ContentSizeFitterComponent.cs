using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(IContentSizeFitterComponent))]
[GenerateBuilderMethods]
public partial class ContentSizeFitterComponent : SubComponent, IContentSizeFitterComponent
{
    public override Utf8String Type => JsonDefaults.ContentSizeFitterData.Type;
    public override ComponentType ComponentType => ComponentType.ContentSizeFitter;
    public override bool AllowMultiple => false;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.ContentSizeFitterData.HorizontalFitName, _horizontalFit, mode);
        writer.AddField(JsonDefaults.ContentSizeFitterData.VerticalFitName, _verticalFit, mode);
    }
}