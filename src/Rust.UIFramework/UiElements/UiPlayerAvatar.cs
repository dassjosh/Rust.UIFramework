using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiPlayerAvatar : BaseUiOutline
{
    public readonly PlayerAvatarComponent Avatar = new();

    public static UiPlayerAvatar Create(in UiPosition pos, in UiOffset offset, UiColor color, string steamId)
    {
        UiPlayerAvatar icon = CreateBase<UiPlayerAvatar>(pos, offset);
        icon.Avatar.Color = color;
        icon.Avatar.SteamId = steamId;
        return icon;
    }
        
    public void SetFadeIn(float duration)
    {
        Avatar.FadeIn = duration;
    }

    protected override void WriteComponents(JsonFrameworkWriter writer)
    {
        Avatar.WriteComponent(writer);
        base.WriteComponents(writer);
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Avatar.Reset();
    }
}