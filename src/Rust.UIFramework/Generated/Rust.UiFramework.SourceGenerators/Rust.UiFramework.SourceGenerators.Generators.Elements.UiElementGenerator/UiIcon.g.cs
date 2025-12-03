using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;

public partial class UiIcon : IUiIcon, IUiIconTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _material = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _fadeIn = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _color = new();

	public partial string Material { get => RawImage.Material; set => RawImage.Material = value; }
	public partial float FadeIn { get => RawImage.FadeIn; set => RawImage.FadeIn = value; }
	public partial Oxide.Ext.UiFramework.Colors.UiColor Color { get => RawImage.Color; set => RawImage.Color = value; }
	IRawImageComponentTrackable IUiIconTrackable.RawImage => RawImage.AsTrackable();

	public IUiIconTrackable AsTrackable() => this;
	public UiIcon SetMaterial(string material)
	{
		Material = material;
		return this;
	}
	public UiIcon SetFadeIn(float fadeIn)
	{
		FadeIn = fadeIn;
		return this;
	}
	public UiIcon SetColor(Oxide.Ext.UiFramework.Colors.UiColor color)
	{
		Color = color;
		return this;
	}
}


