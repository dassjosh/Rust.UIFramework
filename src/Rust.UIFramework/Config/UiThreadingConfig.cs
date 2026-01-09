using Newtonsoft.Json;

namespace Oxide.Ext.UiFramework.Config;

internal class UiThreadingConfig
{
    [JsonProperty("Enable UI Sending Thread")]
    public bool EnableUiSendingThread { get; set; }
}