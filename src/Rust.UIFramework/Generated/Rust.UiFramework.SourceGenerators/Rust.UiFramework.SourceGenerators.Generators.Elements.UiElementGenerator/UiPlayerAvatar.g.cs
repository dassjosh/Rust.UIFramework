using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;

public partial class UiPlayerAvatar : IUiPlayerAvatar, IUiPlayerAvatarTrackable
{
	public partial ulong SteamId { get => Avatar.SteamId; set => Avatar.SteamId = value; }
	public partial Oxide.Ext.UiFramework.Enums.AvatarType AvatarType { get => Avatar.AvatarType; set => Avatar.AvatarType = value; }
	public partial string Material { get => Avatar.Material; set => Avatar.Material = value; }
	public partial float FadeIn { get => Avatar.FadeIn; set => Avatar.FadeIn = value; }
	public partial Oxide.Ext.UiFramework.Colors.UiColor Color { get => Avatar.Color; set => Avatar.Color = value; }
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


