using System.ComponentModel;
using Newtonsoft.Json;

namespace Oxide.Ext.UiFramework.Config;

internal class UiAnimationConfig
{
    [DefaultValue(true)]
    [JsonProperty("Enabled Animations")]
    public bool Enabled { get; set; } = true;

    [DefaultValue(25)]
    [JsonProperty("Animation Update Rate (Milliseconds)")]
    public int UpdateRate { get; set; } = 25;
}