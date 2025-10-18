using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IUiPlayingCard : IMaterial<UiPlayingCard>, IFadeIn<UiPlayingCard>, IUiColor<UiPlayingCard>, IBaseUiComponent
{
    UiSuit Suit { get; set; }
    UiRank Rank { get; set; }
    UiCardType CardType { get; set; }
}