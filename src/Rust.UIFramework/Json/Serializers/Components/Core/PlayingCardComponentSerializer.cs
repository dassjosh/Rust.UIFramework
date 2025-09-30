using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public class PlayingCardComponentSerializer : CoreComponentSerializer<PlayingCardComponent>
{
    protected override void SerializeComponent(JsonFrameworkWriter writer, PlayingCardComponent component, PlayingCardComponent defaults, SerializeMode mode)
    {
        if (mode == SerializeMode.Create)
        {
            writer.AddField(JsonDefaults.BaseImage.SpriteName, UiPlayingCards.GetPlayingCard(component.Suit, component.Rank, component.CardType), JsonDefaults.BaseImage.Sprite);
        }
        else
        {
            writer.AddField(JsonDefaults.BaseImage.SpriteName, UiPlayingCards.GetPlayingCard(component.Suit, component.Rank, component.CardType), UiPlayingCards.GetPlayingCard(defaults.Suit, defaults.Rank, defaults.CardType));
        }
        writer.AddField(JsonDefaults.BaseImage.MaterialName, component.Material, defaults.Material);
        writer.AddField(JsonDefaults.Color.ColorName, component.Color, defaults.Color);
    }
}