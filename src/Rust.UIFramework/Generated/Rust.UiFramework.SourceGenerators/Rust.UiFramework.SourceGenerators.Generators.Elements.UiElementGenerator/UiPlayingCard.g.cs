using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;

public partial class UiPlayingCard : IUiPlayingCard, IUiPlayingCardTrackable
{
	public partial Oxide.Ext.UiFramework.Enums.UiSuit Suit { get => Card.Suit; set => Card.Suit = value; }
	public partial Oxide.Ext.UiFramework.Enums.UiRank Rank { get => Card.Rank; set => Card.Rank = value; }
	public partial Oxide.Ext.UiFramework.Enums.UiCardType CardType { get => Card.CardType; set => Card.CardType = value; }
	public partial string Material { get => Card.Material; set => Card.Material = value; }
	public partial float FadeIn { get => Card.FadeIn; set => Card.FadeIn = value; }
	public partial Oxide.Ext.UiFramework.Colors.UiColor Color { get => Card.Color; set => Card.Color = value; }
	IPlayingCardComponentTrackable IUiPlayingCardTrackable.Card => Card.AsTrackable();

	public IUiPlayingCardTrackable AsTrackable() => this;
	public UiPlayingCard SetSuit(Oxide.Ext.UiFramework.Enums.UiSuit suit)
	{
		Suit = suit;
		return this;
	}
	public UiPlayingCard SetRank(Oxide.Ext.UiFramework.Enums.UiRank rank)
	{
		Rank = rank;
		return this;
	}
	public UiPlayingCard SetCardType(Oxide.Ext.UiFramework.Enums.UiCardType cardType)
	{
		CardType = cardType;
		return this;
	}
	public UiPlayingCard SetMaterial(string material)
	{
		Material = material;
		return this;
	}
	public UiPlayingCard SetFadeIn(float fadeIn)
	{
		FadeIn = fadeIn;
		return this;
	}
	public UiPlayingCard SetColor(Oxide.Ext.UiFramework.Colors.UiColor color)
	{
		Color = color;
		return this;
	}
}


