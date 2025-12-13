using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;

public partial class TextComponent : ITextComponent, ITextComponentTrackable
{
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _color = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Color.ColorValue);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<float> _fadeIn = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Common.FadeIn);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<int> _fontSize = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Text.FontSize);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<string> _font = new(null, Oxide.Ext.UiFramework.Json.JsonDefaults.Text.FontValue);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.TextAnchor> _align = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Text.Align);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<string> _text = new();
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.VerticalWrapMode> _verticalOverflow = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Text.VerticalOverflow);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.UiElements.UiReference> _placeholderFor = new();

	public partial Oxide.Ext.UiFramework.Colors.UiColor Color { get => _color.Value; set => _color.Value = value; }
	public partial float FadeIn { get => _fadeIn.Value; set => _fadeIn.Value = value; }
	public partial int FontSize { get => _fontSize.Value; set => _fontSize.Value = value; }
	public partial string Font { get => _font.Value; set => _font.Value = value; }
	public partial UnityEngine.TextAnchor Align { get => _align.Value; set => _align.Value = value; }
	public partial string Text { get => _text.Value; set => _text.Value = value; }
	public partial UnityEngine.VerticalWrapMode VerticalOverflow { get => _verticalOverflow.Value; set => _verticalOverflow.Value = value; }
	public partial Oxide.Ext.UiFramework.UiElements.UiReference PlaceholderFor { get => _placeholderFor.Value; set => _placeholderFor.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> ITextComponentTrackable.Color => _color;
	Oxide.Ext.UiFramework.Types.Tracked<float> ITextComponentTrackable.FadeIn => _fadeIn;
	Oxide.Ext.UiFramework.Types.Tracked<int> ITextComponentTrackable.FontSize => _fontSize;
	Oxide.Ext.UiFramework.Types.Tracked<string> ITextComponentTrackable.Font => _font;
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.TextAnchor> ITextComponentTrackable.Align => _align;
	Oxide.Ext.UiFramework.Types.Tracked<string> ITextComponentTrackable.Text => _text;
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.VerticalWrapMode> ITextComponentTrackable.VerticalOverflow => _verticalOverflow;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.UiElements.UiReference> ITextComponentTrackable.PlaceholderFor => _placeholderFor;

	public Oxide.Ext.UiFramework.Components.TextComponent SetColor(Oxide.Ext.UiFramework.Colors.UiColor color)
	{
		Color = color;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.TextComponent SetFadeIn(float fadeIn)
	{
		FadeIn = fadeIn;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.TextComponent SetFontSize(int fontSize)
	{
		FontSize = fontSize;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.TextComponent SetFont(string font)
	{
		Font = font;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.TextComponent SetAlign(UnityEngine.TextAnchor align)
	{
		Align = align;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.TextComponent SetText(string text)
	{
		Text = text;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.TextComponent SetVerticalOverflow(UnityEngine.VerticalWrapMode verticalOverflow)
	{
		VerticalOverflow = verticalOverflow;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.TextComponent SetPlaceholderFor(in Oxide.Ext.UiFramework.UiElements.UiReference placeholderFor)
	{
		PlaceholderFor = placeholderFor;
		return this;
	}
	public ITextComponentTrackable AsTrackable() => this;
	public override bool HasChanged() => false || (_color.HasChanged || _fadeIn.HasChanged || _fontSize.HasChanged || _font.HasChanged || _align.HasChanged || _text.HasChanged || _verticalOverflow.HasChanged || _placeholderFor.HasChanged) || base.HasChanged();
	public override void ResetHasChanged()
	{
		base.ResetHasChanged();
		_color.ResetHasChanged();
		_fadeIn.ResetHasChanged();
		_fontSize.ResetHasChanged();
		_font.ResetHasChanged();
		_align.ResetHasChanged();
		_text.ResetHasChanged();
		_verticalOverflow.ResetHasChanged();
		_placeholderFor.ResetHasChanged();
	}
	public override void Reset()
	{
		base.Reset();
		_color.Reset();
		_fadeIn.Reset();
		_fontSize.Reset();
		_font.Reset();
		_align.Reset();
		_text.Reset();
		_verticalOverflow.Reset();
		_placeholderFor.Reset();
	}
}


