using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Data;

namespace Oxide.Ext.UiFramework.Config;

/// <summary>
/// Represents Ui Framework Extension Config
/// </summary>
internal class UiFrameworkConfig : BaseDataFile<UiFrameworkConfig>
{
    /// <summary>
    /// UiFramework Font Options
    /// </summary>
    [JsonProperty("Font")]
    public UiFontConfig Font { get; set; } = new();
    
    /// <summary>
    /// UiFramework Image Storage Options
    /// </summary>
    [JsonProperty("ImageStorage")]
    public UiImageStorageConfig ImageStorage { get; set; } = new();
    
    /// <summary>
    /// UiFramework Image DB Options
    /// </summary>
    [JsonProperty("ImageDB")]
    public UiImageDatabaseConfig ImageDatabase { get; set; } = new();
    
    /// <summary>
    /// UiFramework Animations Options
    /// </summary>
    [JsonProperty("Animations")]
    public UiAnimationConfig Animations { get; set; } = new();
    
    /// <summary>
    /// UiFramework Steam Options
    /// </summary>
    [JsonProperty("Steam")]
    public UiSteamConfig Steam { get; set; } = new();
            
    /// <summary>
    /// UiFramework Threading Options
    /// </summary>
    [JsonProperty("Threading")]
    public UiThreadingConfig Threading { get; set; } = new();
    
    /// <summary>
    /// UiFramework Harmony Options
    /// </summary>
    [JsonProperty("Harmony")]
    public UiHarmonyConfig Harmony { get; set; } = new();
    
    /// <summary>
    /// UiFramework Proxy Options
    /// </summary>
    [JsonProperty("Proxy")]
    public UiProxyConfig Proxy { get; set; } = new();
    
    /// <summary>
    /// UiFramework Logging Options
    /// </summary>
    [JsonProperty("Logging")]
    public UiLoggingConfig Logging { get; set; } = new();
}