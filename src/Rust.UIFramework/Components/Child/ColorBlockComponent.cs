using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(IColorBlockComponent))]
[GenerateBuilderMethods]
public partial class ColorBlockComponent : ChildComponent, IColorBlockComponent
{
    public override ComponentType ComponentType => ComponentType.ColorBlock;

    public static readonly UiColor DefaultHighlightedColor = JsonDefaults.ColorBlock.HighlightedColor;
    public static readonly UiColor DefaultPressedColor = JsonDefaults.ColorBlock.PressedColor;
    public static readonly UiColor DefaultSelectedColor = JsonDefaults.ColorBlock.SelectedColor;

    public override void WriteComponent(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.ColorBlock.HighlightedColorName, _highlightedColor, mode);
        writer.AddField(JsonDefaults.ColorBlock.PressedColorName, _pressedColor, mode);
        writer.AddField(JsonDefaults.ColorBlock.SelectedColorName, _selectedColor, mode);
        writer.AddField(JsonDefaults.ColorBlock.ColorMultiplierName, _colorMultiplier, mode);
        writer.AddField(JsonDefaults.ColorBlock.FadeDurationName, _fadeDuration, mode);
    }
}