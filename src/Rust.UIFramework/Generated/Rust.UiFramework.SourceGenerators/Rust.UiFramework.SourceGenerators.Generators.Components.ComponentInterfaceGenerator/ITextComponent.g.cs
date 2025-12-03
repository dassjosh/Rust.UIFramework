using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface ITextComponent : IBaseTypedComponent
{
	Oxide.Ext.UiFramework.Colors.UiColor Color { get; set; }
	float FadeIn { get; set; }
	int FontSize { get; set; }
	string Font { get; set; }
	UnityEngine.TextAnchor Align { get; set; }
	string Text { get; set; }
	UnityEngine.VerticalWrapMode VerticalOverflow { get; set; }
	Oxide.Ext.UiFramework.UiElements.UiReference PlaceholderFor { get; set; }

	Oxide.Ext.UiFramework.Components.TextComponent SetColor(Oxide.Ext.UiFramework.Colors.UiColor color);
	Oxide.Ext.UiFramework.Components.TextComponent SetFadeIn(float fadeIn);
	Oxide.Ext.UiFramework.Components.TextComponent SetFontSize(int fontSize);
	Oxide.Ext.UiFramework.Components.TextComponent SetFont(string font);
	Oxide.Ext.UiFramework.Components.TextComponent SetAlign(UnityEngine.TextAnchor align);
	Oxide.Ext.UiFramework.Components.TextComponent SetText(string text);
	Oxide.Ext.UiFramework.Components.TextComponent SetVerticalOverflow(UnityEngine.VerticalWrapMode verticalOverflow);
	Oxide.Ext.UiFramework.Components.TextComponent SetPlaceholderFor(in Oxide.Ext.UiFramework.UiElements.UiReference placeholderFor);
}


