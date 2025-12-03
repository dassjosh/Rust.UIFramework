using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;

public partial class PlayerAvatarComponent : IPlayerAvatarComponent, IPlayerAvatarComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<ulong> _steamId = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.AvatarType> _avatarType = new(Oxide.Ext.UiFramework.Enums.AvatarType.Medium);

	public partial ulong SteamId { get => _steamId.Value; set => _steamId.Value = value; }
	public partial Oxide.Ext.UiFramework.Enums.AvatarType AvatarType { get => _avatarType.Value; set => _avatarType.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<ulong> IPlayerAvatarComponentTrackable.SteamId => _steamId;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.AvatarType> IPlayerAvatarComponentTrackable.AvatarType => _avatarType;

	public Oxide.Ext.UiFramework.Components.PlayerAvatarComponent SetSteamId(ulong steamId)
	{
		SteamId = steamId;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.PlayerAvatarComponent SetAvatarType(Oxide.Ext.UiFramework.Enums.AvatarType avatarType)
	{
		AvatarType = avatarType;
		return this;
	}
	public new IPlayerAvatarComponentTrackable AsTrackable() => this;
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_steamId.HasChanged || _avatarType.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_steamId.ResetHasChanged();
		_avatarType.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_steamId.Reset();
		_avatarType.Reset();
	}
}


