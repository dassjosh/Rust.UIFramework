using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.UiElements;

namespace Rust.UiFramework.UnitTests.UiElements;

public class UiPlayingCardTests() : BasePopulateUiElementsTests<UiPlayingCard>(PopulateFluent, PopulateSetters)
{
    private static readonly PlayingCardComponent Card = new()
    {
        Color = UiColors.Gray,
        FadeIn = 1.5f,
        Material = UiMaterials.Icons.IconMaterial,
        Suit = UiSuit.Diamonds,
        Rank = UiRank.Two,
        CardType = UiCardType.SmallTransparent
    };

    private static void PopulateFluent(UiPlayingCard card)
    {
        card
            .SetColor(Card.Color)
            .SetFadeIn(Card.FadeIn)
            .SetMaterial(Card.Material)
            .SetSuit(Card.Suit)
            .SetRank(Card.Rank)
            .SetCardType(Card.CardType);
    }

    private static void PopulateSetters(UiPlayingCard card)
    {
        card.Color = Card.Color;
        card.FadeIn = Card.FadeIn;
        card.Material = Card.Material;
        card.Suit = Card.Suit;
        card.Rank = Card.Rank;
        card.CardType = Card.CardType;
    }

    protected override void AssertValues(UiPlayingCard element)
    {
        element.Color.Should().Be(Card.Color);
        element.FadeIn.Should().Be(Card.FadeIn);
        element.Material.Should().Be(Card.Material);
        element.Suit.Should().Be(Card.Suit);
        element.Rank.Should().Be(Card.Rank);
        element.CardType.Should().Be(Card.CardType);
    }
}