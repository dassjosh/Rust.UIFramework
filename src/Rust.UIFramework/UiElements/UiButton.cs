using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiButton : BaseUiComponent, IImageType<UiButton>, ISprite<UiButton>, IMaterial<UiButton>, IFadeIn<UiButton>, IUiColor<UiButton>
{
    public readonly ButtonComponent Button = new();
    internal override CoreComponent Component => Button;

    public static UiButton CreateCommand(UiColor color, string command)
    {
        UiButton button = CreateBase<UiButton>();
        button.Button.Color = color;
        button.Button.Command = command;
        return button;
    }
    
    public static UiButton CreateCommand(in UiPosition pos, in UiOffset offset, UiColor color, string command)
    {
        UiButton button = CreateBase<UiButton>(pos, offset);
        button.Button.Color = color;
        button.Button.Command = command;
        return button;
    }

    public static UiButton CreateClose(in UiPosition pos, in UiOffset offset, UiColor color, string close)
    {
        UiButton button = CreateBase<UiButton>(pos, offset);
        button.Button.Color = color;
        button.Button.Close = close;
        return button;
    }
    
    public UiColor GetColor() => Button.Color;
    
    void IImageType.SetImageType(Image.Type type) => SetImageType(type);
    void ISprite.SetSprite(string sprite) => SetSprite(sprite);
    void IMaterial.SetMaterial(string material) => SetMaterial(material);
    void IFadeIn.SetFadeIn(float duration) => SetFadeIn(duration);
    void IUiColor.SetColor(UiColor color) => SetColor(color);
        
    public UiButton SetFadeIn(float duration)
    {
        Button.FadeIn = duration;
        return this;
    }

    public UiButton SetImageType(Image.Type type)
    {
        Button.ImageType = type;
        return this;
    }

    public UiButton SetSprite(string sprite)
    {
        Button.Sprite = sprite;
        return this;
    }
        
    public UiButton SetMaterial(string material)
    {
        Button.Material = material;
        return this;
    }
        
    public UiButton SetSpriteMaterialImage(string sprite = null, string material = null, Image.Type type = Image.Type.Simple)
    {
        Button.Sprite = sprite;
        Button.Material = material;
        Button.ImageType = type;
        return this;
    }
    
    public UiButton SetColor(UiColor color)
    {
        Button.Color = color;
        return this;
    }
    
    public UiButton SetCommand(string command)
    {
        Button.Command = command;
        return this;
    }
    
    public UiButton SetClose(string close)
    {
        Button.Close = close;
        return this;
    }
    
    public ColorBlockComponent AddColorBlock(in UiColor? highlightColor = null, in UiColor? pressedColor = null, in UiColor? selectedColor = null, in float? colorMultiplier = null, in float? fadeDuration = null)
    {
        ColorBlockComponent colors = UiFrameworkPool.Get<ColorBlockComponent>();
        if(highlightColor.HasValue) colors.HighlightedColor = highlightColor.Value;
        if(pressedColor.HasValue) colors.PressedColor = pressedColor.Value;
        if(selectedColor.HasValue) colors.SelectedColor = selectedColor.Value;
        if(colorMultiplier.HasValue) colors.ColorMultiplier = colorMultiplier.Value;
        if(fadeDuration.HasValue) colors.FadeDuration = fadeDuration.Value;
        Button.ColorBlock = colors;
        return colors;
    }
}