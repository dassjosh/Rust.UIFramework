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
}


