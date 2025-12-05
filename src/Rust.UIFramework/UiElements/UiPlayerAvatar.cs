using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement]
[GenerateBuilderMethods]
public partial class UiPlayerAvatar : BaseUiComponent, IMaterial<UiPlayerAvatar>, IFadeIn<UiPlayerAvatar>, IUiColor<UiPlayerAvatar>
{
    public partial ulong SteamId { get; set; }
    public partial AvatarType AvatarType { get; set; }
    public partial string Material { get; set; }
    public partial float FadeIn { get; set; }
    public partial UiColor Color { get; set; }
    
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