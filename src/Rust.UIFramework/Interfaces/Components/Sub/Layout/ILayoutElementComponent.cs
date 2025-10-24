using Oxide.Ext.UiFramework.Json;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface ILayoutElementComponent
{
    [TrackedDefaults(typeof(JsonDefaults.LayoutElement), nameof(JsonDefaults.LayoutElement.PreferredWidth))]
    float PreferredWidth { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.LayoutElement), nameof(JsonDefaults.LayoutElement.PreferredHeight))]
    float PreferredHeight { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.LayoutElement), nameof(JsonDefaults.LayoutElement.MinWidth))]
    float MinWidth { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.LayoutElement), nameof(JsonDefaults.LayoutElement.MinHeight))]
    float MinHeight { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.LayoutElement), nameof(JsonDefaults.LayoutElement.FlexibleWidth))]
    float FlexibleWidth { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.LayoutElement), nameof(JsonDefaults.LayoutElement.PreferredWidth))]
    float FlexibleHeight { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.LayoutElement), nameof(JsonDefaults.LayoutElement.IgnoreLayout))]
    bool IgnoreLayout { get; set; }
}