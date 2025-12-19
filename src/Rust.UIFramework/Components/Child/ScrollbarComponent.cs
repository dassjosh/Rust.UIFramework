using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class ScrollbarComponent : ChildComponent
{
    [TrackedDefaults(typeof(JsonDefaults.ScrollBar), nameof(JsonDefaults.ScrollBar.Invert))]
    public partial bool Invert { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ScrollBar), nameof(JsonDefaults.ScrollBar.AutoHide))]
    public partial bool AutoHide { get; set; }
    
    public partial string HandleSprite { get; set; }
    public partial string TrackSprite { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ScrollBar), nameof(JsonDefaults.ScrollBar.Size))]
    public partial float Size { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ScrollBar), nameof(JsonDefaults.ScrollBar.HandleColor))]
    public partial UiColor HandleColor { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ScrollBar), nameof(JsonDefaults.ScrollBar.HighlightColor))]
    public partial UiColor HighlightColor { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ScrollBar), nameof(JsonDefaults.ScrollBar.PressedColor))]
    public partial UiColor PressedColor { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ScrollBar), nameof(JsonDefaults.ScrollBar.TrackColor))]
    public partial UiColor TrackColor { get; set; }
    
    public override ComponentType ComponentType => ComponentType.ScrollBar;

    public override void WriteComponent(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.WriteStartObject();
        writer.AddField(JsonDefaults.ScrollBar.InvertName, InvertTracked, mode);
        writer.AddField(JsonDefaults.ScrollBar.AutoHideName, AutoHideTracked, mode);
        writer.AddField(JsonDefaults.ScrollBar.HandleSprite, HandleSpriteTracked, mode);
        writer.AddField(JsonDefaults.ScrollBar.TrackSprite, TrackSpriteTracked, mode);
        writer.AddField(JsonDefaults.ScrollBar.SizeName, SizeTracked, mode);
        writer.AddField(JsonDefaults.ScrollBar.HandleColorName, HandleColorTracked, mode);
        writer.AddField(JsonDefaults.ScrollBar.HighlightColorName, HighlightColorTracked, mode);
        writer.AddField(JsonDefaults.ScrollBar.PressedColorName, PressedColorTracked, mode);
        writer.AddField(JsonDefaults.ScrollBar.TrackColorName, TrackColorTracked, mode);
        writer.WriteEndObject();
    }
}