using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Config;

public class UiImageDatabaseConfig
{
    [JsonProperty("Enable Image Database")]
    public bool Enabled { get; set; }
    
    [JsonProperty("In Memory Cache Size")]
    public MemorySize CacheSize { get; set; }
    
    [JsonProperty("How long before removing unused images (Days)")]
    public uint UnusedImageMaxDays { get; set; }
}