using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Oxide.Ext.UiFramework.Types;

[JsonConverter(typeof(StringEnumConverter))]
public enum UiTranslateType : byte
{
    Distance,
    Percentage
}