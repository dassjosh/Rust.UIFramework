using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;

public partial class UiRawImage : IUiRawImage, IUiRawImageTrackable
{
	public partial string Image { get => RawImage.Image; set => RawImage.Image = value; }
	public partial Oxide.Ext.UiFramework.UiElements.UiReference PlaceholderFor { get => RawImage.PlaceholderFor; set => RawImage.PlaceholderFor = value; }
	public partial string Material { get => RawImage.Material; set => RawImage.Material = value; }
	public partial float FadeIn { get => RawImage.FadeIn; set => RawImage.FadeIn = value; }
	public partial Oxide.Ext.UiFramework.Colors.UiColor Color { get => RawImage.Color; set => RawImage.Color = value; }
	IRawImageComponentTrackable IUiRawImageTrackable.RawImage => RawImage.AsTrackable();

	public IUiRawImageTrackable AsTrackable() => this;
	public UiRawImage SetImage(string image)
	{
		Image = image;
		return this;
	}
	public UiRawImage SetPlaceholderFor(in Oxide.Ext.UiFramework.UiElements.UiReference placeholderFor)
	{
		PlaceholderFor = placeholderFor;
		return this;
	}
	public UiRawImage SetMaterial(string material)
	{
		Material = material;
		return this;
	}
	public UiRawImage SetFadeIn(float fadeIn)
	{
		FadeIn = fadeIn;
		return this;
	}
	public UiRawImage SetColor(Oxide.Ext.UiFramework.Colors.UiColor color)
	{
		Color = color;
		return this;
	}
}


