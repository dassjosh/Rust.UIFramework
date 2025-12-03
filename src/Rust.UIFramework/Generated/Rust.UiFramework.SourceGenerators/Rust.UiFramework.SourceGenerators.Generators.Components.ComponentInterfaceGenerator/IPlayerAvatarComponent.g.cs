using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IPlayerAvatarComponent : IRawImageComponent
{
	ulong SteamId { get; set; }
	Oxide.Ext.UiFramework.Enums.AvatarType AvatarType { get; set; }

	Oxide.Ext.UiFramework.Components.PlayerAvatarComponent SetSteamId(ulong steamId);
	Oxide.Ext.UiFramework.Components.PlayerAvatarComponent SetAvatarType(Oxide.Ext.UiFramework.Enums.AvatarType avatarType);
}


