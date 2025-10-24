using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement(typeof(IUiPlayerAvatar))]
public partial class UiPlayerAvatar : BaseUiComponent, IUiPlayerAvatar
{
    public readonly PlayerAvatarComponent Avatar;
    
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
}