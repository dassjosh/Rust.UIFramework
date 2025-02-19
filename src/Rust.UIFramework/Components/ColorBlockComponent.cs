using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Components;

public class ColorBlockComponent: BasePoolable, IComponent
{
    public UiColor HighlightedColor = JsonDefaults.ColorBlock.HighlightedColor;
    public UiColor PressedColor = JsonDefaults.ColorBlock.PressedColor;
    public UiColor SelectedColor = JsonDefaults.ColorBlock.SelectedColor;
    public float ColorMultiplier = JsonDefaults.ColorBlock.ColorMultiplier;
    public float FadeDuration = JsonDefaults.ColorBlock.FadeDuration;

    public void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.AddField(JsonDefaults.ColorBlock.HighlightedColorName, HighlightedColor, JsonDefaults.ColorBlock.HighlightedColor);
        writer.AddField(JsonDefaults.ColorBlock.PressedColorName, PressedColor, JsonDefaults.ColorBlock.PressedColor);
        writer.AddField(JsonDefaults.ColorBlock.SelectedColorName, SelectedColor, JsonDefaults.ColorBlock.SelectedColor);
        writer.AddField(JsonDefaults.ColorBlock.ColorMultiplierName, ColorMultiplier, JsonDefaults.ColorBlock.ColorMultiplier);
        writer.AddField(JsonDefaults.ColorBlock.FadeDurationName, FadeDuration, JsonDefaults.ColorBlock.FadeDuration);
    }

    public void Reset() 
    {
        HighlightedColor = JsonDefaults.ColorBlock.HighlightedColor;
        PressedColor = JsonDefaults.ColorBlock.PressedColor;
        SelectedColor = JsonDefaults.ColorBlock.SelectedColor;
        ColorMultiplier = JsonDefaults.ColorBlock.ColorMultiplier;
        FadeDuration = JsonDefaults.ColorBlock.FadeDuration;
    }

    protected override void EnterPool()
    {
        Reset();
    }
}