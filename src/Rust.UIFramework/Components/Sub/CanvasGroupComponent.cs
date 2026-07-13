using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class CanvasGroupComponent : SubComponent
{
    [TrackedDefaults(typeof(JsonDefaults.CanvasGroup), nameof(JsonDefaults.CanvasGroup.Alpha))]
    public partial float Alpha { get; set; }

    [TrackedDefaults(typeof(JsonDefaults.Common), nameof(JsonDefaults.Common.AllowRaycast))]
    public partial bool AllowRaycast { get; set; }

    [TrackedDefaults(typeof(JsonDefaults.CanvasGroup), nameof(JsonDefaults.CanvasGroup.Interactable))]
    public partial bool Interactable { get; set; }

    [TrackedDefaults(typeof(JsonDefaults.CanvasGroup), nameof(JsonDefaults.CanvasGroup.Fade))]
    public partial UiCanvasGroupFade Fade { get; set; }
    
    public override Utf8String Type => JsonDefaults.Slot.Type;
    public override ComponentType ComponentType => ComponentType.CanvasGroup;
    public override bool AllowMultiple => false;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.CanvasGroup.AlphaName, AlphaTracked, mode);
        writer.AddField(JsonDefaults.Common.AllowRaycastName, AllowRaycastTracked, mode);
        writer.AddField(JsonDefaults.CanvasGroup.InteractableName, InteractableTracked, mode);
        writer.AddField(JsonDefaults.CanvasGroup.FadeName, FadeTracked, mode);
    }
}