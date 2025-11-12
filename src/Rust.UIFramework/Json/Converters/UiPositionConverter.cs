using System;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.Json;

public class UiPositionConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteValue(((UiPosition)value).ToJsonString());
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return reader.TokenType switch
        {
            JsonToken.Null => Nullable.GetUnderlyingType(objectType) != null ? null : default(UiPosition),
            JsonToken.String => UiPosition.Parse(reader.Value.ToString()),
            _ => default
        };
    }

    public override bool CanConvert(Type objectType) => objectType == typeof(UiPosition);
}