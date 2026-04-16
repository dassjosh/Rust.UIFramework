using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class ColorBlockComponent : ChildComponent
{
    [TrackedDefaults(typeof(JsonDefaults.ColorBlock), nameof(JsonDefaults.ColorBlock.HighlightedColor))]
    public partial UiColor HighlightedColor { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ColorBlock), nameof(JsonDefaults.ColorBlock.PressedColor))]
    public partial UiColor PressedColor { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ColorBlock), nameof(JsonDefaults.ColorBlock.SelectedColor))]
    public partial UiColor SelectedColor { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ColorBlock), nameof(JsonDefaults.ColorBlock.ColorMultiplier))]
    public partial float ColorMultiplier { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ColorBlock), nameof(JsonDefaults.ColorBlock.FadeDuration))]
    public partial float FadeDuration { get; set; }
    
    public override ComponentType ComponentType => ComponentType.ColorBlock;

    public static readonly UiColor DefaultHighlightedColor = JsonDefaults.ColorBlock.HighlightedColor;
    public static readonly UiColor DefaultPressedColor = JsonDefaults.ColorBlock.PressedColor;
    public static readonly UiColor DefaultSelectedColor = JsonDefaults.ColorBlock.SelectedColor;

    public override void WriteComponent(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.ColorBlock.HighlightedColorName, HighlightedColorTracked, mode);
        writer.AddField(JsonDefaults.ColorBlock.PressedColorName, PressedColorTracked, mode);
        writer.AddField(JsonDefaults.ColorBlock.SelectedColorName, SelectedColorTracked, mode);
        writer.AddField(JsonDefaults.ColorBlock.ColorMultiplierName, ColorMultiplierTracked, mode);
        writer.AddField(JsonDefaults.ColorBlock.FadeDurationName, FadeDurationTracked, mode);
    }
}