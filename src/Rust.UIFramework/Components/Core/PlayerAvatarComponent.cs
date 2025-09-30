using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(PlayerAvatarComponentSerializer))]
public class PlayerAvatarComponent : RawImageComponent
{
    public ulong SteamId;
    public AvatarType AvatarType = AvatarType.Medium;
    
    public override ComponentType ComponentType => ComponentType.PlayerAvatar;

    public override void Reset()
    {
        base.Reset();
        SteamId = default;
        AvatarType = AvatarType.Medium;
    }
    
    public override bool Equals(BaseComponent other)
    {
        if (!base.Equals(other)) return false;
        PlayerAvatarComponent typedOther = (PlayerAvatarComponent)other!;
        return SteamId == typedOther.SteamId 
               && AvatarType == typedOther.AvatarType;
    }
}