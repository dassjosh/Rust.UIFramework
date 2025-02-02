using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public class PlayerAvatarComponent : BaseImageComponent
{
    public string SteamId;

    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.RawImage.Type);
        writer.AddField(JsonDefaults.PlayerAvatar.SteamIdName, SteamId, null);
        base.WriteComponent(writer);
        writer.WriteEndObject();
    }

    public override void Reset()
    {
        base.Reset();
        SteamId = default;
    }
}