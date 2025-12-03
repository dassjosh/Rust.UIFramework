using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IUiLabel : Oxide.Ext.UiFramework.Interfaces.IFadeIn<Oxide.Ext.UiFramework.UiElements.UiLabel>, Oxide.Ext.UiFramework.Interfaces.IUiColor<Oxide.Ext.UiFramework.UiElements.UiLabel>, IBaseUiComponent
{
	int FontSize { get; }
	string Font { get; }
	UnityEngine.TextAnchor Align { get; }
	string TextValue { get; }
	UnityEngine.VerticalWrapMode VerticalOverflow { get; }
	Oxide.Ext.UiFramework.UiElements.UiReference PlaceholderFor { get; }

	Oxide.Ext.UiFramework.UiElements.UiLabel SetFontSize(int fontSize);
	Oxide.Ext.UiFramework.UiElements.UiLabel SetFont(string font);
	Oxide.Ext.UiFramework.UiElements.UiLabel SetAlign(UnityEngine.TextAnchor align);
	Oxide.Ext.UiFramework.UiElements.UiLabel SetTextValue(string textValue);
	Oxide.Ext.UiFramework.UiElements.UiLabel SetVerticalOverflow(UnityEngine.VerticalWrapMode verticalOverflow);
	Oxide.Ext.UiFramework.UiElements.UiLabel SetPlaceholderFor(in Oxide.Ext.UiFramework.UiElements.UiReference placeholderFor);
}


