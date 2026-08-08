using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement]
[GenerateBuilderMethods]
public partial class UiPlayingCard : BaseUiComponent, IMaterial<UiPlayingCard>, IFadeIn<UiPlayingCard>, IUiColor<UiPlayingCard>
{
    public partial UiSuit Suit { get; set; }
    public partial UiRank Rank { get; set; }
    public partial UiCardType CardType { get; set; }
    public partial string Material { get; set; }
    public partial float FadeIn { get; set; }
    public partial UiColor Color { get; set; }
    public partial bool AllowRaycast { get; set; }
    
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