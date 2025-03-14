using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IFadeIn
{
    void SetFadeIn(float duration);
}

public interface IFadeIn<out T> : IFadeIn where T : BaseUiComponent
{
    T SetFadeIn(float duration);
}