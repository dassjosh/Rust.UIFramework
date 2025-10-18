using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;
public partial class ColorBlockComponent : IColorBlockComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _highlightedColor = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ColorBlock.HighlightedColor);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _pressedColor = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ColorBlock.PressedColor);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _selectedColor = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ColorBlock.SelectedColor);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _colorMultiplier = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ColorBlock.ColorMultiplier);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _fadeDuration = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ColorBlock.FadeDuration);

	public Oxide.Ext.UiFramework.Colors.UiColor HighlightedColor { get => _highlightedColor.Value; set => _highlightedColor.Value = value; }
	public Oxide.Ext.UiFramework.Colors.UiColor PressedColor { get => _pressedColor.Value; set => _pressedColor.Value = value; }
	public Oxide.Ext.UiFramework.Colors.UiColor SelectedColor { get => _selectedColor.Value; set => _selectedColor.Value = value; }
	public float ColorMultiplier { get => _colorMultiplier.Value; set => _colorMultiplier.Value = value; }
	public float FadeDuration { get => _fadeDuration.Value; set => _fadeDuration.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> IColorBlockComponentTrackable.HighlightedColor => _highlightedColor;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> IColorBlockComponentTrackable.PressedColor => _pressedColor;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> IColorBlockComponentTrackable.SelectedColor => _selectedColor;
	Oxide.Ext.UiFramework.Types.Tracked<float> IColorBlockComponentTrackable.ColorMultiplier => _colorMultiplier;
	Oxide.Ext.UiFramework.Types.Tracked<float> IColorBlockComponentTrackable.FadeDuration => _fadeDuration;

	public Oxide.Ext.UiFramework.Components.ColorBlockComponent SetHighlightedColor(Oxide.Ext.UiFramework.Colors.UiColor highlightedColor)
	{
		HighlightedColor = highlightedColor;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ColorBlockComponent SetPressedColor(Oxide.Ext.UiFramework.Colors.UiColor pressedColor)
	{
		PressedColor = pressedColor;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ColorBlockComponent SetSelectedColor(Oxide.Ext.UiFramework.Colors.UiColor selectedColor)
	{
		SelectedColor = selectedColor;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ColorBlockComponent SetColorMultiplier(float colorMultiplier)
	{
		ColorMultiplier = colorMultiplier;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ColorBlockComponent SetFadeDuration(float fadeDuration)
	{
		FadeDuration = fadeDuration;
		return this;
	}
	public IColorBlockComponentTrackable AsTrackable() => this;
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_highlightedColor.HasChanged || _pressedColor.HasChanged || _selectedColor.HasChanged || _colorMultiplier.HasChanged || _fadeDuration.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_highlightedColor.ResetHasChanged();
		_pressedColor.ResetHasChanged();
		_selectedColor.ResetHasChanged();
		_colorMultiplier.ResetHasChanged();
		_fadeDuration.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_highlightedColor.Reset();
		_pressedColor.Reset();
		_selectedColor.Reset();
		_colorMultiplier.Reset();
		_fadeDuration.Reset();
	}
}


