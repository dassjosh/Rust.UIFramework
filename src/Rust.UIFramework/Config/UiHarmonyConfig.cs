using System.ComponentModel;
using Newtonsoft.Json;

namespace Oxide.Ext.UiFramework.Config;

internal class UiHarmonyConfig
{
    [DefaultValue(false)]
    [JsonProperty("Harmony Patch Oxide Add UI Method")]
    public bool PatchAddUiMethod { get; set; } = false;
}