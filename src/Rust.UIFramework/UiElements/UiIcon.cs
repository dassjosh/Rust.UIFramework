using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Icon;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiIcon : BaseUiComponent, IMaterial, IFadeIn
{
    public readonly RawImageComponent RawImage = new();
    internal override CoreComponent Component => RawImage;
        
    public static UiIcon CreateIcon(in UiPosition pos, in UiOffset offset, UiColor color, SelectableIcon icon)
    {
        string png = icon.GetIcon();
        return Create(pos, offset, color, png);
    }

    private static UiIcon Create(in UiPosition pos, in UiOffset offset, UiColor color, string png)
    {
        UiIcon image = CreateBase<UiIcon>(pos, offset);
        image.RawImage.Color = color;
        
        if (png.StartsWith("http", StringComparison.OrdinalIgnoreCase))
        {
            image.RawImage.Url = png;
        }
        else
        {
            image.RawImage.Png = png;
        }
        return image;
    }
    
    void IMaterial.SetMaterial(string material) => SetMaterial(material);
    void IFadeIn.SetFadeIn(float duration) => SetFadeIn(duration);

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