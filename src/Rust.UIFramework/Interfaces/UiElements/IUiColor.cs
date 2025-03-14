using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IUiColor
{
    void SetColor(UiColor color);
}

public interface IUiColor<out T> : IUiColor where T : BaseUiComponent
{
    T SetColor(UiColor color);
}