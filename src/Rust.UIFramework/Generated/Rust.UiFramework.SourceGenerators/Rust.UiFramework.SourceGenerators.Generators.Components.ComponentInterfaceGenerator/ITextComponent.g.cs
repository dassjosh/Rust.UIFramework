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
}


