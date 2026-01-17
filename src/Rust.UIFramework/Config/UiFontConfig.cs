using System.ComponentModel;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Config;

internal class UiFontConfig
{
    [DefaultValue(UiFonts.RobotoCondensedRegular)]
    [JsonProperty("Default Font")]
    public string DefaultFont { get; set; } = UiFonts.RobotoCondensedRegular;
}