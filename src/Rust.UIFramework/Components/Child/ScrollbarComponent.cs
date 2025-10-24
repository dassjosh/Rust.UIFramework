using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(IScrollbarComponent))]
[GenerateBuilderMethods]
public partial class ScrollbarComponent : ChildComponent, IScrollbarComponent
{
    public override ComponentType ComponentType => ComponentType.ScrollBar;

    public override void WriteComponent(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.WriteStartObject();
        writer.AddField(JsonDefaults.ScrollBar.InvertName, _invert, mode);
        writer.AddField(JsonDefaults.ScrollBar.AutoHideName, _autoHide, mode);
        writer.AddField(JsonDefaults.ScrollBar.HandleSprite, _handleSprite, mode);
        writer.AddField(JsonDefaults.ScrollBar.TrackSprite, _trackSprite, mode);
        writer.AddField(JsonDefaults.ScrollBar.SizeName, _size, mode);
        writer.AddField(JsonDefaults.ScrollBar.HandleColorName, _handleColor, mode);
        writer.AddField(JsonDefaults.ScrollBar.HighlightColorName, _highlightColor, mode);
        writer.AddField(JsonDefaults.ScrollBar.PressedColorName, _pressedColor, mode);
        writer.AddField(JsonDefaults.ScrollBar.TrackColorName, _trackColor, mode);
        writer.WriteEndObject();
    }
}