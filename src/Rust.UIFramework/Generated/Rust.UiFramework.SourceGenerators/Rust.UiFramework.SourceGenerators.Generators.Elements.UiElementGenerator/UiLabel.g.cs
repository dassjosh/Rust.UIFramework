using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;

public partial class UiLabel : IUiLabel, IUiLabelTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<int> _fontSize = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _font = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.TextAnchor> _align = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _textValue = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.VerticalWrapMode> _verticalOverflow = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.UiElements.UiReference> _placeholderFor = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _fadeIn = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _color = new();

	public partial int FontSize { get => Text.FontSize; set => Text.FontSize = value; }
	public partial string Font { get => Text.Font; set => Text.Font = value; }
	public partial UnityEngine.TextAnchor Align { get => Text.Align; set => Text.Align = value; }
	public partial string TextValue { get => Text.Text; set => Text.Text = value; }
	public partial UnityEngine.VerticalWrapMode VerticalOverflow { get => Text.VerticalOverflow; set => Text.VerticalOverflow = value; }
	public partial Oxide.Ext.UiFramework.UiElements.UiReference PlaceholderFor { get => Text.PlaceholderFor; set => Text.PlaceholderFor = value; }
	public partial float FadeIn { get => Text.FadeIn; set => Text.FadeIn = value; }
	public partial Oxide.Ext.UiFramework.Colors.UiColor Color { get => Text.Color; set => Text.Color = value; }
	ITextComponentTrackable IUiLabelTrackable.Text => Text.AsTrackable();

	public IUiLabelTrackable AsTrackable() => this;
	public UiLabel SetFontSize(int fontSize)
	{
		FontSize = fontSize;
		return this;
	}
	public UiLabel SetFont(string font)
	{
		Font = font;
		return this;
	}
	public UiLabel SetAlign(UnityEngine.TextAnchor align)
	{
		Align = align;
		return this;
	}
	public UiLabel SetTextValue(string textValue)
	{
		TextValue = textValue;
		return this;
	}
	public UiLabel SetVerticalOverflow(UnityEngine.VerticalWrapMode verticalOverflow)
	{
		VerticalOverflow = verticalOverflow;
		return this;
	}
	public UiLabel SetPlaceholderFor(in Oxide.Ext.UiFramework.UiElements.UiReference placeholderFor)
	{
		PlaceholderFor = placeholderFor;
		return this;
	}
	public UiLabel SetFadeIn(float fadeIn)
	{
		FadeIn = fadeIn;
		return this;
	}
	public UiLabel SetColor(Oxide.Ext.UiFramework.Colors.UiColor color)
	{
		Color = color;
		return this;
	}
}


