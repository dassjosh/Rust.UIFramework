using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components
{
    public class OutlineComponent : BasePoolable, IComponent
    {
        private const string Type = "UnityEngine.UI.Outline";

        public UiColor Color;
        public Vector2 Distance = JsonDefaults.Outline.Distance;
        public bool UseGraphicAlpha;

        public virtual void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
            writer.AddField(JsonDefaults.Outline.DistanceName, Distance, JsonDefaults.Outline.FpDistance);
            if (UseGraphicAlpha)
            {
                writer.AddKeyField(JsonDefaults.Outline.UseGraphicAlphaName);
            }
            
            writer.AddField(JsonDefaults.Color.ColorName, Color);
            writer.WriteEndObject();
        }

        public virtual void Reset()
        {
            Distance = JsonDefaults.Outline.Distance;
            UseGraphicAlpha = false;
            Color = default(UiColor);
        }

        protected override void EnterPool()
        {
            Reset();
        }
    }
}