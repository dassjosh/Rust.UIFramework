using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;

public partial class UiNineSlice : IUiNineSlice, IUiNineSliceTrackable
{
	public partial string Png { get => Image.Png; set => Image.Png = value; }
	public partial Oxide.Ext.UiFramework.Types.UiBorderWidth Slice { get => Image.Slice; set => Image.Slice = value; }
	public partial Oxide.Ext.UiFramework.UiElements.UiReference PlaceholderFor { get => Image.PlaceholderFor; set => Image.PlaceholderFor = value; }
	public partial bool FillCenter { get => Image.FillCenter; set => Image.FillCenter = value; }
	public partial UnityEngine.UI.Image.Type ImageType { get => Image.ImageType; set => Image.ImageType = value; }
	public partial string Sprite { get => Image.Sprite; set => Image.Sprite = value; }
	public partial string Material { get => Image.Material; set => Image.Material = value; }
	public partial float FadeIn { get => Image.FadeIn; set => Image.FadeIn = value; }
	public partial Oxide.Ext.UiFramework.Colors.UiColor Color { get => Image.Color; set => Image.Color = value; }
	INineSliceComponentTrackable IUiNineSliceTrackable.Image => Image.AsTrackable();

	public IUiNineSliceTrackable AsTrackable() => this;
	public UiNineSlice SetPng(string png)
	{
		Png = png;
		return this;
	}
	public UiNineSlice SetSlice(in Oxide.Ext.UiFramework.Types.UiBorderWidth slice)
	{
		Slice = slice;
		return this;
	}
	public UiNineSlice SetPlaceholderFor(in Oxide.Ext.UiFramework.UiElements.UiReference placeholderFor)
	{
		PlaceholderFor = placeholderFor;
		return this;
	}
	public UiNineSlice SetFillCenter(bool fillCenter)
	{
		FillCenter = fillCenter;
		return this;
	}
	public UiNineSlice SetImageType(UnityEngine.UI.Image.Type imageType)
	{
		ImageType = imageType;
		return this;
	}
	public UiNineSlice SetSprite(string sprite)
	{
		Sprite = sprite;
		return this;
	}
	public UiNineSlice SetMaterial(string material)
	{
		Material = material;
		return this;
	}
	public UiNineSlice SetFadeIn(float fadeIn)
	{
		FadeIn = fadeIn;
		return this;
	}
	public UiNineSlice SetColor(Oxide.Ext.UiFramework.Colors.UiColor color)
	{
		Color = color;
		return this;
	}
}


