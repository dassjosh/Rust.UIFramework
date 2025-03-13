using Newtonsoft.Json;

namespace Oxide.Ext.UiFramework.Config;

public class UiImageStorageConfig
{
    [JsonProperty("Max Concurrent Image Downloads")]
    public int MaxConcurrentDownloads { get; set; }
    
    [JsonProperty("Max Download Attempts")]
    public int MaxDownloadAttempts { get; set; }
}