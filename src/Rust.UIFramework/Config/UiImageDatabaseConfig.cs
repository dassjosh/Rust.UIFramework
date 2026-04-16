using System.ComponentModel;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Config;

public class UiImageDatabaseConfig
{
    [DefaultValue(true)]
    [JsonProperty("Enable Image Database")]
    public bool Enabled { get; set; } = true;
    
    [DefaultValue(typeof(MemorySize), "25MB")]
    [JsonProperty("In Memory Cache Size")]
    public MemorySize CacheSize { get; set; } = MemorySize.Parse("25MB");

    [JsonProperty("How long before removing unused images (Days)")]
    public uint UnusedImageMaxDays { get; set; } = 30;
}