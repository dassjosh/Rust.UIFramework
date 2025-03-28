using Newtonsoft.Json;

namespace Oxide.Ext.UiFramework.Config;

internal class UiAnimationConfig
{
    [JsonProperty("Enabled Animations")]
    public bool Enabled { get; set; }
    
    [JsonProperty("Animation Update Rate (Milliseconds)")]
    public int UpdateRate { get; set; }
}