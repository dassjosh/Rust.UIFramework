using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements;

public abstract class BaseUiImage<T> : BaseUiComponent, IImageType<T>, ISprite<T>, IMaterial<T>, IFadeIn<T>, IUiColor<T> where T : BaseUiImage<T>
{
    public readonly ImageComponent Image = new();
    internal override CoreComponent Component => Image;

    public UiColor GetColor() => Image.Color;
    
    void IImageType.SetImageType(Image.Type type) => SetImageType(type);
    void ISprite.SetSprite(string sprite) => SetSprite(sprite);
    void IMaterial.SetMaterial(string material) => SetMaterial(material);
    void IFadeIn.SetFadeIn(float duration) => SetFadeIn(duration);
    void IUiColor.SetColor(UiColor color) => SetColor(color);

    public T SetImageType(Image.Type type)
    {
        Image.ImageType = type;
        return (T)this;
    }
        
    public T SetSprite(string sprite)
    {
        Image.Sprite = sprite;
        return (T)this;
    }
        
    public T SetMaterial(string material)
    {
        Image.Material = material;
        return (T)this;
    }
        
    public T SetSpriteMaterialImage(string sprite = null, string material = null, Image.Type type = UnityEngine.UI.Image.Type.Simple)
    {
        Image.Sprite = sprite;
        Image.Material = material;
        Image.ImageType = type;
        return (T)this;
    }
    
    public T SetColor(UiColor color)
    {
        Image.Color = color;
        return (T) this;
    }
        
    public T SetFadeIn(float duration)
    {
        Image.FadeIn = duration;
        return (T)this;
    }
    
    public T SetPng(string png)
    {
        Image.Png = png;
        return (T)this;
    }
}