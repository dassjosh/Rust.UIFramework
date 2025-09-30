using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(RawImageComponentSerializer))]
public class RawImageComponent : CoreComponent, IGraphicalComponent
{
    public UiColor Color;
    public float FadeIn { get; set; }
    public string Image;
    public string Material;
    public UiReference PlaceholderFor;
    
    [Obsolete("Please use Image instead")]
    public string Url { get => Image; set => Image = value; }
    [Obsolete("Please use Image instead")]
    public string Png { get => Image; set => Image = value; }
    [Obsolete("Please use Image instead")]
    public string Texture { get => Image; set => Image = value; }

    public override Utf8String Type => JsonDefaults.RawImage.Type;
    public override ComponentType ComponentType => ComponentType.RawImage;

    public override void Reset()
    {
        base.Reset();
        Color = default;
        FadeIn = JsonDefaults.Common.FadeIn;
        Image = null;
        Material = null;
        PlaceholderFor = default;
    }

    public override void CopyFrom(object value)
    {
        base.CopyFrom(value);
        if (value is RawImageComponent component)
        {
            Color = component.Color;
            FadeIn = component.FadeIn;
            Image = component.Image;
            Material = component.Material;
            PlaceholderFor = component.PlaceholderFor;
        }
    }
    
    public override bool Equals(BaseComponent other)
    {
        if (!base.Equals(other)) return false;
        RawImageComponent typedOther = (RawImageComponent)other!;
        return Color == typedOther.Color 
               && FadeIn == typedOther.FadeIn 
               && Image == typedOther.Image 
               && Material == typedOther.Material 
               && PlaceholderFor == typedOther.PlaceholderFor;
    }
}