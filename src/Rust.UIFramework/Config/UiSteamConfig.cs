using System.ComponentModel;
using Newtonsoft.Json;

namespace Oxide.Ext.UiFramework.Config;

internal class UiSteamConfig
{
    [DefaultValue("")]
    [JsonProperty("Steamworks API Key")]
    public string ApiKey { get; set; } = string.Empty;
}