using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Libraries;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiButton : BaseUiComponent, IImageType<UiButton>, ISprite<UiButton>, IMaterial<UiButton>, IFadeIn<UiButton>, IUiColor<UiButton>
{
    public readonly ButtonComponent Button;

    public Image.Type ImageType { get => Button.ImageType; set => Button.ImageType = value; }
    public string Sprite { get => Button.Sprite; set => Button.Sprite = value; }
    public string Material { get => Button.Material; set => Button.Material = value; }
    public float FadeIn { get => Button.FadeIn; set => Button.FadeIn = value; }
    public UiColor Color { get => Button.Color; set => Button.Color = value; }
    public string Command { get => Button.Command; set => Button.Command = value; }
    public ButtonType ButtonType { get => Button.ButtonType; set => Button.ButtonType = value; }
    public ColorBlockComponent ColorBlock => Button.ColorBlock;
    
    public UiButton() : this(new ButtonComponent()) { }

    private UiButton(ButtonComponent component) : base(component)
    {
        Button = component;
    }

    public UiButton Init(UiColor color, string command, ButtonType buttonType)
    {
        Color = color;
        Command = command;
        ButtonType = buttonType;
        return this;
    }
        
    public UiButton SetFadeIn(float duration)
    {
        FadeIn = duration;
        return this;
    }

    public UiButton SetImageType(Image.Type type)
    {
        ImageType = type;
        return this;
    }

    public UiButton SetSprite(string sprite)
    {
        Sprite = sprite;
        return this;
    }
        
    public UiButton SetMaterial(string material)
    {
        Material = material;
        return this;
    }
        
    public UiButton SetSpriteMaterialImage(string sprite = null, string material = null, Image.Type type = Image.Type.Simple)
    {
        Sprite = sprite;
        Material = material;
        ImageType = type;
        return this;
    }
    
    public UiButton SetColor(UiColor color)
    {
        Color = color;
        return this;
    }
    
    public UiButton SetCommand(string command)
    {
        Command = command;
        ButtonType = ButtonType.Command;
        return this;
    }
    
    public UiButton SetCommand(ICommandBuilder command) => SetCommand(command.Build());
    
    public UiButton SetClose(string close)
    {
        Command = close;
        ButtonType = ButtonType.Close;
        return this;
    }
    
    public UiButton SetHighlightedColor(UiColor color)
    {
        Button.GetOrAddColorBlock();
        ColorBlock.HighlightedColor = color;
        return this;
    }
    
    public UiButton SetPressedColor(UiColor color)
    {
        Button.GetOrAddColorBlock();
        ColorBlock.PressedColor = color;
        return this;
    }
    
    public UiButton SetSelectedColor(UiColor color)
    {
        Button.GetOrAddColorBlock();
        ColorBlock.SelectedColor = color;
        return this;
    }    
    public UiButton SetColorMultiplier(float colorMultiplier)
    {
        Button.GetOrAddColorBlock();
        ColorBlock.ColorMultiplier = colorMultiplier;
        return this;
    }
    
    public UiButton SetFadeDuration(float fadeDuration)
    {
        Button.GetOrAddColorBlock();
        ColorBlock.FadeDuration = fadeDuration;
        return this;
    }
    
    public ColorBlockComponent AddColorBlock(in UiColor? highlightColor = null, in UiColor? pressedColor = null, in UiColor? selectedColor = null, in float? colorMultiplier = null, in float? fadeDuration = null)
    {
        return Button.AddColorBlock(highlightColor, pressedColor, selectedColor, colorMultiplier, fadeDuration);
    }
}