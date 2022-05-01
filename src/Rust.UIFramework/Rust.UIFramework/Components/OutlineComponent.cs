using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components
{
    public class OutlineComponent : BaseColorComponent
    {
        private const string Type = "UnityEngine.UI.Outline";

        public Vector2 Distance;
        public bool UseGraphicAlpha;

        public override void WriteComponent(JsonTextWriter writer)
        {
            writer.WriteStartObject();
            JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
            JsonCreator.AddField(writer, JsonDefaults.Outline.DistanceName, Distance, JsonDefaults.Outline.DistanceValue, VectorExt.ToString(Distance));
            if (UseGraphicAlpha)
            {
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Outline.UseGraphicAlphaName, JsonDefaults.Outline.UseGraphicAlphaValue);
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