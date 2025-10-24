using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IUiNineSlice : IUiBaseImage, IImageType<UiNineSlice>, ISprite<UiNineSlice>, IMaterial<UiNineSlice>, IFadeIn<UiNineSlice>, IUiColor<UiNineSlice>
{
    string Png { get; set; }
    UiBorderWidth Slice { get; set; }
}