using Newtonsoft.Json;

namespace Oxide.Ext.UiFramework.Config;

public class UiImageDbConfig
{
    [JsonProperty("Enable Image DB")]
    public bool Enabled { get; set; }
    
    [JsonProperty("In Memory Image Cache Size (Bytes)")]
    public ulong CacheSize { get; set; }
    
    [JsonProperty("How long before removing unused images (Days)")]
    public uint UnusedImageMaxDays { get; set; }
}