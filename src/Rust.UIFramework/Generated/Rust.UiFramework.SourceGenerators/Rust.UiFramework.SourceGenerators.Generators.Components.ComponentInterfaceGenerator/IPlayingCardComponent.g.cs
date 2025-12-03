using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IPlayingCardComponent : IBaseTypedComponent
{
	Oxide.Ext.UiFramework.Enums.UiSuit Suit { get; set; }
	Oxide.Ext.UiFramework.Enums.UiRank Rank { get; set; }
	Oxide.Ext.UiFramework.Enums.UiCardType CardType { get; set; }
	float FadeIn { get; set; }
	string Material { get; set; }
	Oxide.Ext.UiFramework.Colors.UiColor Color { get; set; }

	Oxide.Ext.UiFramework.Components.PlayingCardComponent SetSuit(Oxide.Ext.UiFramework.Enums.UiSuit suit);
	Oxide.Ext.UiFramework.Components.PlayingCardComponent SetRank(Oxide.Ext.UiFramework.Enums.UiRank rank);
	Oxide.Ext.UiFramework.Components.PlayingCardComponent SetCardType(Oxide.Ext.UiFramework.Enums.UiCardType cardType);
	Oxide.Ext.UiFramework.Components.PlayingCardComponent SetFadeIn(float fadeIn);
	Oxide.Ext.UiFramework.Components.PlayingCardComponent SetMaterial(string material);
	Oxide.Ext.UiFramework.Components.PlayingCardComponent SetColor(Oxide.Ext.UiFramework.Colors.UiColor color);
}


