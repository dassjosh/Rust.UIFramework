using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using UnityEngine;

namespace Oxide.Ext.UiFramework.UiElements
{
    public abstract class BaseUiTextOutline : BaseUiComponent
    {
        public OutlineComponent Outline;
        
        public void AddTextOutline(UiColor color)
        {
            Outline = UiFrameworkPool.Get<OutlineComponent>();
            Outline.Color = color;
        }

        public void AddTextOutline(UiColor color, Vector2 distance)
        {
            AddTextOutline(color);
            Outline.Distance = distance;
        }

        public void AddTextOutline(UiColor color, Vector2 distance, bool useGraphicAlpha)
        {
            AddTextOutline(color, distance);
            Outline.UseGraphicAlpha = useGraphicAlpha;
        }

        protected override void WriteComponents(JsonFrameworkWriter writer)
        {
            Outline?.WriteComponent(writer);
            base.WriteComponents(writer);
        }

        protected override void EnterPool()
        {
            if (Outline != null)
            {
                UiFrameworkPool.Free(ref Outline);
            }
        }
    }
}