using System.Collections.Generic;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Cache
{
    public static class UiLayerCache
    {
        private const string Overall = "Overall";
        private const string Overlay = "Overlay";
        private const string Hud = "Hud";
        private const string HudMenu = "Hud.Menu";
        private const string Under = "Under";

        private static readonly Dictionary<UiLayer, string> Layers = new Dictionary<UiLayer, string>
        {
            [UiLayer.Overall] = Overall,
            [UiLayer.Overlay] = Overlay,
            [UiLayer.Hud] = Hud,
            [UiLayer.HudMenu] = HudMenu,
            [UiLayer.Under] = Under,
        };

        public static string GetLayer(UiLayer layer)
        {
            return Layers[layer];
        }
    }
}