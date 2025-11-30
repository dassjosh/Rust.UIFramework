using System;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Colors;

namespace Oxide.Ext.UiFramework.Json;

public class UiOpacityConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteValue(((UiOpacity)value).Value);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return reader.TokenType switch
        {
            JsonToken.Null => Nullable.GetUnderlyingType(objectType) != null ? null : default(UiOpacity),
            JsonToken.String => UiOpacity.Parse(reader.Value.ToString()),
            _ => default
        };
    }

    public override bool CanConvert(Type objectType) => objectType == typeof(UiOpacity);
}