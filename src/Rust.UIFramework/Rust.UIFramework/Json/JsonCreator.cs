using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Json
{
    public static class JsonCreator
    {
        public static string CreateJson(List<BaseUiComponent> components, bool needsMouse, bool needsKeyboard)
        {
            JsonFrameworkWriter writer = JsonFrameworkWriter.Create();

            writer.WriteStartArray();
            components[0].WriteRootComponent(writer, needsMouse, needsKeyboard);

            for (int index = 1; index < components.Count; index++)
            {
                components[index].WriteComponent(writer);
            }

            writer.WriteEndArray();

            return writer.ToJson();
        }

        public static void AddFieldRaw(JsonFrameworkWriter writer, string name, string value)
        {
            writer.WritePropertyName(name);
            writer.WriteValue(value);
        }

        public static void AddFieldRaw(JsonFrameworkWriter writer, string name, int value)
        {
            writer.WritePropertyName(name);
            writer.WriteValue(value);
        }

        public static void AddFieldRaw(JsonFrameworkWriter writer, string name, bool value)
        {
            writer.WritePropertyName(name);
            writer.WriteValue(value);
        }

        public static void AddField(JsonFrameworkWriter writer, string name, string value, string defaultValue)
        {
            if (value != null && value != defaultValue)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
        }
        
        public static void AddField(JsonFrameworkWriter writer, string name, Vector2 value, Vector2 defaultValue)
        {
            if (value != defaultValue)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
        }

        public static void AddPosition(JsonFrameworkWriter writer, string name, Vector2 value, Vector2 defaultValue)
        {
            if (value != defaultValue)
            {
                writer.WritePropertyName(name);
                writer.WritePosition(value);
            }
        }

        public static void AddOffset(JsonFrameworkWriter writer, string name, Vector2Short value, Vector2Short defaultValue)
        {
            if (value != defaultValue)
            {
                writer.WritePropertyName(name);
                writer.WriteOffset(value);
            }
        }

        public static void AddField(JsonFrameworkWriter writer, string name, TextAnchor value)
        {
            if (value != TextAnchor.UpperLeft)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(EnumExt<TextAnchor>.ToString(value));
            }
        }

        public static void AddField(JsonFrameworkWriter writer, string name, InputField.LineType value)
        {
            if (value != InputField.LineType.SingleLine)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(EnumExt<InputField.LineType>.ToString(value));
            }
        }
        
        public static void AddField(JsonFrameworkWriter writer, string name, Image.Type value)
        {
            if (value != Image.Type.Simple)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(EnumExt<Image.Type>.ToString(value));
            }
        }

        public static void AddField(JsonFrameworkWriter writer, string name, int value, int defaultValue)
        {
            if (value != defaultValue)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
        }

        public static void AddField(JsonFrameworkWriter writer, string name, float value, float defaultValue)
        {
            if (Math.Abs(value - defaultValue) >= 0.0001)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
        }

        public static void AddField(JsonFrameworkWriter writer, string name, UiColor color)
        {
            if (color.Value != JsonDefaults.Color.ColorValue)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(color);
            }
        }
        
        public static void AddTextField(JsonFrameworkWriter writer, string name, string value)
        {
            writer.WritePropertyName(name);
            writer.WriteTextValue(value);
        }

        public static void AddMouse(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.NeedsCursorValue);
            writer.WriteEndObject();
        }

        public static void AddKeyboard(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.NeedsKeyboardValue);
            writer.WriteEndObject();
        }
    }
}