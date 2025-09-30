using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(ColorBlockComponentSerializer))]
public class ColorBlockComponent : ChildComponent
{
    public UiColor HighlightedColor;
    public UiColor PressedColor;
    public UiColor SelectedColor;
    public float ColorMultiplier;
    public float FadeDuration;
    
    public override ComponentType ComponentType => ComponentType.ColorBlock;

    public static readonly UiColor DefaultHighlightedColor = JsonDefaults.ColorBlock.HighlightedColor;
    public static readonly UiColor DefaultPressedColor = JsonDefaults.ColorBlock.PressedColor;
    public static readonly UiColor DefaultSelectedColor = JsonDefaults.ColorBlock.SelectedColor;

    public override void Reset() 
    {
        HighlightedColor = JsonDefaults.ColorBlock.HighlightedColor;
        PressedColor = JsonDefaults.ColorBlock.PressedColor;
        SelectedColor = JsonDefaults.ColorBlock.SelectedColor;
        ColorMultiplier = JsonDefaults.ColorBlock.ColorMultiplier;
        FadeDuration = JsonDefaults.ColorBlock.FadeDuration;
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
    
    public override bool Equals(BaseComponent other)
    {
        if (!base.Equals(other)) return false;
        ColorBlockComponent typedOther = (ColorBlockComponent)other!;
        return HighlightedColor == typedOther.HighlightedColor 
               && PressedColor == typedOther.PressedColor 
               && SelectedColor == typedOther.SelectedColor 
               && ColorMultiplier == typedOther.ColorMultiplier 
               && FadeDuration == typedOther.FadeDuration;
    }
}