using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Interfaces;

[IncludeInParent]
public interface IFadeIn
{
    float FadeIn { get; set; }
}

public interface IFadeIn<out T> : IFadeIn where T : BaseUiComponent
{
    T SetFadeIn(float duration);
}