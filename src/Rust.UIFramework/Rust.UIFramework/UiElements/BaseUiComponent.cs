using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.UiElements
{
    public abstract class BaseUiComponent : BasePoolable
    {
        public string Name;
        public string Parent;
        public float FadeOut;
        public UiPosition Position;
        public UiOffset? Offset;

        protected static T CreateBase<T>(UiPosition pos, UiOffset? offset) where T : BaseUiComponent, new()
        {
            T component = UiFrameworkPool.Get<T>();
            component.Position = pos;
            component.Offset = offset;
            return component;
        }

        public void WriteRootComponent(JsonFrameworkWriter writer, bool needsMouse, bool needsKeyboard)
        {
            writer.WriteStartObject();
            JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentName, Name);
            JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ParentName, Parent);
            JsonCreator.AddField(writer, JsonDefaults.Common.FadeOutName, FadeOut, JsonDefaults.Common.FadeOut);

            writer.WritePropertyName("components");
            writer.WriteStartArray();
            WriteComponents(writer);

            if (needsMouse)
            {
                JsonCreator.AddMouse(writer);
            }

            if (needsKeyboard)
            {
                JsonCreator.AddKeyboard(writer);
            }

            writer.WriteEndArray();
            writer.WriteEndObject();
        }

        public void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentName, Name);
            JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ParentName, Parent);
            JsonCreator.AddField(writer, JsonDefaults.Common.FadeOutName, FadeOut, JsonDefaults.Common.FadeOut);

            writer.WritePropertyName("components");
            writer.WriteStartArray();
            WriteComponents(writer);
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
        
        protected virtual void WriteComponents(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.RectTransformName);
            JsonCreator.AddPosition(writer, JsonDefaults.Position.AnchorMinName, Position.Min, new Vector2(0, 0));
            JsonCreator.AddPosition(writer, JsonDefaults.Position.AnchorMaxName, Position.Max, new Vector2(1, 1));

            if (Offset.HasValue)
            {
                UiOffset offset = Offset.Value;
                JsonCreator.AddOffset(writer, JsonDefaults.Offset.OffsetMinName, offset.Min, new Vector2Short(0, 0));
                JsonCreator.AddOffset(writer, JsonDefaults.Offset.OffsetMaxName, offset.Max, new Vector2Short(1, 1));
            }
            else
            {
                //Fixes issue with UI going outside of bounds
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Offset.OffsetMaxName, JsonDefaults.Offset.DefaultOffsetMax);
            }

            writer.WriteEndObject();
        }

        public void SetFadeOut(float duration)
        {
            FadeOut = duration;
        }

        protected override void EnterPool()
        {
            Name = null;
            Parent = null;
            FadeOut = 0;
            Position = default(UiPosition);
            Offset = null;
        }
    }
}