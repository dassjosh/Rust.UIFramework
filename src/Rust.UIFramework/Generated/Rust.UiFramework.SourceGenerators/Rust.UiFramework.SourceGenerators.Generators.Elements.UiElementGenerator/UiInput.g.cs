using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;

public partial class UiInput : IUiInput, IUiInputTrackable
{
	public partial int FontSize { get => Input.FontSize; set => Input.FontSize = value; }
	public partial string Font { get => Input.Font; set => Input.Font = value; }
	public partial UnityEngine.TextAnchor Align { get => Input.Align; set => Input.Align = value; }
	public partial string TextValue { get => Input.Text; set => Input.Text = value; }
	public partial UnityEngine.VerticalWrapMode VerticalOverflow { get => Input.VerticalOverflow; set => Input.VerticalOverflow = value; }
	public partial int CharsLimit { get => Input.CharsLimit; set => Input.CharsLimit = value; }
	public partial string Command { get => Input.Command; set => Input.Command = value; }
	public partial Oxide.Ext.UiFramework.Enums.InputMode Mode { get => Input.Mode; set => Input.Mode = value; }
	public partial UnityEngine.UI.InputField.LineType LineType { get => Input.LineType; set => Input.LineType = value; }
	public partial Oxide.Ext.UiFramework.UiElements.UiReference Placeholder { get => Input.Placeholder; set => Input.Placeholder = value; }
	public partial float FadeIn { get => Input.FadeIn; set => Input.FadeIn = value; }
	public partial Oxide.Ext.UiFramework.Colors.UiColor Color { get => Input.Color; set => Input.Color = value; }
	public partial bool IsPassword { get => Input.IsPassword; set => Input.IsPassword = value; }
	public partial bool NeedsKeyboard { get => Input.NeedsKeyboard; set => Input.NeedsKeyboard = value; }
	public partial bool HudNeedsKeyboard { get => Input.HudNeedsKeyboard; set => Input.HudNeedsKeyboard = value; }
	public partial bool AutoFocus { get => Input.AutoFocus; set => Input.AutoFocus = value; }
	public partial bool ReadOnly { get => Input.ReadOnly; set => Input.ReadOnly = value; }
	IInputComponentTrackable IUiInputTrackable.Input => Input.AsTrackable();

	public IUiInputTrackable AsTrackable() => this;
	public UiInput SetFontSize(int fontSize)
	{
		FontSize = fontSize;
		return this;
	}
	public UiInput SetFont(string font)
	{
		Font = font;
		return this;
	}
	public UiInput SetAlign(UnityEngine.TextAnchor align)
	{
		Align = align;
		return this;
	}
	public UiInput SetTextValue(string textValue)
	{
		TextValue = textValue;
		return this;
	}
	public UiInput SetVerticalOverflow(UnityEngine.VerticalWrapMode verticalOverflow)
	{
		VerticalOverflow = verticalOverflow;
		return this;
	}
	public UiInput SetCharsLimit(int charsLimit)
	{
		CharsLimit = charsLimit;
		return this;
	}
	public UiInput SetCommand(string command)
	{
		Command = command;
		return this;
	}
	public UiInput SetMode(Oxide.Ext.UiFramework.Enums.InputMode mode)
	{
		Mode = mode;
		return this;
	}
	public UiInput SetLineType(UnityEngine.UI.InputField.LineType lineType)
	{
		LineType = lineType;
		return this;
	}
	public UiInput SetPlaceholder(in Oxide.Ext.UiFramework.UiElements.UiReference placeholder)
	{
		Placeholder = placeholder;
		return this;
	}
	public UiInput SetFadeIn(float fadeIn)
	{
		FadeIn = fadeIn;
		return this;
	}
	public UiInput SetColor(Oxide.Ext.UiFramework.Colors.UiColor color)
	{
		Color = color;
		return this;
	}
	public UiInput SetIsPassword(bool isPassword)
	{
		IsPassword = isPassword;
		return this;
	}
	public UiInput SetNeedsKeyboard(bool needsKeyboard)
	{
		NeedsKeyboard = needsKeyboard;
		return this;
	}
	public UiInput SetHudNeedsKeyboard(bool hudNeedsKeyboard)
	{
		HudNeedsKeyboard = hudNeedsKeyboard;
		return this;
	}
	public UiInput SetAutoFocus(bool autoFocus)
	{
		AutoFocus = autoFocus;
		return this;
	}
	public UiInput SetReadOnly(bool readOnly)
	{
		ReadOnly = readOnly;
		return this;
	}
}


