using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(PlayingCardComponentSerializer))]
public class PlayingCardComponent : CoreComponent, IGraphicalComponent
{
    public UiSuit Suit;
    public UiRank Rank;
    public UiCardType CardType;
    public float FadeIn { get; set; }
    public string Material;
    public UiColor Color;

    public override Utf8String Type => UiPlayingCards.GetComponentType(Rank, CardType);
    public override ComponentType ComponentType => ComponentType.PlayingCard;
    
    public override void Reset()
    {
        base.Reset();
        Suit = default;
        Rank = default;
        CardType = default;
        FadeIn = default;
        Material = UiMaterials.Content.Ui.NameFontMaterial;
        Color = UiColors.White;
    }

    public override void CopyFrom(object value)
    {
        base.CopyFrom(value);
        if (value is PlayingCardComponent component)
        {
            Suit = component.Suit;
            Rank = component.Rank;
            CardType = component.CardType;
            FadeIn = component.FadeIn;
            Material = component.Material;
            Color = component.Color;
        }
    }
    
    public override bool Equals(BaseComponent other)
    {
        if (!base.Equals(other)) return false;
        PlayingCardComponent typedOther = (PlayingCardComponent)other!;
        return Suit == typedOther.Suit 
               && Rank == typedOther.Rank 
               && CardType == typedOther.CardType 
               && FadeIn == typedOther.FadeIn 
               && Material == typedOther.Material 
               && Color == typedOther.Color;
    }
}