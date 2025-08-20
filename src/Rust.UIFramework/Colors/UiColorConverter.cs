using System;
using Newtonsoft.Json;

namespace Oxide.Ext.UiFramework.Colors;

public class UiColorConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteValue(((UiColor)value).ToHtmlColor());
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return reader.TokenType switch
        {
            JsonToken.Null => Nullable.GetUnderlyingType(objectType) != null ? null : default(UiColor),
            JsonToken.String => UiColor.ParseHexColor(reader.Value.ToString()),
            _ => default
        };
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(UiColor);
    }
}