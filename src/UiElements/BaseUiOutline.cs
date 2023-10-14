using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using UnityEngine;

namespace Oxide.Ext.UiFramework.UiElements
{
    public abstract class BaseUiOutline : BaseUiComponent
    {
        public OutlineComponent Outline;
        
        public void AddElementOutline(UiColor color)
        {
            Outline = UiFrameworkPool.Get<OutlineComponent>();
            Outline.Color = color;
        }

        public void AddElementOutline(UiColor color, Vector2 distance)
        {
            AddElementOutline(color);
            Outline.Distance = distance;
        }

        public void AddElementOutline(UiColor color, Vector2 distance, bool useGraphicAlpha)
        {
            AddElementOutline(color, distance);
            Outline.UseGraphicAlpha = useGraphicAlpha;
        }

        public void RemoveOutline()
        {
            if (Outline != null)
            {
                Outline.Dispose();
                Outline = null;
            }
        }

        protected override void WriteComponents(JsonFrameworkWriter writer)
        {
            Outline?.WriteComponent(writer);
            base.WriteComponents(writer);
        }

        protected override void EnterPool()
        {
            RemoveOutline();
        }
    }
}