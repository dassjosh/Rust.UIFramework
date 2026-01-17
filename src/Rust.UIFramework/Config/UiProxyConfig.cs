using System.ComponentModel;
using Newtonsoft.Json;

namespace Oxide.Ext.UiFramework.Config;

internal class UiProxyConfig
{
    [DefaultValue(false)]
    [JsonProperty("Enable Proxy")]
    public bool EnableProxy { get; set; } = false;
        
    [DefaultValue("")]
    [JsonProperty("Proxy Url")]
    public string Url { get; set; } = string.Empty;
        
    [DefaultValue("")]
    public string Username { get; set; } = string.Empty;
    
    [DefaultValue("")]
    public string Password { get; set; } = string.Empty;
}