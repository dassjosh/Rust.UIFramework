using Newtonsoft.Json;

namespace Oxide.Ext.UiFramework.Config;

internal class UiHarmonyConfig
{
    [JsonProperty("Harmony Patch Oxide Add UI Method")]
    public bool PatchAddUiMethod { get; set; }
}