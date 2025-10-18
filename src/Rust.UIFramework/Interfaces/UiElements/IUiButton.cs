using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IUiButton : IImageType<UiButton>, ISprite<UiButton>, IMaterial<UiButton>, IFadeIn<UiButton>, IUiColor<UiButton>, IBaseUiComponent
{
    [SkipBuilder]
    string Command { get; set; }
    ButtonType ButtonType { get; set; }
    
    [PropertyTarget(nameof(UiButton.GetOrAddColorBlock), PropertyTargetType.Method)]
    UiColor HighlightedColor { get; set; }
    
    [PropertyTarget(nameof(UiButton.GetOrAddColorBlock), PropertyTargetType.Method)]
    UiColor PressedColor { get; set; }
    
    [PropertyTarget(nameof(UiButton.GetOrAddColorBlock), PropertyTargetType.Method)]
    UiColor SelectedColor { get; set; }
    
    [PropertyTarget(nameof(UiButton.GetOrAddColorBlock), PropertyTargetType.Method)]
    float ColorMultiplier { get; set; }
    
    [PropertyTarget(nameof(UiButton.GetOrAddColorBlock), PropertyTargetType.Method)]
    float FadeDuration { get; set; }
}