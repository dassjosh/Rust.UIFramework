using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class PlayingCardComponent : CoreComponent, IGraphicalComponent
{
    private readonly TrackedValue<UiSuit> _suit = new();
    private readonly TrackedValue<UiRank> _rank = new();
    private readonly TrackedValue<UiCardType> _cardType = new();
    private readonly TrackedValue<float> _fadeIn = new();
    private readonly TrackedValue<string> _material = new(UiMaterials.Content.Ui.NameFontMaterial);
    private readonly TrackedValue<UiColor> _color = new(UiColors.White);
    
    public UiSuit Suit { get => _suit.Value; set => _suit.Value = value; }
    public UiRank Rank { get => _rank.Value; set => _rank.Value = value; }
    public UiCardType CardType { get => _cardType.Value; set => _cardType.Value = value; }
    public float FadeIn { get => _fadeIn.Value; set => _fadeIn.Value = value; }
    public string Material { get => _material.Value; set => _material.Value = value; }
    public UiColor Color { get => _color.Value; set => _color.Value = value; }

    public override Utf8String Type => UiPlayingCards.GetComponentType(Rank, CardType);
    public override ComponentType ComponentType => ComponentType.PlayingCard;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        if (_suit.ShouldSerialize(mode) || _rank.ShouldSerialize(mode) || _cardType.ShouldSerialize(mode))
        {
            writer.AddFieldRaw(JsonDefaults.BaseImage.SpriteName, UiPlayingCards.GetPlayingCard(Suit, Rank, CardType));
        }
        writer.AddField(JsonDefaults.BaseImage.MaterialName, _material, mode);
        writer.AddField(JsonDefaults.Color.ColorName, _color, mode);
        writer.AddField(JsonDefaults.Common.FadeInName, _fadeIn, mode);
    }

    public override void Reset()
    {
        base.Reset();
        _suit.Reset();
        _rank.Reset();
        _cardType.Reset();
        _fadeIn.Reset();
        _material.Reset();
        _color.Reset();
    }
}