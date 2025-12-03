using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IPlayerAvatarComponentTrackable : IRawImageComponentTrackable
{
	Oxide.Ext.UiFramework.Types.Tracked<ulong> SteamId { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.AvatarType> AvatarType { get; }
}


