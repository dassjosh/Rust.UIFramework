using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class RectMask2DComponent : SubComponent
{
    [TrackedDefaults(typeof(JsonDefaults.RectMask2D), nameof(JsonDefaults.RectMask2D.Padding))]
    public partial UiPadding Padding { get; set; }
    
    public override Utf8String Type => JsonDefaults.RectMask2D.Type;
    public override ComponentType ComponentType => ComponentType.RectMask2D;
    public override bool AllowMultiple => false;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.RectMask2D.PaddingName, PaddingTracked, mode, UiPaddingFormat.LBRT);
    }
}