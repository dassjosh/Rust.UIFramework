using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(IButtonComponent))]
public partial class ButtonComponent : CoreComponent, IButtonComponent, IGraphicalComponent
{
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
    
    public override bool HasChanged() => base.HasChanged() || (ColorBlock?.HasChanged() ?? false);

    public override void ResetHasChanged()
    {
        base.ResetHasChanged();
        ColorBlock?.ResetHasChanged();
    }

    public override void Reset()
    {
        base.Reset();
        ColorBlock?.TryDispose();
        ColorBlock = null;
        ButtonType = ButtonType.Command;
    }
}