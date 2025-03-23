using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IUiColor
{
    void SetColor(UiColor color);
    UiColor GetColor();
}

public interface IUiColor<out T> : IUiColor where T : BaseUiComponent
{
    new T SetColor(UiColor color);
}