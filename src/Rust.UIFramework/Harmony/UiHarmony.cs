using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Harmony;

internal static class UiHarmony
{
    internal static HarmonyLib.Harmony Harmony => UiFrameworkPlugin.Instance.Harmony;

    static UiHarmony()
    {
        //HarmonyLib.Harmony.DEBUG = true;
    }
    
    internal static void Initialize()
    {

    }
}