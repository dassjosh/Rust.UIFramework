using Oxide.Ext.UiFramework.Json;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components
{
    public class OutlineComponent : BaseColorComponent
    {
        private const string Type = "UnityEngine.UI.Outline";

        public Vector2 Distance;
        public bool UseGraphicAlpha;

        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
            writer.AddField(JsonDefaults.Outline.DistanceName, Distance, JsonDefaults.Outline.DistanceValue);
            if (UseGraphicAlpha)
            {
                writer.AddFieldRaw(JsonDefaults.Outline.UseGraphicAlphaName, JsonDefaults.Outline.UseGraphicAlphaValue);
            }
            
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }

        protected override void EnterPool()
        {
            Distance = JsonDefaults.Outline.DistanceValue;
            UseGraphicAlpha = false;
        }
    }
}