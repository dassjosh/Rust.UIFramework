using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using Oxide.Plugins;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Json
{
    public static class JsonCreator
    {
        private static readonly StringBuilder _sb = new StringBuilder();
        
        public static string CreateJson(List<BaseUiComponent> components, bool needsMouse, bool needsKeyboard)
        {
            StringBuilder sb = UiFrameworkPool.GetStringBuilder();
            StringWriter sw = new StringWriter(sb);
            JsonTextWriter writer = new JsonTextWriter(sw);

            writer.WriteStartArray();
            
            WriteRootComponent(components[0], writer, needsMouse, needsKeyboard);

            for (int index = 1; index < components.Count; index++)
            {
                WriteComponent(components[index], writer);
            }

            writer.WriteEndArray();

            string json = sw.ToString();
            UiFrameworkPool.FreeStringBuilder(ref sb);
            return json;
        }

        private static void WriteRootComponent(BaseUiComponent component, JsonTextWriter writer, bool needsMouse, bool needsKeyboard)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentName, component.Name);
            AddFieldRaw(writer, JsonDefaults.ParentName, component.Parent);
            AddField(writer, JsonDefaults.FadeOutName, component.FadeOut, JsonDefaults.FadeOutValue);

            writer.WritePropertyName("components");
            writer.WriteStartArray();
            component.WriteComponents(writer);

            if (needsMouse)
            {
                AddMouse(writer);
            }
            
            if (needsKeyboard)
            {
                AddKeyboard(writer);
            }
            
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
        
        private static void WriteComponent(BaseUiComponent component, JsonTextWriter writer)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentName, component.Name);
            AddFieldRaw(writer, JsonDefaults.ParentName, component.Parent);
            AddField(writer, JsonDefaults.FadeOutName, component.FadeOut, JsonDefaults.FadeOutValue);

            writer.WritePropertyName("components");
            writer.WriteStartArray();
            component.WriteComponents(writer);
            writer.WriteEndArray();
            writer.WriteEndObject();
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
        
        public static void AddField(JsonTextWriter writer, string name, TextAnchor value)
        {
            if (value != TextAnchor.UpperLeft)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value.ToString());
            }
        }
        
        public static void AddField(JsonTextWriter writer, string name, InputField.LineType value)
        {
            if (value != InputField.LineType.SingleLine)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value.ToString());
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
            _sb.Clear();
            _sb.Append(UiConstants.Json.QuoteChar);
            _sb.Append(value);
            _sb.Append(UiConstants.Json.QuoteChar);
            writer.WriteRawValue(_sb.ToString());
        }

        public static void AddMultiField(JsonTextWriter writer, string name, string value, string[] defaultValues)
        {
            if (value != null && !defaultValues.Contains(value))
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
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
            if (value != defaultValue)
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

        public static void Add(JsonTextWriter writer, Position position, Offset? offset)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, JsonDefaults.RectTransformName);
            if (!position.IsDefaultMin)
            {
                AddFieldRaw(writer, JsonDefaults.AnchorMinName, position.Min);
            }

            if (!position.IsDefaultMax)
            {
                AddFieldRaw(writer, JsonDefaults.AnchorMaxName, position.Max);
            }

            if (offset.HasValue)
            {
                string min = offset.Value.Min;
                string max = offset.Value.Max;
                AddMultiField(writer, JsonDefaults.OffsetMinName, min, JsonDefaults.DefaultMinValues);
                AddMultiField(writer, JsonDefaults.OffsetMaxName, max, JsonDefaults.DefaultMaxValues);
            }
            else
            {
                //Fixes issue with UI going outside of bounds
                AddFieldRaw(writer, JsonDefaults.OffsetMaxName, JsonDefaults.OffsetMaxValue);
            }

            writer.WriteEndObject();
        }

        public static void Add(JsonTextWriter writer, ButtonComponent button)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, ButtonComponent.Type);
            AddField(writer, JsonDefaults.CommandName, button.Command, JsonDefaults.NullValue);
            AddField(writer, JsonDefaults.CloseName, button.Close, JsonDefaults.NullValue);
            AddField(writer, JsonDefaults.ColorName, button.Color, JsonDefaults.ColorValue);
            AddField(writer, JsonDefaults.SpriteName, button.Sprite, JsonDefaults.SpriteValue);
            AddField(writer, JsonDefaults.FadeInName, button.FadeIn, JsonDefaults.FadeOutValue);
            writer.WriteEndObject();
        }

        public static void Add(JsonTextWriter writer, RawImageComponent image)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, RawImageComponent.Type);
            AddField(writer, JsonDefaults.ColorName, image.Color, JsonDefaults.ColorValue);
            AddField(writer, JsonDefaults.SpriteName, image.Sprite, JsonDefaults.SpriteImageValue);
            AddField(writer, JsonDefaults.FadeInName, image.FadeIn, JsonDefaults.FadeOutValue);
            if (!string.IsNullOrEmpty(image.Url))
            {
                AddField(writer, JsonDefaults.URLName, image.Url, JsonDefaults.EmptyString);
            }

            writer.WriteEndObject();
        }

        public static void Add(JsonTextWriter writer, ImageComponent image)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, BaseImageComponent.Type);
            AddField(writer, JsonDefaults.ColorName, image.Color, JsonDefaults.ColorValue);
            AddField(writer, JsonDefaults.SpriteName, image.Sprite, JsonDefaults.SpriteValue);
            AddField(writer, JsonDefaults.MaterialName, image.Material, JsonDefaults.MaterialValue);
            AddField(writer, JsonDefaults.FadeInName, image.FadeIn, JsonDefaults.FadeOutValue);
            
            if (!string.IsNullOrEmpty(image.Png))
            {
                AddField(writer, JsonDefaults.PNGName, image.Png, JsonDefaults.EmptyString);
            }
            
            writer.WriteEndObject();
        }
        
        public static void Add(JsonTextWriter writer, ItemIconComponent icon)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, BaseImageComponent.Type);
            AddField(writer, JsonDefaults.ColorName, icon.Color, JsonDefaults.ColorValue);
            AddField(writer, JsonDefaults.SpriteName, icon.Sprite, JsonDefaults.SpriteValue);
            AddField(writer, JsonDefaults.MaterialName, icon.Material, JsonDefaults.MaterialValue);
            AddField(writer, JsonDefaults.FadeInName, icon.FadeIn, JsonDefaults.FadeOutValue);
            AddFieldRaw(writer, JsonDefaults.ItemIdName, icon.ItemId);
            AddField(writer, JsonDefaults.SkinIdName, icon.SkinId, JsonDefaults.DefaultSkinId);
            writer.WriteEndObject();
        }

        public static void AddMouse(JsonTextWriter writer)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, JsonDefaults.NeedsCursorValue);
            writer.WriteEndObject();
        }
        
        public static void AddKeyboard(JsonTextWriter writer)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, JsonDefaults.NeedsKeyboardValue);
            writer.WriteEndObject();
        }

        public static void Add(JsonTextWriter writer, OutlineComponent outline)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, OutlineComponent.Type);
            AddField(writer, JsonDefaults.ColorName, outline.Color, JsonDefaults.ColorValue);
            AddField(writer, JsonDefaults.DistanceName, outline.Distance, JsonDefaults.DistanceValue);
            if (outline.UseGraphicAlpha)
            {
                AddFieldRaw(writer, JsonDefaults.UseGraphicAlphaName, JsonDefaults.UseGraphicAlphaValue);
            }

            writer.WriteEndObject();
        }
        
        public static void Add(JsonTextWriter writer, TextComponent text)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, TextComponent.Type);
            AddTextField(writer, JsonDefaults.TextName, text.Text);
            AddField(writer, JsonDefaults.FontSizeName, text.FontSize, JsonDefaults.FontSizeValue);
            AddField(writer, JsonDefaults.FontName, text.Font, JsonDefaults.FontValue);
            AddField(writer, JsonDefaults.ColorName, text.Color, JsonDefaults.ColorValue);
            AddField(writer, JsonDefaults.AlignName, text.Align);
            AddField(writer, JsonDefaults.FadeInName, text.FadeIn, JsonDefaults.FadeOutValue);
            writer.WriteEndObject();
        }

        public static void Add(JsonTextWriter writer, InputComponent input)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, InputComponent.Type);
            AddTextField(writer, JsonDefaults.TextName, input.Text);
            AddField(writer, JsonDefaults.FontSizeName, input.FontSize, JsonDefaults.FontSizeValue);
            AddField(writer, JsonDefaults.FontName, input.Font, JsonDefaults.FontValue);
            AddField(writer, JsonDefaults.AlignName, input.Align);
            AddField(writer, JsonDefaults.ColorName, input.Color, JsonDefaults.ColorValue);
            AddField(writer, JsonDefaults.CharacterLimitName, input.CharsLimit, JsonDefaults.CharacterLimitValue);
            AddField(writer, JsonDefaults.CommandName, input.Command, JsonDefaults.NullValue);
            AddField(writer, JsonDefaults.FadeInName, input.FadeIn, JsonDefaults.FadeOutValue);
            AddField(writer, JsonDefaults.LineTypeName, input.LineType);

            if (input.IsPassword)
            {
                AddFieldRaw(writer, JsonDefaults.PasswordName, JsonDefaults.PasswordValue);
            }

            if (input.IsReadyOnly)
            {
                AddFieldRaw(writer, JsonDefaults.ReadOnlyName, JsonDefaults.ReadOnlyValue);
            }

            writer.WriteEndObject();
        }
    }
}