using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class TooltipComponent : SubComponent
{
    [TrackedDefaults(typeof(JsonDefaults.ToolTip), nameof(JsonDefaults.ToolTip.Text))]
    public partial string Text { get; set; }

    [TrackedDefaults(typeof(JsonDefaults.ToolTip), nameof(JsonDefaults.ToolTip.Delay))]
    public partial Tooltip.DelayType Delay { get; set; }

    [TrackedDefaults(typeof(JsonDefaults.ToolTip), nameof(JsonDefaults.ToolTip.Position))]
    public partial TooltipContainer.PositionMode Position { get; set; }

    [TrackedDefaults(typeof(JsonDefaults.ToolTip), nameof(JsonDefaults.ToolTip.Offset))]
    public partial Vector2 Offset { get; set; }

    [TrackedDefaults(typeof(JsonDefaults.ToolTip), nameof(JsonDefaults.ToolTip.UseCenter))]
    public partial bool UseCenter { get; set; }
    
    public override Utf8String Type => JsonDefaults.ToolTip.Type;
    public override ComponentType ComponentType => ComponentType.ToolTip;
    public override bool AllowMultiple => false;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.ToolTip.TextName, TextTracked, mode);
        writer.AddField(JsonDefaults.ToolTip.DelayName, DelayTracked, mode);
        writer.AddField(JsonDefaults.ToolTip.PositionName, PositionTracked, mode);
        writer.AddField(JsonDefaults.ToolTip.OffsetName, OffsetTracked, mode);
        writer.AddField(JsonDefaults.ToolTip.UseCenterName, UseCenterTracked, mode);
    }
}