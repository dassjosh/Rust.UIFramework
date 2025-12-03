using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IColorBlockComponent
{
	Oxide.Ext.UiFramework.Colors.UiColor HighlightedColor { get; set; }
	Oxide.Ext.UiFramework.Colors.UiColor PressedColor { get; set; }
	Oxide.Ext.UiFramework.Colors.UiColor SelectedColor { get; set; }
	float ColorMultiplier { get; set; }
	float FadeDuration { get; set; }
}


