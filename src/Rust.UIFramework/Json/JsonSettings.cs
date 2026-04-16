using System.Globalization;
using Newtonsoft.Json;

namespace Oxide.Ext.UiFramework.Json;

internal static class JsonSettings
{
    public static readonly JsonSerializerSettings Serializer = new()
    {
        DefaultValueHandling = DefaultValueHandling.Populate,
        Culture = CultureInfo.InvariantCulture,
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore
    };
}