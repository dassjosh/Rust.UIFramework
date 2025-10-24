using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IColorBlockComponent : IChildComponent
{
    [TrackedDefaults(typeof(JsonDefaults.ColorBlock), nameof(JsonDefaults.ColorBlock.HighlightedColor))]
    UiColor HighlightedColor { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ColorBlock), nameof(JsonDefaults.ColorBlock.PressedColor))]
    UiColor PressedColor { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ColorBlock), nameof(JsonDefaults.ColorBlock.SelectedColor))]
    UiColor SelectedColor { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ColorBlock), nameof(JsonDefaults.ColorBlock.ColorMultiplier))]
    float ColorMultiplier { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ColorBlock), nameof(JsonDefaults.ColorBlock.FadeDuration))]
    float FadeDuration { get; set; }
}