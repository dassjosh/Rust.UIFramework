using Facepunch.CardGames;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Types;

public readonly record struct PlayingCardData(UiSuit Suit, UiRank Rank)
{
    public PlayingCardData(Suit suit, Rank rank) : this((UiSuit)suit, GetUiRank(rank)) {}

    public static implicit operator PlayingCardData(PlayingCard card) => new(card.Suit, card.Rank);

    private static UiRank GetUiRank(Rank rank)
    {
        return rank switch
        {
            Facepunch.CardGames.Rank.Ace => UiRank.Ace,
            Facepunch.CardGames.Rank.Jack => UiRank.Jack,
            Facepunch.CardGames.Rank.Queen => UiRank.Queen,
            Facepunch.CardGames.Rank.King => UiRank.King,
            Facepunch.CardGames.Rank.Ten => UiRank.Ten,
            Facepunch.CardGames.Rank.Nine => UiRank.Nine,
            Facepunch.CardGames.Rank.Eight => UiRank.Eight,
            Facepunch.CardGames.Rank.Seven => UiRank.Seven,
            Facepunch.CardGames.Rank.Six => UiRank.Six,
            Facepunch.CardGames.Rank.Five => UiRank.Five,
            Facepunch.CardGames.Rank.Four => UiRank.Four,
            Facepunch.CardGames.Rank.Three => UiRank.Three,
            Facepunch.CardGames.Rank.Two => UiRank.Two,
            _ => UiRank.Joker
        };
    }
}