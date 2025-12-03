using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class ContentSizeFitterComponent : SubComponent
{
    [TrackedDefaults(typeof(JsonDefaults.ContentSizeFitterData), nameof(JsonDefaults.ContentSizeFitterData.HorizontalFit))]
    public partial ContentSizeFitter.FitMode HorizontalFit { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ContentSizeFitterData), nameof(JsonDefaults.ContentSizeFitterData.VerticalFit))]
    public partial ContentSizeFitter.FitMode VerticalFit { get; set; }
    
    public override Utf8String Type => JsonDefaults.ContentSizeFitterData.Type;
    public override ComponentType ComponentType => ComponentType.ContentSizeFitter;
    public override bool AllowMultiple => false;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.ContentSizeFitterData.HorizontalFitName, _horizontalFit, mode);
        writer.AddField(JsonDefaults.ContentSizeFitterData.VerticalFitName, _verticalFit, mode);
    }
}