using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiIcon : BaseUiComponent, IMaterial<UiIcon>, IFadeIn<UiIcon>, IUiColor<UiIcon>
{
    public readonly RawImageComponent RawImage = new();
    internal override CoreComponent Component => RawImage;
        
    public static UiIcon CreateIcon<T>(in UiPosition pos, in UiOffset offset, UiColor color, T icon) where T : struct, Enum
    {
        UiIcon image = CreateBase<UiIcon>(pos, offset);
        image.RawImage.Color = color;
        Singleton<UiIconLib>.Instance.PopulateIconData(image, icon);
        return image;
    }
    
    void IMaterial.SetMaterial(string material) => SetMaterial(material);
    void IFadeIn.SetFadeIn(float duration) => SetFadeIn(duration);
    void IUiColor.SetColor(UiColor color) => SetColor(color);

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