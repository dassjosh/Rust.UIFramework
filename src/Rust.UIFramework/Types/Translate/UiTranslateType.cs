using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Oxide.Ext.UiFramework.Types;

/// <summary>
/// How the element should be translated
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum UiTranslateType : byte
{
    /// <summary>
    /// Translate in pixels
    /// </summary>
    Px,
    
    /// <summary>
    /// Translate as a percentage of the element's size'
    /// </summary>
    Percent
}