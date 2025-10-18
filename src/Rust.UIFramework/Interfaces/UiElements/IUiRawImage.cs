using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IUiRawImage : IMaterial<UiRawImage>, IFadeIn<UiRawImage>, IUiColor<UiRawImage>, IBaseUiComponent
{
    string Image { get; set; }
    UiReference PlaceholderFor { get; set; }
}