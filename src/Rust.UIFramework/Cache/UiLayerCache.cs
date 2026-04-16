using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Helpers;

namespace Oxide.Ext.UiFramework.Cache;

public static class UiLayerCache
{
    private static readonly string[] Layers = CacheHelpers.ExtractCache(typeof(UiLayers), typeof(UiLayer));
    public static string GetLayer(UiLayer layer) => Layers[(byte)layer];
}