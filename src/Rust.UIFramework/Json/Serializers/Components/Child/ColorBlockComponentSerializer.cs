using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public class ColorBlockComponentSerializer : BaseSerializer<ColorBlockComponent>
{
    public override void Serialize(JsonFrameworkWriter writer, ColorBlockComponent component, ColorBlockComponent defaults, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.ColorBlock.HighlightedColorName, component.HighlightedColor, defaults.HighlightedColor);
        writer.AddField(JsonDefaults.ColorBlock.PressedColorName, component.PressedColor, defaults.PressedColor);
        writer.AddField(JsonDefaults.ColorBlock.SelectedColorName, component.SelectedColor, defaults.SelectedColor);
        writer.AddField(JsonDefaults.ColorBlock.ColorMultiplierName, component.ColorMultiplier, defaults.ColorMultiplier);
        writer.AddField(JsonDefaults.ColorBlock.FadeDurationName, component.FadeDuration, defaults.FadeDuration);
    }
}