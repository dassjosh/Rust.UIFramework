using Newtonsoft.Json;

namespace Oxide.Ext.UiFramework.Config;

public class UiFontConfig
{
    [JsonProperty("Default Font")]
    public string DefaultFont { get; set; }
}