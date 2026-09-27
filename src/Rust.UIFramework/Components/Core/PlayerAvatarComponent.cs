using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class PlayerAvatarComponent : RawImageComponent
{
    public partial ulong SteamId { get; set; }
    
    [TrackedDefaults(typeof(AvatarType), nameof(AvatarType.Medium))]
    public partial AvatarType AvatarType { get; set; }
    
    public override ComponentType ComponentType => ComponentType.PlayerAvatar;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        if (SteamIdTracked.ShouldSerialize(mode) || AvatarTypeTracked.ShouldSerialize(mode))
        {
            switch (AvatarType)
            {
                case AvatarType.Small:
                case AvatarType.Medium:
                    writer.AddQuotedField(JsonDefaults.PlayerAvatar.SteamIdName, SteamId);
                    break;
            
                case AvatarType.Large:
                    string avatarUrl = Singleton<UiPlayerAvatars>.Instance.GetAvatarUrl(SteamId, AvatarType);
                    if (!string.IsNullOrEmpty(avatarUrl))
                    {
                        string img = Singleton<UiImageStorage>.Instance.Get(UiFrameworkPlugin.Instance, avatarUrl);
                        if (img.StartsWith("http") || uint.TryParse(img, out uint _))
                        {
                            //Set the image so the RawImageComponent will use the URL.
                            Image = img;
                            break;
                        }
                    }

                    //Fallback to CUI Avatar if large avatar fails to be loaded
                    writer.AddQuotedField(JsonDefaults.PlayerAvatar.SteamIdName, SteamId);
                    break;
            }
            
            //Needs to be after so we don't duplicate URL fields.
            base.WriteComponentFields(writer, mode);
        }
    }
}