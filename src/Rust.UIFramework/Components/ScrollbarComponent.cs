using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public class ScrollbarComponent : BasePoolableComponent
{
    public bool Invert;
    public bool AutoHide;
    public string HandleSprite;
    public string TrackSprite;
    public float Size = JsonDefaults.ScrollBar.Size;
    public UiColor HandleColor = JsonDefaults.ScrollBar.HandleColor;
    public UiColor HighlightColor = JsonDefaults.ScrollBar.HighlightColor;
    public UiColor PressedColor = JsonDefaults.ScrollBar.PressedColor;
    public UiColor TrackColor = JsonDefaults.ScrollBar.TrackColor;
    
    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        base.WriteComponent(writer);
        writer.AddField(JsonDefaults.ScrollBar.Invert, Invert, false);
        writer.AddField(JsonDefaults.ScrollBar.AutoHide, AutoHide, false);
        writer.AddField(JsonDefaults.ScrollBar.HandleSprite, HandleSprite, null);
        writer.AddField(JsonDefaults.ScrollBar.TrackSprite, TrackSprite, null);
        writer.AddField(JsonDefaults.ScrollBar.SizeName, Size, JsonDefaults.ScrollBar.Size);
        writer.AddField(JsonDefaults.ScrollBar.HandleColorName, HandleColor, JsonDefaults.ScrollBar.HandleColor);
        writer.AddField(JsonDefaults.ScrollBar.HighlightColorName, HighlightColor, JsonDefaults.ScrollBar.HighlightColor);
        writer.AddField(JsonDefaults.ScrollBar.PressedColorName, PressedColor, JsonDefaults.ScrollBar.PressedColor);
        writer.AddField(JsonDefaults.ScrollBar.TrackColorName, TrackColor, JsonDefaults.ScrollBar.TrackColor);
        writer.WriteEndObject();
    }

    public override void Reset()
    {
        base.Reset();
        Invert = false;
        AutoHide = false;
        HandleSprite = null;
        TrackSprite = null;
        Size = JsonDefaults.ScrollBar.Size;
        HandleColor = JsonDefaults.ScrollBar.HandleColor;
        HighlightColor = JsonDefaults.ScrollBar.HighlightColor;
        PressedColor = JsonDefaults.ScrollBar.PressedColor;
        TrackColor = JsonDefaults.ScrollBar.TrackColor;
    }
}