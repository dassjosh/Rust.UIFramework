using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Libraries;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement(typeof(IUiButton))]
public partial class UiButton : BaseUiComponent, IUiButton
{
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
    
    public ColorBlockComponent AddColorBlock(in UiColor? highlightColor = null, in UiColor? pressedColor = null, in UiColor? selectedColor = null, in float? colorMultiplier = null, in float? fadeDuration = null)
    {
        return Button.AddColorBlock(highlightColor, pressedColor, selectedColor, colorMultiplier, fadeDuration);
    }

    public ColorBlockComponent GetOrAddColorBlock() => Button.GetOrAddColorBlock();
}