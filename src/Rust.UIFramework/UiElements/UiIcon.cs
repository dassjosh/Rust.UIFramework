using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiIcon : BaseUiComponent, IMaterial<UiIcon>, IFadeIn<UiIcon>, IUiColor<UiIcon>
{
    public readonly RawImageComponent RawImage;

    public string Material { get => RawImage.Material; set => RawImage.Material = value; }
    public float FadeIn { get => RawImage.FadeIn; set => RawImage.FadeIn = value; }
    public UiColor Color { get => RawImage.Color; set => RawImage.Color = value; }
    
    public UiIcon() : this(new RawImageComponent()) { }

    private UiIcon(RawImageComponent component) : base(component)
    {
        RawImage = component;
    }
        
    public UiIcon Init<T>(T icon, UiColor color) where T : struct, Enum
    {
        Color = color;
        SetIcon(icon);
        return this;
    }
    
    public UiIcon SetColor(UiColor color)
    {
        Color = color;
        return this;
    }
    
    public UiIcon SetMaterial(string material)
    {
        Material = material;
        return this;
    }
        
    public UiIcon SetFadeIn(float duration)
    {
        FadeIn = duration;
        return this;
    }
    
    public UiIcon SetIcon<T>(T icon) where T : struct, Enum
    {
        Singleton<UiIconLib>.Instance.PopulateIconData(this, icon);
        return this;
    }
}