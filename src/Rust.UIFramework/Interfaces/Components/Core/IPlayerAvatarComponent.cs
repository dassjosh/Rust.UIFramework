using Oxide.Ext.UiFramework.Enums;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IPlayerAvatarComponent : IRawImageComponent
{
    public ulong SteamId { get; set; }
    [TrackedDefaults(typeof(AvatarType), nameof(AvatarType.Medium))]
    public AvatarType AvatarType { get; set; }
}