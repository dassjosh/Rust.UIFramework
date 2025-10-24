using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement(typeof(IUiPlayingCard))]
public partial class UiPlayingCard : BaseUiComponent, IUiPlayingCard
{
    public readonly PlayingCardComponent Card;
    
    public UiPlayingCard() : this(new PlayingCardComponent()) { }

    private UiPlayingCard(PlayingCardComponent component) : base(component)
    {
        Card = component;
    }
    
    public UiPlayingCard Init(PlayingCardData card, UiCardType type, UiColor color)
    {
        Color = color;
        Rank = card.Rank;
        Suit = card.Suit;
        CardType = type;
        return this;
    }
}