using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class ButtonComponent : CoreComponent, IGraphicalComponent
{
    public partial string Command { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.Color), nameof(JsonDefaults.Color.ColorValue))]
    public partial UiColor Color { get; set; }
    public partial float FadeIn { get; set; }
    public partial string Sprite { get; set; }
    public partial string Material { get; set; }
    public partial Image.Type ImageType { get; set; }
    public ButtonType ButtonType;
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
                writer.AddCommandField(JsonDefaults.Common.CommandName, _command, mode);
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

    protected override void OnReset()
    {
        ButtonType = ButtonType.Command;
    }
}