using Facepunch;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements
{
    public abstract class BaseUiComponent : Pool.IPooled
    {
        public string Name;
        public string Parent;
        public float FadeOut;
        public Position Position;
        public Offset? Offset;
        private bool _inPool = true;

        protected static T CreateBase<T>(UiPosition pos, UiOffset offset) where T : BaseUiComponent, new()
        {
            T component = UiFrameworkPool.Get<T>();
            component.Position = pos.ToPosition();
            component.Offset = offset?.ToOffset();
            if (component._inPool)
            {
                component.LeavePool();
            }
            return component;
        }

        protected static T CreateBase<T>(Position pos, Offset? offset) where T : BaseUiComponent, new()
        {
            T component = UiFrameworkPool.Get<T>();
            component.Position = pos;
            component.Offset = offset;
            if (component._inPool)
            {
                component.LeavePool();
            }
            return component;
        }

        protected static T CreateBase<T>(UiPosition pos) where T : BaseUiComponent, new()
        {
            T component = UiFrameworkPool.Get<T>();
            component.Position = pos.ToPosition();
            if (component._inPool)
            {
                component.LeavePool();
            }
            return component;
        }

        public void WriteRootComponent(JsonTextWriter writer, bool needsMouse, bool needsKeyboard)
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

        public void WriteComponent(JsonTextWriter writer)
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
        
        protected virtual void WriteComponents(JsonTextWriter writer)
        {
            writer.WriteStartObject();
            JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.RectTransformName);
            JsonCreator.AddField(writer, JsonDefaults.Position.AnchorMinName, Position.Min, JsonDefaults.Position.AnchorMin, Position.MinString);
            JsonCreator.AddField(writer, JsonDefaults.Position.AnchorMaxName, Position.Max, JsonDefaults.Position.AnchorMax, Position.MaxString);

            if (Offset.HasValue)
            {
                Offset offset = Offset.Value;
                JsonCreator.AddField(writer, JsonDefaults.Offset.OffsetMinName, offset.Min, JsonDefaults.Offset.OffsetMin, offset.MinString);
                JsonCreator.AddField(writer, JsonDefaults.Offset.OffsetMaxName, offset.Max, JsonDefaults.Offset.OffsetMax, offset.MaxString);
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

        public virtual void EnterPool()
        {
            Name = null;
            Parent = null;
            FadeOut = 0;
            Position = default(Position);
            Offset = null;
            _inPool = true;
        }

        public virtual void LeavePool()
        {
            _inPool = false;
        }
    }
}