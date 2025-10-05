using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiPlayerAvatar : BaseUiComponent, IMaterial<UiPlayerAvatar>, IFadeIn<UiPlayerAvatar>, IUiColor<UiPlayerAvatar>
{
    public readonly PlayerAvatarComponent Avatar;

    public string Material { get => Avatar.Material; set => Avatar.Material = value; }
    public float FadeIn { get => Avatar.FadeIn; set => Avatar.FadeIn = value; }
    public UiColor Color { get => Avatar.Color; set => Avatar.Color = value; }
    public ulong SteamId { get => Avatar.SteamId; set => Avatar.SteamId = value; }
    public AvatarType AvatarType { get => Avatar.AvatarType; set => Avatar.AvatarType = value; }
    
    public UiPlayerAvatar() : this(new PlayerAvatarComponent()) { }

    private UiPlayerAvatar(PlayerAvatarComponent component) : base(component)
    {
        Avatar = component;
    }
    
    public UiPlayerAvatar Init(ulong steamId, AvatarType type, UiColor color)
    {
        Color = color;
        SteamId = steamId;
        AvatarType = type;
        return this;
    }
        
    public UiPlayerAvatar SetFadeIn(float duration)
    {
        FadeIn = duration;
        return this;
    }
    
    public UiPlayerAvatar SetMaterial(string material)
    {
        Material = material;
        return this;
    }
    
    public UiPlayerAvatar SetColor(UiColor color)
    {
        Color = color;
        return this;
    }

    public UiPlayerAvatar SetSteamId(ulong steamId)
    {
        SteamId = steamId;
        return this;
    }
    
    public UiPlayerAvatar SetAvatarType(AvatarType avatarType)
    {
        AvatarType = avatarType;
        return this;
    }
}