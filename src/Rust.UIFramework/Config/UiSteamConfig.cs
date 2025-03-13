using Newtonsoft.Json;

namespace Oxide.Ext.UiFramework.Config;

public class UiSteamConfig
{
    [JsonProperty("Steamworks API Key")]
    public string ApiKey { get; set; }
}