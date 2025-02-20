using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public class PlayerAvatarComponent : BaseImageComponent
{
    private const string Type = "UnityEngine.UI.RawImage";
    
    public string SteamId;

    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
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