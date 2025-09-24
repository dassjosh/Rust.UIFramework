using Newtonsoft.Json;

namespace Oxide.Ext.UiFramework.Config;

public class UiProxyConfig
{
    [JsonProperty("Enable Proxy")]
    public bool EnableProxy { get; set; }
        
    [JsonProperty("Proxy Url")]
    public string Url { get; set; }
        
    public string Username { get; set; }
    public string Password { get; set; }

    [JsonConstructor]
    public UiProxyConfig() { }

    public UiProxyConfig(UiProxyConfig config)
    {
        EnableProxy = config?.EnableProxy ?? false;
        Url = config?.Url ?? string.Empty;
        Username = config?.Username ?? string.Empty;
        Password = config?.Password ?? string.Empty;
    }
}