using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class PlayingCardComponent : CoreComponent
{
    public UiSuit Suit;
    public UiRank Rank;
    public UiCardType CardType;
    public float FadeIn;
    public string Material;
    public UiColor Color;

    public override Utf8String Type => UiPlayingCards.GetComponentType(Rank, CardType);

    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        writer.AddField(JsonDefaults.BaseImage.SpriteName, UiPlayingCards.GetPlayingCard(Suit, Rank, CardType), JsonDefaults.BaseImage.Sprite);
        writer.AddField(JsonDefaults.BaseImage.MaterialName, Material, JsonDefaults.BaseImage.Material);
        writer.AddField(JsonDefaults.Common.FadeInName, FadeIn, JsonDefaults.Common.FadeIn);
        writer.AddField(JsonDefaults.Color.ColorName, Color);
    }
    
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
}