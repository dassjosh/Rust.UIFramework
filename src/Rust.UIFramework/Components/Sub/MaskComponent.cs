using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class MaskComponent : SubComponent
{
    [TrackedDefaults(typeof(JsonDefaults.Mask), nameof(JsonDefaults.Mask.ShowMaskGraphic))]
    public partial bool ShowMaskGraphic { get; set; }
    
    public override Utf8String Type => JsonDefaults.Mask.Type;
    public override ComponentType ComponentType => ComponentType.Mask;
    public override bool AllowMultiple => false;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.Mask.ShowMaskGraphicName, ShowMaskGraphicTracked, mode);
    }
}