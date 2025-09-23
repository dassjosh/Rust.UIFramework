using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements;

public abstract class BaseUiImage<T>(ImageComponent image) : BaseUiComponent(image), IImageType<T>, ISprite<T>, IMaterial<T>, IFadeIn<T>, IUiColor<T> where T : BaseUiImage<T>
{
    public Image.Type ImageType { get => image.ImageType; set => image.ImageType = value; }
    public string Sprite { get => image.Sprite; set => image.Sprite = value; }
    public string Material { get => image.Material; set => image.Material = value; }
    public float FadeIn { get => image.FadeIn; set => image.FadeIn = value; }
    public UiColor Color { get => image.Color; set => image.Color = value; }
    public UiReference PlaceholderFor { get => image.PlaceholderFor; set => image.PlaceholderFor = value; }

    public T SetImageType(Image.Type type)
    {
        ImageType = type;
        return (T)this;
    }
        
    public T SetSprite(string sprite)
    {
        Sprite = sprite;
        return (T)this;
    }
        
    public T SetMaterial(string material)
    {
        Material = material;
        return (T)this;
    }
        
    public T SetSpriteMaterialImage(string sprite = null, string material = null, Image.Type type = Image.Type.Simple)
    {
        Sprite = sprite;
        Material = material;
        ImageType = type;
        return (T)this;
    }
    
    public T SetColor(UiColor color)
    {
        Color = color;
        return (T) this;
    }
        
    public T SetFadeIn(float duration)
    {
        FadeIn = duration;
        return (T)this;
    }
    
    public T SetPlaceholderFor(in UiReference placeholder)
    {
        PlaceholderFor = placeholder;
        return (T)this;
    }

    public T SetPlaceholderFor(UiInput input) => SetPlaceholderFor(input.Reference);
}