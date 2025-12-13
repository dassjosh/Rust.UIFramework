using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;

public partial class ColorBlockComponent : IColorBlockComponent, IColorBlockComponentTrackable
{
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _highlightedColor = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ColorBlock.HighlightedColor);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _pressedColor = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ColorBlock.PressedColor);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _selectedColor = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ColorBlock.SelectedColor);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<float> _colorMultiplier = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ColorBlock.ColorMultiplier);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<float> _fadeDuration = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ColorBlock.FadeDuration);

	public partial Oxide.Ext.UiFramework.Colors.UiColor HighlightedColor { get => _highlightedColor.Value; set => _highlightedColor.Value = value; }
	public partial Oxide.Ext.UiFramework.Colors.UiColor PressedColor { get => _pressedColor.Value; set => _pressedColor.Value = value; }
	public partial Oxide.Ext.UiFramework.Colors.UiColor SelectedColor { get => _selectedColor.Value; set => _selectedColor.Value = value; }
	public partial float ColorMultiplier { get => _colorMultiplier.Value; set => _colorMultiplier.Value = value; }
	public partial float FadeDuration { get => _fadeDuration.Value; set => _fadeDuration.Value = value; }
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
	public override bool HasChanged() => false || (_highlightedColor.HasChanged || _pressedColor.HasChanged || _selectedColor.HasChanged || _colorMultiplier.HasChanged || _fadeDuration.HasChanged) || base.HasChanged();
	public override void ResetHasChanged()
	{
		base.ResetHasChanged();
		_highlightedColor.ResetHasChanged();
		_pressedColor.ResetHasChanged();
		_selectedColor.ResetHasChanged();
		_colorMultiplier.ResetHasChanged();
		_fadeDuration.ResetHasChanged();
	}
	public override void Reset()
	{
		base.Reset();
		_highlightedColor.Reset();
		_pressedColor.Reset();
		_selectedColor.Reset();
		_colorMultiplier.Reset();
		_fadeDuration.Reset();
	}
}


