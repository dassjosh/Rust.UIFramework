using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Oxide.Ext.UiFramework.Logging;

namespace Oxide.Ext.UiFramework.Config;

/// <summary>
/// Represents Ui Framework Logging Config
/// </summary>
internal class UiLoggingConfig : IUiLoggingConfig
{
    /// <summary>
    /// Server Console Log Level
    /// </summary>
    [DefaultValue(UiLogLevel.Info)]
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty("Server Console Log Level")]
    public UiLogLevel ConsoleLogLevel { get; set; } = UiLogLevel.Info;
        
    /// <summary>
    /// File Log Level
    /// </summary>
    [DefaultValue(UiLogLevel.Off)]
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty("File Log Level")]
    public UiLogLevel FileLogLevel { get; set; } = UiLogLevel.Off;

    /// <summary>
    /// DateTime format for file logging
    /// </summary>
    [DefaultValue("HH:mm:ss.ff")]
    [JsonProperty("File DateTime Format")]
    public string FileDateTimeFormat { get; set; } = "HH:mm:ss.ff";
}