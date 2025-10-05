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
    public UiReference PlaceholderFor { get => RawImage.PlaceholderFor; set => RawImage.PlaceholderFor = value; }
    
    public UiRawImage() : this(new RawImageComponent()) { }

    private UiRawImage(RawImageComponent component) : base(component)
    {
        RawImage = component;
    }
    
    public UiRawImage Init(string image, in UiColor color)
    {
        Color = color;
        Image = image;
        return this;
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
    
    public UiRawImage SetPlaceholderFor(in UiReference placeholder)
    {
        PlaceholderFor = placeholder;
        return this;
    }

    public UiRawImage SetPlaceholderFor(UiInput input) => SetPlaceholderFor(input.Reference);
}