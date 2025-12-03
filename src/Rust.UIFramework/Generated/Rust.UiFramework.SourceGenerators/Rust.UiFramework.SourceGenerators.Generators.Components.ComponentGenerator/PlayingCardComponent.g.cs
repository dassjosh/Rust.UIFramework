using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;
public partial class PlayingCardComponent : IPlayingCardComponent, IPlayingCardComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.UiSuit> _suit = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.UiRank> _rank = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.UiCardType> _cardType = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _fadeIn = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _material = new(Oxide.Ext.UiFramework.Constants.UiMaterials.Content.Ui.NameFontMaterial);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _color = new(Oxide.Ext.UiFramework.Colors.UiColors.White);

	public partial Oxide.Ext.UiFramework.Enums.UiSuit Suit { get => _suit.Value; set => _suit.Value = value; }
	public partial Oxide.Ext.UiFramework.Enums.UiRank Rank { get => _rank.Value; set => _rank.Value = value; }
	public partial Oxide.Ext.UiFramework.Enums.UiCardType CardType { get => _cardType.Value; set => _cardType.Value = value; }
	public partial float FadeIn { get => _fadeIn.Value; set => _fadeIn.Value = value; }
	public partial string Material { get => _material.Value; set => _material.Value = value; }
	public partial Oxide.Ext.UiFramework.Colors.UiColor Color { get => _color.Value; set => _color.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.UiSuit> IPlayingCardComponentTrackable.Suit => _suit;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.UiRank> IPlayingCardComponentTrackable.Rank => _rank;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.UiCardType> IPlayingCardComponentTrackable.CardType => _cardType;
	Oxide.Ext.UiFramework.Types.Tracked<float> IPlayingCardComponentTrackable.FadeIn => _fadeIn;
	Oxide.Ext.UiFramework.Types.Tracked<string> IPlayingCardComponentTrackable.Material => _material;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> IPlayingCardComponentTrackable.Color => _color;

	public Oxide.Ext.UiFramework.Components.PlayingCardComponent SetSuit(Oxide.Ext.UiFramework.Enums.UiSuit suit)
	{
		Suit = suit;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.PlayingCardComponent SetRank(Oxide.Ext.UiFramework.Enums.UiRank rank)
	{
		Rank = rank;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.PlayingCardComponent SetCardType(Oxide.Ext.UiFramework.Enums.UiCardType cardType)
	{
		CardType = cardType;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.PlayingCardComponent SetFadeIn(float fadeIn)
	{
		FadeIn = fadeIn;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.PlayingCardComponent SetMaterial(string material)
	{
		Material = material;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.PlayingCardComponent SetColor(Oxide.Ext.UiFramework.Colors.UiColor color)
	{
		Color = color;
		return this;
	}
	public IPlayingCardComponentTrackable AsTrackable() => this;
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_suit.HasChanged || _rank.HasChanged || _cardType.HasChanged || _fadeIn.HasChanged || _material.HasChanged || _color.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_suit.ResetHasChanged();
		_rank.ResetHasChanged();
		_cardType.ResetHasChanged();
		_fadeIn.ResetHasChanged();
		_material.ResetHasChanged();
		_color.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_suit.Reset();
		_rank.Reset();
		_cardType.Reset();
		_fadeIn.Reset();
		_material.Reset();
		_color.Reset();
	}
}


