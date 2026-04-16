using System.ComponentModel;
using Newtonsoft.Json;

namespace Oxide.Ext.UiFramework.Config;

internal class UiThreadingConfig
{
    [DefaultValue(true)]
    [JsonProperty("Enable UI Sending Thread")]
    public bool EnableUiSendingThread { get; set; } = true;

    [DefaultValue(true)]
    [JsonProperty("Enable Image Download Thread")]
    public bool EnableImageDownloadThread { get; set; } = true;

    [DefaultValue(true)]
    [JsonProperty("Enable Animation Thread")]
    public bool EnableAnimationThread { get; set; } = true;
}