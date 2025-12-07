using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class PlayingCardComponent : CoreComponent, IGraphicalComponent
{
    public partial UiSuit Suit { get; set; }
    public partial UiRank Rank { get; set; }
    public partial UiCardType CardType { get; set; }
    public partial float FadeIn { get; set; }
    [TrackedDefaults(typeof(UiMaterials.Content.Ui), nameof(UiMaterials.Content.Ui.NameFontMaterial))]
    public partial string Material { get; set; }
    [TrackedDefaults(typeof(UiColors), nameof(UiColors.White))]
    public partial UiColor Color { get; set; }
    
    public override Utf8String Type => UiPlayingCards.GetComponentType(Rank, CardType);
    public override ComponentType ComponentType => ComponentType.PlayingCard;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        if (_suit.ShouldSerialize(mode) || _rank.ShouldSerialize(mode) || _cardType.ShouldSerialize(mode))
        {
            writer.AddField(JsonDefaults.BaseImage.SpriteName, UiPlayingCards.GetPlayingCard(Suit, Rank, CardType));
        }
        writer.AddField(JsonDefaults.BaseImage.MaterialName, _material, mode);
        writer.AddField(JsonDefaults.Color.ColorName, _color, mode);
        writer.AddField(JsonDefaults.Common.FadeInName, _fadeIn, mode);
    }
}