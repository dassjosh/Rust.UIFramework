using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class PlayerAvatarComponent : BaseImageComponent
{
    public ulong SteamId;
    public AvatarType Type = AvatarType.Medium;

    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.RawImage.Type);

        switch (Type)
        {
            case AvatarType.Medium:
                writer.AddFieldRaw(JsonDefaults.PlayerAvatar.SteamIdName, SteamId);
                break;
            case AvatarType.Small:
            case AvatarType.Large:
                string img = Singleton<UiImageStorage>.Instance.Get(UiFrameworkPlugin.Instance, Singleton<UiPlayerAvatars>.Instance.GetAvatarUrl(SteamId, Type));
                writer.AddFieldRaw(img.StartsWith("http") ? JsonDefaults.Image.UrlName : JsonDefaults.Image.PngName, img);
                break;
        }
        
        base.WriteComponent(writer);
        writer.WriteEndObject();
    }

    public override void Reset()
    {
        base.Reset();
        SteamId = default;
        Type = AvatarType.Medium;
    }
}