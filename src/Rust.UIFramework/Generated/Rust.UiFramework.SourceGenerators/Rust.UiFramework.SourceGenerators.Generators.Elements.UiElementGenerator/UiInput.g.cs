using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;
public partial class UiInput : IUiInputTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<int> _charsLimit = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _command = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.InputMode> _mode = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.InputField.LineType> _lineType = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.UiElements.UiReference> _placeholder = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<int> _fontSize = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _font = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.TextAnchor> _align = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _textValue = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.VerticalWrapMode> _verticalOverflow = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _fadeIn = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _color = new();

	public int CharsLimit { get => Input.CharsLimit; set => Input.CharsLimit = value; }
	public string Command { get => Input.Command; set => Input.Command = value; }
	public Oxide.Ext.UiFramework.Enums.InputMode Mode { get => Input.Mode; set => Input.Mode = value; }
	public UnityEngine.UI.InputField.LineType LineType { get => Input.LineType; set => Input.LineType = value; }
	public Oxide.Ext.UiFramework.UiElements.UiReference Placeholder { get => Input.Placeholder; set => Input.Placeholder = value; }
	public int FontSize { get => Input.FontSize; set => Input.FontSize = value; }
	public string Font { get => Input.Font; set => Input.Font = value; }
	public UnityEngine.TextAnchor Align { get => Input.Align; set => Input.Align = value; }
	public string TextValue { get => Input.Text; set => Input.Text = value; }
	public UnityEngine.VerticalWrapMode VerticalOverflow { get => Input.VerticalOverflow; set => Input.VerticalOverflow = value; }
	public float FadeIn { get => Input.FadeIn; set => Input.FadeIn = value; }
	public Oxide.Ext.UiFramework.Colors.UiColor Color { get => Input.Color; set => Input.Color = value; }
	IInputComponentTrackable IUiInputTrackable.Input => Input.AsTrackable();

	public IUiInputTrackable AsTrackable() => this;
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
}


