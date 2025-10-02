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
    
    public override void CopyFrom(object value)
    {
        base.CopyFrom(value);
        if (value is PlayerAvatarComponent component)
        {
            SteamId = component.SteamId;
            AvatarType = component.AvatarType;
        }
    }
    
    public override bool AreEquivalent(BaseComponent other)
    {
        if (!base.AreEquivalent(other)) return false;
        PlayerAvatarComponent typedOther = (PlayerAvatarComponent)other!;
        return SteamId == typedOther.SteamId 
               && AvatarType == typedOther.AvatarType;
    }
}