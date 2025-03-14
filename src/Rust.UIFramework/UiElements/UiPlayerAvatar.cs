using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiPlayerAvatar : BaseUiComponent, IMaterial<UiPlayerAvatar>, IFadeIn<UiPlayerAvatar>, IUiColor<UiPlayerAvatar>
{
    public readonly PlayerAvatarComponent Avatar = new();
    internal override CoreComponent Component => Avatar;

    public static UiPlayerAvatar Create(in UiPosition pos, in UiOffset offset, UiColor color, ulong steamId, AvatarType type)
    {
        UiPlayerAvatar icon = CreateBase<UiPlayerAvatar>(pos, offset);
        icon.Avatar.Color = color;
        icon.Avatar.SteamId = steamId;
        icon.Avatar.Type = type;
        return icon;
    }
    
    void IMaterial.SetMaterial(string material) => SetMaterial(material);
    void IFadeIn.SetFadeIn(float duration) => SetFadeIn(duration);
    void IUiColor.SetColor(UiColor color) => SetColor(color);
        
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