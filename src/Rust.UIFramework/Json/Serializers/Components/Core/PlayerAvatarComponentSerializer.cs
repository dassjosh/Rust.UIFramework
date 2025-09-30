using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Json;

public class PlayerAvatarComponentSerializer : RawImageComponentSerializer<PlayerAvatarComponent>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void SerializeComponent(JsonFrameworkWriter writer, PlayerAvatarComponent component, PlayerAvatarComponent defaults, SerializeMode mode)
    {
        base.SerializeComponent(writer, component, defaults, mode);

        if (component.SteamId != defaults.SteamId)
        {
            switch (component.AvatarType)
            {
                case AvatarType.Small:
                case AvatarType.Medium:
                    writer.AddFieldRaw(JsonDefaults.PlayerAvatar.SteamIdName, component.SteamId);
                    break;
            
                case AvatarType.Large:
                    string avatarUrl = Singleton<UiPlayerAvatars>.Instance.GetAvatarUrl(component.SteamId, component.AvatarType);
                    if (!string.IsNullOrEmpty(avatarUrl))
                    {
                        string img = Singleton<UiImageStorage>.Instance.Get(UiFrameworkPlugin.Instance, avatarUrl);
                        if (img.StartsWith("http"))
                        {
                            writer.AddFieldRaw(JsonDefaults.Image.UrlName, img);
                        }
                        else if (uint.TryParse(img, out uint _))
                        {
                            writer.AddFieldRaw(JsonDefaults.Image.PngName, img);
                        }
                        else
                        {
                            writer.AddFieldRaw(JsonDefaults.PlayerAvatar.SteamIdName, component.SteamId);
                        }
                    }
                    else
                    {
                        writer.AddFieldRaw(JsonDefaults.PlayerAvatar.SteamIdName, component.SteamId);
                    }

                    break;
            }
        }
    }
}