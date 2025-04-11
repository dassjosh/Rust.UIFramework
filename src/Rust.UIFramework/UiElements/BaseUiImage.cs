using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements;

public abstract class BaseUiImage<T>(ImageComponent component) : BaseUiComponent(component), IImageType<T>, ISprite<T>, IMaterial<T>, IFadeIn<T>, IUiColor<T> where T : BaseUiImage<T>
{
    public Image.Type ImageType { get => component.ImageType; set => component.ImageType = value; }
    public string Sprite { get => component.Sprite; set => component.Sprite = value; }
    public string Material { get => component.Material; set => component.Material = value; }
    public float FadeIn { get => component.FadeIn; set => component.FadeIn = value; }
    public UiColor Color { get => component.Color; set => component.Color = value; }

    public T SetImageType(Image.Type type)
    {
        component.ImageType = type;
        return (T)this;
    }
        
    public T SetSprite(string sprite)
    {
        component.Sprite = sprite;
        return (T)this;
    }
        
    public T SetMaterial(string material)
    {
        component.Material = material;
        return (T)this;
    }
        
    public T SetSpriteMaterialImage(string sprite = null, string material = null, Image.Type type = Image.Type.Simple)
    {
        component.Sprite = sprite;
        component.Material = material;
        component.ImageType = type;
        return (T)this;
    }
    
    public T SetColor(UiColor color)
    {
        component.Color = color;
        return (T) this;
    }
        
    public T SetFadeIn(float duration)
    {
        component.FadeIn = duration;
        return (T)this;
    }
}