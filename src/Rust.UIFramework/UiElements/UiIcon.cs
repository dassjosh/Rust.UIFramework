using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiIcon : BaseUiComponent, IMaterial<UiIcon>, IFadeIn<UiIcon>, IUiColor<UiIcon>
{
    public readonly RawImageComponent RawImage = new();
    internal override CoreComponent Component => RawImage;
    
    public string Material { get => RawImage.Material; set => RawImage.Material = value; }
    public float FadeIn { get => RawImage.FadeIn; set => RawImage.FadeIn = value; }
    public UiColor Color { get => RawImage.Color; set => RawImage.Color = value; }
        
    public static UiIcon CreateIcon<T>(T icon, UiColor color) where T : struct, Enum
    {
        UiIcon image = CreateBase<UiIcon>();
        image.RawImage.Color = color;
        Singleton<UiIconLib>.Instance.PopulateIconData(image, icon);
        return image;
    }
    
    public UiIcon SetColor(UiColor color)
    {
        RawImage.Color = color;
        return this;
    }
    
    public UiIcon SetMaterial(string material)
    {
        RawImage.Material = material;
        return this;
    }
        
    public UiIcon SetFadeIn(float duration)
    {
        RawImage.FadeIn = duration;
        return this;
    }
}