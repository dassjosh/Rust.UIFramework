using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class OutlineComponent : SubComponent
{
    public partial UiColor Color { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Outline), nameof(JsonDefaults.Outline.Distance), typeof(JsonDefaults.Outline), nameof(JsonDefaults.Outline.FpDistance))]
    public partial Vector2 Distance { get; set; }
    
    public partial bool UseGraphicAlpha { get; set; }
    
    public override Utf8String Type => JsonDefaults.Outline.Type;
    public override ComponentType ComponentType => ComponentType.Outline;
    public override bool AllowMultiple => true;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.Color.ColorName, ColorTracked, mode);
        writer.AddField(JsonDefaults.Outline.DistanceName, DistanceTracked, mode);
        writer.AddKeyField(JsonDefaults.Outline.UseGraphicAlphaName, UseGraphicAlphaTracked, mode);
    }
}