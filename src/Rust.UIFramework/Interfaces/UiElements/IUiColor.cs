using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IUiColor
{
    UiColor Color { get; set; }
}

public interface IUiColor<out T> : IUiColor where T : BaseUiComponent
{
    T SetColor(UiColor color);
}