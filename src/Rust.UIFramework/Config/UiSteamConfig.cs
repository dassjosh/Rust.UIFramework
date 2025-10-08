using Newtonsoft.Json;

namespace Oxide.Ext.UiFramework.Config;

internal class UiSteamConfig
{
    [JsonProperty("Steamworks API Key")]
    public string ApiKey { get; set; }
}