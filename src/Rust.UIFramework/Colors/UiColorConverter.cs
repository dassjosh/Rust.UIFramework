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
        switch (reader.TokenType)
        {
            case JsonToken.Null:
                if (Nullable.GetUnderlyingType(objectType) != null)
                {
                    return null;
                }

                return default(UiColor);

            case JsonToken.String:
                return UiColor.ParseHexColor(reader.Value.ToString());
        }

        return default(UiColor);
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(UiColor);
    }
}