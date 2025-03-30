using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiPlayingCard : BaseUiComponent, IMaterial<UiPlayingCard>, IFadeIn<UiPlayingCard>, IUiColor<UiPlayingCard>
{
    public readonly PlayingCardComponent Card = new();

    internal override CoreComponent Component => Card;
    
    public string Material { get => Card.Material; set => Card.Material = value; }
    public float FadeIn { get => Card.FadeIn; set => Card.FadeIn = value; }
    public UiColor Color { get => Card.Color; set => Card.Color = value; }
    
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
        Card.Material = material;
        return this;
    }
    
    public UiPlayingCard SetColor(UiColor color)
    {
        Card.Color = color;
        return this;
    }
        
    public UiPlayingCard SetFadeIn(float duration)
    {
        Card.FadeIn = duration;
        return this;
    }
}