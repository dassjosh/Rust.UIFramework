using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Libraries;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement]
[GenerateBuilderMethods]
public partial class UiButton : BaseUiComponent, IImageType<UiButton>, ISprite<UiButton>, IMaterial<UiButton>, IFadeIn<UiButton>, IUiColor<UiButton>
{
    [SkipBuilder]
    public partial string Command { get; set; }
    public partial ButtonType ButtonType { get; set; }
    
    [PropertyTarget(nameof(GetOrAddColorBlock), PropertyTargetType.Method)]
    public partial UiColor HighlightedColor { get; set; }
    
    [PropertyTarget(nameof(GetOrAddColorBlock), PropertyTargetType.Method)]
    public partial UiColor PressedColor { get; set; }
    
    [PropertyTarget(nameof(GetOrAddColorBlock), PropertyTargetType.Method)]
    public partial UiColor SelectedColor { get; set; }
    
    [PropertyTarget(nameof(GetOrAddColorBlock), PropertyTargetType.Method)]
    public partial float ColorMultiplier { get; set; }
    
    [PropertyTarget(nameof(GetOrAddColorBlock), PropertyTargetType.Method)]
    public partial float FadeDuration { get; set; }
    public partial Image.Type ImageType { get; set; }
    public partial string Sprite { get; set; }
    public partial string Material { get; set; }
    public partial float FadeIn { get; set; }
    public partial UiColor Color { get; set; }
    
    public readonly ButtonComponent Button;
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
        
    [Obsolete("Use SetSprite().SetMaterial().SetImageType() instead.")]
    public UiButton SetSpriteMaterialImage(string sprite = null, string material = null, Image.Type type = Image.Type.Simple)
    {
        Sprite = sprite;
        Material = material;
        ImageType = type;
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
    
    public ColorBlockComponent AddColorBlock(in UiColor? highlightColor = null, in UiColor? pressedColor = null, in UiColor? selectedColor = null, in UiColor? disabledColor = null, in float? colorMultiplier = null, in float? fadeDuration = null)
    {
        return Button.AddColorBlock(highlightColor, pressedColor, selectedColor, disabledColor, colorMultiplier, fadeDuration);
    }

    public ColorBlockComponent GetOrAddColorBlock() => Button.GetOrCreateColorBlock();
}