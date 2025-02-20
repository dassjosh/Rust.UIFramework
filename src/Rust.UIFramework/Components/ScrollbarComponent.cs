using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public class ScrollbarComponent : ChildComponent
{
    public bool Invert = JsonDefaults.ScrollBar.Invert;
    public bool AutoHide = JsonDefaults.ScrollBar.AutoHide;
    public string HandleSprite = JsonDefaults.Common.NullValue;
    public string TrackSprite = JsonDefaults.Common.NullValue;
    public float Size = JsonDefaults.ScrollBar.Size;
    public UiColor HandleColor = JsonDefaults.ScrollBar.HandleColor;
    public UiColor HighlightColor = JsonDefaults.ScrollBar.HighlightColor;
    public UiColor PressedColor = JsonDefaults.ScrollBar.PressedColor;
    public UiColor TrackColor = JsonDefaults.ScrollBar.TrackColor;
    
    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddField(JsonDefaults.ScrollBar.InvertName, Invert, JsonDefaults.ScrollBar.Invert);
        writer.AddField(JsonDefaults.ScrollBar.AutoHideName, AutoHide, JsonDefaults.ScrollBar.AutoHide);
        writer.AddField(JsonDefaults.ScrollBar.HandleSprite, HandleSprite, JsonDefaults.Common.NullValue);
        writer.AddField(JsonDefaults.ScrollBar.TrackSprite, TrackSprite, JsonDefaults.Common.NullValue);
        writer.AddField(JsonDefaults.ScrollBar.SizeName, Size, JsonDefaults.ScrollBar.Size);
        writer.AddField(JsonDefaults.ScrollBar.HandleColorName, HandleColor, JsonDefaults.ScrollBar.HandleColor);
        writer.AddField(JsonDefaults.ScrollBar.HighlightColorName, HighlightColor, JsonDefaults.ScrollBar.HighlightColor);
        writer.AddField(JsonDefaults.ScrollBar.PressedColorName, PressedColor, JsonDefaults.ScrollBar.PressedColor);
        writer.AddField(JsonDefaults.ScrollBar.TrackColorName, TrackColor, JsonDefaults.ScrollBar.TrackColor);
        writer.WriteEndObject();
    }

    public override void Reset()
    {
        Invert = JsonDefaults.ScrollBar.Invert;
        AutoHide = JsonDefaults.ScrollBar.AutoHide;
        HandleSprite = JsonDefaults.Common.NullValue;
        TrackSprite = JsonDefaults.Common.NullValue;
        Size = JsonDefaults.ScrollBar.Size;
        HandleColor = JsonDefaults.ScrollBar.HandleColor;
        HighlightColor = JsonDefaults.ScrollBar.HighlightColor;
        PressedColor = JsonDefaults.ScrollBar.PressedColor;
        TrackColor = JsonDefaults.ScrollBar.TrackColor;
    }
}