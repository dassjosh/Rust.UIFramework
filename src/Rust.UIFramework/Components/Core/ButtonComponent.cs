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
    [TrackedDefaults(typeof(JsonDefaults.Common), nameof(JsonDefaults.Common.FadeIn))]
    public partial float FadeIn { get; set; }
    public partial string Sprite { get; set; }
    public partial string Material { get; set; }
    public partial Image.Type ImageType { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.Common), nameof(JsonDefaults.Common.AllowRaycast))]
    public partial bool AllowRaycast { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.Common), nameof(JsonDefaults.Common.Interactable))]
    public partial bool Interactable { get; set; }
    public ButtonType ButtonType { get; set; }
    public ColorBlockComponent ColorBlock { get; private set; }
    public override Utf8String Type => JsonDefaults.Button.Type;
    public override ComponentType ComponentType => ComponentType.Button;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.BaseImage.SpriteName, SpriteTracked, mode);
        writer.AddField(JsonDefaults.BaseImage.MaterialName, MaterialTracked, mode);
        writer.AddField(JsonDefaults.Color.ColorName, ColorTracked, mode);
        writer.AddField(JsonDefaults.Common.FadeInName, FadeInTracked, mode);
        writer.AddField(JsonDefaults.Image.ImageTypeName, ImageTypeTracked, mode);
        writer.AddField(JsonDefaults.Common.AllowRaycastName, AllowRaycastTracked, mode);
        writer.AddField(JsonDefaults.Common.InteractableName, InteractableTracked, mode);
        switch (ButtonType)
        {
            case ButtonType.Command:
                writer.AddCommandField(JsonDefaults.Common.CommandName, CommandTracked, mode);
                break;
            case ButtonType.Close:
                writer.AddField(JsonDefaults.Button.CloseName, CommandTracked, mode);
                break;
        }

        ColorBlock?.WriteComponent(writer, mode);
    }

    internal ColorBlockComponent GetOrCreateColorBlock() => ColorBlock ??= PluginPool.Get<ColorBlockComponent>();

    internal ColorBlockComponent AddColorBlock(in UiColor? highlightColor, in UiColor? pressedColor, in UiColor? selectedColor, in UiColor? disabledColor, in float? colorMultiplier, in float? fadeDuration)
    {
        ColorBlockComponent colors = GetOrCreateColorBlock();
        if(highlightColor.HasValue) colors.HighlightedColor = highlightColor.Value;
        if(pressedColor.HasValue) colors.PressedColor = pressedColor.Value;
        if(selectedColor.HasValue) colors.SelectedColor = selectedColor.Value;
        if(disabledColor.HasValue) colors.DisabledColor = disabledColor.Value;
        if(colorMultiplier.HasValue) colors.ColorMultiplier = colorMultiplier.Value;
        if(fadeDuration.HasValue) colors.FadeDuration = fadeDuration.Value;
        return colors;
    }

    protected override void OnReset()
    {
        ButtonType = ButtonType.Command;
    }
}