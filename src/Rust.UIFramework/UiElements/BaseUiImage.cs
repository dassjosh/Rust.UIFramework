using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements;

public abstract class BaseUiImage<T> : BaseUiComponent, IImageType<T>, ISprite<T>, IMaterial<T>, IFadeIn<T>, IUiColor<T> where T : BaseUiImage<T>
{
    private ImageComponent Image => (ImageComponent)Component;
    
    public Image.Type ImageType { get => Image.ImageType; set => Image.ImageType = value; }
    public string Sprite { get => Image.Sprite; set => Image.Sprite = value; }
    public string Material { get => Image.Material; set => Image.Material = value; }
    public float FadeIn { get => Image.FadeIn; set => Image.FadeIn = value; }
    public UiColor Color { get => Image.Color; set => Image.Color = value; }

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
}