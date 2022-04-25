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
            JsonCreator.AddFieldRaw(writer, JsonDefaults.ComponentTypeName, Type);
            JsonCreator.AddField(writer, JsonDefaults.DistanceName, Distance, JsonDefaults.DistanceValue, VectorExt.ToString(Distance));
            if (UseGraphicAlpha)
            {
                JsonCreator.AddFieldRaw(writer, JsonDefaults.UseGraphicAlphaName, JsonDefaults.UseGraphicAlphaValue);
            }
            
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }

        public override void EnterPool()
        {
            base.EnterPool();
            Distance = JsonDefaults.DistanceValue;
            UseGraphicAlpha = false;
        }
    }
}