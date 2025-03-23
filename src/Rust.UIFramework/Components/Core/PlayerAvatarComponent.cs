using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class PlayerAvatarComponent : BaseImageComponent
{
    public ulong SteamId;
    public AvatarType AvatarType = AvatarType.Medium;
    
    public override Utf8String Type => JsonDefaults.RawImage.Type;

    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);

        switch (AvatarType)
        {
            case AvatarType.Small:
            case AvatarType.Medium:
                writer.AddFieldRaw(JsonDefaults.PlayerAvatar.SteamIdName, SteamId);
                break;
            
            case AvatarType.Large:
                string avatarUrl = Singleton<UiPlayerAvatars>.Instance.GetAvatarUrl(SteamId, AvatarType);
                if (!string.IsNullOrEmpty(avatarUrl))
                {
                    string img = Singleton<UiImageStorage>.Instance.Get(UiFrameworkPlugin.Instance, avatarUrl);
                    writer.AddFieldRaw(img.StartsWith("http") ? JsonDefaults.Image.UrlName : JsonDefaults.Image.PngName, img);
                }
                else
                {
                    writer.AddFieldRaw(JsonDefaults.PlayerAvatar.SteamIdName, SteamId);
                }

                break;
        }
        
        base.WriteComponent(writer);
        writer.WriteEndObject();
    }

    public override void Reset()
    {
        base.Reset();
        SteamId = default;
        AvatarType = AvatarType.Medium;
    }
}