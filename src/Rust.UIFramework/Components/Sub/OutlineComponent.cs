using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(IOutlineComponent))]
[GenerateBuilderMethods]
public partial class OutlineComponent : SubComponent, IOutlineComponent
{
    public override Utf8String Type => JsonDefaults.Outline.Type;
    public override ComponentType ComponentType => ComponentType.Outline;
    public override bool AllowMultiple => true;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.Color.ColorName, _color, mode);
        writer.AddField(JsonDefaults.Outline.DistanceName, _distance, mode);
        writer.AddKeyField(JsonDefaults.Outline.UseGraphicAlphaName, _useGraphicAlpha.ShouldSerialize(mode) && _useGraphicAlpha.Value);
    }
}