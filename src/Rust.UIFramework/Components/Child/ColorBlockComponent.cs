using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(ColorBlockComponentSerializer))]
public class ColorBlockComponent : ChildComponent
{
    private readonly TrackedValue<UiColor> _highlightColor = new(JsonDefaults.ColorBlock.HighlightedColor);
    private readonly TrackedValue<UiColor> _pressedColor = new(JsonDefaults.ColorBlock.PressedColor);
    private readonly TrackedValue<UiColor> _selectedColor = new(JsonDefaults.ColorBlock.SelectedColor);
    private readonly TrackedValue<float> _colorMultiplier = new(JsonDefaults.ColorBlock.ColorMultiplier);
    private readonly TrackedValue<float> _fadeDuration = new(JsonDefaults.ColorBlock.FadeDuration);
    
    public UiColor HighlightedColor { get => _highlightColor.Value; set => _highlightColor.Value = value; }
    public UiColor PressedColor { get => _pressedColor.Value; set => _pressedColor.Value = value; }
    public UiColor SelectedColor { get => _selectedColor.Value; set => _selectedColor.Value = value; }
    public float ColorMultiplier { get => _colorMultiplier.Value; set => _colorMultiplier.Value = value; }
    public float FadeDuration { get => _fadeDuration.Value; set => _fadeDuration.Value = value; }
    
    public override ComponentType ComponentType => ComponentType.ColorBlock;

    public static readonly UiColor DefaultHighlightedColor = JsonDefaults.ColorBlock.HighlightedColor;
    public static readonly UiColor DefaultPressedColor = JsonDefaults.ColorBlock.PressedColor;
    public static readonly UiColor DefaultSelectedColor = JsonDefaults.ColorBlock.SelectedColor;

    public override void WriteComponent(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.ColorBlock.HighlightedColorName, _highlightColor, mode);
        writer.AddField(JsonDefaults.ColorBlock.PressedColorName, _pressedColor, mode);
        writer.AddField(JsonDefaults.ColorBlock.SelectedColorName, _selectedColor, mode);
        writer.AddField(JsonDefaults.ColorBlock.ColorMultiplierName, _colorMultiplier, mode);
        writer.AddField(JsonDefaults.ColorBlock.FadeDurationName, _fadeDuration, mode);
    }

    public override void Reset() 
    {
        _highlightColor.Reset();
        _pressedColor.Reset();
        _selectedColor.Reset();
        _colorMultiplier.Reset();
        _fadeDuration.Reset();
    }

    public override void CopyFrom(object value)
    {
        if (value is ColorBlockComponent component)
        {
            HighlightedColor = component.HighlightedColor;
            PressedColor = component.PressedColor;
            SelectedColor = component.SelectedColor;
            ColorMultiplier = component.ColorMultiplier;
            FadeDuration = component.FadeDuration;
        }
    }
    
    public override bool AreEquivalent(BaseComponent other)
    {
        if (!base.AreEquivalent(other)) return false;
        ColorBlockComponent typedOther = (ColorBlockComponent)other!;
        return HighlightedColor == typedOther.HighlightedColor 
               && PressedColor == typedOther.PressedColor 
               && SelectedColor == typedOther.SelectedColor 
               && ColorMultiplier == typedOther.ColorMultiplier 
               && FadeDuration == typedOther.FadeDuration;
    }
}