using System.Collections.Concurrent;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Cache;

public static class UiLayerCache
{
    public const string Overall = "Overall";
    public const string OverlayNonScaled = "OverlayNonScaled";
    public const string Overlay = "Overlay";
    public const string Hud = "Hud";
    public const string HudMenu = "Hud.Menu";
    public const string Under = "Under";
    public const string UnderNonScaled = "UnderNonScaled";
    public const string Inventory = "Inventory";
    public const string Crafting = "Crafting";
    public const string Contacts = "Contacts";
    public const string Clans = "Clans";
    public const string TechTree = "TechTree";
    public const string Map = "Map";

    private static readonly ConcurrentDictionary<UiLayer, string> Layers = new()
    {
        [UiLayer.Overall] = Overall,
        [UiLayer.Overlay] = Overlay,
        [UiLayer.OverlayNonScaled] = OverlayNonScaled,
        [UiLayer.Hud] = Hud,
        [UiLayer.HudMenu] = HudMenu,
        [UiLayer.Under] = Under,
        [UiLayer.UnderNonScaled] = UnderNonScaled,
        [UiLayer.Inventory] = Inventory,
        [UiLayer.Crafting] = Crafting,
        [UiLayer.Contacts] = Contacts,
        [UiLayer.Clans] = Clans,
        [UiLayer.TechTree] = TechTree,
        [UiLayer.Map] = Map,
    };

    public static string GetLayer(UiLayer layer)
    {
        return Layers[layer];
    }
}