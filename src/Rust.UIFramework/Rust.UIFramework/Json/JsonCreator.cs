using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Json
{
    public static class JsonCreator
    {
        public static string CreateJson(List<BaseUiComponent> components, bool needsMouse, bool needsKeyboard)
        {
            StringBuilder sb = UiFrameworkPool.GetStringBuilder();
            StringWriter sw = new StringWriter(sb);
            JsonTextWriter writer = new JsonTextWriter(sw);

            writer.WriteStartArray();
            components[0].WriteRootComponent(writer, needsMouse, needsKeyboard);

            for (int index = 1; index < components.Count; index++)
            {
                components[index].WriteComponent(writer);
            }

            writer.WriteEndArray();

            string json = sw.ToString();
            UiFrameworkPool.FreeStringBuilder(ref sb);
            return json;
        }

        public static void AddFieldRaw(JsonTextWriter writer, string name, string value)
        {
            writer.WritePropertyName(name);
            writer.WriteValue(value);
        }

        public static void AddFieldRaw(JsonTextWriter writer, string name, int value)
        {
            writer.WritePropertyName(name);
            writer.WriteValue(value);
        }

        public static void AddFieldRaw(JsonTextWriter writer, string name, bool value)
        {
            writer.WritePropertyName(name);
            writer.WriteValue(value);
        }

        public static void AddField(JsonTextWriter writer, string name, string value, string defaultValue)
        {
            if (value != null && value != defaultValue)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
        }

        public static void AddField(JsonTextWriter writer, string name, Vector2 value, Vector2 defaultValue, string valueString)
        {
            if (value != defaultValue)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(valueString);
            }
        }

        public static void AddField(JsonTextWriter writer, string name, Vector2Int value, Vector2Int defaultValue, string valueString)
        {
            if (value != defaultValue)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(valueString);
            }
        }

        public static void AddField(JsonTextWriter writer, string name, TextAnchor value)
        {
            if (value != TextAnchor.UpperLeft)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(EnumExt<TextAnchor>.ToString(value));
            }
        }

        public static void AddField(JsonTextWriter writer, string name, InputField.LineType value)
        {
            if (value != InputField.LineType.SingleLine)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(EnumExt<InputField.LineType>.ToString(value));
            }
        }

        public static void AddTextField(JsonTextWriter writer, string name, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                AddFieldRaw(writer, name, string.Empty);
                return;
            }

            //We need to write it this way so \n type characters are sent over and processed correctly
            writer.WritePropertyName(name);
            StringBuilder sb = UiFrameworkPool.GetStringBuilder();
            sb.Append(UiConstants.Json.QuoteChar);
            sb.Append(value);
            sb.Append(UiConstants.Json.QuoteChar);
            UiFrameworkPool.FreeStringBuilder(ref sb);
            writer.WriteRawValue(sb.ToString());
        }

        public static void AddField(JsonTextWriter writer, string name, int value, int defaultValue)
        {
            if (value != defaultValue)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
        }

        public static void AddField(JsonTextWriter writer, string name, float value, float defaultValue)
        {
            if (Math.Abs(value - defaultValue) > 0.0001)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
        }

        public static void AddField(JsonTextWriter writer, string name, UiColor value, UiColor defaultValue)
        {
            if (value.Value != defaultValue.Value)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value.Color);
            }
        }

        public static void AddMouse(JsonTextWriter writer)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.NeedsCursorValue);
            writer.WriteEndObject();
        }

        public static void AddKeyboard(JsonTextWriter writer)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.NeedsKeyboardValue);
            writer.WriteEndObject();
        }
    }
}