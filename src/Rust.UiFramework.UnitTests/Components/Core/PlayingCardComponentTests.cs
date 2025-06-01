using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Rust.UiFramework.UnitTests.Global.Generators;

namespace Rust.UiFramework.UnitTests.Components.Core;

public class PlayingCardComponentTests() : BaseTheoryComponentTests<PlayingCardComponent, PlayingCardComponentTests.TheoryRow>(ComponentHelpers.PopulatePlayingCard)
{
    public record TheoryRow(UiSuit Suit, UiRank Rank, UiCardType CardType);
    
    protected override void PopulateTheory(PlayingCardComponent component, TheoryRow row)
    {
        component.Suit = row.Suit;
        component.Rank = row.Rank;
        component.CardType = row.CardType;
    }

    public static TheoryData<TheoryRow> TheoryData
        => TheoryDataGenerator.Generate<TheoryRow, UiSuit, UiRank, UiCardType>((suit, rank, type) => new TheoryRow(suit, rank, type), null, [UiRank.Ace, UiRank.Back, UiRank.Joker, UiRank.Two]);
}