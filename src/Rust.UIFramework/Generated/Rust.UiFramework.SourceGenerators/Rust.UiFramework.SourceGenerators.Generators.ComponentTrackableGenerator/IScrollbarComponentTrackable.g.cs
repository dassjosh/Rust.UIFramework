using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IScrollbarComponentTrackable
{

	Oxide.Ext.UiFramework.Types.Tracked<bool> Invert { get; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> AutoHide { get; }
	Oxide.Ext.UiFramework.Types.Tracked<string> HandleSprite { get; }
	Oxide.Ext.UiFramework.Types.Tracked<string> TrackSprite { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> Size { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> HandleColor { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> HighlightColor { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> PressedColor { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> TrackColor { get; }

}


