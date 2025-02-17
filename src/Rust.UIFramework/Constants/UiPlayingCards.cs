using System;
using System.Collections.Generic;
using Facepunch.CardGames;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Constants;

public static class UiPlayingCards
{
    private static readonly Dictionary<Card, string> Cards = new();
    
    public static string GetPlayingCard(Suit suit, Rank rank, PlayingCardType type)
    {
        Card card = new(suit, rank, type);
        if (!Cards.TryGetValue(card, out string assetPath))
        {
            Cards[card] = assetPath = GetCardAssetPath(card);
        }

        return assetPath;
    }

    private static string GetCardAssetPath(Card card)
    {
        return string.Format(GetCardFormat(card.Type), EnumCache<Suit>.ToLower(card.Suit), GetRankName(card.Rank));
    }

    private static string GetCardFormat(PlayingCardType type)
    {
        return type switch
        {
            PlayingCardType.Normal => "assets/content/ui/gameui/cardgames/deck/{0}/{1}_{0}.png",
            PlayingCardType.Small => "assets/content/ui/gameui/cardgames/deck_small/{0}/{1}_small_{0}.png",
            PlayingCardType.Transparent => "assets/content/ui/gameui/cardgames/deck_transparent/{0}/{1}_transparent_{0}.png",
            PlayingCardType.Transparent | PlayingCardType.Small => "assets/content/ui/gameui/cardgames/deck_small_world_transparent/{0}/{1}_{0}.png",
            _ => null
        };
    }

    private static string GetRankName(Rank rank)
    {
        return rank switch
        {
            Rank.Two => "2",
            Rank.Three => "3",
            Rank.Four => "4",
            Rank.Five => "5",
            Rank.Six => "6",
            Rank.Seven => "7",
            Rank.Eight => "8",
            Rank.Nine => "9",
            Rank.Ten => "10",
            Rank.Jack => "jack",
            Rank.Queen => "queen",
            Rank.King => "king",
            Rank.Ace => "ace",
            _ => throw new ArgumentOutOfRangeException(nameof(rank), rank, null)
        };
    }

    private readonly record struct Card(Suit Suit, Rank Rank, PlayingCardType Type);
}