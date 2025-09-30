using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(ButtonComponentSerializer))]
public class ButtonComponent : CoreComponent, IGraphicalComponent
{
    public string Command;
    public ButtonType ButtonType;
    public UiColor Color;
    public float FadeIn { get; set; }
    public string Sprite;
    public string Material;
    public Image.Type ImageType;
    public ColorBlockComponent ColorBlock { get; private set; }
    public override Utf8String Type => JsonDefaults.Button.Type;
    public override ComponentType ComponentType => ComponentType.Button;

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

    public override void CopyFrom(object value)
    {
        base.CopyFrom(value);
        if (value is ButtonComponent component)
        {
            Command = component.Command;
            ButtonType = component.ButtonType;
            Color = component.Color;
            FadeIn = component.FadeIn;
            Sprite = component.Sprite;
            Material = component.Material;
            ImageType = component.ImageType;
            ColorBlock =  CopyChild(ColorBlock, component.ColorBlock);
        }
    }
    
    public override bool Equals(BaseComponent other)
    {
        if (!base.Equals(other)) return false;
        ButtonComponent typedOther = (ButtonComponent)other!;
        return Command == typedOther.Command 
               && ButtonType == typedOther.ButtonType 
               && Color == typedOther.Color 
               && FadeIn == typedOther.FadeIn 
               && Sprite == typedOther.Sprite 
               && Material == typedOther.Material 
               && ImageType == typedOther.ImageType 
               && ColorBlock == typedOther.ColorBlock;
    }
}