using System;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Json;

public class MemorySizeConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteValue(((MemorySize)value).ToString());
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        string value = reader.Value.ToString();
        if (!MemorySize.TryParse(value, out MemorySize size))
        {
            throw new JsonException($"MemorySize {value} is not in the correct format. Please make sure you have numbers followed by B, KB, MB, or GB. EX: \"100B\" or \"50KB\" or \"25MB\" or \"1GB\"");
        }

        return size;
    }

    public override bool CanConvert(Type objectType) => objectType == typeof(MemorySize);
}