using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class ButtonComponent : CoreComponent
{
    public string Command;
    public ButtonType ButtonType;
    public UiColor Color;
    public float FadeIn;
    public string Sprite;
    public string Material;
    public Image.Type ImageType;
    public ColorBlockComponent ColorBlock { get; private set; }
    public override Utf8String Type => JsonDefaults.Button.Type;

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
    
    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        writer.AddField(JsonDefaults.BaseImage.SpriteName, Sprite, JsonDefaults.BaseImage.Sprite);
        writer.AddField(JsonDefaults.BaseImage.MaterialName, Material, JsonDefaults.BaseImage.Material);
        writer.AddField(JsonDefaults.Common.FadeInName, FadeIn, JsonDefaults.Common.FadeIn);
        writer.AddField(JsonDefaults.Color.ColorName, Color);
        writer.AddField(JsonDefaults.Image.ImageTypeName, ImageType, JsonDefaults.Image.ImageType);
        switch (ButtonType)
        {
            case ButtonType.Command:
                writer.AddCommand(JsonDefaults.Common.CommandName, Command);
                break;
            case ButtonType.Close:
                writer.AddField(JsonDefaults.Button.CloseName, Command, JsonDefaults.Common.NullValue);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        ColorBlock?.WriteComponent(writer);
    }
    
    public override void Reset()
    {
        base.Reset();
        ColorBlock?.Dispose();
        ColorBlock = null;
        Command = null;
        ButtonType = ButtonType.Command;
        Color = JsonDefaults.Color.ColorValue;
        FadeIn = JsonDefaults.Common.FadeIn;
        Sprite = null;
        Material = null;
        ImageType = JsonDefaults.Image.ImageType;
    }
}