using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements
{
    public abstract class BaseUiComponent : BasePoolable
    {
        public UiReference Reference;
        public float FadeOut;
        public UiPosition Position;
        public UiOffset Offset;

        protected static T CreateBase<T>(UiPosition pos, UiOffset offset) where T : BaseUiComponent, new()
        {
            T component = UiFrameworkPool.Get<T>();
            component.Position = pos;
            component.Offset = offset;
            return component;
        }

        public void WriteRootComponent(JsonFrameworkWriter writer, bool needsMouse, bool needsKeyboard, bool autoDestroy)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentName, Reference.Name);
            writer.AddFieldRaw(JsonDefaults.Common.ParentName, Reference.Parent);
            writer.AddField(JsonDefaults.Common.FadeOutName, FadeOut, JsonDefaults.Common.FadeOut);
            
            if (autoDestroy)
            {
                writer.AddFieldRaw(JsonDefaults.Common.AutoDestroy, Reference.Name);
            }

            writer.WritePropertyName("components");
            writer.WriteStartArray();
            WriteComponents(writer);

            if (needsMouse)
            {
                writer.AddMouse();
            }

            if (needsKeyboard)
            {
                writer.AddKeyboard();
            }

            writer.WriteEndArray();
            writer.WriteEndObject();
        }

        public void WriteUpdateComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentName, Reference.Name);
            writer.AddFieldRaw(JsonDefaults.Common.ParentName, Reference.Parent);
            writer.AddField(JsonDefaults.Common.FadeOutName, FadeOut, JsonDefaults.Common.FadeOut);
            writer.AddFieldRaw(JsonDefaults.Common.AutoDestroy, Reference.Name);

            writer.WritePropertyName("components");
            writer.WriteStartArray();
            WriteComponents(writer);
            writer.WriteEndArray();
            writer.WriteEndObject();
        }

        public void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentName, Reference.Name);
            writer.AddFieldRaw(JsonDefaults.Common.ParentName, Reference.Parent);
            writer.AddField(JsonDefaults.Common.FadeOutName, FadeOut, JsonDefaults.Common.FadeOut);

            writer.WritePropertyName("components");
            writer.WriteStartArray();
            WriteComponents(writer);
            writer.WriteEndArray();
            writer.WriteEndObject();
        }

        protected virtual void WriteComponents(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.RectTransformName);
            writer.AddPosition(JsonDefaults.Position.AnchorMinName, Position.Min, JsonDefaults.Common.Min);
            writer.AddPosition(JsonDefaults.Position.AnchorMaxName, Position.Max, JsonDefaults.Common.Max);
            writer.AddOffset(JsonDefaults.Offset.OffsetMinName, Offset.Min, JsonDefaults.Common.Min);
            writer.AddOffset(JsonDefaults.Offset.OffsetMaxName, Offset.Max, JsonDefaults.Common.Max);
            writer.WriteEndObject();
        }

        public void SetFadeOut(float duration)
        {
            FadeOut = duration;
        }

        protected override void EnterPool()
        {
            Reference = default(UiReference);
            FadeOut = 0;
            Position = default(UiPosition);
            Offset = default(UiOffset);
        }

        public static implicit operator UiReference(BaseUiComponent component) => component.Reference;
    }
}