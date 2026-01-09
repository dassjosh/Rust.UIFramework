using Newtonsoft.Json;

namespace Oxide.Ext.UiFramework.Config;

internal class UiThreadingConfig
{
    [JsonProperty("Enable UI Sending Thread")]
    public bool EnableUiSendingThread { get; set; }
    
    [JsonProperty("Enable Image Download Thread")]
    public bool EnableImageDownloadThread { get; set; }
    
    [JsonProperty("Enable Animation Thread")]
    public bool EnableAnimationThread { get; set; }
}