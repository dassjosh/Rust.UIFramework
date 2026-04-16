using System;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Json;

public class UiTranslateDirectionConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteValue(((UiTranslateDirection)value).ToString());
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return reader.TokenType switch
        {
            JsonToken.Null => Nullable.GetUnderlyingType(objectType) != null ? null : default(UiTranslateDirection),
            JsonToken.String => UiTranslateDirection.Parse(reader.Value.ToString()),
            _ => default
        };
    }

    public override bool CanConvert(Type objectType) => objectType == typeof(UiTranslateDirection);
}