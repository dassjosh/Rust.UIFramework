using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IUiItemIcon : IMaterial<UiItemIcon>, IFadeIn<UiItemIcon>, IUiColor<UiItemIcon>, IImageType<UiItemIcon>, IBaseUiComponent
{
    int ItemId { get; set; }
    ulong SkinId { get; set; }
}