using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IPlayingCardComponent
{
    UiSuit Suit { get; set; }
    UiRank Rank { get; set; }
    UiCardType CardType { get; set; }
    float FadeIn { get; set; }
    [TrackedDefaults(typeof(UiMaterials.Content.Ui), nameof(UiMaterials.Content.Ui.NameFontMaterial))]
    string Material { get; set; }
    [TrackedDefaults(typeof(UiColors), nameof(UiColors.White))]
    UiColor Color { get; set; }
}