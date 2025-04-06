using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiRawImage : BaseUiComponent, IMaterial<UiRawImage>, IFadeIn<UiRawImage>, IUiColor<UiRawImage>
{
    public readonly RawImageComponent RawImage = new();
    internal override CoreComponent Component => RawImage;
    
    public string Material { get => RawImage.Material; set => RawImage.Material = value; }
    public float FadeIn { get => RawImage.FadeIn; set => RawImage.FadeIn = value; }
    public UiColor Color { get => RawImage.Color; set => RawImage.Color = value; }
        
    public static UiRawImage Create(string image, in UiColor color)
    {
        UiRawImage rawImage = CreateBase<UiRawImage>();
        rawImage.RawImage.Color = color;
        rawImage.RawImage.Image = image;
        return rawImage;
    }

    public UiRawImage SetColor(UiColor color)
    {
        RawImage.Color = color;
        return this;
    }
    
    public UiRawImage SetImage(string image)
    {
        RawImage.Image = image;
        return this;
    }
    
    public UiRawImage SetMaterial(string material)
    {
        RawImage.Material = material;
        return this;
    }
        
    public UiRawImage SetFadeIn(float duration)
    {
        RawImage.FadeIn = duration;
        return this;
    }
}