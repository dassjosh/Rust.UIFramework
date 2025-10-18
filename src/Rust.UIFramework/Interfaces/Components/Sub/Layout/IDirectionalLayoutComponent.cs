using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IDirectionalLayoutComponent : ILayoutComponent
{
    [TrackedDefaults(typeof(JsonDefaults.DirectionalLayout), nameof(JsonDefaults.DirectionalLayout.Spacing))]
    float Spacing { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.DirectionalLayout), nameof(JsonDefaults.DirectionalLayout.ChildForceExpandWidth))]
    bool ChildForceExpandWidth { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.DirectionalLayout), nameof(JsonDefaults.DirectionalLayout.ChildForceExpandHeight))]
    bool ChildForceExpandHeight { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.DirectionalLayout), nameof(JsonDefaults.DirectionalLayout.ChildControlWidth))]
    bool ChildControlWidth { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.DirectionalLayout), nameof(JsonDefaults.DirectionalLayout.ChildControlHeight))]
    bool ChildControlHeight { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.DirectionalLayout), nameof(JsonDefaults.DirectionalLayout.ChildScaleWidth))]
    bool ChildScaleWidth { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.DirectionalLayout), nameof(JsonDefaults.DirectionalLayout.ChildScaleHeight))]
    bool ChildScaleHeight { get; set; }
    
    LayoutDirection Direction { get; set; }
}