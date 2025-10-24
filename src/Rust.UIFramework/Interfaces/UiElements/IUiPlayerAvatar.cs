using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IUiPlayerAvatar : IMaterial<UiPlayerAvatar>, IFadeIn<UiPlayerAvatar>, IUiColor<UiPlayerAvatar>, IBaseUiComponent
{
    ulong SteamId { get; set; }
    AvatarType AvatarType { get; set; }
}