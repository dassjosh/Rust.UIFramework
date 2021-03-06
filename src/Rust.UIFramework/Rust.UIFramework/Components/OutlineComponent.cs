using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
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
            writer.AddField(JsonDefaults.Outline.DistanceName, Distance, new Vector2(1.0f, -1.0f));
            if (UseGraphicAlpha)
            {
                writer.AddKeyField(JsonDefaults.Outline.UseGraphicAlphaName);
            }
            
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }

        protected override void EnterPool()
        {
            Distance = new Vector2(1.0f, -1.0f);
            UseGraphicAlpha = false;
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
}