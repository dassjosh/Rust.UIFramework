using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;

public partial class UiButton : IUiButton, IUiButtonTrackable
{
	public partial string Command { get => Button.Command; set => Button.Command = value; }
	public partial Oxide.Ext.UiFramework.Enums.ButtonType ButtonType { get => Button.ButtonType; set => Button.ButtonType = value; }
	public partial Oxide.Ext.UiFramework.Colors.UiColor HighlightedColor { get => GetOrAddColorBlock().HighlightedColor; set => GetOrAddColorBlock().HighlightedColor = value; }
	public partial Oxide.Ext.UiFramework.Colors.UiColor PressedColor { get => GetOrAddColorBlock().PressedColor; set => GetOrAddColorBlock().PressedColor = value; }
	public partial Oxide.Ext.UiFramework.Colors.UiColor SelectedColor { get => GetOrAddColorBlock().SelectedColor; set => GetOrAddColorBlock().SelectedColor = value; }
	public partial float ColorMultiplier { get => GetOrAddColorBlock().ColorMultiplier; set => GetOrAddColorBlock().ColorMultiplier = value; }
	public partial float FadeDuration { get => GetOrAddColorBlock().FadeDuration; set => GetOrAddColorBlock().FadeDuration = value; }
	public partial UnityEngine.UI.Image.Type ImageType { get => Button.ImageType; set => Button.ImageType = value; }
	public partial string Sprite { get => Button.Sprite; set => Button.Sprite = value; }
	public partial string Material { get => Button.Material; set => Button.Material = value; }
	public partial float FadeIn { get => Button.FadeIn; set => Button.FadeIn = value; }
	public partial Oxide.Ext.UiFramework.Colors.UiColor Color { get => Button.Color; set => Button.Color = value; }
	IButtonComponentTrackable IUiButtonTrackable.Button => Button.AsTrackable();

	public IUiButtonTrackable AsTrackable() => this;
	public UiButton SetButtonType(Oxide.Ext.UiFramework.Enums.ButtonType buttonType)
	{
		ButtonType = buttonType;
		return this;
	}
	public UiButton SetHighlightedColor(Oxide.Ext.UiFramework.Colors.UiColor highlightedColor)
	{
		HighlightedColor = highlightedColor;
		return this;
	}
	public UiButton SetPressedColor(Oxide.Ext.UiFramework.Colors.UiColor pressedColor)
	{
		PressedColor = pressedColor;
		return this;
	}
	public UiButton SetSelectedColor(Oxide.Ext.UiFramework.Colors.UiColor selectedColor)
	{
		SelectedColor = selectedColor;
		return this;
	}
	public UiButton SetColorMultiplier(float colorMultiplier)
	{
		ColorMultiplier = colorMultiplier;
		return this;
	}
	public UiButton SetFadeDuration(float fadeDuration)
	{
		FadeDuration = fadeDuration;
		return this;
	}
	public UiButton SetImageType(UnityEngine.UI.Image.Type imageType)
	{
		ImageType = imageType;
		return this;
	}
	public UiButton SetSprite(string sprite)
	{
		Sprite = sprite;
		return this;
	}
	public UiButton SetMaterial(string material)
	{
		Material = material;
		return this;
	}
	public UiButton SetFadeIn(float fadeIn)
	{
		FadeIn = fadeIn;
		return this;
	}
	public UiButton SetColor(Oxide.Ext.UiFramework.Colors.UiColor color)
	{
		Color = color;
		return this;
	}
}


