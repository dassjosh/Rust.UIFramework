using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;

public partial class RawImageComponent : IRawImageComponent, IRawImageComponentTrackable
{
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _color = new();
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<float> _fadeIn = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Common.FadeIn);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<string> _image = new();
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<string> _material = new();
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.UiElements.UiReference> _placeholderFor = new();

	public partial Oxide.Ext.UiFramework.Colors.UiColor Color { get => _color.Value; set => _color.Value = value; }
	public partial float FadeIn { get => _fadeIn.Value; set => _fadeIn.Value = value; }
	public partial string Image { get => _image.Value; set => _image.Value = value; }
	public partial string Material { get => _material.Value; set => _material.Value = value; }
	public partial Oxide.Ext.UiFramework.UiElements.UiReference PlaceholderFor { get => _placeholderFor.Value; set => _placeholderFor.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> IRawImageComponentTrackable.Color => _color;
	Oxide.Ext.UiFramework.Types.Tracked<float> IRawImageComponentTrackable.FadeIn => _fadeIn;
	Oxide.Ext.UiFramework.Types.Tracked<string> IRawImageComponentTrackable.Image => _image;
	Oxide.Ext.UiFramework.Types.Tracked<string> IRawImageComponentTrackable.Material => _material;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.UiElements.UiReference> IRawImageComponentTrackable.PlaceholderFor => _placeholderFor;

	public Oxide.Ext.UiFramework.Components.RawImageComponent SetColor(Oxide.Ext.UiFramework.Colors.UiColor color)
	{
		Color = color;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.RawImageComponent SetFadeIn(float fadeIn)
	{
		FadeIn = fadeIn;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.RawImageComponent SetImage(string image)
	{
		Image = image;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.RawImageComponent SetMaterial(string material)
	{
		Material = material;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.RawImageComponent SetPlaceholderFor(in Oxide.Ext.UiFramework.UiElements.UiReference placeholderFor)
	{
		PlaceholderFor = placeholderFor;
		return this;
	}
	public IRawImageComponentTrackable AsTrackable() => this;
	public override bool HasChanged() => false || (_color.HasChanged || _fadeIn.HasChanged || _image.HasChanged || _material.HasChanged || _placeholderFor.HasChanged) || base.HasChanged();
	public override void ResetHasChanged()
	{
		base.ResetHasChanged();
		_color.ResetHasChanged();
		_fadeIn.ResetHasChanged();
		_image.ResetHasChanged();
		_material.ResetHasChanged();
		_placeholderFor.ResetHasChanged();
	}
	public override void Reset()
	{
		base.Reset();
		_color.Reset();
		_fadeIn.Reset();
		_image.Reset();
		_material.Reset();
		_placeholderFor.Reset();
	}
}


