using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(ImageComponentSerializer))]
public class ImageComponent : CoreComponent, IGraphicalComponent
{
    public UiColor Color;
    public float FadeIn { get; set; }
    public string Sprite;
    public string Material;
    public Image.Type ImageType;
    public UiReference PlaceholderFor;
    public bool FillCenter;
    
    public override Utf8String Type => JsonDefaults.Image.Type;
    public override ComponentType ComponentType => ComponentType.Image;

    public override void Reset()
    {
        base.Reset();
        Color = JsonDefaults.Color.ColorValue;
        FadeIn = JsonDefaults.Common.FadeIn;
        Sprite = null;
        Material = null;
        ImageType = JsonDefaults.Image.ImageType;
        PlaceholderFor = default;
        FillCenter = JsonDefaults.Image.FillCenter;
    }

    public override void CopyFrom(object value)
    {
        base.CopyFrom(value);
        if (value is ImageComponent component)
        {
            Color = component.Color;
            FadeIn = component.FadeIn;
            Sprite = component.Sprite;
            Material = component.Material;
            ImageType = component.ImageType;
            PlaceholderFor = component.PlaceholderFor;
            FillCenter = component.FillCenter;
        }
    }
    
    public override bool AreEquivalent(BaseComponent other)
    {
        if (!base.AreEquivalent(other)) return false;
        ImageComponent typedOther = (ImageComponent)other!;
        return Color == typedOther.Color 
               && FadeIn == typedOther.FadeIn 
               && Sprite == typedOther.Sprite 
               && Material == typedOther.Material 
               && ImageType == typedOther.ImageType 
               && PlaceholderFor == typedOther.PlaceholderFor 
               && FillCenter == typedOther.FillCenter;
    }
}