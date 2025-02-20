using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiRawImage : BaseUiComponent
{
    public readonly RawImageComponent RawImage = new();
    internal override CoreComponent Component => RawImage;

    public static UiRawImage CreateDefault(in UiPosition pos, in UiOffset offset)
    {
        UiRawImage image = CreateBase<UiRawImage>(pos, offset);
        return image;
    } 
        
    public static UiRawImage CreateUrl(in UiPosition pos, in UiOffset offset, UiColor color, string url)
    {
        UiRawImage image = CreateBase<UiRawImage>(pos, offset);
        image.RawImage.Color = color;
        image.RawImage.Url = url;
        return image;
    }
        
    public static UiRawImage CreateTexture(in UiPosition pos, in UiOffset offset, UiColor color, string icon)
    {
        UiRawImage image = CreateBase<UiRawImage>(pos, offset);
        image.RawImage.Color = color;
        image.RawImage.Texture = icon;
        return image;
    }
        
    public static UiRawImage CreateFileImage(in UiPosition pos, in UiOffset offset, UiColor color, string png)
    {
        UiRawImage image = CreateBase<UiRawImage>(pos, offset);
        image.RawImage.Color = color;
        image.RawImage.Png = png;
        return image;
    }

    public void SetMaterial(string material)
    {
        RawImage.Material = material;
    }
        
    public void SetFadeIn(float duration)
    {
        RawImage.FadeIn = duration;
    }
}