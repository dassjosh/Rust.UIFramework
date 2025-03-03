using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Oxide.Ext.UiFramework.Logging;

namespace Oxide.Ext.UiFramework.Config
{
    /// <summary>
    /// Represents Ui Framework Logging Config
    /// </summary>
    internal class UiLoggingConfig : IUiLoggingConfig
    {
        /// <summary>
        /// Server Console Log Level
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("Server Console Log Level")]
        public UiLogLevel ConsoleLogLevel { get; set; }
        
        /// <summary>
        /// File Log Level
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("File Log Level")]
        public UiLogLevel FileLogLevel { get; set; }
        
        /// <summary>
        /// DateTime format for file logging
        /// </summary>
        [JsonProperty("File DateTime Format")]
        public string FileDateTimeFormat { get; set; }
    }
}