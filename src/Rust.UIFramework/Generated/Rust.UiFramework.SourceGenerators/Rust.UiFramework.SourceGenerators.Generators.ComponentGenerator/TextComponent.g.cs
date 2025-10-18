using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;
public partial class TextComponent : ITextComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _color = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Color.ColorValue);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _fadeIn = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Common.FadeIn);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<int> _fontSize = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Text.FontSize);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _font = new(null, Oxide.Ext.UiFramework.Json.JsonDefaults.Text.FontValue);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.TextAnchor> _align = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Text.Align);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _text = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.VerticalWrapMode> _verticalOverflow = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Text.VerticalOverflow);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.UiElements.UiReference> _placeholderFor = new();

	public Oxide.Ext.UiFramework.Colors.UiColor Color { get => _color.Value; set => _color.Value = value; }
	public float FadeIn { get => _fadeIn.Value; set => _fadeIn.Value = value; }
	public int FontSize { get => _fontSize.Value; set => _fontSize.Value = value; }
	public string Font { get => _font.Value; set => _font.Value = value; }
	public UnityEngine.TextAnchor Align { get => _align.Value; set => _align.Value = value; }
	public string Text { get => _text.Value; set => _text.Value = value; }
	public UnityEngine.VerticalWrapMode VerticalOverflow { get => _verticalOverflow.Value; set => _verticalOverflow.Value = value; }
	public Oxide.Ext.UiFramework.UiElements.UiReference PlaceholderFor { get => _placeholderFor.Value; set => _placeholderFor.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> ITextComponentTrackable.Color => _color;
	Oxide.Ext.UiFramework.Types.Tracked<float> ITextComponentTrackable.FadeIn => _fadeIn;
	Oxide.Ext.UiFramework.Types.Tracked<int> ITextComponentTrackable.FontSize => _fontSize;
	Oxide.Ext.UiFramework.Types.Tracked<string> ITextComponentTrackable.Font => _font;
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.TextAnchor> ITextComponentTrackable.Align => _align;
	Oxide.Ext.UiFramework.Types.Tracked<string> ITextComponentTrackable.Text => _text;
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.VerticalWrapMode> ITextComponentTrackable.VerticalOverflow => _verticalOverflow;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.UiElements.UiReference> ITextComponentTrackable.PlaceholderFor => _placeholderFor;

	public ITextComponentTrackable AsTrackable() => this;
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_color.HasChanged || _fadeIn.HasChanged || _fontSize.HasChanged || _font.HasChanged || _align.HasChanged || _text.HasChanged || _verticalOverflow.HasChanged || _placeholderFor.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_color.ResetHasChanged();
		_fadeIn.ResetHasChanged();
		_fontSize.ResetHasChanged();
		_font.ResetHasChanged();
		_align.ResetHasChanged();
		_text.ResetHasChanged();
		_verticalOverflow.ResetHasChanged();
		_placeholderFor.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
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


