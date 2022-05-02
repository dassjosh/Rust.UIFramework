using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements
{
    public abstract class BaseUiComponent : BasePoolable
    {
        public string Name;
        public string Parent;
        public float FadeOut;
        public Position Position;
        public Offset? Offset;

        protected static T CreateBase<T>(UiPosition pos, UiOffset offset) where T : BaseUiComponent, new()
        {
            T component = UiFrameworkPool.Get<T>();
            component.Position = pos.ToPosition();
            component.Offset = offset?.ToOffset();
            return component;
        }

        protected static T CreateBase<T>(Position pos, Offset? offset) where T : BaseUiComponent, new()
        {
            T component = UiFrameworkPool.Get<T>();
            component.Position = pos;
            component.Offset = offset;
            return component;
        }

        protected static T CreateBase<T>(UiPosition pos) where T : BaseUiComponent, new()
        {
            T component = UiFrameworkPool.Get<T>();
            component.Position = pos.ToPosition();
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
            JsonCreator.AddField(writer, JsonDefaults.Position.AnchorMinName, Position.Min, JsonDefaults.Position.AnchorMin);
            JsonCreator.AddField(writer, JsonDefaults.Position.AnchorMaxName, Position.Max, JsonDefaults.Position.AnchorMax);

            if (Offset.HasValue)
            {
                Offset offset = Offset.Value;
                JsonCreator.AddField(writer, JsonDefaults.Offset.OffsetMinName, offset.Min, JsonDefaults.Offset.OffsetMin);
                JsonCreator.AddField(writer, JsonDefaults.Offset.OffsetMaxName, offset.Max, JsonDefaults.Offset.OffsetMax);
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

        public abstract void SetFadeIn(float duration);

        protected override void EnterPool()
        {
            Name = null;
            Parent = null;
            FadeOut = 0;
            Position = default(Position);
            Offset = null;
        }
    }
}