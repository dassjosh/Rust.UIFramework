using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IUiLabel : IUiBaseText, IFadeIn<UiLabel>, IUiColor<UiLabel>
{
    UiReference PlaceholderFor { get; set; }
}