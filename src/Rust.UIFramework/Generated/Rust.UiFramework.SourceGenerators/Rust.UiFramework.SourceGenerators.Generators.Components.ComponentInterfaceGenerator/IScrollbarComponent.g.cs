using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IScrollbarComponent
{
	bool Invert { get; set; }
	bool AutoHide { get; set; }
	string HandleSprite { get; set; }
	string TrackSprite { get; set; }
	float Size { get; set; }
	Oxide.Ext.UiFramework.Colors.UiColor HandleColor { get; set; }
	Oxide.Ext.UiFramework.Colors.UiColor HighlightColor { get; set; }
	Oxide.Ext.UiFramework.Colors.UiColor PressedColor { get; set; }
	Oxide.Ext.UiFramework.Colors.UiColor TrackColor { get; set; }

	Oxide.Ext.UiFramework.Components.ScrollbarComponent SetInvert(bool invert);
	Oxide.Ext.UiFramework.Components.ScrollbarComponent SetAutoHide(bool autoHide);
	Oxide.Ext.UiFramework.Components.ScrollbarComponent SetHandleSprite(string handleSprite);
	Oxide.Ext.UiFramework.Components.ScrollbarComponent SetTrackSprite(string trackSprite);
	Oxide.Ext.UiFramework.Components.ScrollbarComponent SetSize(float size);
	Oxide.Ext.UiFramework.Components.ScrollbarComponent SetHandleColor(Oxide.Ext.UiFramework.Colors.UiColor handleColor);
	Oxide.Ext.UiFramework.Components.ScrollbarComponent SetHighlightColor(Oxide.Ext.UiFramework.Colors.UiColor highlightColor);
	Oxide.Ext.UiFramework.Components.ScrollbarComponent SetPressedColor(Oxide.Ext.UiFramework.Colors.UiColor pressedColor);
	Oxide.Ext.UiFramework.Components.ScrollbarComponent SetTrackColor(Oxide.Ext.UiFramework.Colors.UiColor trackColor);
}


