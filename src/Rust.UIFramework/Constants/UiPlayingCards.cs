using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Constants;

public static class UiPlayingCards
{
    private static readonly Dictionary<Card, string> Cards = new();
    
    public static string GetPlayingCard(UiSuit suit, UiRank rank, UiCardType type)
    {
        Card card = new(suit, rank, type);
        if (!Cards.TryGetValue(card, out string assetPath))
        {
            Cards[card] = assetPath = GetCardAssetPath(card);
        }

        return assetPath;
    }
    
    internal static Utf8String GetComponentType(UiRank rank, UiCardType cardType) => rank == UiRank.Joker && cardType == UiCardType.Normal ? JsonDefaults.RawImage.Type : JsonDefaults.Image.Type;

    private static string GetCardAssetPath(in Card card)
    {
        return card.Rank switch
        {
            UiRank.Joker => GetJokerPath(card),
            UiRank.Back => GetBackPath(card),
            _ => string.Format(GetCardFormat(card.Type), EnumCache<UiSuit>.ToLower(card.Suit), GetRankName(card.Rank))
        };
    }

    private static string GetJokerPath(in Card card)
    {
        if (card.Type.HasFlag(UiCardType.Small))
        {
            return UiSprites.Content.Ui.GameUi.CardGames.DeckSmall.JokerSmall;
        }
        
        return card.Suit switch
        {
            UiSuit.Hearts or UiSuit.Diamonds => UiTextures.Content.Ui.GameUi.CardGames.Deck.Jokers.JokerRed,
            UiSuit.Spades or UiSuit.Clubs => UiTextures.Content.Ui.GameUi.CardGames.Deck.Jokers.JokerBlack,
            _ => throw new ArgumentOutOfRangeException(nameof(card.Suit), card.Suit, null)
        };
    }
    
    private static string GetBackPath(in Card card)
    {
        return card.Type.HasFlag(UiCardType.Small) ? UiSprites.Content.Ui.GameUi.CardGames.DeckSmall.BackSmall : UiSprites.Content.Ui.GameUi.CardGames.Deck.Back;
    }

    private static string GetCardFormat(UiCardType type)
    {
        return type switch
        {
            UiCardType.Normal => "assets/content/ui/gameui/cardgames/deck/{0}/{1}_{0}.png",
            UiCardType.Small => "assets/content/ui/gameui/cardgames/deck_small/{0}/{1}_small_{0}.png",
            UiCardType.Transparent => "assets/content/ui/gameui/cardgames/deck_transparent/{0}/{1}_transparent_{0}.png",
            UiCardType.SmallTransparent => "assets/content/ui/gameui/cardgames/deck_small_world_transparent/{0}/{1}_{0}.png",
            _ => null
        };
    }

    private static string GetRankName(UiRank rank)
    {
        return rank switch
        {
            UiRank.Two => "2",
            UiRank.Three => "3",
            UiRank.Four => "4",
            UiRank.Five => "5",
            UiRank.Six => "6",
            UiRank.Seven => "7",
            UiRank.Eight => "8",
            UiRank.Nine => "9",
            UiRank.Ten => "10",
            UiRank.Jack => "jack",
            UiRank.Queen => "queen",
            UiRank.King => "king",
            UiRank.Ace => "ace",
            _ => throw new ArgumentOutOfRangeException(nameof(rank), rank, null)
        };
    }
    
    private readonly record struct Card(UiSuit Suit, UiRank Rank, UiCardType Type);
}