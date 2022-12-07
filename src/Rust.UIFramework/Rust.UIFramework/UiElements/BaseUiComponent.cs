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
            writer.AddFieldRaw(JsonDefaults.Common.ComponentName, Name);
            writer.AddFieldRaw(JsonDefaults.Common.ParentName, Parent);
            writer.AddField(JsonDefaults.Common.FadeOutName, FadeOut, JsonDefaults.Common.FadeOut);
            
            if (autoDestroy)
            {
                writer.AddFieldRaw(JsonDefaults.Common.AutoDestroy, Name);
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

        public void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentName, Name);
            writer.AddFieldRaw(JsonDefaults.Common.ParentName, Parent);
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
            writer.AddPosition(JsonDefaults.Position.AnchorMinName, Position.Min, new Vector2(0, 0));
            writer.AddPosition(JsonDefaults.Position.AnchorMaxName, Position.Max, new Vector2(1, 1));
            writer.AddOffset(JsonDefaults.Offset.OffsetMinName, Offset.Min, new Vector2(0, 0));
            writer.AddOffset(JsonDefaults.Offset.OffsetMaxName, Offset.Max, new Vector2(1, 1));
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
            Offset = default(UiOffset);
        }
    }
}