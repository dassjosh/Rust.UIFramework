using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiRawImage : BaseUiComponent, IMaterial<UiRawImage>, IFadeIn<UiRawImage>, IUiColor<UiRawImage>
{
    public readonly RawImageComponent RawImage;

    public string Material { get => RawImage.Material; set => RawImage.Material = value; }
    public float FadeIn { get => RawImage.FadeIn; set => RawImage.FadeIn = value; }
    public UiColor Color { get => RawImage.Color; set => RawImage.Color = value; }
    public string Image { get => RawImage.Image; set => RawImage.Image = value; }
    
    public UiRawImage() : this(new RawImageComponent()) { }

    private UiRawImage(RawImageComponent component) : base(component)
    {
        RawImage = component;
    }
    
    public static UiRawImage Create(string image, in UiColor color)
    {
        UiRawImage rawImage = CreateBase<UiRawImage>();
        rawImage.RawImage.Color = color;
        rawImage.RawImage.Image = image;
        return rawImage;
    }

    public UiRawImage SetColor(UiColor color)
    {
        Color = color;
        return this;
    }
    
    public UiRawImage SetImage(string image)
    {
        Image = image;
        return this;
    }
    
    public UiRawImage SetMaterial(string material)
    {
        Material = material;
        return this;
    }
        
    public UiRawImage SetFadeIn(float duration)
    {
        FadeIn = duration;
        return this;
    }
}