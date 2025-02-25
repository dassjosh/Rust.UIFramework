using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiPlayerAvatar : BaseUiComponent, IMaterial, IFadeIn
{
    public readonly PlayerAvatarComponent Avatar = new();
    internal override CoreComponent Component => Avatar;

    public static UiPlayerAvatar Create(in UiPosition pos, in UiOffset offset, UiColor color, ulong steamId)
    {
        UiPlayerAvatar icon = CreateBase<UiPlayerAvatar>(pos, offset);
        icon.Avatar.Color = color;
        icon.Avatar.SteamId = steamId;
        return icon;
    }
    
    void IMaterial.SetMaterial(string material) => SetMaterial(material);
    void IFadeIn.SetFadeIn(float duration) => SetFadeIn(duration);
        
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

    public UiPlayerAvatar SetSteamId(ulong steamId)
    {
        Avatar.SteamId = steamId;
        return this;
    }
}