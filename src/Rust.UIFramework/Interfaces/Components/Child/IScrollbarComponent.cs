using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IScrollbarComponent : IChildComponent
{
    [TrackedDefaults(typeof(JsonDefaults.ScrollBar), nameof(JsonDefaults.ScrollBar.Invert))]
    bool Invert { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ScrollBar), nameof(JsonDefaults.ScrollBar.AutoHide))]
    bool AutoHide { get; set; }
    
    string HandleSprite { get; set; }
    string TrackSprite { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ScrollBar), nameof(JsonDefaults.ScrollBar.Size))]
    float Size { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ScrollBar), nameof(JsonDefaults.ScrollBar.HandleColor))]
    UiColor HandleColor { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ScrollBar), nameof(JsonDefaults.ScrollBar.HighlightColor))]
    UiColor HighlightColor { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ScrollBar), nameof(JsonDefaults.ScrollBar.PressedColor))]
    UiColor PressedColor { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ScrollBar), nameof(JsonDefaults.ScrollBar.TrackColor))]
    UiColor TrackColor { get; set; }
}