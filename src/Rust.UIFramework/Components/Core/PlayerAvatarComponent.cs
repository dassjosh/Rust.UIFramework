using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class PlayerAvatarComponent : RawImageComponent
{
    private readonly TrackedValue<ulong> _steamId = new();
    private readonly TrackedValue<AvatarType> _avatarType = new(AvatarType.Medium);
    
    public ulong SteamId { get => _steamId.Value; set => _steamId.Value = value; }
    public AvatarType AvatarType { get => _avatarType.Value; set => _avatarType.Value = value; }
    
    public override ComponentType ComponentType => ComponentType.PlayerAvatar;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        base.WriteComponentFields(writer, mode);
        if (_steamId.ShouldSerialize(mode) || _avatarType.ShouldSerialize(mode))
        {
            switch (AvatarType)
            {
                case AvatarType.Small:
                case AvatarType.Medium:
                    writer.AddFieldRaw(JsonDefaults.PlayerAvatar.SteamIdName, StringCache<ulong>.ToString(SteamId));
                    break;
            
                case AvatarType.Large:
                    string avatarUrl = Singleton<UiPlayerAvatars>.Instance.GetAvatarUrl(SteamId, AvatarType);
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
                            writer.AddFieldRaw(JsonDefaults.PlayerAvatar.SteamIdName, StringCache<ulong>.ToString(SteamId));
                        }
                    }
                    else
                    {
                        writer.AddFieldRaw(JsonDefaults.PlayerAvatar.SteamIdName, StringCache<ulong>.ToString(SteamId));
                    }

                    break;
            }
        }
    }
    
    public override void ResetHasChanged()
    {
        base.ResetHasChanged();
        _steamId.ResetHasChanged();
        _avatarType.ResetHasChanged();
    }

    public override void Reset()
    {
        base.Reset();
        _steamId.Reset();
        _avatarType.Reset();
    }
}