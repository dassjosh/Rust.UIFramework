using System.ComponentModel;
using Newtonsoft.Json;

namespace Oxide.Ext.UiFramework.Config;

internal class UiImageStorageConfig
{
    [DefaultValue(5)]
    [JsonProperty("Max Concurrent Image Downloads")]
    public int MaxConcurrentDownloads { get; set; } = 5;

    [DefaultValue(3)]
    [JsonProperty("Max Download Attempts")]
    public int MaxDownloadAttempts { get; set; } = 3;
}