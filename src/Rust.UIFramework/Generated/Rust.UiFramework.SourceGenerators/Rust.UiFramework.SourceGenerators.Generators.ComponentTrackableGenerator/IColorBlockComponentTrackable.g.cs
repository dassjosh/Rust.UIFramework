using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IColorBlockComponentTrackable
{
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> HighlightedColor { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> PressedColor { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> SelectedColor { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> ColorMultiplier { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> FadeDuration { get; }
}


