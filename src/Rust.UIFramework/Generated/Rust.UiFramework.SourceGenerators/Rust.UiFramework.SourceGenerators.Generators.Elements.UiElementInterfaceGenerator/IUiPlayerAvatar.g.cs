using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IUiPlayerAvatar : Oxide.Ext.UiFramework.Interfaces.IMaterial<Oxide.Ext.UiFramework.UiElements.UiPlayerAvatar>, Oxide.Ext.UiFramework.Interfaces.IFadeIn<Oxide.Ext.UiFramework.UiElements.UiPlayerAvatar>, Oxide.Ext.UiFramework.Interfaces.IUiColor<Oxide.Ext.UiFramework.UiElements.UiPlayerAvatar>, IBaseUiComponent
{
	ulong SteamId { get; }
	Oxide.Ext.UiFramework.Enums.AvatarType AvatarType { get; }

	Oxide.Ext.UiFramework.UiElements.UiPlayerAvatar SetSteamId(ulong steamId);
	Oxide.Ext.UiFramework.UiElements.UiPlayerAvatar SetAvatarType(Oxide.Ext.UiFramework.Enums.AvatarType avatarType);
}


