using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;
public partial class UiPlayerAvatar : IUiPlayerAvatarTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<ulong> _steamId = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.AvatarType> _avatarType = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _material = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _fadeIn = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _color = new();

	public ulong SteamId { get => Avatar.SteamId; set => Avatar.SteamId = value; }
	public Oxide.Ext.UiFramework.Enums.AvatarType AvatarType { get => Avatar.AvatarType; set => Avatar.AvatarType = value; }
	public string Material { get => Avatar.Material; set => Avatar.Material = value; }
	public float FadeIn { get => Avatar.FadeIn; set => Avatar.FadeIn = value; }
	public Oxide.Ext.UiFramework.Colors.UiColor Color { get => Avatar.Color; set => Avatar.Color = value; }
	IPlayerAvatarComponentTrackable IUiPlayerAvatarTrackable.Avatar => Avatar.AsTrackable();

	public IUiPlayerAvatarTrackable AsTrackable() => this;
	public UiPlayerAvatar SetSteamId(ulong steamId)
	{
		SteamId = steamId;
		return this;
	}
	public UiPlayerAvatar SetAvatarType(Oxide.Ext.UiFramework.Enums.AvatarType avatarType)
	{
		AvatarType = avatarType;
		return this;
	}
	public UiPlayerAvatar SetMaterial(string material)
	{
		Material = material;
		return this;
	}
	public UiPlayerAvatar SetFadeIn(float fadeIn)
	{
		FadeIn = fadeIn;
		return this;
	}
	public UiPlayerAvatar SetColor(Oxide.Ext.UiFramework.Colors.UiColor color)
	{
		Color = color;
		return this;
	}
}


