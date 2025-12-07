using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IUiInput : Oxide.Ext.UiFramework.Interfaces.IFadeIn<Oxide.Ext.UiFramework.UiElements.UiInput>, Oxide.Ext.UiFramework.Interfaces.IUiColor<Oxide.Ext.UiFramework.UiElements.UiInput>, IBaseUiComponent
{
	int FontSize { get; }
	string Font { get; }
	UnityEngine.TextAnchor Align { get; }
	string TextValue { get; }
	UnityEngine.VerticalWrapMode VerticalOverflow { get; }
	int CharsLimit { get; }
	string Command { get; }
	Oxide.Ext.UiFramework.Enums.InputMode Mode { get; }
	UnityEngine.UI.InputField.LineType LineType { get; }
	Oxide.Ext.UiFramework.UiElements.UiReference Placeholder { get; }
	bool IsPassword { get; }
	bool NeedsKeyboard { get; }
	bool HudNeedsKeyboard { get; }
	bool AutoFocus { get; }
	bool ReadOnly { get; }

	Oxide.Ext.UiFramework.UiElements.UiInput SetFontSize(int fontSize);
	Oxide.Ext.UiFramework.UiElements.UiInput SetFont(string font);
	Oxide.Ext.UiFramework.UiElements.UiInput SetAlign(UnityEngine.TextAnchor align);
	Oxide.Ext.UiFramework.UiElements.UiInput SetTextValue(string textValue);
	Oxide.Ext.UiFramework.UiElements.UiInput SetVerticalOverflow(UnityEngine.VerticalWrapMode verticalOverflow);
	Oxide.Ext.UiFramework.UiElements.UiInput SetCharsLimit(int charsLimit);
	Oxide.Ext.UiFramework.UiElements.UiInput SetCommand(string command);
	Oxide.Ext.UiFramework.UiElements.UiInput SetMode(Oxide.Ext.UiFramework.Enums.InputMode mode);
	Oxide.Ext.UiFramework.UiElements.UiInput SetLineType(UnityEngine.UI.InputField.LineType lineType);
	Oxide.Ext.UiFramework.UiElements.UiInput SetPlaceholder(in Oxide.Ext.UiFramework.UiElements.UiReference placeholder);
	Oxide.Ext.UiFramework.UiElements.UiInput SetIsPassword(bool isPassword);
	Oxide.Ext.UiFramework.UiElements.UiInput SetNeedsKeyboard(bool needsKeyboard);
	Oxide.Ext.UiFramework.UiElements.UiInput SetHudNeedsKeyboard(bool hudNeedsKeyboard);
	Oxide.Ext.UiFramework.UiElements.UiInput SetAutoFocus(bool autoFocus);
	Oxide.Ext.UiFramework.UiElements.UiInput SetReadOnly(bool readOnly);
}


