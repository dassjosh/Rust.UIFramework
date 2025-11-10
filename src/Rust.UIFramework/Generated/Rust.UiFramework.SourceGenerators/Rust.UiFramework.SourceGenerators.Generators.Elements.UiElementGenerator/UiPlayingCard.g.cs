using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;
public partial class UiPlayingCard : IUiPlayingCardTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.UiSuit> _suit = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.UiRank> _rank = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.UiCardType> _cardType = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _material = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _fadeIn = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _color = new();

	public Oxide.Ext.UiFramework.Enums.UiSuit Suit { get => Card.Suit; set => Card.Suit = value; }
	public Oxide.Ext.UiFramework.Enums.UiRank Rank { get => Card.Rank; set => Card.Rank = value; }
	public Oxide.Ext.UiFramework.Enums.UiCardType CardType { get => Card.CardType; set => Card.CardType = value; }
	public string Material { get => Card.Material; set => Card.Material = value; }
	public float FadeIn { get => Card.FadeIn; set => Card.FadeIn = value; }
	public Oxide.Ext.UiFramework.Colors.UiColor Color { get => Card.Color; set => Card.Color = value; }
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


