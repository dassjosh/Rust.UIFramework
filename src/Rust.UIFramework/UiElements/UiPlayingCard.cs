using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiPlayingCard : BaseUiComponent, IMaterial<UiPlayingCard>, IFadeIn<UiPlayingCard>, IUiColor<UiPlayingCard>
{
    public readonly PlayingCardComponent Card;

    public string Material { get => Card.Material; set => Card.Material = value; }
    public float FadeIn { get => Card.FadeIn; set => Card.FadeIn = value; }
    public UiColor Color { get => Card.Color; set => Card.Color = value; }
    
    public UiSuit Suit { get => Card.Suit; set => Card.Suit = value; }
    public UiRank Rank { get => Card.Rank; set => Card.Rank = value; }
    public UiCardType CardType { get => Card.CardType; set => Card.CardType = value; }
    
    public UiPlayingCard() : this(new PlayingCardComponent()) { }

    private UiPlayingCard(PlayingCardComponent component) : base(component)
    {
        Card = component;
    }
    
    public static UiPlayingCard Create(PlayingCardData card, UiCardType type, UiColor color)
    {
        UiPlayingCard image = CreateBase<UiPlayingCard>();
        image.Card.Color = color;
        image.Card.Rank = card.Rank;
        image.Card.Suit = card.Suit;
        image.Card.CardType = type;
        return image;
    }
    
    public UiPlayingCard SetMaterial(string material)
    {
        Material = material;
        return this;
    }
    
    public UiPlayingCard SetColor(UiColor color)
    {
        Color = color;
        return this;
    }
        
    public UiPlayingCard SetFadeIn(float duration)
    {
        FadeIn = duration;
        return this;
    }
    
    public UiPlayingCard SetSuit(UiSuit suit)
    {
        Suit = suit;
        return this;
    }
    
    public UiPlayingCard SetRank(UiRank rank)
    {
        Rank = rank;
        return this;
    }
    
    public UiPlayingCard SetCardType(UiCardType cardType)
    {
        CardType = cardType;
        return this;
    }
}