using System;
using Microsoft.CodeAnalysis;

namespace Rust.UiFramework.SourceGenerators.Extensions;

public static class AttributeDataExt
{
    extension(AttributeData attribute)
    {
        public T? GetConstructorValue<T>(int index) where T : struct, Enum
        {
            object value = attribute.GetConstructorValue(index);
            if (value is null)
            {
                return default;
            }

            return (T)Enum.ToObject(typeof(T), value);
        }

        public string GetConstructorString(int index, string defaultValue = "")
        {
            object value = attribute.GetConstructorValue(index);
            return value is null ? defaultValue : value.ToString();
        }

        private object GetConstructorValue(int index)
        {
            if (attribute == null || attribute.ConstructorArguments.IsEmpty || attribute.ConstructorArguments.Length < index || attribute.ConstructorArguments[index].IsNull)
            {
                return default;
            }

            return attribute.ConstructorArguments[index].Value!;
        }
    }
}