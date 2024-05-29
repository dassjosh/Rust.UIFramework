using System.Collections.Concurrent;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Cache;

public static class UiLayerCache
{
    private const string Overall = "Overall";
    private const string OverlayNonScaled = "OverlayNonScaled";
    private const string Overlay = "Overlay";
    private const string Hud = "Hud";
    private const string HudMenu = "Hud.Menu";
    private const string Under = "Under";
    private const string UnderNonScaled = "UnderNonScaled";

    private static readonly ConcurrentDictionary<UiLayer, string> Layers = new()
    {
        [UiLayer.Overall] = Overall,
        [UiLayer.Overlay] = Overlay,
        [UiLayer.OverlayNonScaled] = OverlayNonScaled,
        [UiLayer.Hud] = Hud,
        [UiLayer.HudMenu] = HudMenu,
        [UiLayer.Under] = Under,
        [UiLayer.UnderNonScaled] = UnderNonScaled,
    };

    public static string GetLayer(UiLayer layer)
    {
        return Layers[layer];
    }
}