using Newtonsoft.Json;

namespace Oxide.Ext.UiFramework.Config;

internal class UiFontConfig
{
    [JsonProperty("Default Font")]
    public string DefaultFont { get; set; }
}