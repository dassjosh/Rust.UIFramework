using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Oxide.Plugins;
using UI.Framework.Rust.Colors;
using UI.Framework.Rust.Components;
using UI.Framework.Rust.Positions;
using UI.Framework.Rust.UiElements;

namespace UI.Framework.Rust.Json
{
    public static class JsonCreator
    {
        public static string CreateJson(List<BaseUiComponent> components)
        {
            StringWriter sw = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);

            writer.WriteStartArray();

            for (int index = 0; index < components.Count; index++)
            {
                BaseUiComponent component = components[index];
                writer.WriteStartObject();
                AddFieldRaw(writer, JsonDefaults.ComponentName, component.Name);
                AddFieldRaw(writer, JsonDefaults.ParentName, component.Parent);
                AddField(writer, JsonDefaults.FadeInName, ref component.FadeIn, ref JsonDefaults.FadeoutValue);
                AddField(writer, JsonDefaults.FadeoutName, ref component.FadeOut, ref JsonDefaults.FadeoutValue);

                writer.WritePropertyName("components");
                writer.WriteStartArray();
                component.WriteComponents(writer);
                writer.WriteEndArray();
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            return sw.ToString();
        }

        public static void AddFieldRaw(JsonTextWriter writer, string name, string value)
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

        public static void AddMultiField(JsonTextWriter writer, string name, string value, string[] defaultValues)
        {
            if (value != null && !defaultValues.Contains(value))
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
        }

        public static void AddField(JsonTextWriter writer, string name, ref int value, ref int defaultValue)
        {
            if (value != defaultValue)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
        }

        public static void AddField(JsonTextWriter writer, string name, ref float value, ref float defaultValue)
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

        public static void Add(JsonTextWriter writer, ref Position position, ref Offset? offset)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, JsonDefaults.RectTransformName);
            AddMultiField(writer, JsonDefaults.AnchorMinName, position.Min, JsonDefaults.DefaultMinValues);
            AddMultiField(writer, JsonDefaults.AnchorMaxName, position.Max, JsonDefaults.DefaultMaxValues);

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
            writer.WriteEndObject();
        }

        public static void Add(JsonTextWriter writer, TextComponent textComponent)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, TextComponent.Type);
            AddField(writer, JsonDefaults.TextName, textComponent.Text, JsonDefaults.TextValue);
            AddField(writer, JsonDefaults.FontSizeName, ref textComponent.FontSize, ref JsonDefaults.FontSizeValue);
            AddField(writer, JsonDefaults.FontName, textComponent.Font, JsonDefaults.FontValue);
            AddField(writer, JsonDefaults.ColorName, textComponent.Color, JsonDefaults.ColorValue);
            string align = textComponent.Align.ToString();
            AddField(writer, JsonDefaults.AlignName, align, JsonDefaults.AlignValue);
            writer.WriteEndObject();
        }

        public static void Add(JsonTextWriter writer, RawImageComponent image)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, RawImageComponent.Type);
            AddField(writer, JsonDefaults.ColorName, image.Color, JsonDefaults.ColorValue);
            AddField(writer, JsonDefaults.SpriteName, image.Sprite, JsonDefaults.SpriteImageValue);
            if (!string.IsNullOrEmpty(image.Png))
            {
                AddField(writer, JsonDefaults.PNGName, image.Png, JsonDefaults.EmptyString);
            }

            if (!string.IsNullOrEmpty(image.Url))
            {
                AddField(writer, JsonDefaults.URLName, image.Url, JsonDefaults.EmptyString);
            }

            writer.WriteEndObject();
        }

        public static void Add(JsonTextWriter writer, ImageComponent image)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, ImageComponent.Type);
            AddField(writer, JsonDefaults.ColorName, image.Color, JsonDefaults.ColorValue);
            AddField(writer, JsonDefaults.SpriteName, image.Sprite, JsonDefaults.SpriteValue);
            AddField(writer, JsonDefaults.MaterialName, image.Material, JsonDefaults.MaterialValue);
            writer.WriteEndObject();
        }

        public static void AddCursor(JsonTextWriter writer)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, JsonDefaults.NeedsCursorValue);
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

        public static void Add(JsonTextWriter writer, InputComponent input)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, InputComponent.Type);
            AddField(writer, JsonDefaults.FontSizeName, ref input.FontSize, ref JsonDefaults.FontSizeValue);
            AddField(writer, JsonDefaults.FontName, input.Font, JsonDefaults.FontValue);
            string align = input.Align.ToString();
            AddField(writer, JsonDefaults.AlignName, align, JsonDefaults.AlignValue);
            AddField(writer, JsonDefaults.ColorName, input.Color, JsonDefaults.ColorValue);
            AddField(writer, JsonDefaults.CharacterLimitName, ref input.CharsLimit, ref JsonDefaults.CharacterLimitValue);
            AddField(writer, JsonDefaults.CommandName, input.Command, JsonDefaults.NullValue);

            if (input.IsPassword)
            {
                AddFieldRaw(writer, JsonDefaults.PasswordName, JsonDefaults.PasswordValue);
            }

            writer.WriteEndObject();
        }
    }
}