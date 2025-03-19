using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public class ColorBlockComponent : ChildComponent
{
    public UiColor HighlightedColor;
    public UiColor PressedColor;
    public UiColor SelectedColor;
    public float ColorMultiplier;
    public float FadeDuration;

    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.AddField(JsonDefaults.ColorBlock.HighlightedColorName, HighlightedColor, JsonDefaults.ColorBlock.HighlightedColor);
        writer.AddField(JsonDefaults.ColorBlock.PressedColorName, PressedColor, JsonDefaults.ColorBlock.PressedColor);
        writer.AddField(JsonDefaults.ColorBlock.SelectedColorName, SelectedColor, JsonDefaults.ColorBlock.SelectedColor);
        writer.AddField(JsonDefaults.ColorBlock.ColorMultiplierName, ColorMultiplier, JsonDefaults.ColorBlock.ColorMultiplier);
        writer.AddField(JsonDefaults.ColorBlock.FadeDurationName, FadeDuration, JsonDefaults.ColorBlock.FadeDuration);
    }

    public override void Reset() 
    {
        HighlightedColor = JsonDefaults.ColorBlock.HighlightedColor;
        PressedColor = JsonDefaults.ColorBlock.PressedColor;
        SelectedColor = JsonDefaults.ColorBlock.SelectedColor;
        ColorMultiplier = JsonDefaults.ColorBlock.ColorMultiplier;
        FadeDuration = JsonDefaults.ColorBlock.FadeDuration;
    }
}