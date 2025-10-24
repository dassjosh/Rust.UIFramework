using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IUiLabel : IUiText, IFadeIn<UiLabel>, IUiColor<UiLabel>
{
    UiReference PlaceholderFor { get; set; }
}