using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public abstract partial class BaseLayoutComponent : SubComponent
{
    [TrackedDefaults(typeof(JsonDefaults.Layout), nameof(JsonDefaults.Layout.ChildAlignment))]
    public partial TextAnchor ChildAlignment { get; set; }
    public partial UiPadding Padding { get; set; }
    
    public BaseUiComponent Owner { get; internal set; }
    public UiReference Reference => Owner.Reference;
    
    public override bool AllowMultiple => false;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.Layout.ChildAlignmentName, ChildAlignmentTracked, mode);
        writer.AddField(JsonDefaults.Layout.PaddingName, PaddingTracked, mode, UiPaddingFormat.LTRB);
    }

    public static implicit operator UiReference(BaseLayoutComponent layout) => layout.Reference;

    protected override void OnReset()
    {
        Owner = null;
    }
}