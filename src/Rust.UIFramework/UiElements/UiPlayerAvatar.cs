using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiPlayerAvatar : BaseUiComponent, IMaterial<UiPlayerAvatar>, IFadeIn<UiPlayerAvatar>, IUiColor<UiPlayerAvatar>
{
    public readonly PlayerAvatarComponent Avatar = new();
    internal override CoreComponent Component => Avatar;
    
    public string Material { get => Avatar.Material; set => Avatar.Material = value; }
    public float FadeIn { get => Avatar.FadeIn; set => Avatar.FadeIn = value; }
    public UiColor Color { get => Avatar.Color; set => Avatar.Color = value; }

    public static UiPlayerAvatar Create(ulong steamId, AvatarType type, UiColor color)
    {
        UiPlayerAvatar icon = CreateBase<UiPlayerAvatar>();
        icon.Avatar.Color = color;
        icon.Avatar.SteamId = steamId;
        icon.Avatar.AvatarType = type;
        return icon;
    }
        
    public UiPlayerAvatar SetFadeIn(float duration)
    {
        Avatar.FadeIn = duration;
        return this;
    }
    
    public UiPlayerAvatar SetMaterial(string material)
    {
        Avatar.Material = material;
        return this;
    }
    
    public UiPlayerAvatar SetColor(UiColor color)
    {
        Avatar.Color = color;
        return this;
    }

    public UiPlayerAvatar SetSteamId(ulong steamId)
    {
        Avatar.SteamId = steamId;
        return this;
    }
}