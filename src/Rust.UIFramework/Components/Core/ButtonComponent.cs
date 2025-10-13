using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class ButtonComponent : CoreComponent, IGraphicalComponent
{
    private readonly TrackedValue<string> _command = new();
    private readonly TrackedValue<UiColor> _color = new(JsonDefaults.Color.ColorValue);
    private readonly TrackedValue<float> _fadeIn = new(JsonDefaults.Common.FadeIn);
    private readonly TrackedValue<string> _sprite = new();
    private readonly TrackedValue<string> _material = new();
    private readonly TrackedValue<Image.Type> _imageType = new(JsonDefaults.Image.ImageType);
    
    public string Command { get => _command.Value; set => _command.Value = value; }
    public ButtonType ButtonType;
    public UiColor Color { get => _color.Value; set => _color.Value = value; }
    public float FadeIn { get => _fadeIn.Value; set => _fadeIn.Value = value; }
    public string Sprite { get => _sprite.Value; set => _sprite.Value = value; }
    public string Material { get => _material.Value; set => _material.Value = value; }
    public Image.Type ImageType { get => _imageType.Value; set => _imageType.Value = value; }
    public ColorBlockComponent ColorBlock { get; private set; }
    public override Utf8String Type => JsonDefaults.Button.Type;
    public override ComponentType ComponentType => ComponentType.Button;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.BaseImage.SpriteName, _sprite, mode);
        writer.AddField(JsonDefaults.BaseImage.MaterialName, _material, mode);
        writer.AddField(JsonDefaults.Color.ColorName, _color, mode);
        writer.AddField(JsonDefaults.Common.FadeInName, _fadeIn, mode);
        writer.AddField(JsonDefaults.Image.ImageTypeName, _imageType, mode);
        switch (ButtonType)
        {
            case ButtonType.Command:
                writer.AddCommand(JsonDefaults.Common.CommandName, _command, mode);
                break;
            case ButtonType.Close:
                writer.AddField(JsonDefaults.Button.CloseName, _command, mode);
                break;
        }

        ColorBlock?.WriteComponent(writer, mode);
    }

    internal ColorBlockComponent GetOrAddColorBlock() => ColorBlock ??= PluginPool.Get<ColorBlockComponent>();

    internal ColorBlockComponent AddColorBlock(in UiColor? highlightColor, in UiColor? pressedColor, in UiColor? selectedColor, in float? colorMultiplier, in float? fadeDuration)
    {
        ColorBlockComponent colors = GetOrAddColorBlock();
        if(highlightColor.HasValue) colors.HighlightedColor = highlightColor.Value;
        if(pressedColor.HasValue) colors.PressedColor = pressedColor.Value;
        if(selectedColor.HasValue) colors.SelectedColor = selectedColor.Value;
        if(colorMultiplier.HasValue) colors.ColorMultiplier = colorMultiplier.Value;
        if(fadeDuration.HasValue) colors.FadeDuration = fadeDuration.Value;
        return colors;
    }
    
    public override bool HasChanged() => base.HasChanged() 
                                         || _command.HasChanged 
                                         || _color.HasChanged 
                                         || _fadeIn.HasChanged 
                                         || _sprite.HasChanged 
                                         || _material.HasChanged 
                                         || _imageType.HasChanged 
                                         || (ColorBlock?.HasChanged() ?? false);

    public override void ResetHasChanged()
    {
        base.ResetHasChanged();
        _command.ResetHasChanged();
        _color.ResetHasChanged();
        _fadeIn.ResetHasChanged();
        _sprite.ResetHasChanged();
        _material.ResetHasChanged();
        _imageType.ResetHasChanged();
    }

    public override void Reset()
    {
        base.Reset();
        ColorBlock?.Dispose();
        ColorBlock = null;
        _command.Reset();
        _color.Reset();
        _fadeIn.Reset();
        _sprite.Reset();
        _material.Reset();
        _imageType.Reset();
        ButtonType = ButtonType.Command;
    }
}