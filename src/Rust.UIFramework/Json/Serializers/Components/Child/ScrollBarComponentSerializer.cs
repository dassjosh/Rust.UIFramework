using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public class ScrollBarComponentSerializer : BaseSerializer<ScrollbarComponent>
{
    public override void Serialize(JsonFrameworkWriter writer, ScrollbarComponent component, ScrollbarComponent defaults, SerializeMode mode)
    {
        writer.WriteStartObject();
        writer.AddField(JsonDefaults.ScrollBar.InvertName, component.Invert, defaults.Invert);
        writer.AddField(JsonDefaults.ScrollBar.AutoHideName, component.AutoHide, defaults.AutoHide);
        writer.AddField(JsonDefaults.ScrollBar.HandleSprite, component.HandleSprite, defaults.HandleSprite);
        writer.AddField(JsonDefaults.ScrollBar.TrackSprite, component.TrackSprite, defaults.TrackSprite);
        writer.AddField(JsonDefaults.ScrollBar.SizeName, component.Size, defaults.Size);
        writer.AddField(JsonDefaults.ScrollBar.HandleColorName, component.HandleColor, defaults.HandleColor);
        writer.AddField(JsonDefaults.ScrollBar.HighlightColorName, component.HighlightColor, defaults.HighlightColor);
        writer.AddField(JsonDefaults.ScrollBar.PressedColorName, component.PressedColor, defaults.PressedColor);
        writer.AddField(JsonDefaults.ScrollBar.TrackColorName, component.TrackColor, defaults.TrackColor);
        writer.WriteEndObject();
    }
}