using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;
public partial class ScrollbarComponent : IScrollbarComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _invert = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ScrollBar.Invert);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _autoHide = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ScrollBar.AutoHide);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _handleSprite = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _trackSprite = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _size = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ScrollBar.Size);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _handleColor = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ScrollBar.HandleColor);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _highlightColor = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ScrollBar.HighlightColor);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _pressedColor = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ScrollBar.PressedColor);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _trackColor = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ScrollBar.TrackColor);

	public bool Invert { get => _invert.Value; set => _invert.Value = value; }
	public bool AutoHide { get => _autoHide.Value; set => _autoHide.Value = value; }
	public string HandleSprite { get => _handleSprite.Value; set => _handleSprite.Value = value; }
	public string TrackSprite { get => _trackSprite.Value; set => _trackSprite.Value = value; }
	public float Size { get => _size.Value; set => _size.Value = value; }
	public Oxide.Ext.UiFramework.Colors.UiColor HandleColor { get => _handleColor.Value; set => _handleColor.Value = value; }
	public Oxide.Ext.UiFramework.Colors.UiColor HighlightColor { get => _highlightColor.Value; set => _highlightColor.Value = value; }
	public Oxide.Ext.UiFramework.Colors.UiColor PressedColor { get => _pressedColor.Value; set => _pressedColor.Value = value; }
	public Oxide.Ext.UiFramework.Colors.UiColor TrackColor { get => _trackColor.Value; set => _trackColor.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> IScrollbarComponentTrackable.Invert => _invert;
	Oxide.Ext.UiFramework.Types.Tracked<bool> IScrollbarComponentTrackable.AutoHide => _autoHide;
	Oxide.Ext.UiFramework.Types.Tracked<string> IScrollbarComponentTrackable.HandleSprite => _handleSprite;
	Oxide.Ext.UiFramework.Types.Tracked<string> IScrollbarComponentTrackable.TrackSprite => _trackSprite;
	Oxide.Ext.UiFramework.Types.Tracked<float> IScrollbarComponentTrackable.Size => _size;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> IScrollbarComponentTrackable.HandleColor => _handleColor;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> IScrollbarComponentTrackable.HighlightColor => _highlightColor;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> IScrollbarComponentTrackable.PressedColor => _pressedColor;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> IScrollbarComponentTrackable.TrackColor => _trackColor;

	public Oxide.Ext.UiFramework.Components.ScrollbarComponent SetInvert(bool invert)
	{
		Invert = invert;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ScrollbarComponent SetAutoHide(bool autoHide)
	{
		AutoHide = autoHide;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ScrollbarComponent SetHandleSprite(string handleSprite)
	{
		HandleSprite = handleSprite;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ScrollbarComponent SetTrackSprite(string trackSprite)
	{
		TrackSprite = trackSprite;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ScrollbarComponent SetSize(float size)
	{
		Size = size;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ScrollbarComponent SetHandleColor(Oxide.Ext.UiFramework.Colors.UiColor handleColor)
	{
		HandleColor = handleColor;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ScrollbarComponent SetHighlightColor(Oxide.Ext.UiFramework.Colors.UiColor highlightColor)
	{
		HighlightColor = highlightColor;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ScrollbarComponent SetPressedColor(Oxide.Ext.UiFramework.Colors.UiColor pressedColor)
	{
		PressedColor = pressedColor;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ScrollbarComponent SetTrackColor(Oxide.Ext.UiFramework.Colors.UiColor trackColor)
	{
		TrackColor = trackColor;
		return this;
	}
	public IScrollbarComponentTrackable AsTrackable() => this;
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_invert.HasChanged || _autoHide.HasChanged || _handleSprite.HasChanged || _trackSprite.HasChanged || _size.HasChanged || _handleColor.HasChanged || _highlightColor.HasChanged || _pressedColor.HasChanged || _trackColor.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
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
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
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


