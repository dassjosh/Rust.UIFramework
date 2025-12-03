using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IPlayerAvatarComponent : IRawImageComponent
{
	ulong SteamId { get; set; }
	Oxide.Ext.UiFramework.Enums.AvatarType AvatarType { get; set; }
}


