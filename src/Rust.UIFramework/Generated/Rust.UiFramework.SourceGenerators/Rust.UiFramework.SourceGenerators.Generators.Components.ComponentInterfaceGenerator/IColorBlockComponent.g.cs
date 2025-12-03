using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IColorBlockComponent
{
	Oxide.Ext.UiFramework.Colors.UiColor HighlightedColor { get; set; }
	Oxide.Ext.UiFramework.Colors.UiColor PressedColor { get; set; }
	Oxide.Ext.UiFramework.Colors.UiColor SelectedColor { get; set; }
	float ColorMultiplier { get; set; }
	float FadeDuration { get; set; }

	Oxide.Ext.UiFramework.Components.ColorBlockComponent SetHighlightedColor(Oxide.Ext.UiFramework.Colors.UiColor highlightedColor);
	Oxide.Ext.UiFramework.Components.ColorBlockComponent SetPressedColor(Oxide.Ext.UiFramework.Colors.UiColor pressedColor);
	Oxide.Ext.UiFramework.Components.ColorBlockComponent SetSelectedColor(Oxide.Ext.UiFramework.Colors.UiColor selectedColor);
	Oxide.Ext.UiFramework.Components.ColorBlockComponent SetColorMultiplier(float colorMultiplier);
	Oxide.Ext.UiFramework.Components.ColorBlockComponent SetFadeDuration(float fadeDuration);
}


