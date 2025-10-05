using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class ScrollbarComponent : ChildComponent
{
    private readonly TrackedValue<bool> _invert = new(JsonDefaults.ScrollBar.Invert);
    private readonly TrackedValue<bool> _autoHide = new(JsonDefaults.ScrollBar.AutoHide);
    private readonly TrackedValue<string> _handleSprite = new(JsonDefaults.Common.NullValue);
    private readonly TrackedValue<string> _trackSprite = new(JsonDefaults.Common.NullValue);
    private readonly TrackedValue<float> _size = new(JsonDefaults.ScrollBar.Size);
    private readonly TrackedValue<UiColor> _handleColor = new(JsonDefaults.ScrollBar.HandleColor);
    private readonly TrackedValue<UiColor> _highlightColor = new(JsonDefaults.ScrollBar.HighlightColor);
    private readonly TrackedValue<UiColor> _pressedColor = new(JsonDefaults.ScrollBar.PressedColor);
    private readonly TrackedValue<UiColor> _trackColor = new(JsonDefaults.ScrollBar.TrackColor);
    
    public bool Invert { get => _invert.Value; set => _invert.Value = value; }
    public bool AutoHide { get => _autoHide.Value; set => _autoHide.Value = value; }
    public string HandleSprite { get => _handleSprite.Value; set => _handleSprite.Value = value; }
    public string TrackSprite { get => _trackSprite.Value; set => _trackSprite.Value = value; }
    public float Size { get => _size.Value; set => _size.Value = value; }
    public UiColor HandleColor { get => _handleColor.Value; set => _handleColor.Value = value; }
    public UiColor HighlightColor { get => _highlightColor.Value; set => _highlightColor.Value = value; }
    public UiColor PressedColor { get => _pressedColor.Value; set => _pressedColor.Value = value; }
    public UiColor TrackColor { get => _trackColor.Value; set => _trackColor.Value = value; }
    
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

    public override void ResetHasChanged()
    {
        _invert.ResetHasChanged();
        _autoHide.ResetHasChanged();
        _handleSprite.ResetHasChanged();
        _trackSprite.ResetHasChanged();
        _size.ResetHasChanged();
        _handleColor.ResetHasChanged();
        _highlightColor.ResetHasChanged();
        _pressedColor.ResetHasChanged();
        _trackColor.ResetHasChanged();
    }

    public override void Reset()
    {
        _invert.Reset();
        _autoHide.Reset();
        _handleSprite.Reset();
        _trackSprite.Reset();
        _size.Reset();
        _handleColor.Reset();
        _highlightColor.Reset();
        _pressedColor.Reset();
        _trackColor.Reset();
    }
}