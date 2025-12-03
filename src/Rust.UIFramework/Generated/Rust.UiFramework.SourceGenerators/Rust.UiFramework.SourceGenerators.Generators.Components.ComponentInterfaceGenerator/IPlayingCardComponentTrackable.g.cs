using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IPlayingCardComponentTrackable : IBaseTypedComponentTrackable
{
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.UiSuit> Suit { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.UiRank> Rank { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.UiCardType> CardType { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> FadeIn { get; }
	Oxide.Ext.UiFramework.Types.Tracked<string> Material { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> Color { get; }
}


