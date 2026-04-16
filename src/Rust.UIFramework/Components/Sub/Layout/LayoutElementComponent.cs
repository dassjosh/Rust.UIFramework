using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class LayoutElementComponent : SubComponent
{
    [TrackedDefaults(typeof(JsonDefaults.LayoutElement), nameof(JsonDefaults.LayoutElement.PreferredWidth))]
    public partial float PreferredWidth { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.LayoutElement), nameof(JsonDefaults.LayoutElement.PreferredHeight))]
    public partial float PreferredHeight { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.LayoutElement), nameof(JsonDefaults.LayoutElement.MinWidth), typeof(JsonDefaults.LayoutElement), nameof(JsonDefaults.LayoutElement.FpMinWidth))]
    public partial float MinWidth { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.LayoutElement), nameof(JsonDefaults.LayoutElement.MinHeight), typeof(JsonDefaults.LayoutElement), nameof(JsonDefaults.LayoutElement.FpMinHeight))]
    public partial float MinHeight { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.LayoutElement), nameof(JsonDefaults.LayoutElement.FlexibleWidth), typeof(JsonDefaults.LayoutElement), nameof(JsonDefaults.LayoutElement.FpFlexibleWidth))]
    public partial float FlexibleWidth { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.LayoutElement), nameof(JsonDefaults.LayoutElement.FlexibleHeight), typeof(JsonDefaults.LayoutElement), nameof(JsonDefaults.LayoutElement.FpFlexibleHeight))]
    public partial float FlexibleHeight { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.LayoutElement), nameof(JsonDefaults.LayoutElement.IgnoreLayout))]
    public partial bool IgnoreLayout { get; set; }
    
    public override Utf8String Type => JsonDefaults.LayoutElement.Type;
    public override ComponentType ComponentType => ComponentType.LayoutElement;
    public override bool AllowMultiple => false;
    
    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.LayoutElement.PreferredWidthName, PreferredWidthTracked, mode);
        writer.AddField(JsonDefaults.LayoutElement.PreferredHeightName, PreferredHeightTracked, mode);
        writer.AddField(JsonDefaults.LayoutElement.MinWidthName, MinWidthTracked, mode);
        writer.AddField(JsonDefaults.LayoutElement.MinHeightName, MinHeightTracked, mode);
        writer.AddField(JsonDefaults.LayoutElement.FlexibleWidthName, FlexibleWidthTracked, mode);
        writer.AddField(JsonDefaults.LayoutElement.FlexibleHeightName, FlexibleHeightTracked, mode);
        writer.AddField(JsonDefaults.LayoutElement.IgnoreLayoutName, IgnoreLayoutTracked, mode);
    }
}