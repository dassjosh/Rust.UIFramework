using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public class PlayerAvatarComponent : BaseImageComponent
{
    public string SteamId;

    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.RawImage.Type);
        writer.AddFieldRaw(JsonDefaults.PlayerAvatar.SteamIdName, SteamId);
        base.WriteComponent(writer);
        writer.WriteEndObject();
    }

    public override void Reset()
    {
        base.Reset();
        SteamId = default;
    }
}