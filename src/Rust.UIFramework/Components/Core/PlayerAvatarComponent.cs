using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(IPlayerAvatarComponent))]
[GenerateBuilderMethods]
public partial class PlayerAvatarComponent : RawImageComponent, IPlayerAvatarComponent
{
    public override ComponentType ComponentType => ComponentType.PlayerAvatar;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
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
                        if (img.StartsWith("http") || uint.TryParse(img, out uint _))
                        {
                            Image = img;
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
            
            //Needs to be after so we don't duplicate URL fields.
            base.WriteComponentFields(writer, mode);
        }
    }
}