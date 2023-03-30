using Oxide.Ext.UiFramework.Json;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components
{
    public class OutlineComponent : BaseColorComponent
    {
        private const string Type = "UnityEngine.UI.Outline";

        public Vector2 Distance = JsonDefaults.Outline.Distance;
        public bool UseGraphicAlpha;

        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
            writer.AddField(JsonDefaults.Outline.DistanceName, Distance, JsonDefaults.Outline.FpDistance);
            if (UseGraphicAlpha)
            {
                writer.AddKeyField(JsonDefaults.Outline.UseGraphicAlphaName);
            }
            
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }

        protected override void LeavePool()
        {
            Distance = JsonDefaults.Outline.Distance;
            UseGraphicAlpha = false;
        }
    }
}